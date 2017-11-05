using System;
using System.Drawing;
using System.Windows.Forms;

namespace UmlDesigner2.Settings.SetShortcut
{
    public partial class SetShortcutWindow : Form
    {
        public Keys Shortcut { get; set; } = Keys.None;
        public SetShortcutWindow()
        {
            InitializeComponent();
            View();
        }
        public void View()
        {
            //BackColor = Helper.BackColor;
            Ok.BackColor = Helper.ButtonColor;
            Ok.ForeColor = Helper.TextColor;
            Ok.Click += Ok_Click;
            Anuluj.BackColor = Helper.ButtonColor;
            Anuluj.ForeColor = Helper.TextColor;
            Anuluj.Click += Close_Click;
            Ok.Location = new Point(Width / 2 - Ok.Width, ClientRectangle.Height - Ok.Height - 10);
            Anuluj.Location = new Point(Width / 2 + 5, ClientRectangle.Height - Anuluj.Height - 10);
        }


        private void Close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }
        private void Ok_Click(object sender, EventArgs e)
        {
            if (keyboard1.SelectedKeys != Keys.None)
            {
                Shortcut = keyboard1.SelectedKeys;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Skrót nie jest poprawny." + Environment.NewLine +
                    "Powinien się składać z Ctrl i/lub Alt i/lub Shift oraz innego znaku");
            }
        }
    }
}
