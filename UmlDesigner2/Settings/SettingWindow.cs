using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Settings
{
    public partial class SettingWindow : Form
    {

        private PropertyGrid _pg;
        public SettingWindow()
        {
            InitializeComponent();
            _pg = new PropertyGrid()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right|AnchorStyles.Bottom,
                PropertySort = PropertySort.Categorized|PropertySort.Alphabetical,
            };
            _pg.SelectedObject = new SettingsPropertyGrid();
            tabControl1.TabPages[1].Controls.Add(_pg);
            _pg.SetBounds(0, 0, Width, Height);
            _pg.Dock =DockStyle.Fill;
        }
    }
}
