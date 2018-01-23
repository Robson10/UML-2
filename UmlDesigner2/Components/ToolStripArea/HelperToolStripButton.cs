using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SbWinNew.Class;

namespace SbWinNew.Components.ToolStripArea
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
                case (StripButtons.NewFile): return "Nowy projekt"+Environment.NewLine +KeyToString(Helper.KeyNewFile);
                case (StripButtons.OpenFile): return "Otwórz projekt" + Environment.NewLine + KeyToString(Helper.KeyOpenFile);
                case (StripButtons.SaveFile): return "Zapisz projekt" + Environment.NewLine + KeyToString(Helper.KeySaveFile);
                case (StripButtons.SaveFileAs): return "Zapisz jako" + Environment.NewLine + KeyToString(Helper.KeySaveFileAs);
                case (StripButtons.Undo): return "Cofnij" + Environment.NewLine + KeyToString(Helper.KeyUndo);
                case (StripButtons.Redo): return "Powtórz" + Environment.NewLine + KeyToString(Helper.KeyRedo);
                case (StripButtons.Options): return "Opcje";
                case (StripButtons.LogIn): return "Zaloguj";
                case (StripButtons.OpenFileFromServer): return "Otwórz plik z serwera" + Environment.NewLine + KeyToString(Helper.KeyOpenFileFromServer);
                case (StripButtons.SaveFileOnServer): return "Zapisz projekt na serwerze" + Environment.NewLine + KeyToString(Helper.KeySaveFileOnServer);
                case (StripButtons.Run): return "Uruchom projekt" + Environment.NewLine + KeyToString(Helper.KeyRun);
                case (StripButtons.Debug): return "Debuguj projekt" + Environment.NewLine + KeyToString(Helper.KeyDebug);
                default: return "Error";
            }
        }

        public static int IconSize = 3;
        public static Image GetIcon(StripButtons buttonType, int size)
        {
            string path = "SbWinNew.Icons.";
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
        private static string KeyToString(Keys shortcut)
        {
            var text = string.Empty;
                var keyArr = shortcut.ToString().Split(',').ToList();
                findKey(Keys.Control, ref text, ref keyArr);
                findKey(Keys.Shift, ref text, ref keyArr);
                findKey(Keys.Alt, ref text, ref keyArr);
                for (int j = 0; j < keyArr.Count; j++)
                {
                    if (!text.Equals(string.Empty))
                        text += " + ";
                    text += keyArr[j];
                }
                return text;
            
        }
        private static void findKey(Keys key, ref string text, ref List<string> keyArr)
        {
            var index = keyArr.FindIndex(stringToCheck => stringToCheck.Contains(key.ToString()));
            if (index != -1)
            {
                if (!text.Equals(string.Empty))
                    text += " +";
                text += keyArr[index];
                keyArr.RemoveAt(index);
            }
        }
    }
}
