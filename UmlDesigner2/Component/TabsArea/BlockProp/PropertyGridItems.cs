using System.ComponentModel;
using System.Drawing;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2.Component.TabsArea.BlockPropertis
{
    class PropertyGridItems
    {
        private MyBlock _block;

        /// <summary>
        /// Konstruktor tworzący uchwyt do zmiennej typu MyBlock (ref)
        /// </summary>
        /// <param name="block"></param>
        public PropertyGridItems(MyBlock block)
        {
            _block = null;
            _block = block;
        }

        /// <summary>
        /// Property do pola IsLocked
        /// </summary>
        [Category("Parametry")]
        [Description("Zablokowane bloku przed jego edycją")]
        [DisplayName("Zablokowane")]
        public bool IsLocked
        {
            get { return _block.IsLocked; }
            set{_block.IsLocked = value;}
        }

        /// <summary>
        /// Property do pola AutoResize
        /// </summary>
        [Category("Parametry")]
        [Description("Auto dopasowywanie rozmiaru bloku do czcionki i tekstu")]
        [DisplayName("Auto dopasowywanie")]
        public bool AutoResize
        {
            get { return _block.AutoResize; }
            set
            {
                if (!_block.IsLocked)
                    _block.AutoResize = value;
            }
        }

        /// <summary>
        /// Property do pola FontSize
        /// </summary>
        [Category("Parametry")]
        [Description("Rozmiar Czcionki")]
        [DisplayName("Rozmiar Czcionki")]
        public int FontSize
        {
            get { return _block.FontSize; }
            set
            {
                if (!_block.IsLocked)
                    _block.FontSize = value;
            }
        }

        /// <summary>
        /// Property do pola FontColor
        /// </summary>
        [Category("Parametry")]
        [Description("Kolor czcionki")]
        [DisplayName("Kolor czcionki")]
        public Color FontColor
        {
            get { return _block.FontColor; }
            set
            {
                if (!_block.IsLocked)
                    _block.FontColor = value;
            }
        }

        /// <summary>
        /// Property do pola BackColorStorage
        /// </summary>
        [Category("Parametry")]
        [Description("Kolor tła")]
        [DisplayName("Kolor tła")]
        public Color BackColor
        {
            get { return _block.BackColorStorage; }
            set
            {
                if (!_block.IsLocked)
                    _block.BackColorStorage = value;
            }
        }

        /// <summary>
        /// Property do pola Rect.Location
        /// </summary>
        [Category("Parametry")]
        [Description("Położenie")]
        [DisplayName("Położenie")]
        public Point Location
        {
            get { return _block.Rect.Location; }
            set
            {
                if (!_block.IsLocked)
                    _block.Rect = new Rectangle(value, _block.Rect.Size);
            }
        }

        /// <summary>
        /// Property do pola Rect.Size
        /// </summary>
        [Category("Parametry")]
        [Description("Wymiary")]
        [DisplayName("Wymiary")]
        public Size Size
        {
            get { return _block.Rect.Size; }
            set
            {
                if (!_block.IsLocked)
                    _block.Rect = new Rectangle(_block.Rect.Location, value);
            }
        }
    }
}
