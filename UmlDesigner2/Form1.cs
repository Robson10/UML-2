using System;
using System.Windows.Forms;
using UmlDesigner2.Component.TabsArea.BlockPropertis;
using UmlDesigner2.Component.ToolStripArea;
using UmlDesigner2.Component.Workspace;
namespace UmlDesigner2
{
    public partial class Form1 : Form
    {
        private BlockProp temp = new BlockProp();
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

            //tabsConnector1.SizeMode = TabSizeMode.FillToRight;
            //AddStripMenuEvents();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            temp.BringToFront();
            temp.Width = splitContainer2.Panel2.Width;
            temp.Height = splitContainer2.Panel2.Height;
            temp.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Panel2.Controls.Add(this.temp);
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

            if (e.Control)
                switch (e.KeyCode)
                {
                    case Keys.C:
                        canvas1.Copy();
                        break;
                    case Keys.X:
                        canvas1.Cut();
                        break;
                    case Keys.V:
                        canvas1.Paste();
                        break;
                    case Keys.Z:
                        canvas1.Undo();
                        break;
                    case Keys.Y:
                        canvas1.Redo();
                        break;
                    case Keys.ControlKey:
                        canvas1.IsMultiSelect = true;
                        break;
                }
            else
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        canvas1.AbortAddingObject();
                        break;
                    case Keys.Delete:
                        canvas1.Delete();
                        break;
                }
        }



        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    myToolStrip.MyResize(ClientSize);
        //    Canvas.ResizeAll(ClientSize);
        //}

    }
}
