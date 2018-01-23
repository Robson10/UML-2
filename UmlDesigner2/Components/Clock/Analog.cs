using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SbWinNew.Class;

namespace SbWinNew.Components.Clock
{
    //Analog Clock Class
    partial class Clock
    {

        private DateTime _timeNow;
        private Point _center;
        private int ThicknessHandOfClock = 1;
        private float _thicknessClockScale = 1;

        private float _hourLength,_minLength,_secLength;
        private float _startAngle,_endAngle;
        private float _radius;
        private Rectangle _partOfTimeArea;
        private void AnalogUpdate()
        {
            Size=new Size(Helper.ClockSize, Helper.ClockSize);
            _radius = (int)(Height / 1.6);
            _center = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
            _hourLength = (float)Height / 2 / 1.65F;
            _minLength = (float)Height / 2 / 1.20F;
            _secLength = (float)Height / 2 / 1.15F;
            _partOfTimeArea = new Rectangle((int)(Width*0.1),(int)(Height*0.1),(int)(Width*0.8),(int)(Height*0.8));
        }

        private void DrawAnalog(ref PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (_endExam != DateTime.MinValue)
            {
                _startAngle = 360f / (12f * 60f) * ((_beginExam.Hour % 12) * 60f + _beginExam.Minute);
                _endAngle = 360f / (12f * 60f) * ((_endExam.Hour % 12) * 60f + _endExam.Minute);
                _endAngle = (_startAngle > _endAngle) ? 360f - _startAngle + _endAngle : _endAngle - _startAngle;
                e.Graphics.FillPie(Helper.ClockPartOfTimeColor, _partOfTimeArea, -90f + _startAngle, _endAngle);
            }
            _timeNow = DateTime.Now;

            for (int i = 0; i < 60; i++)
            {
                if (i % 5 == 0) // Draw 5 minute line
                {
                    e.Graphics.DrawLine(new Pen(Helper.ClockColorScale, _thicknessClockScale),
                        _center.X + (float)(_radius / 1.50F * Math.Sin(i * 6 * Math.PI / 180)),
                        _center.Y - (float)(_radius / 1.50F * Math.Cos(i * 6 * Math.PI / 180)),
                        _center.X + (float)(_radius / 1.95F * Math.Sin(i * 6 * Math.PI / 180)),
                        _center.Y - (float)(_radius / 1.95F * Math.Cos(i * 6 * Math.PI / 180)));
                }
                else  // draw 1 minute line
                {
                    e.Graphics.DrawLine(new Pen(Helper.ClockColorScale, _thicknessClockScale),
                        _center.X + (float)(_radius / 1.50F * Math.Sin(i * 6 * Math.PI / 180)),
                        _center.Y - (float)(_radius / 1.50F * Math.Cos(i * 6 * Math.PI / 180)),
                        _center.X + (float)(_radius / 1.65F * Math.Sin(i * 6 * Math.PI / 180)),
                        _center.Y - (float)(_radius / 1.65F * Math.Cos(i * 6 * Math.PI / 180)));
                }
            }

            var radHr = (float)((_timeNow.Hour % 12 + _timeNow.Minute / 60F) * 30 * Math.PI / 180);
            var radMin = (float)((_timeNow.Minute) * 6 * Math.PI / 180);
            var radSec = (float)((_timeNow.Second) * 6 * Math.PI / 180);


            DrawPolygon(ThicknessHandOfClock, _hourLength, radHr, e);
            DrawPolygon(ThicknessHandOfClock, _minLength, radMin, e);
            DrawLine(ThicknessHandOfClock, _secLength, radSec, e);
        }

        private void DrawLine(float thickness, float length,  float radians,PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawLine(new Pen(Helper.ClockColorHand, thickness),
                _center.X - (float)(length / 9 * Math.Sin(radians)),
                _center.Y + (float)(length / 9 * Math.Cos(radians)),
                _center.X + (float)(length * Math.Sin(radians)),
                _center.Y - (float)(length * Math.Cos(radians)));
        }
        
        private void DrawPolygon(float thickness, float length,  float radians,PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            PointF[] points =
            {
                new PointF((float) (_center.X + thickness * 2 * Math.Sin(radians + Math.PI / 2)),
                    (float) (_center.Y - thickness * 2 * Math.Cos(radians + Math.PI / 2))),

                new PointF((float) (_center.X + thickness * 2 * Math.Sin(radians - Math.PI / 2)),
                    (float) (_center.Y - thickness * 2 * Math.Cos(radians - Math.PI / 2))),

                new PointF((float) (_center.X + length * Math.Sin(radians)),
                    (float) (_center.Y - length * Math.Cos(radians))),

                new PointF((float) (_center.X - thickness * 4 * Math.Sin(radians)),
                    (float) (_center.Y + thickness * 4 * Math.Cos(radians)))
            };
            e.Graphics.FillPolygon(new SolidBrush(Helper.ClockColorHand), points);
        }
    }
}
