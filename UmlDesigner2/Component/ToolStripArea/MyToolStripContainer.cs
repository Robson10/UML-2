using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace UmlDesigner2.Component.ToolStripArea
{
    public partial class MyToolStrip : ToolStrip
    {
        //private ToolStrip Blocks = new ToolStrip();
        private Size ButtonSize = new Size(40, 40);

        #region ToolStripObjects

        private MyButton NewFile = new MyButton(ToolStripButtonParameters.StripButtons.NewFile);
        private MyButton OpenFile = new MyButton(ToolStripButtonParameters.StripButtons.OpenFile);
        private MyButton SaveFile = new MyButton(ToolStripButtonParameters.StripButtons.SaveFile);
        private MyButton SaveFileAs = new MyButton(ToolStripButtonParameters.StripButtons.SaveFileAs);
        private ToolStripSeparator s1 = new ToolStripSeparator();

        private MyButton Redo = new MyButton(ToolStripButtonParameters.StripButtons.Redo);
        private MyButton Undo = new MyButton(ToolStripButtonParameters.StripButtons.Undo);
        private ToolStripSeparator s2 = new ToolStripSeparator();

        private MyButton Options = new MyButton(ToolStripButtonParameters.StripButtons.Options);
        private MyButton LogIn = new MyButton(ToolStripButtonParameters.StripButtons.LogIn);
        private MyButton OpenCloudFile = new MyButton(ToolStripButtonParameters.StripButtons.OpenFileFromServer);
        private ToolStripSeparator s3 = new ToolStripSeparator();

        private MyButton Run = new MyButton(ToolStripButtonParameters.StripButtons.Run);
        private MyButton Debug = new MyButton(ToolStripButtonParameters.StripButtons.Debug);
        #endregion
        
        public MyToolStrip()
        {
            Settings();
            AddElements();
        }
        private void Settings()
        {
            //Default.Dock = DockStyle.None;
            this.ImageScalingSize = ButtonSize;
            //Default.RenderMode = ToolStripRenderMode.System;//usuwanie z rogów artefaktow

            //Default.ParentChanged += Default_ParentChanged;
            
        }

        private void AddElements()
        {            
            this.Items.Add(NewFile);
            this.Items.Add(OpenFile);
            this.Items.Add(SaveFile);
            this.Items.Add(SaveFileAs);
            this.Items.Add(s1);

            this.Items.Add(Undo);
            this.Items.Add(Redo);
            this.Items.Add(s2);

            this.Items.Add(Options);
            this.Items.Add(LogIn);
            this.Items.Add(OpenCloudFile);
            this.Items.Add(s3);

            this.Items.Add(Run);
            this.Items.Add(Debug);
        }
               


    }
    //eventHandlers
    partial class MyToolStrip
    {
        #region eventHandlers
        public event EventHandler NewFile_Click
        {
            add
            { NewFile.Click += value; }
            remove
            { NewFile.Click -= value; }
        }
        public event EventHandler OpenFile_Click
        {
            add
            { OpenFile.Click += value; }
            remove
            { OpenFile.Click -= value; }
        }
        public event EventHandler SaveFile_Click
        {
            add
            { SaveFile.Click += value; }
            remove
            { SaveFile.Click -= value; }
        }
        public event EventHandler SaveFileAs_Click
        {
            add
            { SaveFileAs.Click += value; }
            remove
            { SaveFileAs.Click -= value; }
        }
        public event EventHandler Redo_Click
        {
            add
            { Redo.Click += value; }
            remove
            { Redo.Click -= value; }
        }
        public event EventHandler Undo_Click
        {
            add
            { Undo.Click += value; }
            remove
            { Undo.Click -= value; }
        }
        public event EventHandler Options_Click
        {
            add
            { Options.Click += value; }
            remove
            { Options.Click -= value; }
        }
        public event EventHandler LogIn_Click
        {
            add
            { LogIn.Click += value; }
            remove
            { LogIn.Click -= value; }
        }
        public event EventHandler OpenCloudFile_Click
        {
            add
            { OpenCloudFile.Click += value; }
            remove
            { OpenCloudFile.Click -= value; }
        }
        public event EventHandler Run_Click
        {
            add
            { Run.Click += value; }
            remove
            { Run.Click -= value; }
        }
        public event EventHandler Debug_Click
        {
            add
            { Debug.Click += value; }
            remove
            { Debug.Click -= value; }
        }

      
        #endregion
    }
    //Events
    partial class MyToolStrip
    {
        private void Default_ParentChanged(object sender, EventArgs e)
        {
            //Blocks.MinimumSize = (ButtonSize);
            //Default.MinimumSize = (ButtonSize);
        }
    }
    //Methoods
    partial class MyToolStrip
    {
        public void MyResize(Size clientSize)
        {
        //    this.Size = new Size(clientSize.Width, clientSize.Height);
        //    if (TopToolStripPanel.Contains(Default) || BottomToolStripPanel.Contains(Default))
        //    {
        //        int defWidth = clientSize.Width * 2 / 3;
        //        int defNormWidth = (Default.Items.Count - 1) * ButtonSize.Width;
        //        Default.MinimumSize = new Size((defWidth > defNormWidth) ? defNormWidth : defWidth, ButtonSize.Height);
        //    }
            //else
            //{
            //    int defHeight = clientSize.Height * 2 / 3;
            //    int defNormHeight = (Blocks.Items.Count - 1) * ButtonSize.Height;
            //    Blocks.MinimumSize = new Size(ButtonSize.Width, (defHeight > defNormHeight) ? defNormHeight : defHeight);
            //}
            //if (TopToolStripPanel.Contains(Blocks) || BottomToolStripPanel.Contains(Blocks))
            //{
            //    int bloWidth = clientSize.Width * 1 / 3;
            //    int bloNormWidth = (Blocks.Items.Count - 1) * ButtonSize.Width;
            //    Blocks.MinimumSize = new Size((bloWidth > bloNormWidth) ? bloNormWidth : bloWidth, ButtonSize.Height);
            //}
            //else
            //{
            //    int bloHeight = clientSize.Height * 1 / 3;
            //    int bloNormHeight = (Blocks.Items.Count - 1) * ButtonSize.Height;
            //    Blocks.MinimumSize = new Size(ButtonSize.Width, (bloHeight > bloNormHeight) ? bloNormHeight : bloHeight);
            //}
        }
    }
    public class MyButton : ToolStripButton
    {
        public ToolStripButtonParameters.StripButtons ButtonType { get; private set; }
        public MyButton(ToolStripButtonParameters.StripButtons buttonType)
        {
            ButtonType = buttonType;
            this.ToolTipText = ToolStripButtonParameters.StripButtonToolTip(ButtonType);
            this.Image = ToolStripButtonParameters.GetIcon(ButtonType, ToolStripButtonParameters.IconSize);
        }
    }
}

