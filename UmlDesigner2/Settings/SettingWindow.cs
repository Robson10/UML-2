using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UmlDesigner2.Settings.SetShortcut;
using UmlDesigner2.Settings.SetShortcut.Keyboard;

namespace UmlDesigner2.Settings
{
    public partial class SettingWindow : Form
    {
        //todo zapisać i odczytywac ustawienia np blokow -  zrobilem tylko skoty klawiaturowe
        private PropertyGrid _pg;
        private List<KeyboardKey> KeyList = new List<KeyboardKey>();
        public SettingWindow()
        {
            InitializeComponent();
            _pg = new PropertyGrid()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right|AnchorStyles.Bottom,
                PropertySort = PropertySort.Categorized|PropertySort.Alphabetical,
            };
            _pg.SelectedObject = new SettingsPropertyGrid();
            tabControl1.TabPages[1].Controls.Add(_pg);
            _pg.SetBounds(0, 0, Width, Height);
            _pg.Dock =DockStyle.Fill;
            signKeyToButton();
            AddButtonsToList();
            SetButtonText();
        }

        private void AddButtonsToList()
        {
            KeyList.Add(CopyBT);
            KeyList.Add(PasteBT);
            KeyList.Add(CutBT);
            KeyList.Add(DebugBT);
            KeyList.Add(CompileBT);
            KeyList.Add(NewFileBT);
            KeyList.Add(OpenFileBT);
            KeyList.Add(OpenFileFromServerBT);
            KeyList.Add(SaveFileBT);
            KeyList.Add(SaveFileAsBT);
            KeyList.Add(UndoBT);
            KeyList.Add(RedoBT);
        }
        private void signKeyToButton()
        {
            CopyBT.Key = Helper.KeyCopy;
            PasteBT.Key = Helper.KeyPaste;
            CutBT.Key = Helper.KeyCut;
            DebugBT.Key = Helper.KeyDebug;
            CompileBT.Key = Helper.KeyRun;
            NewFileBT.Key = Helper.KeyNewFile;
            OpenFileBT.Key = Helper.KeyOpenFile;
            OpenFileFromServerBT.Key = Helper.KeyOpenFileFromServer;
            SaveFileBT.Key = Helper.KeySaveFile;
            SaveFileAsBT.Key = Helper.KeySaveFileAs;
            UndoBT.Key = Helper.KeyUndo;
            RedoBT.Key = Helper.KeyRedo;
        }

        /// <summary>
        /// Metoda zamieniająca zmienną typu Keys na string i przypisująca ją do przycisku.
        /// </summary>
        /// <param name="bt"></param>
        /// <param name="key"></param>
        public void SetButtonText()
        {
            for (int i = 0; i < KeyList.Count; i++)
            {
                var text = string.Empty;
                var keyArr = KeyList[i].Key.ToString().Split(',').ToList();
                findKey(Keys.Control, ref text, ref keyArr);
                findKey(Keys.Shift, ref text, ref keyArr);
                findKey(Keys.Alt, ref text, ref keyArr);
                for (int j = 0; j < keyArr.Count; j++)
                {
                    if (!text.Equals(string.Empty))
                        text += " + ";
                    text += keyArr[j];
                }
                KeyList[i].Text = text;
            }
        }

        /// <summary>
        /// Metoda wyszukująca w tablicy element odpowiadający wartości key a następnie dodająca go do zmiennej text i usuwająca go z tablicy
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <param name="keyArr"></param>
        private void findKey(Keys key,ref string text,ref List<string> keyArr)
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

        private void changeShortcut(object sender, EventArgs e)
        {
            var setShortcutWindow = new SetShortcutWindow();
            if (setShortcutWindow.ShowDialog() == DialogResult.OK)
            {
                (sender as KeyboardKey).Key = setShortcutWindow.Shortcut;
                ChangeShortcutInHelper();
            }
        }

        private void ChangeShortcutInHelper()
        {
            Helper.KeyCopy = CopyBT.Key ;
            Helper.KeyPaste = PasteBT.Key;
            Helper.KeyCut = CutBT.Key;
            Helper.KeyDebug = DebugBT.Key;
            Helper.KeyRun = CompileBT.Key;
            Helper.KeyNewFile = NewFileBT.Key;
            Helper.KeyOpenFile = OpenFileBT.Key;
            Helper.KeyOpenFileFromServer = OpenFileFromServerBT.Key;
            Helper.KeySaveFile = SaveFileBT.Key;
            Helper.KeySaveFileAs = SaveFileAsBT.Key;
            Helper.KeyUndo = UndoBT.Key;
            Helper.KeyRedo = RedoBT.Key;
            SetButtonText();
            Helper.SaveShortcuts();
        }
    }
    
}
