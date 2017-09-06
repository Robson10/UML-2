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
            throw new NotImplementedException();
        }

        //todo zablokować wczytywanie pliku
        private void Clock1_EgzamStarted(object sender, EventArgs e)
        {
            canvas1.ClearCanvas();
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
            if (e.Modifiers==Keys.Control )
                switch (e.KeyCode)
                {
                    case Keys.C://kopiowanie
                        canvas1.Copy();
                        break;
                    case Keys.X://wycinanie
                        canvas1.Cut();
                        break;
                    case Keys.V://wklejanie
                        canvas1.Paste();
                        break;
                    case Keys.Z://cofanie
                        if (canvas1.Focused)
                        canvas1.Undo();
                        break;
                    case Keys.Y://do przodu
                        if (canvas1.Focused)
                        canvas1.Redo();
                        break;
                    case Keys.S://Zapisz
                        SaveFile();
                        break;
                    case Keys.N://Nowy Plik
                        NewFile();
                        break;
                    case Keys.O://otworz plik
                        OpenFile();
                        break;
                    case Keys.ControlKey:
                        canvas1.IsMultiSelect = true;
                        break;
                }
            else if (e.Modifiers == Keys.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.F5: //debug
                        Debug();
                        break;
                }
            }
            else if (e.Modifiers==(Keys.Control |Keys.Shift))
            {
                switch (e.KeyCode)
                {
                    case Keys.S://zapisz jako
                        SaveFileAs();
                        break;
                    case Keys.O: //otworz plik zdalny
                        OpenFileFromServer();
                        break;
                }
            }
            else
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        canvas1.AbortAddingObject();
                        break;
                    case Keys.Delete:
                        if (_properties != null && !_properties.ContainsFocus)
                        canvas1.Delete();
                        break;
                    case Keys.F5://start
                        Run();
                        break;
                }
        }


    }
}
