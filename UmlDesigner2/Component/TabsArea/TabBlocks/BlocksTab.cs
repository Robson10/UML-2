using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.TabsArea.TabBlocks
{
    public class BlocksTab : TabPage
    {
        private readonly OAKListView _listViewOfBlocks = new OAKListView();

        /// <summary>
        /// Konstruktor nadający nazwę zakładi z pola BlockData.BlockTabText a następnie dodający listę bloków (obrazek,etykieta)
        /// </summary>
        public BlocksTab()
        {
            Text = Helper.BlockTabText;
            CreateList();
        }

        /// <summary>
        /// Metoda dodająca do listy obrazki i etykiety bloków. Dodatkowo ustawia odpowiedni wygląd kontrolki
        /// </summary>
        private void CreateList()
        {
            _listViewOfBlocks.View = View.Details;
            _listViewOfBlocks.Size = new Size(Width, Height);
            _listViewOfBlocks.Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left |
                                        AnchorStyles.Right);
            _listViewOfBlocks.Columns.Add(new ColumnHeader() {Width = ClientRectangle.Width});
            _listViewOfBlocks.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            _listViewOfBlocks.HeaderStyle = ColumnHeaderStyle.None;
            _listViewOfBlocks.FullRowSelect = true;
            _listViewOfBlocks.MultiSelect = false;
            FillList();
            Controls.Add(_listViewOfBlocks);
        }

        /// <summary>
        /// Metoda dodająca do listy elementy (obrazek, etykieta)
        /// </summary>
        private void FillList()
        {
            _listViewOfBlocks.SmallImageList = LoadImageList();
            for (int i = 1; i <= Enum.GetValues(typeof(Helper.Shape)).Cast<int>().Max(); i++)
            {
                _listViewOfBlocks.Items.Add(
                    new ListViewItem(Helper.DefaultBlocksSettings[(Helper.Shape) i].Label) {ImageIndex = i - 1});
            }
        }

        /// <summary>
        /// Metoda ładująca obrazki i zwracająca listę obrazków na wyjściu. Ścieżki do obrazków pobierane są z BlokData.ImgPath 
        /// a następnie odczytywane z dysku o dpdawane do ImageList
        /// </summary>
        /// <returns></returns>
        private ImageList LoadImageList()
        {
            var imageList1 = new ImageList();
            imageList1.ImageSize = new Size(40, 40);
            imageList1.TransparentColor = Color.Transparent;
            for (int i = 1; i <= Enum.GetValues(typeof(Helper.Shape)).Cast<int>().Max(); i++)
            {
                using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream(Helper.DefaultBlocksSettings[(Helper.Shape) i].ImgPath))
                {
                    imageList1.Images.Add(Image.FromStream(stream));
                }
            }
            return imageList1;
        }

        /// <summary>
        /// Nodpisanie  zdarzenia OnResize.
        /// </summary>
        /// <param name="eventargs"></param>
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            _listViewOfBlocks.Columns[0].Width = ClientRectangle.Width - 4;
        }

        /// <summary>
        ///Dodanie EventHandlera dla wybrania elementu z listy poprzez pojedyncze kliknięcie LPM
        /// </summary>
        public event EventHandler ListItemClick
        {
            add { _listViewOfBlocks.Click += value; }
            remove { _listViewOfBlocks.Click -= value; }
        }

        /// <summary>
        ///Dodanie EventHandlera dla wybrania elementu z listy poprzez podwójne kliknięcie LPM
        /// </summary>
        public event EventHandler ListItemDoubleClick
        {
            add { _listViewOfBlocks.DoubleClick += value; }
            remove { _listViewOfBlocks.DoubleClick -= value; }
        }
    }
}
