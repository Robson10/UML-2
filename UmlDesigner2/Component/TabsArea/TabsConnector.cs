using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmlDesigner2.Component.TabsArea.TabBlocks;

namespace UmlDesigner2.Component.TabsArea
{
    class TabsConnector:TabControl
    {
        private BlocksTab _blocksTab = new BlocksTab();
        public TabsConnector()
        {
            //TabPages.Clear();
            Alignment= TabAlignment.Left;
            _blocksTab.Width = 1000;
            Controls.Add(_blocksTab);
            Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
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
    }
}
