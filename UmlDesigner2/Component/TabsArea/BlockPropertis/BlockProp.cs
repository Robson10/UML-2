using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.TabsArea.BlockPropertis
{
    public partial class BlockProp : UserControl
    {
        private GroupBox grLabel = new GroupBox();
        private GroupBox grCode = new GroupBox();
        private PropertyGrid Pg= new PropertyGrid();
        public BlockProp()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            Anchor = (AnchorStyles) (1| 2| 3| 4);

            grLabel.Text = "Etykieta";
            Controls.Add(grCode);
            grLabel.Visible = false;

            grCode.Text = "Kod";
            grCode.Location = new Point(grLabel.Left, grLabel.Bottom);
            Controls.Add(grLabel);

            Pg.Width = 200;
            Pg.Height = 400;
            Pg.Location = new Point(grCode.Left, grCode.Bottom);
            Controls.Add(Pg);
            Pg.SelectedObject = new PropertyGridItems();
            
        }
       
    }
    class PropertyGridItems
    {
        bool isLocked = false;
        [Category("Category1")]                    // Category that I want
        [Description("IsLocked")]        // yet one hint
        [DisplayName("IsLocked")]  // that is a question
        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }

        private int fontSize=10;
        [Category("Category1")]                    // Category that I want
        [Description("FontSize")]        // yet one hint
        [DisplayName("FontSize")]  // that is a question
        public int FontSize
        {
            get => fontSize;
            set => fontSize = value;
        }
        private Color fontColor = Color.Wheat;
        [Category("Category1")]                    // Category that I want
        [Description("FontColor")]        // yet one hint
        [DisplayName("FontColor")]  // that is a question
        public Color FontColor
        {
            get => fontColor;
            set => fontColor = value;
        }

        private Color backColor = Color.Silver;
        [Category("Category1")]                    // Category that I want
        [Description("BackColor")]        // yet one hint
        [DisplayName("BackColor")]  // that is a question
        public Color BackColor
        {
            get => backColor;
            set => backColor = value;
        }
    }
}
