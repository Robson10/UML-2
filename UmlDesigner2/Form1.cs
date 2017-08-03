using System;
using System.Windows.Forms;
using UmlDesigner2.Component.ToolStripArea;
using UmlDesigner2.Component.Workspace;
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

            //tabsConnector1.SizeMode = TabSizeMode.FillToRight;
            //AddStripMenuEvents();
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
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    canvas1.AbortAddingObject();
                    break;
                case Keys.ControlKey:
                    canvas1.IsMultiSelect = true;
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
