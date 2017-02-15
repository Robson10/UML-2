using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace UmlDesigner2.Component
{
    public partial class MyToolStripContainer : ToolStripContainer
    {
        #region Objects
        private ToolStrip Default = new ToolStrip();
        private ToolStrip Blocks = new ToolStrip();
        private Size ButtonSize = new Size(40, 40);

        MyButton Start = new MyButton(MyDictionary.StripButtons.Start);
        MyButton Input = new MyButton(MyDictionary.StripButtons.Input);
        MyButton Execution = new MyButton(MyDictionary.StripButtons.Execution);
        MyButton Decision = new MyButton(MyDictionary.StripButtons.Decision);
        MyButton End = new MyButton(MyDictionary.StripButtons.End);

        MyButton NewFile = new MyButton(MyDictionary.StripButtons.NewFile);
        MyButton OpenFile = new MyButton(MyDictionary.StripButtons.OpenFile);
        MyButton SaveFile = new MyButton(MyDictionary.StripButtons.SaveFile);
        MyButton SaveFileAs = new MyButton(MyDictionary.StripButtons.SaveFileAs);
        ToolStripSeparator s1 = new ToolStripSeparator();

        MyButton Redo = new MyButton(MyDictionary.StripButtons.Redo);
        MyButton Undo = new MyButton(MyDictionary.StripButtons.Undo);
        ToolStripSeparator s2 = new ToolStripSeparator();

        MyButton Options = new MyButton(MyDictionary.StripButtons.Options);
        MyButton LogIn = new MyButton(MyDictionary.StripButtons.LogIn);
        MyButton OpenCloudFile = new MyButton(MyDictionary.StripButtons.OpenFileFromServer);
        ToolStripSeparator s3 = new ToolStripSeparator();

        MyButton Run = new MyButton(MyDictionary.StripButtons.Run);
        MyButton Debug = new MyButton(MyDictionary.StripButtons.Debug);
        #endregion

        #region Methods
        public MyToolStripContainer()
        {
            Settings();
            AddElements();
        }
        private void Settings()
        {
            Default.Dock = DockStyle.None;
            Blocks.Dock = DockStyle.None;
            Default.ImageScalingSize = ButtonSize;
            Blocks.ImageScalingSize = ButtonSize;
            Default.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;//usuwanie z rogów artefaktow
            Blocks.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            
        }

        private void AddElements()
        {

            Blocks.Items.Add(Start);
            Blocks.Items.Add(Input);
            Blocks.Items.Add(Execution);
            Blocks.Items.Add(Decision);
            Blocks.Items.Add(End);
            TopToolStripPanel.Controls.Add(Blocks);

            Default.Items.Add(NewFile);
            Default.Items.Add(OpenFile);
            Default.Items.Add(SaveFile);
            Default.Items.Add(SaveFileAs);
            Default.Items.Add(s1);

            Default.Items.Add(Undo);
            Default.Items.Add(Redo);
            Default.Items.Add(s2);

            Default.Items.Add(Options);
            Default.Items.Add(LogIn);
            Default.Items.Add(OpenCloudFile);
            Default.Items.Add(s3);

            Default.Items.Add(Run);
            Default.Items.Add(Debug);
            
            TopToolStripPanel.Controls.Add(Default);
            Default.ParentChanged += Default_ParentChanged;
        }

        private void Default_ParentChanged(object sender, EventArgs e)
        {
            Blocks.MinimumSize = (ButtonSize);
            Default.MinimumSize = (ButtonSize);
        }

        public void MyResize(Size clientSize)
        {
            this.Size = new Size(clientSize.Width, clientSize.Height);
            if (TopToolStripPanel.Contains(Default) || BottomToolStripPanel.Contains(Default))
            {
                int defWidth = clientSize.Width * 2 / 3;
                int defNormWidth = (Default.Items.Count - 1) * ButtonSize.Width;
                Default.MinimumSize = new Size((defWidth > defNormWidth) ? defNormWidth : defWidth, ButtonSize.Height);
            }
            else
            {
                int defHeight = clientSize.Height * 2 / 3;
                int defNormHeight = (Blocks.Items.Count - 1) * ButtonSize.Height;
                Blocks.MinimumSize = new Size(ButtonSize.Width, (defHeight > defNormHeight) ? defNormHeight : defHeight);
            }
            if (TopToolStripPanel.Contains(Blocks) || BottomToolStripPanel.Contains(Blocks))
            {
                int bloWidth = clientSize.Width * 1 / 3;
                int bloNormWidth = (Blocks.Items.Count - 1) * ButtonSize.Width;
                Blocks.MinimumSize = new Size((bloWidth > bloNormWidth) ? bloNormWidth : bloWidth, ButtonSize.Height);
            }
            else
            {
                int bloHeight = clientSize.Height * 1 / 3;
                int bloNormHeight = (Blocks.Items.Count - 1) * ButtonSize.Height;
                Blocks.MinimumSize = new Size(ButtonSize.Width, (bloHeight > bloNormHeight) ? bloNormHeight : bloHeight);
            }
        }
        #endregion

        #region eventHandlers
        public event EventHandler NewFile_Click
        {
            add
            {NewFile.Click += value;}
            remove
            { NewFile.Click -= value;}
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
            {Redo.Click += value; }
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
            {Run.Click -= value; }
        }
        public event EventHandler Debug_Click
        {
            add
            { Debug.Click += value; }
            remove
            { Debug.Click -= value; }
        }
        
        public event EventHandler Start_Click
        {
            add
            { Start.Click += value; }
            remove
            { Start.Click -= value; }
        }
        public event EventHandler End_Click
        {
            add
            {End.Click += value; }
            remove
            { End.Click -= value; }
        }
        public event EventHandler Input_Click
        {
            add
            { Input.Click += value; }
            remove
            { Input.Click -= value; }
        }
        public event EventHandler Execution_Click
        {
            add
            { Execution.Click += value; }
            remove
            { Execution.Click -= value; }
        }
        public event EventHandler Decision_Click
        {
            add
            { Decision.Click += value; }
            remove
            { Decision.Click -= value; }
        }
        #endregion
    }

    public class MyButton : ToolStripButton
    {
        public MyDictionary.StripButtons BlockType { get; private set; }
        public MyButton(MyDictionary.StripButtons Shape)
        {
            BlockType = Shape;
            this.ToolTipText = MyDictionary.StripButtonToolTip(BlockType);
            this.Image = MyDictionary.GetIcon(BlockType, MyDictionary.IconSize);
        }
    }
}

