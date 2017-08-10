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
            _block = block;
            PrepareControlView();
            Pg.SelectedObject = new PropertyGridItems(_block);
        }
        /// <summary>
        /// Metoda służąca do aktualizowania zmian w obszarze ProperisGrid po wykonaniu zmian w Canvas na zaznaczonej figurze
        /// </summary>
        public void UpdateProperties()
        {
            Pg.SelectedObject= new PropertyGridItems(_block);
        }
        /// <summary>
        /// Metoda ta ma za zadanie utworzenie wszystkich niezbędnych komponentów zgodnie z wybranym blokiem
        /// oraz nadanie samej sobie niezbędnych ustawień
        /// </summary>
        private void PrepareControlView()
        {
            BackColor = Color.White;
            Location = new Point(0, 0);
            AutoScroll = false;
            VerticalScroll.Visible = true;
            AutoScroll = true;
            AutoScrollPosition = new Point(0, 0);
            Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            HorizontalScroll.Maximum = 0;

            if (_block.Shape != BlocksData.Shape.Start && _block.Shape != BlocksData.Shape.End)
            {
                grLabel = new GroupBox();
                Controls.Add(grLabel);
                grLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                grLabel.Text = "Etykieta";
                grLabel.Font = new Font("Arial", 13);
                grLabel.Location = new Point(5, 0);
                grLabel.Size = new Size(ClientRectangle.Width - 10, 70);

                tbLabel = new TextBox();
                grLabel.Controls.Add(tbLabel);
                tbLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                tbLabel.Font = new Font("Arial", 12);
                tbLabel.Location = new Point(5, 20);
                tbLabel.Size = new Size(grLabel.ClientRectangle.Width - 10, grLabel.ClientRectangle.Height - 25);
                tbLabel.Multiline = true;
                tbLabel.ScrollBars = ScrollBars.Vertical;
                tbLabel.KeyUp += TbLabel_KeyUp;

                grCode = new GroupBox();
                Controls.Add(grCode);
                grCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                grCode.Text = "Kod";
                grCode.Font = new Font("Arial", 13);
                grCode.Location = new Point(grLabel.Left, grLabel.Bottom);
                grCode.Size = new Size(ClientRectangle.Width - 10, 100);

                tbCode = new TextBox();
                grCode.Controls.Add(tbCode);
                tbCode.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                tbCode.Font = new Font("Arial", 12);
                tbCode.Location = new Point(5, 20);
                tbCode.Size = new Size(grCode.ClientRectangle.Width - 10, grCode.ClientRectangle.Height - 25);
                tbCode.Multiline = true;
                tbCode.ScrollBars = ScrollBars.Vertical;
                tbCode.KeyUp += TbCode_KeyUp;


                tbLabel.Text = _block.Label;
                tbCode.Text = _block.Code;
            }

            Pg = new PropertyGrid();
            Pg.Location = new Point(grCode?.Left ?? 0, grCode?.Bottom ?? 0);
            Pg.Anchor= AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Pg.Size = new Size(ClientRectangle.Size.Width-10, 250);
            Controls.Add(Pg);
            Pg.PropertySort = PropertySort.NoSort;
            SetPropertyLabelColumnWidth(Pg, Pg.Width * 60 / 100);
            Pg.PropertyValueChanged += Pg_PropertyValueChanged;
        }
        /// <summary>
        /// Metoda zmieniająca szerokość pierwszej kolumny w kontrolce typu PropertyGrid zgodnie z zadaną szerokością (<paramref name="width"/>)
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="width"></param>
        private static void SetPropertyLabelColumnWidth(PropertyGrid grid, int width)
        {
            var memberInfo = grid.GetType().GetField("gridView", BindingFlags.Instance | BindingFlags.NonPublic);
            if (memberInfo != null)
            {
                var temp = (Control)memberInfo.GetValue(grid);
                var tempFieldInfo = temp.GetType().GetField("labelWidth", BindingFlags.Instance | BindingFlags.NonPublic);
                if (tempFieldInfo != null)
                    tempFieldInfo.SetValue(temp, width);
                temp.Invalidate();
            }
        }

        private void TbLabel_KeyUp(object sender, KeyEventArgs e)
        {
            _block.Label = tbLabel.Text;
            (Parent.Parent.Parent.Parent.Parent as Form1)?.CanvasInvalidateForProperties();
        }

        private void TbCode_KeyUp(object sender, KeyEventArgs e)
        {
            _block.Code = tbCode.Text;
            (Parent.Parent.Parent.Parent.Parent as Form1)?.CanvasInvalidateForProperties();
        }

        private void Pg_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            (Parent.Parent.Parent.Parent.Parent as Form1)?.CanvasInvalidateForProperties();
        }
        
        



        //protected override void OnResize(EventArgs e)
        //{
        //    Location = new Point(0, 0);
        //    AutoScrollPosition = new Point(0,0);

        //    if (tbCode != null)
        //    {
        //        if (grLabel != null)
        //        {
        //            grLabel.Location = new Point(5, 0);
        //            grLabel.Size = new Size(ClientRectangle.Width - 10, 70);
        //            tbLabel.Location = new Point(5, 0 + 20);
        //            tbLabel.Size = new Size(grLabel.ClientRectangle.Width - 10, grLabel.ClientRectangle.Height - 25);
        //        }
        //        if (grCode != null)
        //        {
        //            grCode.Location = new Point(grLabel.Left, grLabel.Bottom);
        //            grCode.Size = new Size(ClientRectangle.Width - 10, 100);
        //            tbCode.Location = new Point(5, 20);
        //            tbCode.Size = new Size(grCode.ClientRectangle.Width - 10, grCode.ClientRectangle.Height - 25);
        //        }
        //    }
        //    Pg.Location = new Point(grCode?.Left ?? 0, grCode?.Bottom ?? 0);
        //    Pg.Size = new Size(ClientRectangle.Size.Width-10,200);
        //    //Parent.Parent.Parent.MinimumSize = new Size(250, 0);
        //    SetPropertyLabelColumnWidth(Pg, Pg.Width*60/100);
        //    AutoScroll = true;


        //}
        protected override void OnResize(EventArgs e)
        {
            SetPropertyLabelColumnWidth(Pg, Pg.Width * 60 / 100);
            AutoScroll = true;
        }
        public bool ShouldRefresh(MyBlock newBlock)
        {
            return (newBlock == _block);
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
        [Description("Położenie")]        
        [DisplayName("Położenie")]  
        public Point Location
        {
            get { return _block.Rect.Location; }
            set { _block.Rect.Location = value; }
        }
        [Category("Parametry")]
        [Description("Wymiary")]
        [DisplayName("Wymiary")]
        public Size Size
        {
            get { return _block.Rect.Size; }
            set { _block.Rect.Size = value; }
        }
    }
}
