﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2
{
    partial class Form1
    {
        private void TabsAddEvents()
        {
            tabsArea1.BlocksListItemClick += tabsArea1_BlocksListItemClick;
            tabsArea1.BlocksListItemDoubleClick += tabsArea1_BlocksListItemDoubleClick;
            tabsArea1.SchematsListItemClick += tabsArea1_SchematsListItemClick;
            tabsArea1.SchematsListItemDoubleClick += tabsArea1_SchematsListItemDoubleClick;
        }

        private void tabsArea1_SchematsListItemDoubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void tabsArea1_SchematsListItemClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
