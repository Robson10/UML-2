using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmlDesigner2.Component
{
    public class ToolStripButtonParameters
    {
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
            Run = 15,
            Debug = 16
        };
        public static string StripButtonToolTip(StripButtons buttonType)
        {
            switch (buttonType)
            {
                case (StripButtons.Start): return "Start";
                case (StripButtons.Input): return "Input";
                case (StripButtons.Execution): return "Execution";
                case (StripButtons.End): return "End";
                case (StripButtons.Decision): return "Decision";

                case (StripButtons.NewFile): return "NewFile";
                case (StripButtons.OpenFile): return "OpenFile";
                case (StripButtons.SaveFile): return "SaveFile";
                case (StripButtons.SaveFileAs): return "SaveFileAs";
                case (StripButtons.Redo): return "Redo";
                case (StripButtons.Undo): return "Undo";
                case (StripButtons.Options): return "Settings";
                case (StripButtons.LogIn): return "LogIn";
                case (StripButtons.OpenFileFromServer): return "OpenCloudFile";
                case (StripButtons.Run): return "Run";
                case (StripButtons.Debug): return "Debug";
                default: return "Error";
            }
        }

        public static int IconSize = 3;
        public static Image GetIcon(StripButtons buttonType, int size)
        {
            string path = "UmlDesigner2.Icons.";
            if (size == 1)
                path += "Small.";
            else if (size == 2)
                path += "Medium.";
            else
                path += "Big.";
            switch (buttonType)
            {

                case (StripButtons.Start): return ImageRead(path + "Start.jpg");
                case (StripButtons.Input): return ImageRead(path + "Input.jpg");
                case (StripButtons.Execution): return ImageRead(path + "Execution.jpg");
                case (StripButtons.End): return ImageRead(path + "End.jpg");
                case (StripButtons.Decision): return ImageRead(path + "Decision.jpg");

                case (StripButtons.NewFile): return ImageRead(path + "NewFile.png");
                case (StripButtons.OpenFile): return ImageRead(path + "OpenFile.png");
                case (StripButtons.SaveFile): return ImageRead(path + "SaveFile.png");
                case (StripButtons.SaveFileAs): return ImageRead(path + "SaveFileAs.png");
                case (StripButtons.Redo): return ImageRead(path + "Redo.png");
                case (StripButtons.Undo): return ImageRead(path + "Undo.png");
                case (StripButtons.Options): return ImageRead(path + "Settings.png");
                case (StripButtons.LogIn): return ImageRead(path + "LogIn.png");
                case (StripButtons.OpenFileFromServer): return ImageRead(path + "OpenCloudFile.png");
                case (StripButtons.Run): return ImageRead(path + "Run.png");
                case (StripButtons.Debug): return ImageRead(path + "Debug.png");

                default: return ImageRead(path + "Error.png");
            }
        }
        private static Image ImageRead(string SolutionPath)
        {
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(SolutionPath))
            {
                return Image.FromStream(stream);
            }
        }
    }
}
