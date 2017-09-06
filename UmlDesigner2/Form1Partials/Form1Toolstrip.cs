using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2
{
    partial class Form1
    {
        private void ToolStripAddEvents()
        {
            myToolStrip1.NewFileClick += MyToolStripContainer_NewFile_Click;
            myToolStrip1.OpenFileClick += MyToolStripContainer_OpenFile_Click;
            myToolStrip1.SaveFileClick += MyToolStripContainer_SaveFile_Click;
            myToolStrip1.SaveFileAsClick += MyToolStripContainer_SaveFileAs_Click;
            myToolStrip1.UndoClick += MyToolStripContainer_Undo_Click;
            myToolStrip1.RedoClick += MyToolStripContainer_Redo_Click;
            myToolStrip1.OptionsClick += MyToolStripContainer_Options_Click;
            myToolStrip1.LogInClick += MyToolStripContainer_LogIn_Click;
            myToolStrip1.OpenCloudFileClick += MyToolStripContainer_OpenCloudFile_Click;
            myToolStrip1.RunClick += MyToolStripContainer_Run_Click;
            myToolStrip1.DebugClick += MyToolStripContainer_Debug_Click;
        }
       private void MyToolStripContainer_Debug_Click(object sender, EventArgs e)
       {
           Debug();
       }

        private void MyToolStripContainer_Run_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void MyToolStripContainer_OpenCloudFile_Click(object sender, EventArgs e)
        {
            OpenFileFromServer();
        }

        private void MyToolStripContainer_LogIn_Click(object sender, EventArgs e)
        {
            LogIn();
        }

        private void MyToolStripContainer_Options_Click(object sender, EventArgs e)
        {
            Options();
        }

        private void MyToolStripContainer_Redo_Click(object sender, EventArgs e)
        {
            canvas1.Redo();
        }

        private void MyToolStripContainer_Undo_Click(object sender, EventArgs e)
        {
            canvas1.Undo();
        }

        private void MyToolStripContainer_SaveFileAs_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void MyToolStripContainer_SaveFile_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void MyToolStripContainer_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void MyToolStripContainer_NewFile_Click(object sender, EventArgs e)
        {
            NewFile();
        }

    }
}
