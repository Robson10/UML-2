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
        //Workspace Canvas = new Workspace();
        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            KeyPreview = true;
            ToolStripPresets();
            TabsPresets();
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            splitContainer2.Panel2.BackColor = System.Drawing.Color.White;
            splitContainer3.Panel2.BackColor = System.Drawing.Color.Gray;
            splitContainer3.Panel2.Controls.Add(_results);
            _results.Size = splitContainer3.Panel2.ClientRectangle.Size;
        }

        private static Component.Workspace.ResultComponent.Results _results = new Component.Workspace.ResultComponent.Results();

        private static Component.TabsArea.BlockPropertis.Properties _properties;
        public void MyCreateBlockProp(MyBlock temp2)
        {

            if (_properties != null)
                if (_properties.ShouldRefresh(temp2))
                {
                    _properties.UpdateProperties();
                    return;
                }
                else
                    splitContainer2.Panel2.Controls.Remove(_properties);
            _properties = new Component.TabsArea.BlockPropertis.Properties(temp2);
            splitContainer2.Panel2.Controls.Add(_properties);
            _properties.Width = splitContainer2.Panel2.Width;
            _properties.Height = splitContainer2.Panel2.Height;
            _properties.Location = new System.Drawing.Point(0, 0);
        }

        public void MyRemoveBlockProp()
        {
            splitContainer2.Panel2.Controls.Remove(_properties);
            _properties = null;
        }

        public void CanvasInvalidatebyInvalidateByProperties()
        {
            canvas1.OnPropertiesChange();
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
                        break;
                    case Keys.N://Nowy Plik
                        break;
                    case Keys.O://otworz plik
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
                        break;
                }
            }
            else if (e.Modifiers==(Keys.Control |Keys.Shift))
            {
                switch (e.KeyCode)
                {
                    case Keys.S://zapisz jako
                        break;
                    case Keys.O: //otworz plik zdalny
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
                        break;
                }
        }


    }
}
