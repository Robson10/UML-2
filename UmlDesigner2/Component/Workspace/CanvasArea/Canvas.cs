using System;
using System.Drawing;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    sealed partial class Canvas : UserControl
    {
        private readonly Clock.Clock _clock = new Clock.Clock();
        private static ListCanvasBlocks _canvObj = new ListCanvasBlocks(); //lista blokow wyrysowanych na ekranie
        private static readonly ListCanvasLines _canvLines = new ListCanvasLines()
            ; //lista blokow wyrysowanych na ekranie

        private readonly Rubbers _rubbers = new Rubbers(ref _canvObj);

        public bool IsMultiSelect { get; set; }
        private MyDictionary.Shape _shapeToDraw = MyDictionary.Shape.Nothing;

        private MyDictionary.Shape ShapeToDraw
        {
            get => _shapeToDraw;
            set
            {
                _shapeToDraw = value;
                Cursor = (value == MyDictionary.Shape.Nothing) ? Cursors.Default : Cursors.Cross;
            }
        }

        private bool _ppm = true; //czy teraz resize czu moze menu kontekstowe
        private Point _mouseDownLocation;

        public Canvas()
        {
            DoubleBuffered = true;
            BackColor = CanvasVariables.BgColor;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(_clock);
            ContextMenuPresets();
            _rubbers.AddRubbersToControl(this);
            
        }
        
        protected override void OnMouseClick(MouseEventArgs e)
        {
            //po kliknięciu ppm bez przesówania pojawia się menu kontekstowe a także przerywane jest dodawanie elementów do canvasa
            if (e.Button == MouseButtons.Right && _ppm == true)
            {
                PPM_TryAbortAddingObject();
                PPM_TryShowContextMenu(e.Location);
            }
        }
        bool _selectByRect = false; //true jeżeli mamy zaznaczać
        protected override void OnMouseDown(MouseEventArgs e)
        {
            //metoda dodająca obiekt do canvasu po LPM, zaznaczająca element jeżeli nie dodajemy obiektu. W przypadku PPM Zmienia cursor Na resize i sprawdza który obiekt bedzie resizowany.
            if (!IsMultiSelect) _canvObj.My_IsSelectedSetForAll(false);

            if (e.Button == MouseButtons.Left)
            {
                LPM_TryAddObject(e.Location);
                LPM_SelectObjectByClick(e.Location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                PPM_SelectForResizinrOrContextMenu(e.Location);
            }
            _mouseDownLocation = e.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Focus();
            //metoda przemieszczająca obiekt dla LPM i zmieniająca jego rozmiar dla PPM
            if (ShapeToDraw != MyDictionary.Shape.ConnectionLine)
                if (e.Button == MouseButtons.Left)
                {
                    if(SelectRect==Rectangle.Empty)
                    _selectByRect = LPM_MoveObject(e.Location);
                    if (_selectByRect)
                        LPM_SelectObjectByRect(_mouseDownLocation,e.Location);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    PPM_ResizeObject(e.Location);
                }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (ShapeToDraw != MyDictionary.Shape.ConnectionLine)
                Cursor = Cursors.Default;
            HideSelectionRect();

            ShowProperties();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < _canvLines.Count; i++)
            {
                if (_canvLines[i].BackColor == MyDictionary.DefaultBackColor(MyDictionary.Shape.ConnectionLine))
                    _canvLines[i].My_DrawConnectionLine(e.Graphics);
                else
                    _canvLines[i].My_DrawConnectionLineForDecisionBlock(e.Graphics);
            }

            for (int i = _canvObj.Count - 1; i >= 0; i--)
                _canvObj[i].Draw(e.Graphics);

            if (SelectRect != Rectangle.Empty)
                e.Graphics.FillRectangle(CanvasVariables.SelectionRectBrush, SelectRect);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _clock.Update();
        }
    }

    public static class CanvasVariables
    {
        public static Color BgColor = Color.White;
        public static Color SelectionBgColor = Color.DarkOrange;
        public static Color DefaultBgColor = Color.Gray;
        public static Keys MultiselectKey = Keys.ControlKey;
        public static SolidBrush SelectionRectBrush = new SolidBrush(Color.FromArgb(70, Color.Blue));

        public static StringFormat BlockStringFormat = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
    }
}
