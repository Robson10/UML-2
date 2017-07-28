using System;
using System.Drawing;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    sealed partial class Canvas : UserControl
    {
        private readonly Clock.Clock _clock = new Clock.Clock();
        private static ListCanvasObjects _canvObj = new ListCanvasObjects(); //lista blokow wyrysowanych na ekranie
        private readonly Rubbers _rubbers = new Rubbers(ref _canvObj);

        public bool IsMultiSelect { get; set; }
        private BlocksData.Shape _shapeToDraw = CanvasVariables.defaultvalue;

        private BlocksData.Shape ShapeToDraw
        {
            get => _shapeToDraw;
            set
            {
                _shapeToDraw = value;
                Cursor = (value == BlocksData.Shape.Nothing) ? Cursors.Default : Cursors.Cross;
            }
        }

        bool ppm = true; //czy teraz resize czu moze menu kontekstowe

        private Point MouseDownLocation;

        public Canvas()
        {
            DoubleBuffered = true;
            BackColor = CanvasVariables.BgColor;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(_clock);
            ContextMenuPresets();
            _rubbers.AddRubbersToControl(this);
        }


        public void AddObjectInstant(BlocksData.Shape shape)
        {
            if (shape != BlocksData.Shape.ConnectionLine)
            {
                _canvObj.Insert(0,
                    (new MyCanvasFigure(
                        new Rectangle(Width * 10 / 100, Height * 10 / 100, BlocksData.DefaultSize.Width,
                            BlocksData.DefaultSize.Height), shape)));
                ShapeToDraw = BlocksData.Shape.Nothing;
                Invalidate();
            }
            else
                MessageBox.Show(
                    "Niestety linia nie może zostać dodana poprzez 2xLPM. Należy wybrać linie a następnie wskazać blok początkowy oraz blok końcowy dla lini");
        }

        public void AddObjectAfterClick(BlocksData.Shape shape)
        {
            ShapeToDraw = shape;
        }

        public void AbortAddingObject()
        {
            if (_canvObj[0].Rect.Size.Width == 0) _canvObj.RemoveAt(0);
            ShapeToDraw = BlocksData.Shape.Nothing;
        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            //po kliknięciu ppm bez przesówania pojawia się menu kontekstowe a także przerywane jest dodawanie elementów do canvasa
            if (e.Button == MouseButtons.Right && ppm == true)
            {
                if (ShapeToDraw != BlocksData.Shape.Nothing)
                {
                    AbortAddingObject();
                }
                if (_canvObj.IsAnyObjectContainingPoint(e.Location))
                {
                    ShowContextMenu(e);
                    _rubbers.ShowRubbers(_canvObj[0]);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //metoda dodająca obiekt do canvasu po LPM, zaznaczająca element jeżeli nie dodajemy obiektu. W przypadku PPM Zmienia cursor Na resize i sprawdza który obiekt bedzie resizowany.
            if (!IsMultiSelect) _canvObj.IsSelectedSetForAll(false);

            if (e.Button == MouseButtons.Left)
            {
                if (ShapeToDraw != BlocksData.Shape.Nothing)
                {
                    if (ShapeToDraw == BlocksData.Shape.ConnectionLine)
                    {
                        if (_canvObj[0].Shape == BlocksData.Shape.ConnectionLine &&
                            _canvObj[0].Rect.Size.Width == 0)
                        {
                            _canvObj[0].Rect.Size = new Size(e.Location);
                            ShapeToDraw = BlocksData.Shape.Nothing;
                        }
                        else
                            _canvObj.Insert(0,new MyCanvasFigure(new Rectangle(e.Location, new Size(0, 0)), ShapeToDraw));
                    }
                    else
                    {
                        _canvObj.Insert(0,(new MyCanvasFigure(new Rectangle(e.Location.X - BlocksData.DefaultSize.Width / 2,
                                    e.Location.Y - BlocksData.DefaultSize.Height / 2, BlocksData.DefaultSize.Width,BlocksData.DefaultSize.Height), ShapeToDraw)));
                        ShapeToDraw = BlocksData.Shape.Nothing;
                    }
                }
                else
                {
                    if (_canvObj.Count > 0)
                    {
                        _canvObj.SelectObjectContainingPoint(e.Location);
                        _rubbers.ShowRubbers(_canvObj[0]);
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Cursor = Cursors.SizeAll;
                _canvObj.SelectObjectContainingPoint(e.Location);
                ppm = true;
            }
            MouseDownLocation = e.Location;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //metoda przemieszczająca obiekt dla LPM i zmieniająca jego rozmiar dla PPM
            if (e.Button == MouseButtons.Left)
            {
                _canvObj.MoveSelectedObjects(ref MouseDownLocation, e.Location);
                if (_canvObj.Count > 0)
                    _rubbers.ShowRubbers(_canvObj[0]); //zawsze index 0 to to ostatni zaznaczony objekt
                MouseDownLocation = e.Location;
                Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                ppm = false;
                _canvObj.ResizeSelectedObjects(ref MouseDownLocation, e.Location);
                if (_canvObj.Count > 0)
                    _rubbers.ShowRubbers(_canvObj[0]); //zawsze index 0 to to ostatni zaznaczony objekt
                MouseDownLocation = e.Location;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if(ShapeToDraw!=BlocksData.Shape.ConnectionLine)
            Cursor = Cursors.Default;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = _canvObj.Count - 1; i >= 0; i--)
                _canvObj[i].Draw(e.Graphics);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _clock.Update();
        }
    }

    public static class CanvasVariables
    {
        public static Color BgColor = Color.Salmon;
        public static Color SelectionBgColor= Color.DarkOrange;
        public static Color DefaultBgColor = Color.Gray;
        public static BlocksData.Shape defaultvalue = BlocksData.Shape.Nothing;
        public static Keys MultiselectKey = Keys.ControlKey;
    }
}
