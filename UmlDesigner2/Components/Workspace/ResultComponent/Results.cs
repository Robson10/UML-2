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
            Text = "Kod po skompilowaniu";
            Anchor = (AnchorStyles) (1 | 2 | 4 | 8);
            BackColor = Color.DarkGray;
            Multiline = true;
            ScrollBars = ScrollBars.Both;
            WordWrap = false;
            Compile.Results.TextChanged += Results_TextChanged;
        }


        private void Results_TextChanged(object sender, EventArgs e)
        {
            Text = "Kod po skompilowaniu";
            var temp=Compile.Results.Text.Split(new[] { Environment.NewLine },StringSplitOptions.None).ToList();
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i]= Environment.NewLine+(i+1)+"     "+temp[i];
                Text += temp[i];
            }
        }
    }
}
