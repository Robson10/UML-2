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

        private DateTime _timeNow;
        private Point _center;
        private int ThicknessHandOfClock = 1;
        private float _thicknessClockScale = 1;

        float fHourLength;
        float fMinLength;
        float fSecLength;
        private float startAngle;
        private float endAngle;
        float _radius;
        private Rectangle partOfTimeArea;
        private void AnalogUpdate()
        {
            Size=new Size(ClockVariables.ClockSize, ClockVariables.ClockSize);
            _radius = (int)(Height / 1.6);
            _center = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
            fHourLength = (float)Height / 2 / 1.65F;
            fMinLength = (float)Height / 2 / 1.20F;
            fSecLength = (float)Height / 2 / 1.15F;
            partOfTimeArea = new Rectangle((int)(Width*0.1),(int)(Height*0.1),(int)(Width*0.8),(int)(Height*0.8));
        }

        private void DrawAnalog(ref PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (_endExam != DateTime.MinValue)
            {
                startAngle = 360f / (12f * 60f) * ((_beginExam.Hour % 12) * 60f + _beginExam.Minute);
                endAngle = 360f / (12f * 60f) * ((_endExam.Hour % 12) * 60f + _endExam.Minute);
                endAngle = (startAngle > endAngle) ? 360f - startAngle + endAngle : endAngle - startAngle;
                e.Graphics.FillPie(ClockVariables.PartOfTimeColor, partOfTimeArea, -90f + startAngle, endAngle);
            }
            _timeNow = DateTime.Now;

            for (int i = 0; i < 60; i++)
            {
                if (i % 5 == 0) // Draw 5 minute line
                {
                    e.Graphics.DrawLine(new Pen(ClockVariables.ColorOfClockScale, _thicknessClockScale),
                        _center.X + (float)(_radius / 1.50F * Math.Sin(i * 6 * Math.PI / 180)),
                        _center.Y - (float)(_radius / 1.50F * Math.Cos(i * 6 * Math.PI / 180)),
                        _center.X + (float)(_radius / 1.95F * Math.Sin(i * 6 * Math.PI / 180)),
                        _center.Y - (float)(_radius / 1.95F * Math.Cos(i * 6 * Math.PI / 180)));
                }
                else  // draw 1 minute line
                {
                    e.Graphics.DrawLine(new Pen(ClockVariables.ColorOfClockScale, _thicknessClockScale),
                        _center.X + (float)(_radius / 1.50F * Math.Sin(i * 6 * Math.PI / 180)),
                        _center.Y - (float)(_radius / 1.50F * Math.Cos(i * 6 * Math.PI / 180)),
                        _center.X + (float)(_radius / 1.65F * Math.Sin(i * 6 * Math.PI / 180)),
                        _center.Y - (float)(_radius / 1.65F * Math.Cos(i * 6 * Math.PI / 180)));
                }
            }

            var radHr = (float)((_timeNow.Hour % 12 + _timeNow.Minute / 60F) * 30 * Math.PI / 180);
            var radMin = (float)((_timeNow.Minute) * 6 * Math.PI / 180);
            var radSec = (float)((_timeNow.Second) * 6 * Math.PI / 180);


            DrawPolygon(ThicknessHandOfClock, fHourLength, radHr, e);
            DrawPolygon(ThicknessHandOfClock, fMinLength, radMin, e);
            DrawLine(ThicknessHandOfClock, fSecLength, radSec, e);
        }

        private void DrawLine(float fThickness, float fLength,  float fRadians,PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawLine(new Pen(ClockVariables.ColorHandOfClock, fThickness),
                _center.X - (float)(fLength / 9 * Math.Sin(fRadians)),
                _center.Y + (float)(fLength / 9 * Math.Cos(fRadians)),
                _center.X + (float)(fLength * Math.Sin(fRadians)),
                _center.Y - (float)(fLength * Math.Cos(fRadians)));
        }


        private void DrawPolygon(float fThickness, float fLength,  float fRadians,PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            PointF[] points =
            {
                new PointF((float) (_center.X + fThickness * 2 * Math.Sin(fRadians + Math.PI / 2)),
                    (float) (_center.Y - fThickness * 2 * Math.Cos(fRadians + Math.PI / 2))),

                new PointF((float) (_center.X + fThickness * 2 * Math.Sin(fRadians - Math.PI / 2)),
                    (float) (_center.Y - fThickness * 2 * Math.Cos(fRadians - Math.PI / 2))),

                new PointF((float) (_center.X + fLength * Math.Sin(fRadians)),
                    (float) (_center.Y - fLength * Math.Cos(fRadians))),

                new PointF((float) (_center.X - fThickness * 4 * Math.Sin(fRadians)),
                    (float) (_center.Y + fThickness * 4 * Math.Cos(fRadians)))
            };
            e.Graphics.FillPolygon(new SolidBrush(ClockVariables.ColorHandOfClock), points);
        }
    }

}
