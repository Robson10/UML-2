using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2.Component.TabsArea.BlockPropertis
{
    public partial class Properties : UserControl
    {
        private GroupBox grLabel;
        private TextBox tbLabel;
        private GroupBox grCode;
        private TextBox tbCode;

        private PropertyGrid Pg;
        private MyBlock _block;

        public Properties(MyBlock block)
        {
            BackColor = Color.White;
            Anchor = AnchorStyles.Bottom|AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right;
            Location = new Point(0, 0);
            HorizontalScroll.Maximum = 0;
            AutoScroll = false;
            VerticalScroll.Visible = true;
            AutoScroll = true;
            _block = block;
            PrepareView();

            Pg.SelectedObject = new PropertyGridItems(_block);
            Pg.PropertyValueChanged += Pg_PropertyValueChanged;
            tbLabel.KeyUp += TbLabel_KeyUp;
        }

        private void TbLabel_KeyUp(object sender, KeyEventArgs e)
        {
            (Parent.Parent.Parent.Parent.Parent as Form1)?.CanvasInvalidateForProperties();
        }

        private void Pg_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            (Parent.Parent.Parent.Parent.Parent as Form1)?.CanvasInvalidateForProperties();
        }


        public bool ShouldRefresh(MyBlock newBlock)
        {
            return (newBlock == _block);
        }
        private void  PrepareView()
        {
            if (_block.Shape != BlocksData.Shape.Start && _block.Shape != BlocksData.Shape.End)
            {
                grLabel = new GroupBox();
                grLabel.Text = "Etykieta";
                grLabel.Font= new Font("Arial", 13);
                Controls.Add(grLabel);
                grLabel.Location = new Point(5, 0);
                grLabel.Size = new Size(ClientRectangle.Width - 10, 70);

                tbLabel = new TextBox();
                grLabel.Controls.Add(tbLabel);
                tbLabel.Location = new Point(0 + 5, 0 + 20);
                tbLabel.Size = new Size(grLabel.ClientRectangle.Width - 10, grLabel.ClientRectangle.Height - 25);
                tbLabel.Multiline = true;
                tbLabel.Font = new Font("Arial", 12);
                tbLabel.ScrollBars = ScrollBars.Vertical;

                grCode = new GroupBox();
                grCode.Text = "Kod";
                grCode.Font = new Font("Arial", 13);
                grCode.Location = new Point(grLabel.Left, grLabel.Bottom);
                grCode.Size = new Size(ClientRectangle.Width - 10, 100);
                Controls.Add(grCode);

                tbCode = new TextBox();
                tbCode.Location = new Point(0, 0);
                grCode.Controls.Add(tbCode);
                tbCode.Size = grCode.ClientSize;
                tbCode.Multiline = true;
                tbCode.Font = new Font("Arial", 12);
                tbCode.ScrollBars = ScrollBars.Vertical;


                tbLabel.Text = _block.Label;
                tbCode.Text = _block.Code;
            }

            Pg = new PropertyGrid();
            Pg.Location = new Point((grCode != null) ? grCode.Left : 0, (grCode != null) ? grCode.Bottom : 0);
            Pg.Size = new Size(ClientRectangle.Size.Width, 200);
            Controls.Add(Pg);
            Pg.PropertySort = PropertySort.NoSort;
            SetLabelColumnWidth(Pg, Pg.Width * 60 / 100);
        }

        public static void SetLabelColumnWidth(PropertyGrid grid, int width)
        {
            Control temp = (Control)grid.GetType().GetField("gridView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(grid);
            FieldInfo tempFieldInfo = temp.GetType().GetField("labelWidth", BindingFlags.Instance | BindingFlags.NonPublic);
            tempFieldInfo.SetValue(temp, width);
            temp.Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            Location = new Point(0, 0);
            AutoScrollPosition = new Point(0,0);

            if (tbCode != null)
            {
                if (grLabel != null)
                {
                    grLabel.Location = new Point(5, 0);
                    grLabel.Size = new Size(ClientRectangle.Width - 10, 70);
                    tbLabel.Location = new Point(5, 0 + 20);
                    tbLabel.Size = new Size(grLabel.ClientRectangle.Width - 10, grLabel.ClientRectangle.Height - 25);
                }
                if (grCode != null)
                {
                    grCode.Location = new Point(grLabel.Left, grLabel.Bottom);
                    grCode.Size = new Size(ClientRectangle.Width - 10, 100);
                    tbCode.Location = new Point(5, 20);
                    tbCode.Size = new Size(grCode.ClientRectangle.Width - 10, grCode.ClientRectangle.Height - 25);
                }
            }
            Pg.Location = new Point(grCode?.Left ?? 0, grCode?.Bottom ?? 0);
            Pg.Size = new Size(ClientRectangle.Size.Width-10,200);
            //Parent.Parent.Parent.MinimumSize = new Size(250, 0);
            SetLabelColumnWidth(Pg, Pg.Width*60/100);
            AutoScroll = true;


        }
    }
    class PropertyGridItems
    {
        private MyBlock _block;
        public PropertyGridItems(MyBlock block)
        {
            _block = null;
            _block = block;
        }
        [Category("Parametry")]
        [Description("Shape")]
        [DisplayName("Shape")]
        public BlocksData.Shape Shape
        {
            get { return _block.Shape; }
            set
            {
                _block.Shape = value; 
            }
        }
        [Category("Parametry")]
        [Description("Zablokowane")]
        [DisplayName("Zablokowane")]
        public bool IsLocked
        {
            get { return _block.IsLocked; }
            set { _block.IsLocked = value; }
        }

        [Category("Parametry")]
        [Description("Auto dopasowywanie")]
        [DisplayName("Auto dopasowywanie")]
        public bool AutoResize
        {
            get { return _block.AutoResize; }
            set { _block.AutoResize = value; }
        }

        [Category("Parametry")]                    
        [Description("Rozmiar Czcionki")]        
        [DisplayName("Rozmiar Czcionki")]  
        public int FontSize
        {
            get { return _block.FontSize; }
            set { _block.FontSize = value; }
        }

        [Category("Parametry")]                    
        [Description("Kolor czcionki")]        
        [DisplayName("Kolor czcionki")]  
        public Color FontColor
        {
            get { return _block.FontColor; }
            set { _block.FontColor = value; }
        }
        [Category("Parametry")]                    
        [Description("Kolor tła")]        
        [DisplayName("Kolor tła")]  
        public Color BackColor
        {
            get { return _block.BackColor; }
            set { _block.BackColor = value; }
        }
        [Category("Parametry")]                    
        [Description("Rozmiar: \nX, Y, Width, Height")]        
        [DisplayName("Wymiary")]  
        public Rectangle Size
        {
            get { return _block.Rect; }
            set { _block.Rect = value; }
        }
    }
}
