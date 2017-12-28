using System.Drawing;

namespace UmlDesigner2.Components.ToolStripArea
{
    public class ToolStripButtonParameters
    {
        public enum StripButtons
        {
            NewFile,
            OpenFile,
            SaveFile,
            SaveFileAs,
            Redo,
            Undo,
            Options,
            LogIn,
            OpenFileFromServer,
            SaveFileOnServer,
            Run,
            Debug
        };
        public static string StripButtonToolTip(StripButtons buttonType)
        {
            switch (buttonType)
            {
                case (StripButtons.NewFile): return "Nowy projekt";
                case (StripButtons.OpenFile): return "Otwórz projekt";
                case (StripButtons.SaveFile): return "Zapisz projekt";
                case (StripButtons.SaveFileAs): return "Zapisz jako";
                case (StripButtons.Redo): return "Cofnij";
                case (StripButtons.Undo): return "Powtórz";
                case (StripButtons.Options): return "Opcje";
                case (StripButtons.LogIn): return "Zaloguj";
                case (StripButtons.OpenFileFromServer): return "Otwórz plik z serwera";
                case (StripButtons.SaveFileOnServer): return "Zapisz projekt na serwerze";
                case (StripButtons.Run): return "Uruchom projekt";
                case (StripButtons.Debug): return "Debuguj projekt";
                default: return "Error";
            }
        }

        public static int IconSize = 3;
        public static Image GetIcon(StripButtons buttonType, int size)
        {
            string path = "UmlDesigner2.Icons.";
            switch (buttonType)
            {
                case (StripButtons.NewFile): return ImageRead(path + "NewFile.png");
                case (StripButtons.OpenFile): return ImageRead(path + "OpenFile.png");
                case (StripButtons.SaveFile): return ImageRead(path + "SaveFile.png");
                case (StripButtons.SaveFileAs): return ImageRead(path + "SaveFileAs.png");
                case (StripButtons.Redo): return ImageRead(path + "Redo.png");
                case (StripButtons.Undo): return ImageRead(path + "Undo.png");
                case (StripButtons.Options): return ImageRead(path + "Settings.png");
                case (StripButtons.LogIn): return ImageRead(path + "LogIn.png");
                case (StripButtons.OpenFileFromServer): return ImageRead(path + "OpenFileFromServer.png");
                case (StripButtons.SaveFileOnServer):return ImageRead(path + "SaveFileOnServer.png");
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
