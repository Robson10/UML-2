using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2
{
    public class BlocksData
    {
        public static Size RubberSize = new Size(10, 10);
        public static Color RubberColor = Color.Silver;
        public static Color TrueLineBackColor = (Color.Green);
        public static Color FalseLineBackColor = (Color.Red);

        public static System.Windows.Forms.Keys MultiselectKey = System.Windows.Forms.Keys.ControlKey;

        public static Size DefaultSize = new Size(100, 50);
        public static Size MinSize = new Size(10, 10);

        public enum Shape
        {
            Nothing = 0,
            Start = 1,
            Input = 2,
            Execution = 3,
            Decision = 4,
            End = 5,
            ConnectionLine = 6
        };

        public static Color BackColor(Shape shape)
        {
            switch (shape)
            {
                case Shape.Start:
                    return (Color.FromArgb(255, 50, 50, 50));
                case Shape.Input:
                    return (Color.FromArgb(255, 70, 70, 70));
                case Shape.Execution:
                    return (Color.FromArgb(255, 90, 90, 90));
                case Shape.End:
                    return (Color.FromArgb(255, 110, 110, 110));
                case Shape.Decision:
                    return (Color.FromArgb(255, 130, 130, 130));
                case Shape.ConnectionLine:
                    return (Color.FromArgb(255, 0, 0, 0));
            }
            return Color.FromArgb(0, 250, 0, 0);
        }

        public static Color FontColor(Shape shape)
        {
            switch (shape)
            {
                case Shape.Start:
                    return Color.Wheat;
                case Shape.Input:
                    return Color.Wheat;
                case Shape.Execution:
                    return Color.Wheat;
                case Shape.End:
                    return Color.Wheat;
                case Shape.Decision:
                    return Color.Wheat;
                default:
                    return Color.Red;
            }
        }

        public static int FontSize(Shape shape)
        {
            switch (shape)
            {
                case Shape.Start:
                    return 21;
                case Shape.Input:
                    return 21;
                case Shape.Execution:
                    return 21;
                case Shape.End:
                    return 21;
                case Shape.Decision:
                    return 21;
                default:
                    return 1;
            }
        }

        public static string Name(Shape shape)
        {
            switch (shape)
            {
                case Shape.Start:
                    return "Start";
                case Shape.Input:
                    return "Input";
                case Shape.Execution:
                    return "Execution";
                case Shape.End:
                    return "End";
                case Shape.Decision:
                    return "Decision";
                case Shape.ConnectionLine:
                    return "ConnectionLine";
                default:
                    return "Error";
            }
        }

        public static string Text(Shape shape)
        {
            switch (shape)
            {
                case Shape.Start:
                    return "Blok Startu";
                case Shape.Input:
                    return "Blok Wprowadzania";
                case Shape.Execution:
                    return "Blok Wykonawczy";
                case Shape.End:
                    return "Blok Końca";
                case Shape.Decision:
                    return "Blok Decyzyjny";
                case Shape.ConnectionLine:
                    return "Linia łącząca";
                default:
                    return "Error";
            }
        }

        public static string ImgPath(Shape shape)
        {
            switch (shape)
            {
                case Shape.Start:
                    return "UmlDesigner2.Icons.Start.jpg";
                case Shape.Input:
                    return "UmlDesigner2.Icons.Input.jpg";
                case Shape.Execution:
                    return "UmlDesigner2.Icons.Execution.jpg";
                case Shape.End:
                    return "UmlDesigner2.Icons.End.jpg";
                case Shape.Decision:
                    return "UmlDesigner2.Icons.Decision.jpg";
                case Shape.ConnectionLine:
                    return "UmlDesigner2.Icons.ConnectionLine.jpg";
                default:
                    return "Error";
            }
        }
    }
}
