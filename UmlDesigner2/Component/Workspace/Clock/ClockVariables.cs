using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlDesigner2.Component.Workspace.Clock
{
    public static class ClockVariables
    {
        public static bool isRunnable = true;
        public static bool isRunning = false;
        public static Color bgColor = Color.White;
        public enum ClockType
        {
            Analog = 1,
            DigitalCountingDown = 2,
                DigitalCountingUp =3
        };
        public static ClockType ChoosenClockType = ClockType.DigitalCountingDown;
        public static Size ClockSize = new Size(200,100);
        public static TimeSpan TimeForExam = new TimeSpan(0, 0, 15);
        public static int ClockFontSize = 16;
    }
}
