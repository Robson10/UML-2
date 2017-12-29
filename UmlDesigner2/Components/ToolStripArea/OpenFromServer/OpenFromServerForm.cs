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
            comboFiles.Items.Clear();
            comboFiles.Text = "";
            var query = "select Name from SbWinNEW.dbo.Files where IdUser=" + Login.LoginForm.UserID;
            var temp = Helper.DataBaseSelect(query).Tables[0];
            for (int i = 0; i < temp.Rows.Count; i++)
                comboFiles.Items.Add(temp.Rows[i].Field<string>(0));

            if (temp.Rows.Count > 0)
                comboFiles.SelectedIndex = 0;
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            if (!comboFiles.SelectedItem.Equals(""))
            {
                var query = "select blocks,lines from SbWinNEW.dbo.Files where IdUser=" + Login.LoginForm.UserID+ "and Name='"+comboFiles.SelectedItem+"'";
                var temp = Helper.DataBaseSelect(query).Tables[0];
                Canvas.CanvObj=SqlVarcharToList(Canvas.CanvObj, temp.Rows[0].Field<string>(0));
                Canvas.CanvLines=SqlVarcharToList(Canvas.CanvLines, temp.Rows[0].Field<string>(1));
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private T SqlVarcharToList<T>(T mylist,string data)
        {
            using (var reader = new StringReader(data))
            {
                var serializer = new XmlSerializer(mylist.GetType());
                return (T)serializer.Deserialize(reader);
                reader.Close();
            }
        }

        private void btAbort_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void comboFiles_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btLoad_Click(btLoad, null);
            }
            if (e.KeyChar == (char)Keys.Escape)
                btAbort_Click(btAbort, null);
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (!comboFiles.SelectedItem.Equals(""))
            {
                var query = "DELETE FROM SbWinNew.dbo.Files WHERE IdUser=" + Login.LoginForm.UserID + " and Name='" + comboFiles.SelectedItem + "'";
                Helper.DatabaseExecuteQuery(query);
                FillCombo();
            }
        }
    }
}
