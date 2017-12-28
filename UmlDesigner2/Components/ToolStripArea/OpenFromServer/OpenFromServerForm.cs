using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using UmlDesigner2.Class;
using UmlDesigner2.Components.Workspace;

namespace UmlDesigner2.Components.ToolStripArea.OpenFromServer
{
    public partial class OpenFromServerForm : Form
    {
        public OpenFromServerForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            this.Shown += OpenFromServerForm_Shown;
            FillCombo();
        }

        private void OpenFromServerForm_Shown(object sender, EventArgs e)
        {
            Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - Height);
        }

        private void FillCombo()
        {
            var query = "select Name from SbWinNEW.dbo.Files where IdUser=" + Login.LoginForm.UserID;
            var temp = Helper.DataBaseSelect(query).Tables[0];
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                comboFiles.Items.Add(temp.Rows[i].Field<string>(0));
            }
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            if (!comboFiles.SelectedItem.Equals(""))
            {
                var query = "select blocks,lines from SbWinNEW.dbo.Files where IdUser=" + Login.LoginForm.UserID+ "and Name='"+comboFiles.SelectedItem+"'";
                var temp = Helper.DataBaseSelect(query).Tables[0];
                var data = temp.Rows[0].Field<string>(0);

                var serializer = new XmlSerializer(typeof(ListCanvasBlocks));
                var reader = new StringReader(data);
                Canvas.CanvObj = (ListCanvasBlocks)serializer.Deserialize(reader);

                data = temp.Rows[0].Field<string>(1);
                serializer = new XmlSerializer(typeof(ListCanvasLines));
                reader = new StringReader(data);
                Canvas.CanvLines = (ListCanvasLines)serializer.Deserialize(reader);
                
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btAbort_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}
