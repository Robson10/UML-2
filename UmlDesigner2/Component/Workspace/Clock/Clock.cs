using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.Clock
{
    public partial class Clock: Panel
    {
        Timer timer = new Timer() { Interval = 1000 };
        DateTime BeginExam;
        DateTime EndExam;
        public Clock()
        {
            this.DoubleBuffered = true;
            update();

            timer.Tick += Timer_Tick;
            Start();//bedzie wywolywane z zewnątrz

        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            //if (ClockVariables.TimeForExam != null)
            //{
            //    if (DateTime.Now > EndExam)
            //    {
            //        string asd = DateTime.Now.ToLongTimeString() + "XXX" + EndExam.ToLongTimeString();
            //        End();
            //    }
            //    else
            //    {
            //    }
            //}
            this.Invalidate();
        }
        public void Start()
        {
            if (ClockVariables.isRunnable)
            {
                timer.Start();
                BeginExam = new DateTime(DateTime.Now.Ticks);
                if (ClockVariables.TimeForExam != null)
                    EndExam = BeginExam.Add(ClockVariables.TimeForExam);
                ClockVariables.isRunning = true;
                ClockVariables.isRunnable = false;
            }

        }
        //public void End()
        //{
        //    if (!ClockVariables.isRunnable)
        //    {
        //        timer.Stop();
        //        ClockVariables.isRunning = false;
        //        ClockVariables.isRunnable = true;
        //        MessageBox.Show("koniec");
        //    }
        //}

        protected override void CreateHandle()//ustawienie położenia po dodaniu kontrolki do "parenta"
        {
            base.CreateHandle();
            this.Location = new Point(this.Parent.Size.Width - this.Width, 0);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.Parent!=null)
            this.Location = new Point(this.Parent.Size.Width - this.Width, 0);
        }

        //rysowanie odpowiedniego zegara na podstawie wybranej opcji
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            switch(ClockVariables.ChoosenClockType)
            {
                case (ClockVariables.ClockType.DigitalCountingDown):
                    DrawDigitalCountingDown(ref e);
                    break;
                case (ClockVariables.ClockType.DigitalCountingUp):
                    DrawDigitalCountingUp(ref e);
                    break;
                case (ClockVariables.ClockType.Analog):
                    DrawAnalog(ref e);
                    break;
            }
        }
        private void update() //metoda służąca do aktualizowania wrazie zmian.
        {
            this.BackColor = ClockVariables.bgColor;
            this.Size = ClockVariables.ClockSize;
        }
    }

    //Digital Clock Class
    partial class Clock
    {
        StringBuilder digitalTime=new StringBuilder();
        private void DrawDigitalCountingUp(ref PaintEventArgs e)
        {
            digitalTime.Clear();
            digitalTime.Append(BeginExam.ToLongTimeString()+ Environment.NewLine + (BeginExam-DateTime.Now).ToString());
            e.Graphics.DrawString(digitalTime.ToString(), FindMeasuredFont(e.Graphics, digitalTime, this.Size, new Font("Arial", 10)), Brushes.Black, new PointF(0, 0));
        }
        private void DrawDigitalCountingDown(ref PaintEventArgs e)
        {
            digitalTime.Clear();
            digitalTime.Append(BeginExam.ToLongTimeString() + Environment.NewLine + new DateTime(DateTime.Now.Ticks - BeginExam.Ticks).ToLongTimeString());
            e.Graphics.DrawString(digitalTime.ToString(), FindMeasuredFont(e.Graphics, digitalTime, this.Size, new Font("Arial", 10)), Brushes.Black, new PointF(0, 0));
        }
        private Font FindMeasuredFont(System.Drawing.Graphics g, StringBuilder longString, Size Room, Font PreferedFont)
        {
            SizeF RealSize = g.MeasureString(longString.ToString(), PreferedFont);
            float HeightScaleRatio = Room.Height / RealSize.Height;
            float WidthScaleRatio = Room.Width / RealSize.Width;
            float ScaleRatio = (HeightScaleRatio < WidthScaleRatio) ? ScaleRatio = HeightScaleRatio : ScaleRatio = WidthScaleRatio;
            float ScaleFontSize = PreferedFont.Size * ScaleRatio;
            return new Font(PreferedFont.FontFamily, ScaleFontSize);
        }
    }
    //Analog Clock Class
    partial class Clock
    {
        private void DrawAnalog(ref PaintEventArgs e)
        {
            //e.Graphics.DrawString(DateTime.Now.ToLongTimeString(), FindMeasuredFont(e.Graphics, DateTime.Now.ToLongTimeString(), new Size(100, 10), new Font("Arial", 40)), Brushes.Black, new PointF(0, 0));
        }
    }
}
