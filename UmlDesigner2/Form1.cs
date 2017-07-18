using System;
using System.Windows.Forms;
using UmlDesigner2.Component.ToolStripArea;
using UmlDesigner2.Component.Workspace;
namespace UmlDesigner2
{
    public partial class Form1 : Form
    {
        MyToolStrip myToolStrip = new MyToolStrip();
        //Workspace Canvas = new Workspace();
        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            KeyPreview = true;
            //tabsConnector1.SizeMode = TabSizeMode.FillToRight;
            //AddStripMenuEvents();
        }

        //private void AddStripMenuEvents()
        //{
        //    //myToolStrip.ContentPanel.Controls.Add(Canvas);
        //    myToolStrip.NewFile_Click += MyToolStripContainer_NewFile_Click;
        //    myToolStrip.OpenFile_Click += MyToolStripContainer_OpenFile_Click;
        //    myToolStrip.SaveFile_Click += MyToolStripContainer_SaveFile_Click;
        //    myToolStrip.SaveFileAs_Click += MyToolStripContainer_SaveFileAs_Click;
        //    myToolStrip.Undo_Click += MyToolStripContainer_Undo_Click;
        //    myToolStrip.Redo_Click += MyToolStripContainer_Redo_Click;
        //    myToolStrip.Options_Click += MyToolStripContainer_Options_Click;
        //    myToolStrip.LogIn_Click += MyToolStripContainer_LogIn_Click;
        //    myToolStrip.OpenCloudFile_Click += MyToolStripContainer_OpenCloudFile_Click;
        //    myToolStrip.Run_Click += MyToolStripContainer_Run_Click;
        //    myToolStrip.Debug_Click += MyToolStripContainer_Debug_Click;

        //    //myToolStrip.Start_Click += MyToolStripContainer_Start_Click;
        //    //myToolStrip.End_Click += MyToolStripContainer_End_Click;
        //    //myToolStrip.Input_Click += MyToolStripContainer_Input_Click;
        //    //myToolStrip.Execution_Click += MyToolStripContainer_Execution_Click;
        //    //myToolStrip.Decision_Click += MyToolStripContainer_Decision_Click;
        //}

        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}
        //#region Toolstrip Events
        //private void MyToolStripContainer_Decision_Click(object sender, EventArgs e)
        //{
        //    Canvas.ShapeToDraw = BlockParameters.Shape.Decision;
        //}

        //private void MyToolStripContainer_Execution_Click(object sender, EventArgs e)
        //{
        //    Canvas.ShapeToDraw = BlockParameters.Shape.Execution;
        //}

        //private void MyToolStripContainer_Input_Click(object sender, EventArgs e)
        //{
        //    Canvas.ShapeToDraw = BlockParameters.Shape.Input;
        //}

        //private void MyToolStripContainer_End_Click(object sender, EventArgs e)
        //{
        //    Canvas.ShapeToDraw = BlockParameters.Shape.End;
        //}

        //private void MyToolStripContainer_Start_Click(object sender, EventArgs e)
        //{
        //    Canvas.ShapeToDraw = BlockParameters.Shape.Start;
        //}

        //private void MyToolStripContainer_Debug_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void MyToolStripContainer_Run_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void MyToolStripContainer_OpenCloudFile_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void MyToolStripContainer_LogIn_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void MyToolStripContainer_Options_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void MyToolStripContainer_Redo_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void MyToolStripContainer_Undo_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void MyToolStripContainer_SaveFileAs_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void MyToolStripContainer_SaveFile_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void MyToolStripContainer_OpenFile_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void MyToolStripContainer_NewFile_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
        //#endregion

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    myToolStrip.MyResize(ClientSize);
        //    Canvas.ResizeAll(ClientSize);
        //}

    }
}
