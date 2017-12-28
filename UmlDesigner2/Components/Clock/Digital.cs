using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UmlDesigner2.Class;

namespace UmlDesigner2.Components.Clock
{
    //Digital Clock Class
    partial class Clock
    {
        private readonly StringBuilder _textForDigitalClock = new StringBuilder();

        private void DigitalUpdate()
        {
            using (var f = new Font("Arial", 10))
            using (Graphics g = CreateGraphics())
            {
                var tempText = new StringBuilder(DateTime.Now.ToLongTimeString() + Environment.NewLine +
                                                 DateTime.Now.ToLongTimeString());
                Size = g.MeasureString(tempText.ToString(),
                        DigitalFindMeasuredFont(g, tempText, new Size(Helper.ClockSize, Helper.ClockSize), f))
                    .ToSize();
            }
        }
        
        private void DrawDigitalCountingUp(ref PaintEventArgs e)
        {
            _textForDigitalClock.Clear();
            if (_endExam == DateTime.MinValue)
                _textForDigitalClock.Append(DateTime.Now.ToLongTimeString() + Environment.NewLine +
                                            new DateTime(0).ToLongTimeString());
            else if (DateTime.Now > _endExam)
                _textForDigitalClock.Append(_beginExam.ToLongTimeString() + Environment.NewLine +
                                            new DateTime(_endExam.Ticks - _beginExam.Ticks).ToLongTimeString());
            else
                _textForDigitalClock.Append(_beginExam.ToLongTimeString() + Environment.NewLine +
                                            new DateTime(DateTime.Now.Ticks - _beginExam.Ticks).ToLongTimeString());

            e.Graphics.DrawString(_textForDigitalClock.ToString(),
                DigitalFindMeasuredFont(e.Graphics, _textForDigitalClock, this.Size, new Font("Arial", 10)), Brushes.Black,
                new PointF(0, 0));
        }

        private void DrawDigitalCountingDown(ref PaintEventArgs e)
        {
            _textForDigitalClock.Clear();
            try
            {
                _textForDigitalClock.Append(_beginExam.ToLongTimeString() + Environment.NewLine +
                                            new DateTime(_endExam.Ticks - DateTime.Now.Ticks).ToLongTimeString());
                e.Graphics.DrawString(_textForDigitalClock.ToString(),
                    DigitalFindMeasuredFont(e.Graphics, _textForDigitalClock, Size, new Font("Arial", 10)), Brushes.Black,
                    new PointF(0, 0));
            }
            catch (ArgumentOutOfRangeException)
            {
                _textForDigitalClock.Append(DateTime.Now.ToLongTimeString() + Environment.NewLine +
                                            new DateTime(0).ToLongTimeString());
                e.Graphics.DrawString(_textForDigitalClock.ToString(),
                    DigitalFindMeasuredFont(e.Graphics, _textForDigitalClock, Size, new Font("Arial", 10)), Brushes.Black,
                    new PointF(0, 0));
            }

        }

        private static Font DigitalFindMeasuredFont(Graphics g, StringBuilder longString, Size room,
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

