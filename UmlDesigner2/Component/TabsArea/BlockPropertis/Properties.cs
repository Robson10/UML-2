using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2.Component.TabsArea.BlockPropertis
{
    
    public partial class Properties : UserControl
    {
        private GroupBox _grLabel, _grCode;
        private TextBox _tbLabel, _tbCode;
        private PropertyGrid _pg;
        private MyBlock _block;

        /// <summary>
        /// Konstruktor tworzący kontrolkę. Na podstawie MyBlock wyświetlane są wszystkie niezbędne pola dla użytkownika.
        /// </summary>
        /// <param name="block"></param>
        public Properties(MyBlock block)
        {
            _block = block;
            PrepareControlView();
            _pg.SelectedObject = new PropertyGridItems(_block);
        }

        /// <summary>
        /// Metoda służąca do aktualizowania zmian w obszarze ProperisGrid po wykonaniu zmian w Canvas na zaznaczonej figurze
        /// </summary>
        public void UpdateProperties()
        {
            _pg.SelectedObject= new PropertyGridItems(_block);
            SetPropertyLabelColumnWidth(_pg, _pg.Width * 60 / 100);
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

            if (_block.Shape != MyDictionary.Shape.Start && _block.Shape != MyDictionary.Shape.End)
            {
                _grLabel = new GroupBox();
                Controls.Add(_grLabel);
                _grLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                _grLabel.Text = "Etykieta";
                _grLabel.Font = new Font("Arial", 13);
                _grLabel.Location = new Point(5, 0);
                _grLabel.Size = new Size(ClientRectangle.Width - 10, 70);

                _tbLabel = new TextBox();
                _grLabel.Controls.Add(_tbLabel);
                _tbLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                _tbLabel.Font = new Font("Arial", 12);
                _tbLabel.Location = new Point(5, 20);
                _tbLabel.Size = new Size(_grLabel.ClientRectangle.Width - 10, _grLabel.ClientRectangle.Height - 25);
                _tbLabel.Multiline = true;
                _tbLabel.ScrollBars = ScrollBars.Vertical;
                _tbLabel.KeyUp += TbLabel_KeyUp;

                _grCode = new GroupBox();
                Controls.Add(_grCode);
                _grCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                _grCode.Text = "Kod";
                _grCode.Font = new Font("Arial", 13);
                _grCode.Location = new Point(_grLabel.Left, _grLabel.Bottom);
                _grCode.Size = new Size(ClientRectangle.Width - 10, 100);

                _tbCode = new TextBox();
                _grCode.Controls.Add(_tbCode);
                _tbCode.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                _tbCode.Font = new Font("Arial", 12);
                _tbCode.Location = new Point(5, 20);
                _tbCode.Size = new Size(_grCode.ClientRectangle.Width - 10, _grCode.ClientRectangle.Height - 25);
                _tbCode.Multiline = true;
                _tbCode.ScrollBars = ScrollBars.Vertical;
                _tbCode.KeyUp += TbCode_KeyUp;


                _tbLabel.Text = _block.Label;
                _tbCode.Text = _block.Code;
            }

            _pg = new PropertyGrid();
            _pg.Location = new Point(_grCode?.Left ?? 0, _grCode?.Bottom ?? 0);
            _pg.Anchor= AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _pg.Size = new Size(ClientRectangle.Size.Width-10, 250);
            Controls.Add(_pg);
            _pg.PropertySort = PropertySort.NoSort;
            SetPropertyLabelColumnWidth(_pg, _pg.Width * 60 / 100);
            _pg.PropertyValueChanged += Pg_PropertyValueChanged;
        }

        /// <summary>
        /// Metoda zmieniająca szerokość pierwszej kolumny w kontrolce typu PropertyGrid zgodnie z zadaną szerokością (<paramref name="width"/>)
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="width"></param>
        private static void SetPropertyLabelColumnWidth(PropertyGrid grid, int width)
        {
            width = 130;
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

        /// <summary>
        /// zdarzenie dla pola tekstowego gdzie przechowywana jest etykieta. Aktualizuje pole Label dla bloku oraz wywołuje metodę odświerzającą kontrolkę canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbLabel_KeyUp(object sender, KeyEventArgs e)
        {
            _block.Label = _tbLabel.Text;
            (Parent.Parent.Parent.Parent.Parent as Form1)?.CanvasInvalidatebyInvalidateByProperties();
        }

        /// <summary>
        /// zdarzenie dla pola tekstowego gdzie przechowyway jest część kodu programu. Aktualizuje pole Code dla bloku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbCode_KeyUp(object sender, KeyEventArgs e)
        {
            _block.Code = _tbCode.Text;
        }

        /// <summary>
        /// Zdarzenie wywołujące metodę aktualizującą canvas w chwili gdy któreś pole w kontrolce PropertyGrid ulegnie zmianie.
        /// Wywołuje odświerzenie kontrolki canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pg_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            (Parent.Parent.Parent.Parent.Parent as Form1)?.CanvasInvalidatebyInvalidateByProperties();
        }

        /// <summary>
        /// Zdarzenie wywoływane zmianą rozmiaru kontrolki Properties. Zdarzenie to ustala nowe rozmiary dla przechowywanych kontrolek
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            _pg.Size = new Size(ClientRectangle.Size.Width - 10, 250);
            SetPropertyLabelColumnWidth(_pg, _pg.Width * 60 / 100);
            AutoScroll = true;
        }

        /// <summary>
        /// Metoda zwracająca wartość typu bool. Zadaniem jej jest porównanie nowo zaznaczonego bloku z obecnie wyświetlanym. 
        /// Jeżeli są takie same zwracana jest wartość true i kontrolka powinna zostać tylko zaktualizowana.
        /// </summary>
        /// <param name="newBlock"></param>
        /// <returns></returns>
        public bool ShouldRefresh(MyBlock newBlock)
        {
            return (newBlock == _block);
        }
    }
}