#region backup
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Windows.Forms;
//namespace UmlDesigner2.Component.ToolStripArea
//{
//    public partial class MyToolStrip : ToolStrip
//    {
//        //private ToolStrip Blocks = new ToolStrip();
//        private Size ButtonSize = new Size(40, 40);

//        #region ToolStripObjects
//        //private MyButton Start = new MyButton(ToolStripButtonParameters.StripButtons.Start);
//        //private MyButton Input = new MyButton(ToolStripButtonParameters.StripButtons.Input);
//        //private MyButton Execution = new MyButton(ToolStripButtonParameters.StripButtons.Execution);
//        //private MyButton Decision = new MyButton(ToolStripButtonParameters.StripButtons.Decision);
//        //private MyButton End = new MyButton(ToolStripButtonParameters.StripButtons.End);

//        private MyButton NewFile = new MyButton(ToolStripButtonParameters.StripButtons.NewFile);
//        private MyButton OpenFile = new MyButton(ToolStripButtonParameters.StripButtons.OpenFile);
//        private MyButton SaveFile = new MyButton(ToolStripButtonParameters.StripButtons.SaveFile);
//        private MyButton SaveFileAs = new MyButton(ToolStripButtonParameters.StripButtons.SaveFileAs);
//        private ToolStripSeparator s1 = new ToolStripSeparator();

