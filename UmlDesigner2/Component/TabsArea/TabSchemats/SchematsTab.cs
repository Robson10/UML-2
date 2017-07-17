using System;
using System.Drawing;
using System.Windows.Forms;
using UmlDesigner2.Component.TabsArea.TabBlocks;

namespace UmlDesigner2.Component.TabsArea.TabSchemats
{
    public class SchematsTab : TabPage
    {
        private readonly ListView _listViewOfSchemats = new ListView() { };



        public SchematsTab()
        {
            Text = SchematsTabPropertis.TabText;
            CreateList();
        }

        private void CreateList()
        {
            _listViewOfSchemats.View = View.Details;
            _listViewOfSchemats.Size = new Size(Width, Height);
            _listViewOfSchemats.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left |
                                        AnchorStyles.Right);
            _listViewOfSchemats.OwnerDraw = true;
            _listViewOfSchemats.DrawItem += _listViewOfSchemats_DrawItem;
            _listViewOfSchemats.Columns.Add(new ColumnHeader() {Width = ClientRectangle.Width});
            _listViewOfSchemats.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            _listViewOfSchemats.HeaderStyle = ColumnHeaderStyle.None;
            _listViewOfSchemats.FullRowSelect = true;
            _listViewOfSchemats.MultiSelect = false;
            LoadElementsToList();
            this.Controls.Add(_listViewOfSchemats);
        }

        private void _listViewOfSchemats_DrawItem(object sender, DrawListViewItemEventArgs e)
        {

            TextFormatFlags flags;
            if (e.Item.Index == (sender as ListView).Items.Count - 1)
            {
                e.DrawFocusRectangle();
                flags = TextFormatFlags.HorizontalCenter;
            }
            else
            {
                if (e.Item.Selected)
                {
                    e.Item.BackColor = Color.Orange;
                    e.DrawBackground();
                }
                 flags= TextFormatFlags.Left;
            }
            e.DrawText(flags);
        }

        private void LoadElementsToList()
        {
            if (!System.IO.Directory.Exists(SchematsTabPropertis.Path))
                System.IO.Directory.CreateDirectory(SchematsTabPropertis.Path);
            
            var filesPaths = System.IO.Directory.GetFiles(SchematsTabPropertis.Path, "*"+ SchematsTabPropertis.Extension);

            for (int i = 0; i < filesPaths.Length; i++)
                _listViewOfSchemats.Items.Add(new ListViewItem(filesPaths[i].Replace(SchematsTabPropertis.Path + @"\","").Replace(SchematsTabPropertis.Extension, "")));
            
            _listViewOfSchemats.Items.Add(new ListViewItem("Importuj"){ForeColor=Color.Blue});
            _listViewOfSchemats.RedrawItems(0, 3, true);
            _listViewOfSchemats.Invalidate();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            _listViewOfSchemats.Columns[0].Width = Width;
        }

        public event EventHandler ListItemClick
        {
            add { _listViewOfSchemats.Click += value; }
            remove { _listViewOfSchemats.Click -= value; }
        }

        public event EventHandler ListItemDoubleClick
        {
            add { _listViewOfSchemats.DoubleClick += value; }
            remove { _listViewOfSchemats.DoubleClick -= value; }
        }
    }
}