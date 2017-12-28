using System;
using System.Drawing;
using System.Windows.Forms;
using UmlDesigner2.Class;

namespace UmlDesigner2.Components.Workspace
{
    public partial class Canvas : Panel
    {
        public static ListCanvasBlocks CanvObj = new ListCanvasBlocks(); //lista blokow wyrysowanych na ekranie
        public static ListCanvasLines CanvLines = new ListCanvasLines(); //lista blokow wyrysowanych na ekranie

        private readonly Rubbers _rubbers = new Rubbers(ref CanvObj);

        public bool IsMultiSelect { get; set; }
        private Helper.Shape _shapeToDraw = Helper.Shape.Nothing;

        private Helper.Shape ShapeToDraw
        {
            get => _shapeToDraw;
            set
            {
                _shapeToDraw = value;
                Cursor = (value == Helper.Shape.Nothing) ? Cursors.Default : Cursors.Cross;
            }
        }

        private bool _ppm = true; //czy teraz resize czu moze menu kontekstowe
        private Point _mouseDownLocation;
        public Canvas()
        {
            DoubleBuffered = true;
            BackColor = Helper.CanvasBgColor;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _rubbers.AddRubbersToControl(this);
        }
        
        protected override void OnSizeChanged(EventArgs e)
        {
            AutoScroll = false;
            AutoScrollMinSize = new Size(50000, 50000);
            AutoScroll = true;
            if (Parent != null)
            {
                Location = new Point(0, 0);
                Size = new Size(Parent.ClientRectangle.Size.Width,Parent.ClientRectangle.Height);
            }
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            var y = -AutoScrollPosition.Y + e.Delta / 5;
            AutoScrollPosition = new Point(-AutoScrollPosition.X, (y<=0)?0:y);
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            //po kliknięciu ppm bez przesówania pojawia się menu kontekstowe a także przerywane jest dodawanie elementów do canvasa
            if (e.Button == MouseButtons.Right && _ppm)
            {
                PPM_TryAbortAddingObject();
                //PPM_TryShowContextMenu(e.Location);
            }
        }
        bool _selectByRect = false; //true jeżeli mamy zaznaczać
        protected override void OnMouseDown(MouseEventArgs e)
        {
            //metoda dodająca obiekt do canvasu po LPM, zaznaczająca element jeżeli nie dodajemy obiektu. W przypadku PPM Zmienia cursor Na resize i sprawdza który obiekt bedzie resizowany.
            if (!IsMultiSelect) CanvObj.My_IsSelectedSetForAll(false);

            if (e.Button == MouseButtons.Left)
            {
                LPM_TryAddObject(e.Location);
                LPM_SelectObjectByClick(e.Location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                PPM_SelectForResizeOrContextMenu(e.Location);
            }

            _mouseDownLocation = new Point(e.Location.X+ -AutoScrollPosition.X,e.Location.Y+-AutoScrollPosition.Y);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Focus();
            //metoda przemieszczająca obiekt dla LPM i zmieniająca jego rozmiar dla PPM
            if (ShapeToDraw != Helper.Shape.ConnectionLine)
                if (e.Button == MouseButtons.Left)
                {
                    if(SelectRect==Rectangle.Empty)
                        _selectByRect = LPM_MoveObject(e.Location);
                    if (_selectByRect && !isMoved)
                        LPM_SelectObjectByRect(_mouseDownLocation,e.Location);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    PPM_ResizeObject(e.Location);
                }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (ShapeToDraw != Helper.Shape.ConnectionLine)
                Cursor = Cursors.Default;
            if (e.Button==MouseButtons.Right )
            {
                if (sizeChanged)
                {
                    UndoRedo.Push(CanvObj.ToListHistory(MyAction.EditSize));
                    sizeChanged = false;
                }
                if(UndoRedo.compareWithLastPush(CanvObj.ToListHistory(MyAction.EditSize), MyAction.EditSize))
                    UndoRedo.DeleteLast();
            }
            if (e.Button == MouseButtons.Left)
            {
                if (isMoved)
                {
                    UndoRedo.Push(CanvObj.ToListHistory(MyAction.Move));
                    isMoved = false;
                }
            }
            HideSelectionRect();
            ShowProperties();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(AutoScrollPosition.X,AutoScrollPosition.Y);
            
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < CanvLines.Count; i++)
            {
                if (CanvLines[i].BackColor == Helper.DefaultBlocksSettings[Helper.Shape.ConnectionLine].BackColor)
                    CanvLines[i].My_DrawConnectionLine(e.Graphics);
                else
                    CanvLines[i].My_DrawConnectionLineForDecisionBlock(e.Graphics);
            }

            for (int i = CanvObj.Count - 1; i >= 0; i--)
                CanvObj[i].Draw(e.Graphics);

            if (SelectRect != Rectangle.Empty)
                e.Graphics.FillRectangle(Helper.CanvasSelectionRectBrush, SelectRect);
        }
    }
}
