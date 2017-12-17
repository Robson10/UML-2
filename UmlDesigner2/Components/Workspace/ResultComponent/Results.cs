using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2.Component.Workspace.ResultComponent
{
    //class Results:ListView
    class Results : TextBox
    {
        public Results()
        {
            Text = "Wyniki";
            Anchor = (AnchorStyles) (1 | 2 | 4 | 8);
            BackColor = Color.DarkGray;
            Multiline = true;
            ScrollBars = ScrollBars.Both;
            WordWrap = false;

            //View = View.Details;
            //Columns.Add(new ColumnHeader() { Text="Wyniki", Width = ClientRectangle.Width });
            //HeaderStyle = ColumnHeaderStyle.Nonclickable;
            //FullRowSelect = true;
            //MultiSelect = false;
            Compile.Results.TextChanged += Results_TextChanged;
        }

 
        private void Results_TextChanged(object sender, EventArgs e)
        {
            Text = "";
            var temp=Compile.Results.Text.Split(new[] { Environment.NewLine },StringSplitOptions.None).ToList();
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i]= Environment.NewLine+(i+1)+"     "+temp[i];
                Text += temp[i];
            }
            //Items.Clear();
            //Items.Add(new ListViewItem(Compile.Results.Text));
        }

        protected override void OnResize(EventArgs e)
        {
            //base.OnResize(e);
            //Columns[0].Width = ClientRectangle.Width;
        }
    }
}
