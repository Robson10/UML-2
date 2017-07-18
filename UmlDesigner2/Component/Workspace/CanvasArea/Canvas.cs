using System;
using System.Drawing;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    class Canvas : UserControl
    {
        private readonly Clock.Clock _clock = new Clock.Clock();
        private readonly Rubbers _rubbers = new Rubbers();

        public Canvas()
        {
            DoubleBuffered = true;
            BackColor = CanvasVariables.BgColor;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            //   RubbersPresets();
            Controls.Add(_clock);
            _rubbers.AddRubbersToControl(this);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _clock.Update();
        }

        private BlockParameters.Shape _shapeToDraw = CanvasVariables.defaultvalue;
        public BlockParameters.Shape ShapeToDraw
        {
            get { return _shapeToDraw; }
            set
            {
                _shapeToDraw = value;
                Cursor = value == BlockParameters.Shape.Nothing ? Cursors.Arrow : Cursors.Cross;
            }
        }
        ListCanvasObjects CanvasObjects = new ListCanvasObjects();


        bool ppm = true;//czy teraz resize czu moze menu kontekstowe
        protected override void OnMouseClick(MouseEventArgs e)
        {
            //if (IsMultiSelect != BlockParameters.MultiselectKey) CanvasObjects.IsSelectedSetValueForAll(false);

            if (e.Button == MouseButtons.Left)
            {
                if (ShapeToDraw != BlockParameters.Shape.Nothing)
                {
                    CanvasObjects.Add(new MyCanvasFigure(new Rectangle(e.Location.X - BlockParameters.defaultCanvasControlSize.Width / 2, e.Location.Y - BlockParameters.defaultCanvasControlSize.Height / 2, BlockParameters.defaultCanvasControlSize.Width, BlockParameters.defaultCanvasControlSize.Height), ShapeToDraw));
                    //ShapeToDraw = BlockParameters.Shape.Nothing;
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
            else if (e.Button == MouseButtons.Right && ppm == true)
            {
                if (CanvasObjects.IsAnyObjectContainingPoint(e.Location))
                    MessageBox.Show("menu kontekstowe poszło i do tej pory nie wrociło");
                ShapeToDraw = BlockParameters.Shape.Nothing;
            }
            Invalidate();
        }
        public bool IsMultiSelect
        {
            get { return (_activeKey == CanvasVariables.MultiselectKey); }
        }

        Keys _activeKey = Keys.None;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            _activeKey = e.KeyCode;
            if (_activeKey == Keys.Escape)
            {
                ShapeToDraw = BlockParameters.Shape.Nothing;
                CanvasObjects.IsSelectedSetValueForAll(false);
            }
            MessageBox.Show("e");
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            _activeKey = Keys.None;
        }

        private Point MouseDownLocation;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!IsMultiSelect) CanvasObjects.IsSelectedSetValueForAll(false);

            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                CanvasObjects.SelectObjectContainingPoint(e.Location);
                ppm = true;
            }
            MouseDownLocation = e.Location;
            Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
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
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = CanvasObjects.Count - 1; i >= 0; i--)
                CanvasObjects[i].Draw(e.Graphics);
        }

    }

    public static class CanvasVariables
    {
        public static Color BgColor = Color.Salmon;
        public static Color SelectionBgColor= Color.DarkOrange;
        public static Color DefaultBgColor = Color.Gray;
        public static BlockParameters.Shape defaultvalue = BlockParameters.Shape.Start;
        public static Keys MultiselectKey = Keys.ControlKey;
    }
}
