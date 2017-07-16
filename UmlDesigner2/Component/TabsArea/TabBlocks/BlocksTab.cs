using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.TabsArea.TabBlocks
{
    public class BlocksTab:TabPage
    {

        private OAKListView _listViewOfBlocks = new OAKListView(){};
        public BlocksTab()
        {

            Text = BlocksTabPropertis.TabText;
            this.Padding= new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(232, 203);
            this.UseVisualStyleBackColor = true;
            tu
           
            CreateList();
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }

        private ImageList loadImageList()
        {
            //System.ComponentModel.IContainer components= new System.ComponentModel.Container();
            var imageList1 = new System.Windows.Forms.ImageList();
            imageList1.ImageSize = new System.Drawing.Size(40, 40);
            imageList1.TransparentColor = System.Drawing.Color.Transparent;
            //System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));

            AddImageToImageListFromPath(BlocksTabPropertis.PathToImageStartBlock,ref imageList1);
            AddImageToImageListFromPath(BlocksTabPropertis.PathToImageEndBlock, ref imageList1);
            AddImageToImageListFromPath(BlocksTabPropertis.PathToImageDecisionBlock, ref imageList1);
            AddImageToImageListFromPath(BlocksTabPropertis.PathToImageExecuteBlock, ref imageList1);
            AddImageToImageListFromPath(BlocksTabPropertis.PathToImageInputBlock, ref imageList1);
            return imageList1;
        }

        private void AddImageToImageListFromPath(string path,ref ImageList imageList1)
        {
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            {
                imageList1.Images.Add(Image.FromStream(stream));
            }
        }

        private void CreateList()
        {
            _listViewOfBlocks.View = View.Details;
            _listViewOfBlocks.Columns.Add(new ColumnHeader() { Width = ClientRectangle.Width });
           // _listViewOfBlocks.Size = new System.Drawing.Size(700, 400);
            _listViewOfBlocks.FullRowSelect = true;
            _listViewOfBlocks.MultiSelect = false;
            FillList();
            this.Controls.Add(_listViewOfBlocks);
        }

        private void FillList()
        {
            _listViewOfBlocks.SmallImageList = loadImageList();
            _listViewOfBlocks.Items.Add(new ListViewItem(BlocksTabPropertis.ListItemTextForStart) { ImageIndex = 0 });
            _listViewOfBlocks.Items.Add(new ListViewItem(BlocksTabPropertis.ListItemTextForEnd) { ImageIndex = 1});
            _listViewOfBlocks.Items.Add(new ListViewItem(BlocksTabPropertis.ListItemTextForDecision) { ImageIndex = 2 });
            _listViewOfBlocks.Items.Add(new ListViewItem(BlocksTabPropertis.ListItemTextForExecution) {ImageIndex = 3});
            _listViewOfBlocks.Items.Add(new ListViewItem(BlocksTabPropertis.ListItemTextForInput) { ImageIndex = 4 });
        }
    }


    public class OAKListView : ListView
    {
        //https://www.codeproject.com/Articles/7630/ListView-with-Image-on-SubItems
        [StructLayout(LayoutKind.Sequential)]
        public struct LV_ITEM
        {
            public UInt32 mask;
            public Int32 iItem;
            public Int32 iSubItem;
            public UInt32 state;
            public UInt32 stateMask;
            public String pszText;
            public Int32 cchTextMax;
            public Int32 iImage;
            public IntPtr lParam;
        }

        public const Int32 LVM_FIRST = 0x1000;
        public const Int32 LVM_GETITEM = LVM_FIRST + 5;
        public const Int32 LVM_SETITEM = LVM_FIRST + 6;
        public const Int32 LVIF_TEXT = 0x0001;
        public const Int32 LVIF_IMAGE = 0x0002;

        public const int LVW_FIRST = 0x1000;
        public const int LVM_GETEXTENDEDLISTVIEWSTYLE = LVW_FIRST + 54;

        public const int LVS_EX_GRIDLINES = 0x00000001;
        public const int LVS_EX_SUBITEMIMAGES = 0x00000002;
        public const int LVS_EX_CHECKBOXES = 0x00000004;
        public const int LVS_EX_TRACKSELECT = 0x00000008;
        public const int LVS_EX_HEADERDRAGDROP = 0x00000010;
        public const int LVS_EX_FULLROWSELECT = 0x00000020; // applies to report mode only
        public const int LVS_EX_ONECLICKACTIVATE = 0x00000040;

        /// <summary>
        /// Changing the style of listview to accept image on subitems
        /// </summary>
        public OAKListView()
        {
            // Change the style of listview to accept image on subitems
            System.Windows.Forms.Message m = new Message();
            m.HWnd = this.Handle;
            m.Msg = LVM_GETEXTENDEDLISTVIEWSTYLE;
            m.LParam = (IntPtr)(LVS_EX_GRIDLINES |
                                LVS_EX_FULLROWSELECT |
                                LVS_EX_SUBITEMIMAGES |
                                LVS_EX_CHECKBOXES |
                                LVS_EX_TRACKSELECT);
            m.WParam = IntPtr.Zero;
            this.WndProc(ref m);
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(
            IntPtr hWnd,    // handle to destination window 
            int Msg,        // message 
            IntPtr wParam,  // first message parameter 
            IntPtr lParam   // second message parameter 
        );

        [DllImport("user32.dll")]
        public static extern bool SendMessage(
            IntPtr hWnd,        // handle to destination window 
            Int32 msg,          // message 
            Int32 wParam,
            ref LV_ITEM lParam);// pointer to struct of LV_ITEM
    }
}
