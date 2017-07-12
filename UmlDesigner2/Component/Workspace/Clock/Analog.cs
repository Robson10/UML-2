using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.Clock
{
    //Analog Clock Class
    sealed partial class Clock
    {
        //https://www.codeproject.com/Articles/9593/Analog-clock-control-in-C

        DateTime dateTime;

        float centerX;
        float fCenterY;

        float fHourThickness;
        float fMinThickness;
        float fSecThickness;

        float fHourLength;
        float fMinLength;
        float fSecLength;

        Color hrColor = Color.Black;
        Color minColor = Color.Black;
        Color secColor = Color.Black;

        Color ticksColor = Color.Black;

        float fRadius;
        float fTicksThickness = 1;
        private Rectangle partOfTimeArea;
        private void AnalogUpdate()
        {
            
            Width = Height;
            fRadius = (int)(Height / 1.6);
            centerX = ClientSize.Width / 2;
            fCenterY = ClientSize.Height / 2;
            fHourLength = (float)Height / 2 / 1.65F;
            fMinLength = (float)Height / 2 / 1.20F;
            fSecLength = (float)Height / 2 / 1.15F;
            fHourThickness = (float)Height / 100;
            fMinThickness = (float)Height / 150;
            fSecThickness = (float)Height / 200;
            partOfTimeArea = new Rectangle((int)(Width*0.1),(int)(Height*0.1),(int)(Width*0.8),(int)(Height*0.8));
        }

        private void DrawAnalog(ref PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            dateTime = DateTime.Now;
            float fRadHr = (float)((dateTime.Hour % 12 + dateTime.Minute / 60F) * 30 * Math.PI / 180);
            float fRadMin = (float)((dateTime.Minute) * 6 * Math.PI / 180);
            float fRadSec = (float)((dateTime.Second) * 6 * Math.PI / 180);

            e.Graphics.FillPie(Brushes.YellowGreen,partOfTimeArea,0f,30f);

            DrawPolygon(fHourThickness, fHourLength, hrColor, fRadHr, e);
            DrawPolygon(fMinThickness, fMinLength, minColor, fRadMin, e);
            DrawLine(fSecThickness, fSecLength, secColor, fRadSec, e);

            for (int i = 0; i < 60; i++)
            {
                if (i % 5 == 0) // Draw 5 minute line
                {
                    e.Graphics.DrawLine(new Pen(ticksColor, fTicksThickness),
                        centerX + (float)(fRadius / 1.50F * Math.Sin(i * 6 * Math.PI / 180)),
                        fCenterY - (float)(fRadius / 1.50F * Math.Cos(i * 6 * Math.PI / 180)),
                        centerX + (float)(fRadius / 1.95F * Math.Sin(i * 6 * Math.PI / 180)),
                        fCenterY - (float)(fRadius / 1.95F * Math.Cos(i * 6 * Math.PI / 180)));
                }
                else  // draw 1 minute line
                {
                    e.Graphics.DrawLine(new Pen(ticksColor, fTicksThickness),
                        centerX + (float)(fRadius / 1.50F * Math.Sin(i * 6 * Math.PI / 180)),
                        fCenterY - (float)(fRadius / 1.50F * Math.Cos(i * 6 * Math.PI / 180)),
                        centerX + (float)(fRadius / 1.65F * Math.Sin(i * 6 * Math.PI / 180)),
                        fCenterY - (float)(fRadius / 1.65F * Math.Cos(i * 6 * Math.PI / 180)));
                }
            }
        }

        private void DrawLine(float fThickness, float fLength, Color color, float fRadians,
            PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawLine(new Pen(color, fThickness),
                centerX - (float)(fLength / 9 * Math.Sin(fRadians)),
                fCenterY + (float)(fLength / 9 * Math.Cos(fRadians)),
                centerX + (float)(fLength * Math.Sin(fRadians)),
                fCenterY - (float)(fLength * Math.Cos(fRadians)));
        }


        private void DrawPolygon(float fThickness, float fLength, Color color, float fRadians,
            PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            PointF A = new PointF((float)(centerX + fThickness * 2 * Math.Sin(fRadians + Math.PI / 2)),
                (float)(fCenterY - fThickness * 2 * Math.Cos(fRadians + Math.PI / 2)));
            PointF B = new PointF((float)(centerX + fThickness * 2 * Math.Sin(fRadians - Math.PI / 2)),
                (float)(fCenterY - fThickness * 2 * Math.Cos(fRadians - Math.PI / 2)));
            PointF C = new PointF((float)(centerX + fLength * Math.Sin(fRadians)),
                (float)(fCenterY - fLength * Math.Cos(fRadians)));
            PointF D = new PointF((float)(centerX - fThickness * 4 * Math.Sin(fRadians)),
                (float)(fCenterY + fThickness * 4 * Math.Cos(fRadians)));
            PointF[] points = { A, D, B, C };
            e.Graphics.FillPolygon(new SolidBrush(color), points);
        }
    }

}
