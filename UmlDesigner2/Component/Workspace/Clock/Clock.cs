﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.Clock
{
    public partial class Clock : Panel
    {
        readonly Timer _timer = new Timer() {Interval = 400};
        private DateTime _beginExam;
        private DateTime _endExam;
        private ToolTip _toolTip;
        private ContextMenuStrip _contextMenu;
        public Clock()
        {
            DoubleBuffered = true;
            //Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Update();
            Start(); //bedzie wywolywane z zewnątrz
            _contextMenu=new ContextMenuStrip();
            _contextMenu.Items.Add("Zegar Analogowy");
            _contextMenu.Items.Add("Zegar Cyfrowy #1");
            _contextMenu.Items.Add("Zegar Cyfrowy #2");
            _contextMenu.Items.Add("Wyłącz zegar");
            _contextMenu.ItemClicked += _contextMenu_ItemClicked;
            this.ContextMenuStrip = _contextMenu;
        }
        public void Start()//bedzie wywolywane z zewnątrz
        {
            if (Helper.ClockIsRunnable)
            {
                Helper.ClockIsRunning = true;
                Helper.ClockIsRunnable = false;
                _beginExam = new DateTime(DateTime.Now.Ticks);
                if (Math.Abs(Helper.ClockTimeForExam.TotalSeconds) > 0)
                    _endExam = _beginExam.Add(Helper.ClockTimeForExam);
                if (_endExam != DateTime.MinValue)
                {
                    _timer.Start();
                    _timer.Tick += Timer_Tick;
                }
                else
                    Stop();

                _toolTip = new ToolTip {AutoPopDelay = 3000, InitialDelay = 1000, ReshowDelay = 500, ShowAlways = true};
                //Wyczyścić workSpace przed rozpoczęciem egzaminu
            }
        }

        public void Stop()
        {
            if (!Helper.ClockIsRunnable)
            {
                _timer.Stop();
                Helper.ClockIsRunning = false;
                Helper.ClockIsRunnable = true;
                MessageBox.Show(Helper.ClockMessageWhenTimeIsOver);
                //Wysłać event do workspace by zablokować wszelkie zmiany na nim
            }
        }
        public new void Update()//metoda służąca do aktualizowania wrazie zmian.
        {
            base.Update();
            BackColor = Helper.ClockBackColor;
            Size = new Size(Helper.ClockSize, Helper.ClockSize);
            OnResize(null);
            switch (Helper.ClockChoosenType)
            {
                case (Helper.ClockType.Analog):
                    AnalogUpdate();
                    break;
                default:
                    DigitalUpdate();
                    break;
            }
            Invalidate();

        }

        private void _contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.Equals("Zegar Analogowy"))
            {
                Helper.ClockChoosenType = Helper.ClockType.Analog;
                Update();
            }
            else if (e.ClickedItem.Text.Equals("Zegar Cyfrowy #1") || e.ClickedItem.Text.Equals("Zegar Cyfrowy Odliczający"))
            {
                Helper.ClockChoosenType = Helper.ClockType.DigitalCountingDown;
                Update();
            }
            else if (e.ClickedItem.Text.Equals("Zegar Cyfrowy #2") || e.ClickedItem.Text.Equals("Zegar Cyfrowy Naliczający"))
            {
                Helper.ClockChoosenType = Helper.ClockType.DigitalCountingUp;
                Update();
            }
            else if (e.ClickedItem.Text.Contains("Wyłącz"))
            {
                Dispose();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            if (Math.Abs(Helper.ClockTimeForExam.TotalSeconds) >= 0)
            {
                if (DateTime.Now >= _endExam)
                {
                    Stop();
                }
            }
        }

      
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            try
            {
                _toolTip.SetToolTip(this,
                    "Pozostało ci: " + new DateTime(_endExam.Ticks - DateTime.Now.Ticks).ToLongTimeString());
            }
            catch
            {
                _toolTip.SetToolTip(this,
                    Helper.ClockMessageWhenTimeIsOver);
            }
        }
       
        

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Parent != null)
                Location = new Point(this.Parent.Size.Width - this.Width, 0);
        }

        //rysowanie odpowiedniego zegara na podstawie wybranej opcji
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            switch (Helper.ClockChoosenType)
            {
                case (Helper.ClockType.DigitalCountingDown):
                    DrawDigitalCountingDown(ref e);
                    break;
                case (Helper.ClockType.DigitalCountingUp):
                    DrawDigitalCountingUp(ref e);
                    break;
                case (Helper.ClockType.Analog):
                    DrawAnalog(ref e);
                    break;
            }
        }
    }
}
