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

            //tabsConnector1.SizeMode = TabSizeMode.FillToRight;
            //AddStripMenuEvents();
        }

        private static Component.TabsArea.BlockPropertis.Properties temp;
        public void MyCreateBlockProp(MyBlock temp2)
        {

            if (temp != null)
                if (temp.ShouldRefresh(temp2))
                    return;
                else
                    splitContainer2.Panel2.Controls.Remove(temp);
            temp = new Component.TabsArea.BlockPropertis.Properties(temp2);
            splitContainer2.Panel2.Controls.Add(temp);
            temp.Width = splitContainer2.Panel2.Width;
            temp.Height = splitContainer2.Panel2.Height;
            temp.Location = new System.Drawing.Point(0, 0);

        }

        public void CanvasInvalidateForProperties()
        {
            canvas1.Invalidate();
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
                        if (temp!=null && !temp.ContainsFocus)
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
