using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    sealed partial class Canvas
    {
        private ContextMenuStrip _contextMenu;
        private void ContextMenuPresets()
        {
            _contextMenu = new ContextMenuStrip();
            _contextMenu.Items.Add("Zablokuj/Odblokuj");
            _contextMenu.Items.Add("Przesuń do przodu");
            _contextMenu.Items.Add("Przesuń do tyłu");
            _contextMenu.ItemClicked += _contextMenu_ItemClicked;
        }
        private void _contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void ShowContextMenu(MouseEventArgs e)
        {
            _contextMenu.Visible = true;
            _contextMenu.Show(this, e.Location.X, e.Location.Y);
        }
    }
}
