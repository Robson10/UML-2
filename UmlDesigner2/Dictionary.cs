using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlDesigner2
{
    public static class MyDictionary
    {
        //ToolStrip
        public enum StripButtons
        {
            Start = 1,
            Input = 2,
            Execution = 3,
            Decision = 4,
            End = 5,

            NewFile = 6,
            OpenFile = 7,
            SaveFile = 8,
            SaveFileAs = 9,
            Redo = 10,
            Undo = 11,
            Options = 12,
            LogIn = 13,
            OpenFileFromServer = 14,
            Run=15,
            Debug=16
        };
        public static string StripButtonToolTip(StripButtons shape)
        {
            if (shape == StripButtons.Start)return "Start";
            else if (shape == StripButtons.Input)return "Input";
            else if (shape == StripButtons.Execution)return "Execution";
            else if (shape == StripButtons.End)return "End";
            else if (shape == StripButtons.Decision) return "Decision";

            else if (shape == StripButtons.NewFile) return "NewFile";
            else if (shape == StripButtons.OpenFile) return  "OpenFile";
            else if (shape == StripButtons.SaveFile) return "SaveFile";
            else if (shape == StripButtons.SaveFileAs) return "SaveFileAs";
            else if (shape == StripButtons.Redo) return "Redo";
            else if (shape == StripButtons.Undo) return "Undo";
            else if (shape == StripButtons.Options) return "Settings";
            else if (shape == StripButtons.LogIn) return "LogIn";
            else if (shape == StripButtons.OpenFileFromServer) return "OpenCloudFile";
            else if (shape == StripButtons.Run) return "Run";
            else if (shape == StripButtons.Debug) return "Debug";
            else return "Error";
        }

        public static int IconSize = 3;
        public static Image GetIcon(StripButtons shape,int size)
        {
            string path = "";
            if (size == 1)
                path = @"Ico\Small\";
            else if(size==2)
                path = @"Ico\Medium\";
            else 
                path = @"Ico\Big\";

            if (shape == StripButtons.Start) return Image.FromFile(path+@"Start.jpg");
            else if (shape == StripButtons.Input) return Image.FromFile(path + @"Input.jpg");
            else if (shape == StripButtons.Execution) return Image.FromFile(path + @"Execution.jpg");
            else if (shape == StripButtons.End) return Image.FromFile(path + @"End.jpg");
            else if (shape == StripButtons.Decision) return Image.FromFile(path + @"Decision.jpg");

            else if (shape == StripButtons.NewFile) return Image.FromFile(path + @"NewFile.png");
            else if (shape == StripButtons.OpenFile) return Image.FromFile(path + @"OpenFile.png");
            else if (shape == StripButtons.SaveFile) return Image.FromFile(path + @"SaveFile.png");
            else if (shape == StripButtons.SaveFileAs) return Image.FromFile(path + @"SaveFileAs.png");
            else if (shape == StripButtons.Redo) return Image.FromFile(path + @"Redo.png");
            else if (shape == StripButtons.Undo) return Image.FromFile(path + @"Undo.png");
            else if (shape == StripButtons.Options) return Image.FromFile(path + @"Settings.png");
            else if (shape == StripButtons.LogIn) return Image.FromFile(path + @"LogIn.png");
            else if (shape == StripButtons.OpenFileFromServer) return Image.FromFile(path + @"OpenCloudFile.png");
            else if (shape == StripButtons.Run) return Image.FromFile(path + @"Run.png");
            else if (shape == StripButtons.Debug) return Image.FromFile(path + @"Debug.png");

            else return Image.FromFile(path+@"Error.png");
        }

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
    }
}



















