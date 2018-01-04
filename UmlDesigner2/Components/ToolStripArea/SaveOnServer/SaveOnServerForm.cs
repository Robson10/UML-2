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
using System.Xml;
using System.Xml.Serialization;
using UmlDesigner2.Class;
using UmlDesigner2.Components.Workspace;

namespace UmlDesigner2.Components.ToolStripArea.SaveOnServer
{
    public partial class SaveOnServerForm : Form
    {
        public SaveOnServerForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            this.Shown += SaveOnServerForm_Shown;
        }

        private void SaveOnServerForm_Shown(object sender, EventArgs e)
        {
            Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - Width / 2, Screen.PrimaryScreen.Bounds.Height / 2 - Height);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            var name = "'" + tbProjectName.Text + "'";
            var idUser = Login.LoginForm.UserID;
            var query= "select * from SbWinNEW.dbo.Files where Name="+name+" and IdUser="+idUser+" ";
            var queryResult=Helper.DataBaseSelect(query).Tables[0];


           var sln=DataToSqlVarchar((int) 0);
            var blocks= DataToSqlVarchar(Canvas.CanvObj);
               var lines=DataToSqlVarchar(Canvas.CanvLines);
            
            
           

            if (queryResult.Rows.Count > 0) //Override?
            {
                DialogResult dialogResult = MessageBox.Show("Taki plik już istnieje. Czy chcesz go nadpisać?","Zapisz", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    query = "update SbWinNEW.dbo.Files set" +
                            " sln=" + sln + "," +
                            " lines=" + lines + "," +
                            " blocks=" + blocks +
                            " where IdUser=" + idUser +
                            " and Name=" + name;
                    Helper.DatabaseExecuteQuery(query);
                    DialogResult = DialogResult.OK;
                    Close();
                }

            }
            else
            {
                query = "insert into SbWinNEW.dbo.Files(Name,IdUser,sln,lines,blocks) values (" +
                        name + "," +
                        idUser + "," +
                        sln + "," +
                        lines + "," +
                        blocks + ")";
                Helper.DatabaseExecuteQuery(query);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private string DataToSqlVarchar<T>(T data)
        {

            var xmlWriterSettings = new XmlWriterSettings() { Indent = true, NewLineHandling = NewLineHandling.Entitize };
            XmlSerializer serializer = new XmlSerializer(data.GetType());
            var sw = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(sw,xmlWriterSettings))
            {
                serializer.Serialize(xmlWriter, data);
                return "CONVERT(NVARCHAR(max),'" + sw + "')";
            }
        }

        private void btAbort_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoginTB_TextChanged(object sender, EventArgs e)
        {
            var tb = (sender as TextBox);
            tb.Text = tb.Text.Replace(".xml", "") + ".xml";
            if (tb.Text.Count()> 4)
            {
                tb.SelectionStart = tb.Text.Length - 4;
                tb.SelectionLength = 0;
            }
        }

        private void tbProjectName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btSave_Click(btSave, null);
            }
            if (e.KeyChar == (char)Keys.Escape)
                btAbort_Click(btAbort, null);
        }
    }
}
