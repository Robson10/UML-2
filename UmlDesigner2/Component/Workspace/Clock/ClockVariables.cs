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
        public static bool IsRunnable { get; set; } = true;
        public static bool IsRunning { get; set; } = false;
        public static Color BgColor { get; set; } = Color.Transparent;

        public enum ClockType
        {
            Analog = 1,
            DigitalCountingDown = 2,
            DigitalCountingUp =3
        };
        public static ClockType ChoosenClockType = ClockType.Analog;
        public static int ClockSize = 100;
        public static TimeSpan TimeForExam = new TimeSpan(2, 0, 0);
        public static Color ColorHandOfClock= Color.Black;
        public static Color ColorOfClockScale = Color.Black;
        public static string MessageWhenTimeIsOver = "Skończył ci się czas";
        public static Brush PartOfTimeColor = Brushes.LightSalmon;
    }
}
