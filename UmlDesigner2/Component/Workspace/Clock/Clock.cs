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
    public sealed partial class Clock : Panel
    {
        readonly Timer _timer = new Timer() {Interval = 100};
        private DateTime _beginExam;
        private DateTime _endExam;
        private ToolTip _toolTip;
        private ContextMenuStrip _contextMenu;
        public Clock()
        {
            DoubleBuffered = true;
            UpdateChanges();
            Start(); //bedzie wywolywane z zewnątrz
            _contextMenu=new ContextMenuStrip();
            _contextMenu.Items.Add("Zegar Analogowy");
            _contextMenu.Items.Add("Zegar Cyfrowy #1");
            _contextMenu.Items.Add("Zegar Cyfrowy #2");
            _contextMenu.Items.Add("Wyłącz zegar");
            _contextMenu.ItemClicked += _contextMenu_ItemClicked;
            this.ContextMenuStrip = _contextMenu;
        }
        public void Start()
        {
            if (ClockVariables.isRunnable)
            {
                _timer.Start();
                _timer.Tick += Timer_Tick;
                _beginExam = new DateTime(DateTime.Now.Ticks);
                if (ClockVariables.TimeForExam.TotalSeconds!=0)
                    _endExam = _beginExam.Add(ClockVariables.TimeForExam);
                ClockVariables.isRunning = true;
                ClockVariables.isRunnable = false;
                _toolTip = new ToolTip {AutoPopDelay = 3000, InitialDelay = 1000, ReshowDelay = 500, ShowAlways = true};
            }
        }

        public void End()
        {
            if (!ClockVariables.isRunnable)
            {
                _timer.Stop();
                ClockVariables.isRunning = false;
                ClockVariables.isRunnable = true;
                MessageBox.Show(ClockVariables.MessageWhenTimeIsOver);
            }
        }

        public void UpdateChanges() //metoda służąca do aktualizowania wrazie zmian.
        {
            BackColor = ClockVariables.bgColor;
            Size = new Size(ClockVariables.ClockSize, ClockVariables.ClockSize);
            switch (ClockVariables.ChoosenClockType)
            {
                case (ClockVariables.ClockType.Analog):
                    AnalogUpdate();
                    break;
                default:
                    DigitalUpdate();
                    break;
            }

        }
        private void _contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.Equals("Zegar Analogowy"))
            {
                ClockVariables.ChoosenClockType = ClockVariables.ClockType.Analog;
                UpdateChanges();
            }
            if (e.ClickedItem.Text.Equals("Zegar Cyfrowy #1") || e.ClickedItem.Text.Equals("Zegar Cyfrowy Odliczający"))
            {
                ClockVariables.ChoosenClockType = ClockVariables.ClockType.DigitalCountingDown;
                UpdateChanges();
            }
            if (e.ClickedItem.Text.Equals("Zegar Cyfrowy #2") || e.ClickedItem.Text.Equals("Zegar Cyfrowy Naliczający"))
            {
                ClockVariables.ChoosenClockType = ClockVariables.ClockType.DigitalCountingUp;
                UpdateChanges();
            }
            if (e.ClickedItem.Text.Contains("Wyłącz"))
            {
                this.Dispose();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            if (ClockVariables.TimeForExam.TotalSeconds!=0)
            {
                if (DateTime.Now >= _endExam)
                {
                    End();
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
                    ClockVariables.MessageWhenTimeIsOver);
            }
        }
       

        protected override void CreateHandle() //ustawienie położenia po dodaniu kontrolki do "parenta"
        {
            base.CreateHandle();
            this.Location = new Point(this.Parent.Size.Width - this.Width, 0);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.Parent != null)
                this.Location = new Point(this.Parent.Size.Width - this.Width, 0);
        }

        //rysowanie odpowiedniego zegara na podstawie wybranej opcji
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            switch (ClockVariables.ChoosenClockType)
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
    }
}
