using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmlDesigner2.Class;

namespace UmlDesigner2.Components.Login
{
    public partial class LoginForm : Form
    {
        public static string Login = "", Password="";
        public static int UserID = -1;
        public LoginForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Shown += LoginForm_Shown;
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2-Width/2, Screen.PrimaryScreen.Bounds.Height / 2-Height);
        }


        private void Zaloguj_Click(object sender, EventArgs e)
        {
            var temp = Helper.DataBaseSelect("select * from Users where Login='" + LoginTB.Text + "' and Password='" +
                                             PasswordTB.Text + "'");
            if (temp.Tables[0].Rows.Count > 0)
            {
                Login = LoginTB.Text;
                Password = PasswordTB.Text;
                UserID = temp.Tables[0].Rows[0].Field<Int32>(0);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Dane logowania są niepoprawne");
            }
            

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}
