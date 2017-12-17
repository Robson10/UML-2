using System;
using System.Windows.Forms;
using UmlDesigner2.Component.TabsArea.BlockProp;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2.MainWindow
{
    public partial class Form1 : Form
    {
        private static Component.Workspace.ResultComponent.Results _results =
            new Component.Workspace.ResultComponent.Results();

        private BlockProperties _properties;

        public Form1()
        {
            InitializeComponent();
            View();
            KeyPreview = true;
            AddEvents();
            splitContainer3.Panel2.Controls.Add(_results);
            _results.Size = splitContainer3.Panel2.ClientRectangle.Size;
            Helper.LoadShortcuts();
        }


        //todo zablokowanie wszystkiego procz kompilacji i zapisu pliku
        private void Clock1_EgzamEnded(object sender, EventArgs e)
        {
            myToolStrip1.BlockButtonsForEgzam();
        }

        //todo zablokować wczytywanie pliku
        private void Clock1_EgzamStarted(object sender, EventArgs e)
        {
            canvas1.ClearCanvas();
            myToolStrip1.BlockButtonsForEgzam();
        }
        
        private void _properties_BlockPropertyChanged(object sender, EventArgs e)
        {
            canvas1.UpdatePropertiesSelectedBlock();
        }
        
        private void Canvas1_ShowBlockPoperites(object sender, EventArgs e)
        {
            var temp = (sender as MyBlock);
            if (_properties != null)
                if (_properties.ShouldRefresh(temp))
                {
                    _properties.UpdateProperties();
                    return;
                }
                else
                    splitContainer2.Panel2.Controls.Remove(_properties);

            _properties = new BlockProperties(temp)
            {
                Width = splitContainer2.Panel2.Width,
                Height = splitContainer2.Panel2.Height,
                Location = new System.Drawing.Point(0, 0)
            };

            _properties.BlockPropertyChanged += _properties_BlockPropertyChanged;
            splitContainer2.Panel2.Controls.Add(_properties);
        }

        private void Canvas1_HideBlockPoperites(object sender, EventArgs e)
        {
            splitContainer2.Panel2.Controls.Remove(_properties);
            _properties = null;
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    canvas1.IsMultiSelect = false;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers | e.KeyCode) == (Helper.KeyMultiselect))
                canvas1.IsMultiSelect = true;
            else if ((e.Modifiers | e.KeyCode) == (Helper.KeyRun))
                Run();
            else if ((e.Modifiers | e.KeyCode) == (Helper.SelectAll))
                canvas1.SelectAll();
            else if ((e.Modifiers | e.KeyCode) == (Helper.KeyCopy))
                canvas1.Copy();
            else if ((e.Modifiers | e.KeyCode) == (Helper.KeyCut))
                canvas1.Cut();
            else if ((e.Modifiers | e.KeyCode) == (Helper.KeyPaste))
                canvas1.Paste();
            else if ((e.Modifiers | e.KeyCode) == (Helper.KeyUndo))
                canvas1.Undo();
            else if ((e.Modifiers | e.KeyCode) == (Helper.KeyRedo))
                canvas1.Redo();
            else if ((e.Modifiers | e.KeyCode) == (Helper.KeySaveFile))
                SaveFile();
            else if ((e.Modifiers | e.KeyCode) == (Helper.KeyNewFile))
                NewFile();
            else if ((e.Modifiers | e.KeyCode) == (Helper.KeyOpenFile))
                OpenFile();
            else if ((e.Modifiers | e.KeyCode) == (Helper.KeyDebug))
                Debug();
            else if ((e.Modifiers | e.KeyCode) == (Helper.KeySaveFileAs))
                SaveFileAs();
            else if ((e.Modifiers | e.KeyCode) == (Helper.KeyOpenFileFromServer))
                OpenFileFromServer();
            else if ((e.Modifiers | e.KeyCode) == (Keys.None | Keys.Escape))
                canvas1.AbortAddingObject();
            else if ((e.Modifiers | e.KeyCode) == (Keys.None | Keys.Delete))
            {
                if (_properties != null)
                {
                    if (!_properties.ContainsFocus)
                        canvas1.Delete();
                }
                else
                    canvas1.Delete();
            }
        }
    }
}
