using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlDesigner2.Component.Workspace
{
    public static class BlockParameters
    {
       
        //Canvas Object
        public static Size defaultCanvasControlSize = new Size(100, 50);
        public enum Shape
        {
            Nothing = 0,
            Start = 1,
            Input = 2,
            Execution = 3,
            Decision = 4,
            End = 5
        };
        public static SolidBrush CanvasObjectBackColor(Shape shape)
        {
            if (shape == Shape.Start) return new SolidBrush(Color.FromArgb(255,50,50,50));
            else if (shape == Shape.Input) return new SolidBrush(Color.FromArgb(255, 70, 70, 70));
            else if (shape == Shape.Execution) return new SolidBrush(Color.FromArgb(255, 90, 90, 90));
            else if (shape == Shape.End) return new SolidBrush(Color.FromArgb(255, 110, 110, 110));
            else if (shape == Shape.Decision) return new SolidBrush(Color.FromArgb(255, 130, 130, 130));
            else return new SolidBrush(Color.FromArgb(0, 250, 0, 0));
        }
        public static Color CanvasObjectFontColor(Shape shape)
        {
            if (shape == Shape.Start) return Color.Wheat;
            else if (shape == Shape.Input) return Color.Wheat;
            else if (shape == Shape.Execution) return Color.Wheat;
            else if (shape == Shape.End) return Color.Wheat;
            else if (shape == Shape.Decision) return Color.Wheat;
            else return Color.Red;
        }
        public static int CanvasObjectFontSize(Shape shape)
        {
            if (shape == Shape.Start) return 21;
            else if (shape == Shape.Input) return 21;
            else if (shape == Shape.Execution) return 21;
            else if (shape == Shape.End) return 21;
            else if (shape == Shape.Decision) return 21;
            else return 1;
        }
        public static string CanvasObjectText(Shape shape)
        {
            if (shape == Shape.Start) return "Start";
            else if (shape == Shape.Input) return "Input";
            else if (shape == Shape.Execution) return "Execution";
            else if (shape == Shape.End) return "End";
            else if (shape == Shape.Decision) return "Decision";
            else return "Error";
        }

        public static string BlockStart_Text = "Start";
        public static string BlockEnd_Text = "End";
        public static Size RubberSize = new Size(10, 10);
        public static Size MinSizeForControl = new Size(50, 50);
        public static System.Windows.Forms.Keys MultiselectKey = System.Windows.Forms.Keys.ControlKey;
    }
}



















