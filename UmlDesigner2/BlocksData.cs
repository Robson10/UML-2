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
        public static Size defaultCanvasControlSize = new Size(100, 50);
        public static Size RubberSize = new Size(10, 10);
        public static Color RubberColor = Color.Silver;
        public static Size MinSizeForControl = new Size(50, 50);
        public static System.Windows.Forms.Keys MultiselectKey = System.Windows.Forms.Keys.ControlKey;
        public static Size MinimumBlockSize = new Size(10, 10);
        public enum Shape
        {
            Nothing = 0,
            Start = 1,
            Input = 2,
            Execution = 3,
            Decision = 4,
            End = 5
        };

        public static SolidBrush BackColor(Shape shape)
        {
            switch (shape)
            {
                case Shape.Start:
                    return new SolidBrush(Color.FromArgb(255, 50, 50, 50));
                case Shape.Input:
                    return new SolidBrush(Color.FromArgb(255, 70, 70, 70));
                case Shape.Execution:
                    return new SolidBrush(Color.FromArgb(255, 90, 90, 90));
                case Shape.End:
                    return new SolidBrush(Color.FromArgb(255, 110, 110, 110));
                case Shape.Decision:
                    return new SolidBrush(Color.FromArgb(255, 130, 130, 130));
            }
            return new SolidBrush(Color.FromArgb(0, 250, 0, 0));
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
                default:
                    return "Error";
            }
        }

        public static string ImagePath(Shape shape)
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
                default:
                    return "Error";
            }
        }
    }
}
