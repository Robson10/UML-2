using System;
using System.Windows.Forms;
using UmlDesigner2.Component.TabsArea.BlockPropertis;
using UmlDesigner2.Component.ToolStripArea;
using UmlDesigner2.Component.Workspace;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2
{
    public partial class Form1 : Form
    {
        private static Component.Workspace.ResultComponent.Results _results = new Component.Workspace.ResultComponent.Results();
        private BlockProperties _properties;
        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            KeyPreview = true;
            View();
            AddEvents();
            splitContainer3.Panel2.Controls.Add(_results);
            _results.Size = splitContainer3.Panel2.ClientRectangle.Size;

        }
        //todo zablokowanie wszystkiego procz kompilacji i zapisu pliku
        private void Clock1_EgzamEnded(object sender, EventArgs e)
        {
            //zablokowanie aplikacji procz kompilacji
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
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Helper.KeyCopy)
                    canvas1.Copy();
                else if (e.KeyCode == Helper.KeyCut)
                    canvas1.Cut();
                else if (e.KeyCode == Helper.KeyPaste)
                    canvas1.Paste();
                else if (e.KeyCode == Helper.KeyUndo && canvas1.Focused)
                    canvas1.Undo();
                else if (e.KeyCode == Helper.KeyRedo && canvas1.Focused)
                    canvas1.Redo();
                else if (e.KeyCode == Helper.KeySaveFile)
                    SaveFile();
                else if (e.KeyCode == Helper.KeyNewFile)
                    NewFile();
                else if (e.KeyCode == Helper.KeyOpenFile)
                    OpenFile();
                else if (e.KeyCode == Helper.KeyMultiselect)
                    canvas1.IsMultiSelect = true;
            }
            else if (e.KeyCode == Keys.Escape)
                canvas1.AbortAddingObject();
            else if (e.KeyCode == Helper.KeyRun)
                Run();
            else if (e.KeyCode == Helper.KeyDebug)
                Debug();
            else if (e.KeyCode == Keys.Delete)
            {
                if (_properties != null)
                {
                    if (!_properties.ContainsFocus)
                        canvas1.Delete();
                }
                else
                    canvas1.Delete();
            }
            else if (e.Modifiers == (Keys.Control | Keys.Shift))
            {
                if (e.KeyCode == Helper.KeySaveFileAs)
                    SaveFileAs();
                else if (e.KeyCode == Helper.KeyOpenFileFromServer)
                    OpenFileFromServer();
            }
        }
    }
}
