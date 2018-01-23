using System;
using System.Linq;
using System.Windows.Forms;
using SbWinNew.Class;

namespace SbWinNew.MainWindow
{
    partial class Form1
    {
        private void TabsAddEvents()
        {
            tabsArea1.BlocksListItemClick += tabsArea1_BlocksListItemClick;
            tabsArea1.BlocksListItemDoubleClick += tabsArea1_BlocksListItemDoubleClick;
        }

        private void tabsArea1_BlocksListItemDoubleClick(object sender, EventArgs e)
        {
            var text = (sender as ListView).SelectedItems[0].Text;
            for (int i = 1; i <= Enum.GetValues(typeof(Helper.Shape)).Cast<int>().Max(); i++)
            {
                var blockText = Helper.DefaultBlocksSettings[(Helper.Shape)i].Label;
                if (text.Equals(blockText))
                {
                    canvas1.AddObjectInstant((Helper.Shape) i);
                    break;
                }
            }
        }

        private void tabsArea1_BlocksListItemClick(object sender, EventArgs e)
        {
            var text = (sender as ListView).SelectedItems[0].Text;
            for (int i = 1; i <= Enum.GetValues(typeof(Helper.Shape)).Cast<int>().Max(); i++)
            {
                var blockText = Helper.DefaultBlocksSettings[(Helper.Shape)i].Label;
                if (text.Equals(blockText))
                {
                    canvas1.AddObjectAfterClick((Helper.Shape)i);
                    break;
                }
            }
        }
    }
}
