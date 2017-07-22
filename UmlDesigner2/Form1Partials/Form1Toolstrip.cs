using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2
{
    partial class Form1:Form
    {
        private void ToolStripPresets()
        {
            myToolStrip1.NewFile_Click += MyToolStripContainer_NewFile_Click;
            myToolStrip1.OpenFile_Click += MyToolStripContainer_OpenFile_Click;
            myToolStrip1.SaveFile_Click += MyToolStripContainer_SaveFile_Click;
            myToolStrip1.SaveFileAs_Click += MyToolStripContainer_SaveFileAs_Click;
            myToolStrip1.Undo_Click += MyToolStripContainer_Undo_Click;
            myToolStrip1.Redo_Click += MyToolStripContainer_Redo_Click;
            myToolStrip1.Options_Click += MyToolStripContainer_Options_Click;
            myToolStrip1.LogIn_Click += MyToolStripContainer_LogIn_Click;
            myToolStrip1.OpenCloudFile_Click += MyToolStripContainer_OpenCloudFile_Click;
            myToolStrip1.Run_Click += MyToolStripContainer_Run_Click;
            myToolStrip1.Debug_Click += MyToolStripContainer_Debug_Click;

         
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

    }
}
