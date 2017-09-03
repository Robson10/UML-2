using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.ResultComponent
{
    class Results:ListView
    {
        public Results()
        {
            Text = "Wyniki";
            Anchor = (AnchorStyles) (1 | 2 | 4 | 8);
            BackColor = Color.DarkGray;
            View = View.Details;
            Columns.Add(new ColumnHeader() { Text="Wyniki", Width = ClientRectangle.Width });
            HeaderStyle = ColumnHeaderStyle.Nonclickable;
            FullRowSelect = true;
            MultiSelect = false;
            Compile.Results.TextChanged += Results_TextChanged;
            Compile.Run();
        }

        private void Results_TextChanged(object sender, EventArgs e)
        {
            Items.Add(new ListViewItem(Compile.Results.Text));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Columns[0].Width = ClientRectangle.Width;
        }
    }
}