//        private MyButton Redo = new MyButton(ToolStripButtonParameters.StripButtons.Redo);
//        private MyButton Undo = new MyButton(ToolStripButtonParameters.StripButtons.Undo);
//        private ToolStripSeparator s2 = new ToolStripSeparator();

//        private MyButton Options = new MyButton(ToolStripButtonParameters.StripButtons.Options);
//        private MyButton LogIn = new MyButton(ToolStripButtonParameters.StripButtons.LogIn);
//        private MyButton OpenCloudFile = new MyButton(ToolStripButtonParameters.StripButtons.OpenFileFromServer);
//        private ToolStripSeparator s3 = new ToolStripSeparator();

//        private MyButton Run = new MyButton(ToolStripButtonParameters.StripButtons.Run);
//        private MyButton Debug = new MyButton(ToolStripButtonParameters.StripButtons.Debug);
//        #endregion

//        public MyToolStrip()
//        {
//            Settings();
//            AddElements();
//        }
//        private void Settings()
//        {
//            Default.Dock = DockStyle.None;
//            Default.ImageScalingSize = ButtonSize;
//            Default.RenderMode = ToolStripRenderMode.System;//usuwanie z rogów artefaktow

//            Default.ParentChanged += Default_ParentChanged;

//            //Blocks.Dock = DockStyle.None;
//            //Blocks.ImageScalingSize = ButtonSize;
//            //Blocks.RenderMode = ToolStripRenderMode.System;
//        }

//        private void AddElements()
//        {

//            //Blocks.Items.Add(Start);
//            //Blocks.Items.Add(Input);
//            //Blocks.Items.Add(Execution);
//            //Blocks.Items.Add(Decision);
//            //Blocks.Items.Add(End);
//            //TopToolStripPanel.Controls.Add(Blocks);

//            Default.Items.Add(NewFile);
//            Default.Items.Add(OpenFile);
//            Default.Items.Add(SaveFile);
//            Default.Items.Add(SaveFileAs);
//            Default.Items.Add(s1);

//            Default.Items.Add(Undo);
//            Default.Items.Add(Redo);
//            Default.Items.Add(s2);

//            Default.Items.Add(Options);
//            Default.Items.Add(LogIn);
//            Default.Items.Add(OpenCloudFile);
//            Default.Items.Add(s3);

//            Default.Items.Add(Run);
//            Default.Items.Add(Debug);

//            TopToolStripPanel.Controls.Add(Default);
//        }



//    }
//    //eventHandlers
//    partial class MyToolStrip
//    {
//        #region eventHandlers
//        public event EventHandler NewFile_Click
//        {
//            add
//            { NewFile.Click += value; }
//            remove
//            { NewFile.Click -= value; }
//        }
//        public event EventHandler OpenFile_Click
//        {
//            add
//            { OpenFile.Click += value; }
//            remove
//            { OpenFile.Click -= value; }
//        }
//        public event EventHandler SaveFile_Click
//        {
//            add
//            { SaveFile.Click += value; }
//            remove
//            { SaveFile.Click -= value; }
//        }
//        public event EventHandler SaveFileAs_Click
//        {
//            add
//            { SaveFileAs.Click += value; }
//            remove
//            { SaveFileAs.Click -= value; }
//        }
//        public event EventHandler Redo_Click
//        {
//            add
//            { Redo.Click += value; }
//            remove
//            { Redo.Click -= value; }
//        }
//        public event EventHandler Undo_Click
//        {
//            add
//            { Undo.Click += value; }
//            remove
//            { Undo.Click -= value; }
//        }
//        public event EventHandler Options_Click
//        {
//            add
//            { Options.Click += value; }
//            remove
//            { Options.Click -= value; }
//        }
//        public event EventHandler LogIn_Click
//        {
//            add
//            { LogIn.Click += value; }
//            remove
//            { LogIn.Click -= value; }
//        }
//        public event EventHandler OpenCloudFile_Click
//        {
//            add
//            { OpenCloudFile.Click += value; }
//            remove
//            { OpenCloudFile.Click -= value; }
//        }
//        public event EventHandler Run_Click
//        {
//            add
//            { Run.Click += value; }
//            remove
//            { Run.Click -= value; }
//        }
//        public event EventHandler Debug_Click
//        {
//            add
//            { Debug.Click += value; }
//            remove
//            { Debug.Click -= value; }
//        }

