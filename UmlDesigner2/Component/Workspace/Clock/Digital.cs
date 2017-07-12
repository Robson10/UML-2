using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.Clock
{
        //Digital Clock Class
        sealed partial class Clock
        {
            private void DigitalUpdate()
            {

            }
            readonly StringBuilder _digitalTime = new StringBuilder();

            private void DrawDigitalCountingUp(ref PaintEventArgs e)
            {
                _digitalTime.Clear();
                _digitalTime.Append(_beginExam.ToLongTimeString() + Environment.NewLine + new DateTime(DateTime.Now.Ticks - _beginExam.Ticks).ToLongTimeString());
                e.Graphics.DrawString(_digitalTime.ToString(),
                    FindMeasuredFont(e.Graphics, _digitalTime, this.Size, new Font("Arial", 10)), Brushes.Black,
                    new PointF(0, 0));
            }

            private void DrawDigitalCountingDown(ref PaintEventArgs e)
            {
                _digitalTime.Clear();
                try
                {
                    _digitalTime.Append(_beginExam.ToLongTimeString() + Environment.NewLine +
                                        new DateTime(_endExam.Ticks - DateTime.Now.Ticks).ToLongTimeString());
                    e.Graphics.DrawString(_digitalTime.ToString(),
                        FindMeasuredFont(e.Graphics, _digitalTime, this.Size, new Font("Arial", 10)), Brushes.Black,
                        new PointF(0, 0));
                }
                catch (ArgumentOutOfRangeException)
                {
                    _digitalTime.Append(_beginExam.ToLongTimeString() + Environment.NewLine +
                                        new DateTime(0).ToLongTimeString());
                    e.Graphics.DrawString(_digitalTime.ToString(),
                        FindMeasuredFont(e.Graphics, _digitalTime, this.Size, new Font("Arial", 10)), Brushes.Black,
                        new PointF(0, 0));
                }

            }

            private static Font FindMeasuredFont(System.Drawing.Graphics g, StringBuilder longString, Size room,
                Font preferedFont)
            {
                var realSize = g.MeasureString(longString.ToString(), preferedFont);
                var heightScaleRatio = room.Height / realSize.Height;
                var widthScaleRatio = room.Width / realSize.Width;
                var scaleRatio = (heightScaleRatio < widthScaleRatio) ? heightScaleRatio : widthScaleRatio;
                var scaleFontSize = preferedFont.Size * scaleRatio;
                return new Font(preferedFont.FontFamily, scaleFontSize);
            }
        }
    }

