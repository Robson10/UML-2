using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2
{
    partial class Form1
    {
        private void TabsPresets()
        {
            tabsConnector1.BlocksListItemClick += TabsConnector2_BlocksListItemClick;
            tabsConnector1.BlocksListItemDoubleClick += TabsConnector2_BlocksListItemDoubleClick;
            tabsConnector1.SchematsListItemClick += TabsConnector2_SchematsListItemClick;
            tabsConnector1.SchematsListItemDoubleClick += TabsConnector2_SchematsListItemDoubleClick;
        }

        private void TabsConnector2_SchematsListItemDoubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TabsConnector2_SchematsListItemClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TabsConnector2_BlocksListItemDoubleClick(object sender, EventArgs e)
        {
            var text = (sender as ListView).SelectedItems[0].Text;
            for (int i = 1; i <= Enum.GetValues(typeof(BlocksData.Shape)).Cast<int>().Max(); i++)
            {
                var blockText = BlocksData.DefaultLabel((BlocksData.Shape) i);
                if (text.Equals(blockText))
                {
                    canvas1.AddObjectInstant((BlocksData.Shape) i);
                    break;
                }
            }
        }

        private void TabsConnector2_BlocksListItemClick(object sender, EventArgs e)
        {
            var text = (sender as ListView).SelectedItems[0].Text;
            for (int i = 1; i <= Enum.GetValues(typeof(BlocksData.Shape)).Cast<int>().Max(); i++)
            {
                var blockText = BlocksData.DefaultLabel((BlocksData.Shape)i);
                if (text.Equals(blockText))
                {
                    canvas1.AddObjectAfterClick((BlocksData.Shape)i);
                    break;
                }
            }
        }
    }
}
