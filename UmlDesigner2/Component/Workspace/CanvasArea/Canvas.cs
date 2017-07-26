using System;
using System.Drawing;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    class Canvas : UserControl
    {
        private readonly Clock.Clock _clock = new Clock.Clock();
        private static ListCanvasObjects CanvasObjects = new ListCanvasObjects();//lista blokow wyrysowanych na ekranie
        private readonly Rubbers _rubbers = new Rubbers(ref CanvasObjects);

        public bool IsMultiSelect { get; set; }
        private BlocksData.Shape _shapeToDraw = CanvasVariables.defaultvalue;
        private BlocksData.Shape ShapeToDraw
        {
            get { return _shapeToDraw; }
            set
            {
                _shapeToDraw = value;
                Cursor = (value == BlocksData.Shape.Nothing) ? Cursors.Default : Cursors.Cross;
            }
        }
        bool ppm = true;//czy teraz resize czu moze menu kontekstowe

        public Canvas()
        {
            DoubleBuffered = true;
            BackColor = CanvasVariables.BgColor;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(_clock);
            _rubbers.AddRubbersToControl(this);
        }

        public void AddObjectInstant(BlocksData.Shape shape)
        {
            CanvasObjects.Insert(0,(new MyCanvasFigure(new Rectangle(Width*10/100, Height*10/100, BlocksData.defaultCanvasControlSize.Width, BlocksData.defaultCanvasControlSize.Height),shape)));
            ShapeToDraw = BlocksData.Shape.Nothing;
            Invalidate();
        }

        public void AddObjectAfterClick(BlocksData.Shape shape)
        {
            ShapeToDraw = shape;
        }

        public void AbortAddingObject()
        {
            ShapeToDraw = BlocksData.Shape.Nothing;
        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            //po kliknięciu ppm bez przesówania pojawia się menu kontekstowe a także przerywane jest dodawanie elementów do canvasa
            if (e.Button == MouseButtons.Right && ppm == true)
            {
                if (CanvasObjects.IsAnyObjectContainingPoint(e.Location))
                    ShowContextMenu();
                AbortAddingObject();
                Invalidate();
            }
        }

        private void ShowContextMenu()
        {
            MessageBox.Show("MenuKontekstowe");
        }



        private Point MouseDownLocation;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            //metoda dodająca obiekt do canvasu po LPM, zaznaczająca element jeżeli nie dodajemy obiektu. W przypadku PPM Zmienia cursor Na resize i sprawdza który obiekt bedzie resizowany.
            if (!IsMultiSelect) CanvasObjects.IsSelectedSetForAll(false);

            if (e.Button == MouseButtons.Left)
            {
                if (ShapeToDraw != BlocksData.Shape.Nothing)
                {
                    CanvasObjects.Insert(0,(new MyCanvasFigure(new Rectangle(e.Location.X - BlocksData.defaultCanvasControlSize.Width / 2, e.Location.Y - BlocksData.defaultCanvasControlSize.Height / 2, BlocksData.defaultCanvasControlSize.Width, BlocksData.defaultCanvasControlSize.Height), ShapeToDraw)));
                    ShapeToDraw = BlocksData.Shape.Nothing;
                }
                else
                {
                    if (CanvasObjects.Count > 0)
                    {
                        CanvasObjects.SelectObjectContainingPoint(e.Location);
                        _rubbers.ShowRubbers(CanvasObjects[0]);
                    }
                }
            }
            else if (e.Button==MouseButtons.Right)
            {
                Cursor = Cursors.SizeAll;
                CanvasObjects.SelectObjectContainingPoint(e.Location);
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
                CanvasObjects.MoveSelectedObjects(ref MouseDownLocation, e.Location);
                if (CanvasObjects.Count > 0) _rubbers.ShowRubbers(CanvasObjects[0]);//zawsze index 0 to to ostatni zaznaczony objekt
                MouseDownLocation = e.Location;
                Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                ppm = false;
                CanvasObjects.ResizeSelectedObjects(ref MouseDownLocation, e.Location);
                if (CanvasObjects.Count > 0) _rubbers.ShowRubbers(CanvasObjects[0]);//zawsze index 0 to to ostatni zaznaczony objekt
                MouseDownLocation = e.Location;
                Invalidate();
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Cursor = Cursors.Default;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = CanvasObjects.Count - 1; i >= 0; i--)
                CanvasObjects[i].Draw(e.Graphics);
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
