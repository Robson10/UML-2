using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using UmlDesigner2.Component.Workspace;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2.Component.TabsArea.BlockProp
{
    public partial class BlockProperties : UserControl
    {
        private GroupBox _grLabel, _grCode;
        private TextBox _tbLabel, _tbCode;
        private PropertyGrid _pg;
        private MyBlock _block;

        protected virtual void OnBlockPropertyChanged()
        {
            BlockPropertyChanged?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler BlockPropertyChanged;

        /// <summary>
        /// Konstruktor tworzący kontrolkę. Na podstawie MyBlock wyświetlane są wszystkie niezbędne pola dla użytkownika.
        /// </summary>
        /// <param name="block"></param>
        public BlockProperties(MyBlock block)
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
            SetPropertyLabelColumnWidth(_pg);
        }

        /// <summary>
        /// Metoda ta ma za zadanie utworzenie wszystkich niezbędnych komponentów zgodnie z wybranym blokiem
        /// oraz nadanie samej sobie niezbędnych ustawień
        /// </summary>
        private void PrepareControlView()
        {
            BackColor = Color.White;
            Location = new Point(0, 0);
            Dock = DockStyle.Fill;
            _pg = new PropertyGrid()
            {
                Location = new Point(_grCode?.Left ?? 0, _grCode?.Bottom ?? 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Size = new Size(ClientRectangle.Size.Width - 10, 220),
                PropertySort = PropertySort.NoSort,
            };
            _pg.Dock = DockStyle.Top;
            _pg.PropertyValueChanged += Pg_PropertyValueChanged;
            Controls.Add(_pg);
            if (_block.Shape == Helper.Shape.Start)
            {
                _grCode = new GroupBox()
                {
                    Text = "Variables",
                    Font = new Font("Arial", 13),
                    Size = new Size(ClientRectangle.Width - 10, 100),
                    Dock = DockStyle.Top
                };
                _tbCode = new TextBox()
                {
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Font = new Font("Arial", 12),
                    Location = new Point(5, 20),
                    Size = new Size(_grCode.ClientRectangle.Width - 10, _grCode.ClientRectangle.Height - 25),
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    Text = _block.Variables
                };
                _tbCode.KeyUp += TbCode_KeyUp;

                _grLabel = new GroupBox()
                {
                    Text = "Includes",
                    Font = new Font("Arial", 13),
                    Size = new Size(ClientRectangle.Width - 10, 70),
                    Dock = DockStyle.Top
                };
                _tbLabel = new TextBox()
                {
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Font = new Font("Arial", 12),
                    Location = new Point(5, 20),
                    Size = new Size(_grLabel.ClientRectangle.Width - 10, _grLabel.ClientRectangle.Height - 25),
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    Text = _block.Includes
                };
                _tbLabel.KeyUp += TbLabel_KeyUp;

                Controls.Add(_grCode);
                Controls.Add(_grLabel);
                _grLabel.Controls.Add(_tbLabel);
                _grCode.Controls.Add(_tbCode);
            }
            else if (_block.Shape != Helper.Shape.Start && _block.Shape != Helper.Shape.End)
            {
                _grCode = new GroupBox()
                {
                    Text = "Kod",
                    Font = new Font("Arial", 13),
                    Size = new Size(ClientRectangle.Width - 10, 100),
                    Dock = DockStyle.Top
                };
                _tbCode = new TextBox()
                {
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Font = new Font("Arial", 12),
                    Location = new Point(5, 20),
                    Size = new Size(_grCode.ClientRectangle.Width - 10, _grCode.ClientRectangle.Height - 25),
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    Text = _block.Code
                };
                _tbCode.KeyUp += TbCode_KeyUp;

                _grLabel = new GroupBox()
                {
                    Text = "Etykieta",
                    Font = new Font("Arial", 13),
                    Size = new Size(ClientRectangle.Width - 10, 70),
                    Dock = DockStyle.Top
                };
                _tbLabel = new TextBox()
                {
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Font = new Font("Arial", 12),
                    Location = new Point(5, 20),
                    Size = new Size(_grLabel.ClientRectangle.Width - 10, _grLabel.ClientRectangle.Height - 25),
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    Text = _block.Label
                };
                _tbLabel.KeyUp += TbLabel_KeyUp;

                Controls.Add(_grCode);
                Controls.Add(_grLabel);
                _grLabel.Controls.Add(_tbLabel);
                _grCode.Controls.Add(_tbCode);
            }
        }

        /// <summary>
        /// Metoda zmieniająca szerokość pierwszej kolumny w kontrolce typu PropertyGrid zgodnie z zadaną szerokością (<paramref name="width"/>)
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="width"></param>
        private void SetPropertyLabelColumnWidth(PropertyGrid grid)
        {
            var width = 130;
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
            addChangesToHistory();
            if (_block.Shape == Helper.Shape.Start)
                _block.Includes = _tbCode.Text;
            else
                _block.Label = _tbLabel.Text;
            OnBlockPropertyChanged();
            addChangesToHistory();
        }

        /// <summary>
        /// zdarzenie dla pola tekstowego gdzie przechowyway jest część kodu programu. Aktualizuje pole Code dla bloku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbCode_KeyUp(object sender, KeyEventArgs e)
        {
            addChangesToHistory();
            if (_block.Shape==Helper.Shape.Start)
                _block.Variables = _tbCode.Text;
            else
            _block.Code = _tbCode.Text;
            addChangesToHistory();
        }

        /// <summary>
        /// Zdarzenie wywołujące metodę aktualizującą canvas w chwili gdy któreś pole w kontrolce PropertyGrid ulegnie zmianie.
        /// Wywołuje odświerzenie kontrolki canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pg_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            OnBlockPropertyChanged();
        }

        /// <summary>
        /// Zdarzenie wywoływane zmianą rozmiaru kontrolki Properties. Zdarzenie to ustala nowe rozmiary dla przechowywanych kontrolek
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            AutoScroll = true;
            SetPropertyLabelColumnWidth(_pg);
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
        private void addChangesToHistory()
        {
            UndoRedo.Push(new System.Collections.Generic.List<UndoRedoItem>() { new UndoRedoItem(MyAction.Edit, 
                new MyBlock() { AutoResize = _block.AutoResize,
                        Code = _block.Code,
                        FontColor = _block.FontColor,
                        FontSize = _block.FontSize,
                        IsLocked = _block.IsLocked,
                        IsSelected = _block.IsSelected,
                        Label = _block.Label,
                        BackColor = _block.BackColor,
                        BackColorStorage = _block.BackColorStorage,
                        PointOutput1 = _block.PointOutput1,
                        PointOutput2 = _block.PointOutput2,
                        PointInput = _block.PointInput,
                        Shape = _block.Shape,
                        Rect = _block.Rect,
                        ID = _block.ID},null) });
        }
    }
}
