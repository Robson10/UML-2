using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    partial class Canvas
    {
        private ContextMenuStrip _contextMenu;
        private void ContextMenuPresets()
        {
            _contextMenu = new ContextMenuStrip();
            _contextMenu.Items.Add("Zablokuj/Odblokuj");
            _contextMenu.Items.Add("Usuń");
            _contextMenu.Items.Add("Kopiuj");
            _contextMenu.Items.Add("Wytnij");
            _contextMenu.Items.Add("Autodopasowywanie");
            _contextMenu.ItemClicked += _contextMenu_ItemClicked;
        }
        private void _contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.Equals("Usuń"))
                Delete();
            else if (e.ClickedItem.Text.Equals("Kopiuj"))
                Copy();
            else if (e.ClickedItem.Text.Equals("Wytnij"))
                Cut();
            else if (e.ClickedItem.Text.Equals("Autodopasowywanie"))
                AutoResizeBlockToContent();
            else if (e.ClickedItem.Text.Equals("Zablokuj/Odblokuj"))
                SetIsLockedForObject();

        }
        private void ShowContextMenu(Point e)
        {
            _contextMenu.Visible = true;
            _contextMenu.Show(this, e.X, e.Y);
        }
    }
}
