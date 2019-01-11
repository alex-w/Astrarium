﻿using ADK.Demo.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ADK.Demo.Calculators
{
    public class StarsCalc : BaseSkyCalc
    {
        /// <summary>
        /// Collection of all stars
        /// </summary>
        private ICollection<Star> Stars = new List<Star>();

        public StarsCalc(Sky sky) : base(sky)
        {
            Sky.AddDataProvider("Stars", () => Stars);
        }

        public override void Calculate(CalculationContext context)
        {
            foreach (var star in Stars)
            {
                star.Horizontal = context.Formula<Star, CrdsHorizontal>("Horizontal", star);
            }
        }

        public override void Initialize()
        {
            string file = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data/Stars.dat");

            int i = 0;

            string line = "";
            int len = 0;

            using (var sr = new StreamReader(file, Encoding.Default))
            {
                while (line != null && !sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    len = line.Length;

                    if (line.Length < 197) line += new string(' ', 197 - line.Length);

                    Star star = new Star() { Id = i };

                    if (line[94] == ' ')
                    {
                        Stars.Add(star);
                        i++;
                        continue;
                    }

                    star.Equatorial0.Alpha = new HMS(
                                                Convert.ToUInt32(line.Substring(75, 2)),
                                                Convert.ToUInt32(line.Substring(77, 2)),
                                                Convert.ToDouble(line.Substring(79, 4), CultureInfo.InvariantCulture)
                                            ).ToDecimalAngle();

                    star.Equatorial0.Delta = (line[83] == '-' ? -1 : 1) * new DMS(
                                                Convert.ToUInt32(line.Substring(84, 2)),
                                                Convert.ToUInt32(line.Substring(86, 2)),
                                                Convert.ToUInt32(line.Substring(88, 2))
                                            ).ToDecimalAngle();

                    if (line[148] != ' ')
                    {
                        star.PmAlpha = Convert.ToSingle(line.Substring(148, 6), CultureInfo.InvariantCulture);
                    }
                    if (line[154] != ' ')
                    {
                        star.PmDelta = Convert.ToSingle(line.Substring(154, 6), CultureInfo.InvariantCulture);
                    }

                    star.Mag = Convert.ToSingle(line.Substring(102, 5), CultureInfo.InvariantCulture);

                    star.Color = line[129];

                    i++;

                    Stars.Add(star);
                }
            }

            CalculationContext.AddFormula("PE2000", 
                ctx => {
                    return Precession.ElementsFK5(Date.EPOCH_J2000, Sky.JulianDay);
                });

            CalculationContext.AddFormula("YearsSince2000", ctx => (Sky.JulianDay - Date.EPOCH_J2000) / 365.25);

            CalculationContext.AddFormula<Star, CrdsEquatorial>("Equatorial", Star_Equatorial);
            CalculationContext.AddFormula<Star, CrdsHorizontal>("Horizontal", Star_Horizontal);
        }

        private CrdsHorizontal Star_Horizontal(dynamic context, Star star)
        {
            

            CrdsEquatorial eq = context.Formula<Star, CrdsEquatorial>("Equatorial", star);
            return eq.ToHorizontal(Sky.GeoLocation, Sky.SiderealTime);
        }

        private CrdsEquatorial Star_Equatorial(dynamic context, Star star)
        {
            double years = context.Formula<double>("YearsSince2000");

            PrecessionalElements p = context.Formula<PrecessionalElements>("PE2000");

            // Initial coodinates for J2000 epoch
            CrdsEquatorial eq0 = star.Equatorial0;

            // Take into account effect of proper motion:
            // now coordinates are for the mean equinox of J2000.0,
            // but for epoch of the target date
            eq0.Alpha += star.PmAlpha * years / 3600.0;
            eq0.Delta += star.PmDelta * years / 3600.0;

            // Equatorial coordinates for the mean equinox and epoch of the target date
            var equatorial = Precession.GetEquatorialCoordinates(eq0, p);

            // Nutation effect
            var eq1 = Nutation.NutationEffect(equatorial, Sky.NutationElements, Sky.Epsilon);

            // Aberration effect
            var eq2 = Aberration.AberrationEffect(equatorial, Sky.AberrationElements, Sky.Epsilon);

            // Apparent coordinates of the star
            equatorial += eq1 + eq2;

            return equatorial;
        }
    }
}
