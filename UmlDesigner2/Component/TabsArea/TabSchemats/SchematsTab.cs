using System;
using System.Drawing;
using System.Windows.Forms;
using UmlDesigner2.Component.TabsArea.TabBlocks;

namespace UmlDesigner2.Component.TabsArea.TabSchemats
{
    public class SchematsTab : TabPage
    {
        private readonly ListView _listViewOfSchemats = new ListView();

        /// <summary>
        /// Konstruktor nadający nazwę zakładi z pola BlockData.BlockTabText a następnie dodający listę schematów (etykiety)
        /// </summary>
        public SchematsTab()
        {
            Text = MyDictionary.SchematsTabText;
            CreateList();
        }

        /// <summary>
        /// Metoda dodająca do listy nazwy schematów. Dodatkowo ustawia odpowiedni wygląd kontrolki
        /// </summary>
        private void CreateList()
        {
            _listViewOfSchemats.View = View.Details;
            _listViewOfSchemats.Size = new Size(Width, Height);
            _listViewOfSchemats.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left |
                                        AnchorStyles.Right);
            _listViewOfSchemats.OwnerDraw = true;
            _listViewOfSchemats.DrawItem += _listViewOfSchemats_DrawItem;
            _listViewOfSchemats.Columns.Add(new ColumnHeader() {Width = ClientRectangle.Width});
            _listViewOfSchemats.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            _listViewOfSchemats.HeaderStyle = ColumnHeaderStyle.None;
            _listViewOfSchemats.FullRowSelect = true;
            _listViewOfSchemats.MultiSelect = false;
            LoadElementsToList();
            this.Controls.Add(_listViewOfSchemats);
        }

        /// <summary>
        /// Metoda pozwalająca na edycję wyglądu etykiet - ich wyrównania oraz koloru czcionki czy tła
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _listViewOfSchemats_DrawItem(object sender, DrawListViewItemEventArgs e)
        {

            TextFormatFlags flags;
            if (e.Item.Index == (sender as ListView).Items.Count - 1)
            {
                e.DrawFocusRectangle();
                flags = TextFormatFlags.HorizontalCenter;
            }
            else
            {
                if (e.Item.Selected)
                {
                    e.Item.BackColor = Color.Orange;
                    e.DrawBackground();
                }
                 flags= TextFormatFlags.Left;
            }
            e.DrawText(flags);
        }

        /// <summary>
        /// Metoda sprawdzająca czy folder schematów istnieje. Jeśli nie tworzy go.
        /// Następnie wczytuje wszystko ze ścieżki podanej w MyDictionary.SchematsPath o określonym 
        /// rozszerzeniu MyDictionary.SchematsExtension i dodaje do listy
        /// </summary>
        private void LoadElementsToList()
        {
            if (!System.IO.Directory.Exists(MyDictionary.SchematsPath))
                System.IO.Directory.CreateDirectory(MyDictionary.SchematsPath);
            
            var filesPaths = System.IO.Directory.GetFiles(MyDictionary.SchematsPath, "*"+ MyDictionary.SchematsExtension);

            for (int i = 0; i < filesPaths.Length; i++)
                _listViewOfSchemats.Items.Add(new ListViewItem(filesPaths[i].Replace(MyDictionary.SchematsPath + @"\","").Replace(MyDictionary.SchematsExtension, "")));
            
            _listViewOfSchemats.Items.Add(new ListViewItem("Importuj"){ForeColor=Color.Blue});
            _listViewOfSchemats.RedrawItems(0, 3, true);
            _listViewOfSchemats.Invalidate();
        }

        /// <summary>
        /// Zdarzenie wywoływane zmianą rozmiaru kontrolki. ustawia szerokość pierwszej kolumny zgodnie z maksymalnym dostępnym obszarem
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            _listViewOfSchemats.Columns[0].Width = Width;
        }

        /// <summary>
        ///Dodanie EventHandlera dla wybrania elementu z listy poprzez pojedyncze kliknięcie LPM
        /// </summary>
        public event EventHandler ListItemClick
        {
            add { _listViewOfSchemats.Click += value; }
            remove { _listViewOfSchemats.Click -= value; }
        }

        /// <summary>
        ///Dodanie EventHandlera dla wybrania elementu z listy poprzez podwójne kliknięcie LPM
        /// </summary>
        public event EventHandler ListItemDoubleClick
        {
            add { _listViewOfSchemats.DoubleClick += value; }
            remove { _listViewOfSchemats.DoubleClick -= value; }
        }
    }
}