//        //public event EventHandler Start_Click
//        //{
//        //    add
//        //    { Start.Click += value; }
//        //    remove
//        //    { Start.Click -= value; }
//        //}
//        //public event EventHandler End_Click
//        //{
//        //    add
//        //    { End.Click += value; }
//        //    remove
//        //    { End.Click -= value; }
//        //}
//        //public event EventHandler Input_Click
//        //{
//        //    add
//        //    { Input.Click += value; }
//        //    remove
//        //    { Input.Click -= value; }
//        //}
//        //public event EventHandler Execution_Click
//        //{
//        //    add
//        //    { Execution.Click += value; }
//        //    remove
//        //    { Execution.Click -= value; }
//        //}
//        //public event EventHandler Decision_Click
//        //{
//        //    add
//        //    { Decision.Click += value; }
//        //    remove
//        //    { Decision.Click -= value; }
//        //}
//        #endregion
//    }
//    //Events
//    partial class MyToolStrip
//    {
//        private void Default_ParentChanged(object sender, EventArgs e)
//        {
//            //Blocks.MinimumSize = (ButtonSize);
//            Default.MinimumSize = (ButtonSize);
//        }
//    }
//    //Methoods
//    partial class MyToolStrip
//    {
//        public void MyResize(Size clientSize)
//        {
//            this.Size = new Size(clientSize.Width, clientSize.Height);
//            if (TopToolStripPanel.Contains(Default) || BottomToolStripPanel.Contains(Default))
//            {
//                int defWidth = clientSize.Width * 2 / 3;
//                int defNormWidth = (Default.Items.Count - 1) * ButtonSize.Width;
//                Default.MinimumSize = new Size((defWidth > defNormWidth) ? defNormWidth : defWidth, ButtonSize.Height);
//            }
//            //else
//            //{
//            //    int defHeight = clientSize.Height * 2 / 3;
//            //    int defNormHeight = (Blocks.Items.Count - 1) * ButtonSize.Height;
//            //    Blocks.MinimumSize = new Size(ButtonSize.Width, (defHeight > defNormHeight) ? defNormHeight : defHeight);
//            //}
//            //if (TopToolStripPanel.Contains(Blocks) || BottomToolStripPanel.Contains(Blocks))
//            //{
//            //    int bloWidth = clientSize.Width * 1 / 3;
//            //    int bloNormWidth = (Blocks.Items.Count - 1) * ButtonSize.Width;
//            //    Blocks.MinimumSize = new Size((bloWidth > bloNormWidth) ? bloNormWidth : bloWidth, ButtonSize.Height);
//            //}
//            //else
//            //{
//            //    int bloHeight = clientSize.Height * 1 / 3;
//            //    int bloNormHeight = (Blocks.Items.Count - 1) * ButtonSize.Height;
//            //    Blocks.MinimumSize = new Size(ButtonSize.Width, (bloHeight > bloNormHeight) ? bloNormHeight : bloHeight);
//            //}
//        }
//    }
//    public class MyButton : ToolStripButton
//    {
//        public ToolStripButtonParameters.StripButtons ButtonType { get; private set; }
//        public MyButton(ToolStripButtonParameters.StripButtons buttonType)
//        {
//            ButtonType = buttonType;
//            this.ToolTipText = ToolStripButtonParameters.StripButtonToolTip(ButtonType);
//            this.Image = ToolStripButtonParameters.GetIcon(ButtonType, ToolStripButtonParameters.IconSize);
//        }
//    }
//}

#endregion