using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmlDesigner2.Component.TabsArea.TabBlocks;
using UmlDesigner2.Component.TabsArea.TabSchemats;

namespace UmlDesigner2.Component.TabsArea
{
    class TabsConnector:TabControl
    {
        private BlocksTab _blocksTab = new BlocksTab();
        private SchematsTab _schematsTab = new SchematsTab();
        public TabsConnector()
        {
            //TabPages.Clear();
            Alignment= TabAlignment.Left;
            TabPages.Add(_blocksTab);
            TabPages.Add(_schematsTab);
            Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top);
        }

        public event EventHandler BlocksListItemClick
        {
            add { _blocksTab.ListItemClick += value; }
            remove { _blocksTab.ListItemClick += value; }
        }
        public event EventHandler BlocksListItemDoubleClick
        {
            add { _blocksTab.ListItemDoubleClick += value; }
            remove { _blocksTab.ListItemDoubleClick += value; }
        }
        public event EventHandler SchematsListItemClick
        {
            add { _schematsTab.ListItemClick += value; }
            remove { _schematsTab.ListItemClick += value; }
        }
        public event EventHandler SchematsListItemDoubleClick
        {
            add { _schematsTab.ListItemDoubleClick += value; }
            remove { _schematsTab.ListItemDoubleClick += value; }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Location = new System.Drawing.Point(0, 0);
            Size = new System.Drawing.Size(Parent.Width, Parent.Height);
        }
        /// <summary>
        /// Usuniecie domyślnie tworzonych zakładek przez Form1.designer.cs
        /// </summary>
        /// <param name="e"></param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control.Text.Contains("tabPage"))
            {
                e.Control.Dispose();
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }
    }
}
