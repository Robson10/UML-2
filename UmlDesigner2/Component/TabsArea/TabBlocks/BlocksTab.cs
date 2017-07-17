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
    public class BlocksTab : TabPage
    {
        private readonly OAKListView _listViewOfBlocks = new OAKListView() { };

        public BlocksTab()
        {
            Text = BlocksTabPropertis.TabText;
            CreateList();
        }

        private void CreateList()
        {
            _listViewOfBlocks.View = View.Details;
            _listViewOfBlocks.Size = new Size(Width, Height);
            _listViewOfBlocks.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left |
                                        AnchorStyles.Right);
            _listViewOfBlocks.Columns.Add(new ColumnHeader() {Width = ClientRectangle.Width});
            _listViewOfBlocks.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            _listViewOfBlocks.HeaderStyle = ColumnHeaderStyle.None;
            _listViewOfBlocks.FullRowSelect = true;
            _listViewOfBlocks.MultiSelect = false;
            FillList();
            this.Controls.Add(_listViewOfBlocks);
        }


        private void FillList()
        {
            _listViewOfBlocks.SmallImageList = LoadImageList();
            _listViewOfBlocks.Items.Add(new ListViewItem(BlocksTabPropertis.ListItemTextForStart) {ImageIndex = 0});
            _listViewOfBlocks.Items.Add(new ListViewItem(BlocksTabPropertis.ListItemTextForEnd) {ImageIndex = 1});
            _listViewOfBlocks.Items.Add(new ListViewItem(BlocksTabPropertis.ListItemTextForDecision) {ImageIndex = 2});
            _listViewOfBlocks.Items.Add(new ListViewItem(BlocksTabPropertis.ListItemTextForExecution) {ImageIndex = 3});
            _listViewOfBlocks.Items.Add(new ListViewItem(BlocksTabPropertis.ListItemTextForInput) {ImageIndex = 4});
        }


        private ImageList LoadImageList()
        {
            var imageList1 = new ImageList();
            imageList1.ImageSize = new Size(40, 40);
            imageList1.TransparentColor = Color.Transparent;
            AddImageToImageListFromPath(BlocksTabPropertis.PathToImageStartBlock, ref imageList1);
            AddImageToImageListFromPath(BlocksTabPropertis.PathToImageEndBlock, ref imageList1);
            AddImageToImageListFromPath(BlocksTabPropertis.PathToImageDecisionBlock, ref imageList1);
            AddImageToImageListFromPath(BlocksTabPropertis.PathToImageExecuteBlock, ref imageList1);
            AddImageToImageListFromPath(BlocksTabPropertis.PathToImageInputBlock, ref imageList1);
            return imageList1;
        }

        private void AddImageToImageListFromPath(string path, ref ImageList imageList1)
        {
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(path))
            {
                imageList1.Images.Add(Image.FromStream(stream));
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            _listViewOfBlocks.Columns[0].Width = Width;
        }

        public event EventHandler ListItemClick
        {
            add { _listViewOfBlocks.Click += value; }
            remove { _listViewOfBlocks.Click -= value; }
        }

        public event EventHandler ListItemDoubleClick
        {
            add { _listViewOfBlocks.DoubleClick += value; }
            remove { _listViewOfBlocks.DoubleClick -= value; }
        }
    }
}
