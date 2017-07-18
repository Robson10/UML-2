using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    public partial class Workspace : UserControl
    {
        ListCanvasObjects CanvasObjects = new ListCanvasObjects();
        private BlockParameters.Shape _ShapeToDraw = BlockParameters.Shape.Nothing;
        public BlockParameters.Shape ShapeToDraw
        {
            get {return _ShapeToDraw; }
            set
            {
                _ShapeToDraw = value;
                Cursor = value== BlockParameters.Shape.Nothing ? Cursors.Arrow : Cursors.Cross;
            }
        }
        private UserControl[] Rubbers = new UserControl[8] { new UserControl(), new UserControl(), new UserControl(), new UserControl(), new UserControl(), new UserControl(), new UserControl(), new UserControl() };
        #region Rubbers
        private void RubbersPresets()
        {
            for (int i = 0; i < Rubbers.Length; i++)
            {
                Rubbers[i].BackColor = Color.Silver;
                Controls.Add(Rubbers[i]);
                Rubbers[i].Visible = false;
                Rubbers[i].Size = BlockParameters.RubberSize;
                Rubbers[i].TabIndex = i;
                Rubbers[i].MouseDown += Rubbers_MouseDown;
                Rubbers[i].MouseMove += Rubbers_MouseMove;
            }
            Rubbers[0].Cursor = Cursors.SizeNWSE;
            Rubbers[1].Cursor = Cursors.SizeNS;
            Rubbers[2].Cursor = Cursors.SizeNESW;
            Rubbers[3].Cursor = Cursors.SizeWE;
            Rubbers[4].Cursor = Cursors.SizeNWSE;
            Rubbers[5].Cursor = Cursors.SizeNS;
            Rubbers[6].Cursor = Cursors.SizeNESW;
            Rubbers[7].Cursor = Cursors.SizeWE;
        }
        private Point MouseDownLocation_Rubbers;
        /// <summary>
        /// Metoda służąca do zapisania miejsca wcisniecia LPM na 1z8 gumek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rubbers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseDownLocation_Rubbers = e.Location;
        }
        /// <summary>
        /// Metoda służąca do zmiany rozmiaru kontrolki w wyniku ciągnięcia jednej z gumek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rubbers_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CanvasObjects.ResizeSelectedObjectsByRubbers(ref MouseDownLocation_Rubbers,e.Location, (sender as Label).TabIndex);
                if (CanvasObjects.Count > 0) UpdateRubbers(CanvasObjects[0]);
                Invalidate();
            }
        }
        /// <summary>
        /// Metoda aktualizująca położenie gumek wywoływana przez event OnResize();
        /// </summary>
        protected void UpdateRubbers(MyCanvasFigure canvasObject)
        {
            //po "obróceniu" sie figury wyznaczamy inne XY dla gumek

            int left, right, up, down;
            int centerX = canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width / 2 - BlockParameters.RubberSize.Width / 2;
            int centerY = canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height / 2 - BlockParameters.RubberSize.Height / 2;
            if (canvasObject.Rect.Size.Width < 0)
            {
                left = canvasObject.Rect.Location.X;
                right = canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width - BlockParameters.RubberSize.Width;
            }
            else
            {
                left = canvasObject.Rect.Location.X - BlockParameters.RubberSize.Width;
                right = canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width;
            }
            if (canvasObject.Rect.Size.Height < 0)
            {
                up = canvasObject.Rect.Location.Y;
                down = canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height - BlockParameters.RubberSize.Height;
            }
            else
            {
                up = canvasObject.Rect.Location.Y - BlockParameters.RubberSize.Height;
                down = canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height;
            }
            Point topLeft = new Point(left, up);
            Point topCenter = new Point(centerX, up);
            Point topRight = new Point(right, up);
            Point centerLeft = new Point(left, centerY);
            Point centerRight = new Point(right, centerY);
            Point bottomLeft = new Point(left, down);
            Point bottomCenter = new Point(centerX, down);
            Point bottomRight = new Point(right, down);

            Rubbers[0].Location = topLeft;
            Rubbers[1].Location = topCenter;
            Rubbers[2].Location = topRight;
            Rubbers[3].Location = centerRight;
            Rubbers[4].Location = bottomRight;
            Rubbers[5].Location = bottomCenter;
            Rubbers[6].Location = bottomLeft;
            Rubbers[7].Location = centerLeft;
            UpdateRubberVisible(canvasObject.IsSelected);

        }
        /// <summary>
        /// Metoda aktualizująca widoczność gumek wywoływana w geterze IsSelected
        /// </summary>
        private void UpdateRubberVisible(bool isSelected)
        {
            for (int i = 0; i < Rubbers.Length; i++)
                Rubbers[i].Visible = isSelected;
        }
        #endregion

        private Clock.Clock clock = new Clock.Clock();
        public Workspace()
        {
            InitializeComponent();
            DoubleBuffered = true;
            RubbersPresets();
            this.Controls.Add(clock);
        }
        
        public void ResizeAll( Size clientSize)
        {
            Size = clientSize;
        }
        //lewy utworzenie controlki, zaznaczenie, ppm menu kontekstowe

        Keys _IsMultiSelect = Keys.None;
        public Keys IsMultiSelect
        {
            get { return _IsMultiSelect; }
            set
            {
                _IsMultiSelect = value;
                if (value == Keys.Escape)
                {
                    ShapeToDraw = BlockParameters.Shape.Nothing;
                    CanvasObjects.IsSelectedSetValueForAll(false);
                }

            }
        }
        protected override void OnKeyDown( KeyEventArgs e)
        {
            IsMultiSelect = e.KeyCode;
        }
        protected override void OnKeyUp( KeyEventArgs e)
        {
            IsMultiSelect = Keys.None;
        }

        bool ppm = true;//czy teraz resize czu moze menu kontekstowe
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (IsMultiSelect != BlockParameters.MultiselectKey )CanvasObjects.IsSelectedSetValueForAll(false);

            if (e.Button == MouseButtons.Left)
            {
                if (ShapeToDraw != BlockParameters.Shape.Nothing)
                {
                    CanvasObjects.Add(new MyCanvasFigure(new Rectangle(e.Location.X - BlockParameters.defaultCanvasControlSize.Width / 2, e.Location.Y - BlockParameters.defaultCanvasControlSize.Height / 2, BlockParameters.defaultCanvasControlSize.Width, BlockParameters.defaultCanvasControlSize.Height),ShapeToDraw));
                    ShapeToDraw = BlockParameters.Shape.Nothing;
                }
                else
                {
                    if (CanvasObjects.Count > 0)
                    {
                        CanvasObjects.SelectObjectContainingPoint(e.Location);
                        UpdateRubbers(CanvasObjects[0]);//zawsze index 0 to to ostatni zaznaczony objekt
                    }
                }
            }
            else if (e.Button == MouseButtons.Right&& ppm==true)
            {
                if (CanvasObjects.IsAnyObjectContainingPoint(e.Location))
                        MessageBox.Show("menu kontekstowe poszło i do tej pory nie wrociło");
            }
            Invalidate();
        }
        private Point MouseDownLocation;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (IsMultiSelect != Keys.ControlKey) CanvasObjects.IsSelectedSetValueForAll(false);

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
                if (CanvasObjects.Count > 0) UpdateRubbers(CanvasObjects[0]);//zawsze index 0 to to ostatni zaznaczony objekt
                MouseDownLocation = e.Location;
                Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                ppm = false;
                CanvasObjects.ResizeSelectedObjects(ref MouseDownLocation, e.Location);
                if (CanvasObjects.Count > 0) UpdateRubbers(CanvasObjects[0]);//zawsze index 0 to to ostatni zaznaczony objekt
                MouseDownLocation = e.Location;
                Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = CanvasObjects.Count-1; i >=0 ; i--)
                CanvasObjects[i].Draw( e.Graphics);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

             clock.Size = clock.Size;
        }
    }
   
}
