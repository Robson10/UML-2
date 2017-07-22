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

            //tabsConnector1.SizeMode = TabSizeMode.FillToRight;
            //AddStripMenuEvents();
        }

      

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    myToolStrip.MyResize(ClientSize);
        //    Canvas.ResizeAll(ClientSize);
        //}

    }
}
