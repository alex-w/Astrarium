﻿using Astrarium.Algorithms;
using Astrarium.Types;
using WF = System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace Astrarium.Plugins.Grids
{
    public class CelestialGridRenderer : BaseRenderer
    {
        private readonly CelestialGridCalculator calc;
        private readonly ISettings settings;

        private Font fontLabel = new Font("Arial", 10);

        private string[] nodesLabels = new string[] { "\u260A", "\u260B" };
        private string[] equinoxLabels = new string[] { "\u2648", "\u264E" };
        private string[] horizontalLabels = new string[] { "CelestialGridRenderer.Zenith", "CelestialGridRenderer.Nadir" };
        private string[] equatorialLabels = new string[] { "CelestialGridRenderer.NCP", "CelestialGridRenderer.SCP" };
        private string[] equatorialTooltips = new string[] { "CelestialGridRenderer.NCP.Tooltip", "CelestialGridRenderer.SCP.Tooltip" };
        private string[] equinoxTooltips = new string[] { "CelestialGridRenderer.VernalEquinox.Tooltip", "CelestialGridRenderer.AutumnalEquinox.Tooltip" };
        private string[] nodesTooltips = new string[] { "CelestialGridRenderer.LunarAscendingNode.Tooltip", "CelestialGridRenderer.LunarDescendingNode.Tooltip" };

        private Lazy<TextRenderer> textRenderer = new Lazy<TextRenderer>(() => new TextRenderer(64, 64));
        private List<Tuple<Vec2, string>> labels = new List<Tuple<Vec2, string>>();

        public CelestialGridRenderer(CelestialGridCalculator calc, ISettings settings)
        {
            this.calc = calc;
            this.settings = settings;
        }

        public override void Render(ISkyMap map)
        {
            var prj = map.Projection;
            var nightMode = settings.Get("NightMode");

            labels.Clear();

            Color colorGridEquatorial = settings.Get<Color>("ColorEquatorialGrid").Tint(nightMode);
            Color colorGridHorizontal = settings.Get<Color>("ColorHorizontalGrid").Tint(nightMode);
            Color colorLineEcliptic = settings.Get<Color>("ColorEcliptic").Tint(nightMode);
            Color colorLineGalactic = settings.Get<Color>("ColorGalacticEquator").Tint(nightMode);
            Color colorLineMeridian = settings.Get<Color>("ColorMeridian").Tint(nightMode);

            SolidBrush brushHorizontal = new SolidBrush(colorGridHorizontal);
            SolidBrush brushEquatorial = new SolidBrush(colorGridEquatorial);
            SolidBrush brushEcliptic = new SolidBrush(colorLineEcliptic);

            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.LineSmooth);
            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.LineStipple);
            GL.Enable(EnableCap.CullFace);

            if (!prj.FlipVertical ^ prj.FlipHorizontal)
            {
                GL.CullFace(CullFaceMode.Back);
            }
            else
            {
                GL.CullFace(CullFaceMode.Front);
            }

            if (settings.Get("GalacticEquator"))
            {
                int segments = prj.Fov < 45 ? 128 : 64;
                CrdsGalactical gal = new CrdsGalactical(0, 0);
                Func<int, Vec2> project = (int i) =>
                {
                    gal.l = (double)i / segments * 360;
                    var eq1950 = gal.ToEquatorial();
                    var eq = Precession.GetEquatorialCoordinates(eq1950, calc.PrecessionalElementsB1950ToCurrent);
                    return prj.Project(eq);
                };

                DrawLine(colorLineGalactic, segments, project);
            }

            if (settings.Get("EclipticLine"))
            {
                int segments = prj.Fov < 45 ? 128 : 64;
                CrdsEcliptical ecl = new CrdsEcliptical(0, 0);
                Func<int, Vec2> project = (int i) =>
                {
                    ecl.Lambda = (double)i / segments * 360;
                    return prj.Project(ecl.ToEquatorial(prj.Context.Epsilon));
                };

                DrawLine(colorLineEcliptic, segments, project);

                if (settings.Get("LabelEquinoxPoints"))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var eq = new CrdsEcliptical(i * 180, 0).ToEquatorial(prj.Context.Epsilon);
                        DrawLabel(prj, eq, equinoxLabels[i], Text.Get(equinoxTooltips[i]), brushEcliptic);
                    }
                }

                if (settings.Get("LabelLunarNodes"))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var eq = new CrdsEcliptical(calc.LunarAscendingNodeLongitude + i * 180, 0).ToEquatorial(prj.Context.Epsilon);
                        DrawLabel(prj, eq, nodesLabels[i], Text.Get(nodesTooltips[i]), brushEcliptic);
                    }
                }
            }

            if (settings.Get("HorizontalGrid"))
            {
                CrdsHorizontal h = new CrdsHorizontal();
                Func<double, double, Vec2> project = (double lon, double lat) =>
                {
                    h.Azimuth = lon;
                    h.Altitude = lat;
                    return prj.Project(h);
                };

                DrawGrid(prj, colorGridHorizontal, project);

                if (settings.Get("LabelHorizontalPoles"))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var eq = new CrdsHorizontal(0, 90 * (i == 0 ? 1 : -1)).ToEquatorial(prj.Context.GeoLocation, prj.Context.SiderealTime);
                        DrawLabel(prj, eq, Text.Get(horizontalLabels[i]), Text.Get(horizontalLabels[i]), brushHorizontal);
                    }
                }
            }

            if (settings.Get("EquatorialGrid"))
            {
                CrdsEquatorial eq = new CrdsEquatorial();
                Func<double, double, Vec2> project = (double lon, double lat) =>
                {
                    eq.Alpha = lon;
                    eq.Delta = lat;
                    return prj.Project(eq);
                };

                DrawGrid(prj, colorGridEquatorial, project);

                if (settings.Get("LabelEquatorialPoles"))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        eq = new CrdsEquatorial(0, 90 * (i == 0 ? 1 : -1));
                        DrawLabel(prj, eq, Text.Get(equatorialLabels[i]), Text.Get(equatorialTooltips[i]), brushEquatorial);
                    }
                }
            }

            if (settings.Get("MeridianLine"))
            {
                int segments = prj.Fov < 45 ? 128 : 64;
                CrdsHorizontal hor = new CrdsHorizontal(0, 0);
                Func<int, Vec2> project = (int i) =>
                {
                    hor.Altitude = (double)i / segments * 360;
                    return prj.Project(hor);
                };

                DrawLine(colorLineMeridian, segments, project);
            }

            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.LineSmooth);
            GL.Disable(EnableCap.PointSmooth);
            GL.Disable(EnableCap.LineStipple);
            GL.Disable(EnableCap.CullFace);
        }

        public override void OnMouseMove(ISkyMap map, MouseButton mouseButton)
        {
            if (labels.Any(x => x.Item1.Distance(map.MouseScreenCoordinates) < 5))
            {
                var label = labels.FirstOrDefault(x => x.Item1.Distance(map.MouseScreenCoordinates) < 5);
                if (label != null)
                {
                    ViewManager.ShowTooltipMessage(label.Item1, label.Item2);
                }
            }
        }

        private void DrawLabel(Projection prj, CrdsEquatorial eq, string label, string tooltip, SolidBrush brush)
        {
            Vec2 p = prj.Project(eq);
            if (prj.IsInsideScreen(p))
            {
                GL.Color3(brush.Color);
                GL.PointSize(5);
                GL.Begin(PrimitiveType.Points);
                GL.Vertex2(p.X, p.Y);
                GL.End();
                textRenderer.Value.DrawString(label, fontLabel, brush, new Vec2(p.X + 3, p.Y - 3));
                labels.Add(new Tuple<Vec2, string>(p, tooltip));
            }
        }

        private void DrawLine(Color color, int segments, Func<int, Vec2> projectPoint)
        {
            GL.Color3(color);
            GL.LineStipple(1, 0xAAAA);

            GL.Begin(PrimitiveType.LineStrip);

            for (int i = 0; i <= segments; i++)
            {
                var p = projectPoint(i);

                if (p != null)
                {
                    GL.Vertex2(p.X, p.Y);
                }
                else
                {
                    GL.End();
                    GL.Begin(PrimitiveType.LineStrip);
                }
            }

            GL.End();
        }

        private void DrawGrid(Projection prj, Color color, Func<double, double, Vec2> projectPoint)
        {
            int segments = prj.Fov < 45 ? 128 : 64;

            GL.Color3(color);
            GL.LineStipple(1, 0xAAAA);

            // HOR. GRID
            {
                // parallels
                for (int alt = -80; alt <= 80; alt += 10)
                {
                    GL.Begin(PrimitiveType.LineStrip);

                    for (int i = 0; i <= segments; i++)
                    {
                        double lon = i / (double)segments * 360;
                        double lat = alt;

                        var p = projectPoint(lon, lat);

                        if (p != null)
                        {
                            GL.Vertex2(p.X, p.Y);
                        }
                        else
                        {
                            GL.End();
                            GL.Begin(PrimitiveType.LineStrip);
                        }
                    }

                    GL.End();
                }

                // meridians
                for (int i = 0; i < 24; i++)
                {
                    GL.Begin(PrimitiveType.LineStrip);

                    for (int alt = -80; alt <= 80; alt += 2)
                    {
                        double lon = i / 24.0 * 360;
                        double lat = alt;

                        var p = projectPoint(lon, lat);
                        if (p != null)
                        {
                            GL.Vertex2(p.X, p.Y);
                        }
                        else
                        {
                            GL.End();
                            GL.Begin(PrimitiveType.LineStrip);
                        }
                    }

                    GL.End();
                }
            }
        }

        public override RendererOrder Order => RendererOrder.Grids;
    }
}
