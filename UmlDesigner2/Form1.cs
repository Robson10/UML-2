using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmlDesigner2.Component;
namespace UmlDesigner2
{
    public partial class Form1 : Form
    {
        MyToolStripContainer myToolStripContainer = new MyToolStripContainer();
        Canvas Canvas = new Canvas();
        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            AddStripMenuEvents();
        }
        private void AddStripMenuEvents()
        {
            myToolStripContainer.ContentPanel.Controls.Add(Canvas);
            myToolStripContainer.NewFile_Click += MyToolStripContainer_NewFile_Click;
            myToolStripContainer.OpenFile_Click += MyToolStripContainer_OpenFile_Click;
            myToolStripContainer.SaveFile_Click += MyToolStripContainer_SaveFile_Click;
            myToolStripContainer.SaveFileAs_Click += MyToolStripContainer_SaveFileAs_Click;
            myToolStripContainer.Undo_Click += MyToolStripContainer_Undo_Click;
            myToolStripContainer.Redo_Click += MyToolStripContainer_Redo_Click;
            myToolStripContainer.Options_Click += MyToolStripContainer_Options_Click;
            myToolStripContainer.LogIn_Click += MyToolStripContainer_LogIn_Click;
            myToolStripContainer.OpenCloudFile_Click += MyToolStripContainer_OpenCloudFile_Click;
            myToolStripContainer.Run_Click += MyToolStripContainer_Run_Click;
            myToolStripContainer.Debug_Click += MyToolStripContainer_Debug_Click;

            myToolStripContainer.Start_Click += MyToolStripContainer_Start_Click;
            myToolStripContainer.End_Click += MyToolStripContainer_End_Click;
            myToolStripContainer.Input_Click += MyToolStripContainer_Input_Click;
            myToolStripContainer.Execution_Click += MyToolStripContainer_Execution_Click;
            myToolStripContainer.Decision_Click += MyToolStripContainer_Decision_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myToolStripContainer.Size = ClientSize;
            Controls.Add(myToolStripContainer);

        }
        #region Toolstrip Events
        private void MyToolStripContainer_Decision_Click(object sender, EventArgs e)
        {
            Canvas.ShapeToDraw = MyDictionary.Shape.Decision;
        }

        private void MyToolStripContainer_Execution_Click(object sender, EventArgs e)
        {
            Canvas.ShapeToDraw = MyDictionary.Shape.Execution;
        }

        private void MyToolStripContainer_Input_Click(object sender, EventArgs e)
        {
            Canvas.ShapeToDraw = MyDictionary.Shape.Input;
        }

        private void MyToolStripContainer_End_Click(object sender, EventArgs e)
        {
            Canvas.ShapeToDraw = MyDictionary.Shape.End;
        }

        private void MyToolStripContainer_Start_Click(object sender, EventArgs e)
        {
            Canvas.ShapeToDraw = MyDictionary.Shape.Start;
        }

        private void MyToolStripContainer_Debug_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyToolStripContainer_Run_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyToolStripContainer_OpenCloudFile_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyToolStripContainer_LogIn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyToolStripContainer_Options_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyToolStripContainer_Redo_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyToolStripContainer_Undo_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyToolStripContainer_SaveFileAs_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyToolStripContainer_SaveFile_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyToolStripContainer_OpenFile_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyToolStripContainer_NewFile_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            myToolStripContainer.MyResize(ClientSize);
            Canvas.ResizeAll(ClientSize);
        }

    }
}
