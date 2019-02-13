﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADK.Demo.Renderers
{
    public class GroundRenderer : IRenderer
    {
        private readonly ISettings settings;

        public GroundRenderer(ISettings settings)
        {
            this.settings = settings;
        }

        public void Initialize() { }

        public int ZOrder => 800;

        public void Render(IMapContext map)
        {
            if (settings.Get<bool>("Ground"))
            {
                const int POINTS_COUNT = 64;
                PointF[] hor = new PointF[POINTS_COUNT];
                double step = 2 * map.ViewAngle / (POINTS_COUNT - 1);
                SolidBrush brushGround = new SolidBrush(Color.FromArgb(4, 10, 10));

                // Bottom part of ground shape

                for (int i = 0; i < POINTS_COUNT; i++)
                {
                    var h = new CrdsHorizontal(map.Center.Azimuth - map.ViewAngle + step * i, 0);
                    hor[i] = map.Project(h);
                }
                if (hor[0].X >= 0) hor[0].X = -1;
                if (hor[POINTS_COUNT - 1].X <= map.Width) hor[POINTS_COUNT - 1].X = map.Width + 1;

                if (hor.Any(h => !map.IsOutOfScreen(h)))
                {
                    GraphicsPath gp = new GraphicsPath();

                    gp.AddCurve(hor);
                    gp.AddLines(new PointF[]
                    {
                        new PointF(map.Width + 1, map.Height + 1),
                        new PointF(-1, map.Height + 1)
                    });

                    map.Graphics.FillPath(brushGround, gp);
                }
                else if (map.Center.Altitude <= 0)
                {
                    map.Graphics.FillRectangle(brushGround, 0, 0, map.Width, map.Height);
                }

                // Top part of ground shape 

                if (map.Center.Altitude > 0)
                {
                    for (int i = 0; i < POINTS_COUNT; i++)
                    {
                        var h = new CrdsHorizontal(map.Center.Azimuth - map.ViewAngle - step * i, 0);
                        hor[i] = map.Project(h);
                    }

                    if (hor.Count(h => !map.IsOutOfScreen(h)) > 2)
                    {
                        GraphicsPath gp = new GraphicsPath();

                        gp.AddCurve(hor);
                        gp.AddLines(new PointF[]
                        {
                            new PointF(map.Width + 1, -1),
                            new PointF(-1, -1),
                        });

                        map.Graphics.FillPath(brushGround, gp);
                    }
                }
            }

            if (settings.Get<bool>("LabelCardinalDirections"))
            {
                Brush brushCardinalLabels = new SolidBrush(settings.Get<Color>("CardinalDirections.Color"));
                string[] labels = new string[] { "S", "SW", "W", "NW", "N", "NE", "E", "SE" };
                StringFormat format = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
                for (int i = 0; i < labels.Length; i++)
                {
                    var h = new CrdsHorizontal(i * 360 / labels.Length, 0);
                    if (Angle.Separation(h, map.Center) < map.ViewAngle * 1.2)
                    {
                        PointF p = map.Project(h);
                        map.Graphics.DrawStringOpaque(labels[i], SystemFonts.DefaultFont, brushCardinalLabels, Brushes.Black, p, format);
                    }
                }
            }
        }
    }
}
