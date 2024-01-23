﻿using Astrarium.Types;
using Astrarium.Algorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Astrarium.Plugins.Satellites
{
    public class SatellitesCalculator : BaseCalc
    {
        public ICollection<Satellite> Satellites { get; private set; }

        /// <inheritdoc />
        public IEnumerable<Satellite> GetCelestialObjects() => Satellites;

        public double JulianDay { get; private set; }

        /// <inheritdoc />
        public override void Initialize()
        {
            string file = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data/Brightest.tle");
            Satellites = LoadSatellites(file);
        }

        public override void Calculate(SkyContext context)
        {
            double deltaT = Date.DeltaT(context.JulianDay);
            var jd = context.JulianDay - deltaT / 86400;
            foreach (var s in Satellites)
            {
                Norad.SGP4(s.Tle, jd, s.Position, s.Velocity);
            }
            JulianDay = context.JulianDay;
        }

        private ICollection<Satellite> LoadSatellites(string file)
        {
            var satellites = new List<Satellite>();

            using (var sr = new StreamReader(file, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string name = sr.ReadLine();
                    string line1 = sr.ReadLine();
                    string line2 = sr.ReadLine();
                    satellites.Add(new Satellite(name.Trim(), new TLE(line1, line2)));
                }
                sr.Close();
            }

            return satellites;
        }
    }
}
