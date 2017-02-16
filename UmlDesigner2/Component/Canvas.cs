using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component
{
    public partial class Canvas : UserControl
    {
        ListCanvasObject CanvasObjects = new ListCanvasObject();
        private MyDictionary.Shape _ShapeToDraw = MyDictionary.Shape.Nothing;
        public MyDictionary.Shape ShapeToDraw
        {
            get {return _ShapeToDraw; }
            set
            {
                _ShapeToDraw = value;
                if(value==MyDictionary.Shape.Nothing)
                {
                    Cursor = Cursors.Arrow;
                }
                else
                {
                    Cursor = Cursors.Cross;
                }
            }
        }

        private Label[] Rubbers = new Label[8] { new Label(), new Label(), new Label(), new Label(), new Label(), new Label(), new Label(), new Label() };
        private void RubbersPresets()
        {
            for (int i = 0; i < Rubbers.Length; i++)
            {
                Rubbers[i].BackColor = Color.Silver;
                Controls.Add(Rubbers[i]);
                Rubbers[i].Visible = false;
                Rubbers[i].Size = MyDictionary.RubberSize;
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
                //if ((sender as Label).TabIndex == 0) { Left = e.X + Left - MouseDownLocation_Rubbers.X; Width -= e.X - MouseDownLocation_Rubbers.X; Top = e.Y + Top - MouseDownLocation_Rubbers.Y; Height -= e.Y - MouseDownLocation_Rubbers.Y; }
                //else if ((sender as Label).TabIndex == 1) { Top = e.Y + Top - MouseDownLocation_Rubbers.Y; Height -= e.Y - MouseDownLocation_Rubbers.Y; }
                //else if ((sender as Label).TabIndex == 2) { Top = e.Y + Top - MouseDownLocation_Rubbers.Y; Height -= e.Y - MouseDownLocation_Rubbers.Y; Width += e.X; }
                //else if ((sender as Label).TabIndex == 3) { Width += e.X; }
                //else if ((sender as Label).TabIndex == 4) { Width += e.X; Height += e.Y; }
                //else if ((sender as Label).TabIndex == 5) { Height += e.Y; }
                //else if ((sender as Label).TabIndex == 6) { Left = e.X + Left - MouseDownLocation_Rubbers.X; Width -= e.X - MouseDownLocation_Rubbers.X; Height += e.Y; }
                //else if ((sender as Label).TabIndex == 7) { Left = e.X + Left - MouseDownLocation_Rubbers.X; Width -= e.X - MouseDownLocation_Rubbers.X; }
                CanvasObjects.ResizeSelectedObjectsByRubbers(ref MouseDownLocation_Rubbers,e.Location, (sender as Label).TabIndex);
                if (CanvasObjects.Count > 0) UpdateRubbers(CanvasObjects[0]);
                Invalidate();
            }
        }
        /// <summary>
        /// Metoda aktualizująca położenie gumek wywoływana przez event OnResize();
        /// </summary>
        protected void UpdateRubbers(MyCanvasObject canvasObject)
        {

                int left = canvasObject.Rect.Location.X;
                int centerX = canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width / 2;
                int right = canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width;
                int up = canvasObject.Rect.Location.Y;
                int centerY = canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height / 2;
                int down = canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height;

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

        public Canvas()
        {
            InitializeComponent();
            DoubleBuffered = true;
            RubbersPresets();
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
                    ShapeToDraw = MyDictionary.Shape.Nothing;
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
            if (IsMultiSelect != Keys.ControlKey) CanvasObjects.IsSelectedSetValueForAll(false);

            if (e.Button == MouseButtons.Left)
            {
                if (ShapeToDraw != MyDictionary.Shape.Nothing)
                {
                    CanvasObjects.Add(new MyCanvasObject(new Rectangle(e.Location.X - MyDictionary.defaultCanvasControlSize.Width / 2, e.Location.Y - MyDictionary.defaultCanvasControlSize.Height / 2, MyDictionary.defaultCanvasControlSize.Width, MyDictionary.defaultCanvasControlSize.Height),ShapeToDraw));
                    ShapeToDraw = MyDictionary.Shape.Nothing;
                }
                else
                {
                    CanvasObjects.SelectObjectContainingPoint(e.Location);
                    UpdateRubbers(CanvasObjects[0]);//zawsze index 0 to to ostatni zaznaczony objekt

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
        //http://stackoverflow.com/questions/15981840/how-to-draw-and-move-shapes-using-mouse-in-c-sharp
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
    }
    public class ListCanvasObject : List<MyCanvasObject>
    {

        public void SelectObjectContainingPoint(Point location)
        {
            for (int i = 0; i < base.Count; i++)
                if (base[i].IsContain(location))
                {
                    base[i].IsSelected = true;
                    base[i].BackColor = (base[i].IsSelected) ? new SolidBrush(Color.Yellow) : new SolidBrush(Color.Gray);
                    base.Insert(0, base[i]);
                    base.RemoveAt(i + 1);
                    break;
                }
        }
        public bool IsAnyObjectContainingPoint(Point location)
        {
            for (int i = 0; i < base.Count; i++)
                if (base[i].IsContain(location))
                    return true;
            return false;
        }
        public void MoveSelectedObjects(ref Point MouseDownLocation, Point e)
        {
            for (int i = 0; i < base.Count; i++)
                if (base[i].IsSelected && !base[i].IsLocked)
                {
                    base[i].Rect.Location = new Point((e.X - MouseDownLocation.X) + base[i].Rect.Left, (e.Y - MouseDownLocation.Y) + base[i].Rect.Top);
                }
        }
        public void ResizeSelectedObjects(ref Point MouseDownLocation, Point e)
        {
            for (int i = 0; i < base.Count; i++)
            {
                if (base[i].IsSelected && !base[i].IsLocked)
                {
                    base[i].Rect.Width = e.X - base[i].Rect.X + (base[i].Rect.X + base[i].Rect.Width - MouseDownLocation.X);
                    base[i].Rect.Height = e.Y - base[i].Rect.Y + (base[i].Rect.Y + base[i].Rect.Height - MouseDownLocation.Y);
                }
            }
        }
        public void ResizeSelectedObjectsByRubbers(ref Point MouseDownLocation, Point e,int RubberID)
        {
            //zaradzenie na obroconą kontrolkę
            switch(RubberID)
            {
                case 0:
                    for (int i = 0; i < base.Count; i++)
                        if (base[i].IsSelected && !base[i].IsLocked)
                        {
                            base[i].Rect.Location = new Point( e.X + base[i].Rect.Location.X - MouseDownLocation.X, e.Y + base[i].Rect.Location.Y - MouseDownLocation.Y);
                            base[i].Rect.Width -= e.X - MouseDownLocation.X;
                            base[i].Rect.Height -= e.Y - MouseDownLocation.Y;
                        }
                    break;
                case 1:
                    for (int i = 0; i < base.Count; i++)
                        if (base[i].IsSelected && !base[i].IsLocked)
                        {
                            base[i].Rect.Location= new Point(base[i].Rect.Location.X, e.Y + base[i].Rect.Location.Y - MouseDownLocation.Y);
                            base[i].Rect.Height -= e.Y - MouseDownLocation.Y;
                        }
                    break;
                case 2:
                    for (int i = 0; i < base.Count; i++)
                        if (base[i].IsSelected && !base[i].IsLocked)
                        {
                            base[i].Rect.Location = new Point(base[i].Rect.Location.X,e.Y + base[i].Rect.Location.Y - MouseDownLocation.Y);
                            base[i].Rect.Height -= e.Y - MouseDownLocation.Y;
                            base[i].Rect.Width += e.X;
                        }
                    break;
                case 3:
                    for (int i = 0; i < base.Count; i++)
                        if (base[i].IsSelected && !base[i].IsLocked)
                            base[i].Rect.Width = e.X - base[i].Rect.X + (base[i].Rect.X + base[i].Rect.Width - MouseDownLocation.X);
                    break;
                case 4:
                    for (int i = 0; i < base.Count; i++)
                        if (base[i].IsSelected && !base[i].IsLocked)
                        {
                            base[i].Rect.Width = e.X - base[i].Rect.X + (base[i].Rect.X + base[i].Rect.Width - MouseDownLocation.X);
                            base[i].Rect.Height = e.Y - base[i].Rect.Y + (base[i].Rect.Y + base[i].Rect.Height - MouseDownLocation.Y);
                        }
                    break;
                case 5: 
                    for (int i = 0; i < base.Count; i++)
                        if (base[i].IsSelected && !base[i].IsLocked)
                            base[i].Rect.Height = e.Y - base[i].Rect.Y + (base[i].Rect.Y + base[i].Rect.Height - MouseDownLocation.Y);
                    break;
                case 6:
                    for (int i = 0; i < base.Count; i++)
                        if (base[i].IsSelected && !base[i].IsLocked)
                        {
                            base[i].Rect.Location = new Point(e.X + base[i].Rect.Location.X - MouseDownLocation.X, base[i].Rect.Location.Y);
                            base[i].Rect.Width -= e.X - MouseDownLocation.X;
                            base[i].Rect.Height += e.Y;
                        }
                    break;
                case 7:
                    for (int i = 0; i < base.Count; i++)
                        if (base[i].IsSelected && !base[i].IsLocked)
                        {
                            base[i].Rect.Location = new Point(e.X + base[i].Rect.Location.X - MouseDownLocation.X, base[i].Rect.Location.Y);
                            base[i].Rect.Width -= e.X - MouseDownLocation.X;
                        }
                    break;
            }
            
            //for (int i = 0; i < base.Count; i++)
            //{
            //    if (base[i].IsSelected && !base[i].IsLocked)
            //    {
            //        base[i].Rect.Width = e.X - base[i].Rect.X + (base[i].Rect.X + base[i].Rect.Width - MouseDownLocation.X);
            //        base[i].Rect.Height = e.Y - base[i].Rect.Y + (base[i].Rect.Y + base[i].Rect.Height - MouseDownLocation.Y);
            //    }
            //}
        }

        public void IsSelectedSetValueForAll(bool isSelected)
        {
            for (int i = 0; i < base.Count; i++)
            {
                base[i].BackColor = (isSelected) ? new SolidBrush(Color.Yellow) : new SolidBrush(Color.Gray);//-----------------------------------
                base[i].IsSelected = isSelected;
            }
        }
    }

    public class MyCanvasObject
    {
        public MyCanvasObject(Rectangle rect,MyDictionary.Shape shape)
        {
            Rect = rect;
            Shape = shape;
            Text = MyDictionary.CanvasObjectText(Shape);
        }



        public int FontSize;
        public string Text; //zawartosc tekstowa kontrolki
        public bool IsSelected=false;//czy jest zaznaczona
        public bool IsLocked = false;
        public uint ID;
        public int In, Out, Out2;

        #region Done
        public Rectangle Rect;//obszar dla figury - point , size
        private MyDictionary.Shape _Shape;
        public MyDictionary.Shape Shape//jaką kontrolke rysujemy
        {
            get { return _Shape; }
            set
            {
                _Shape = value;
                BackColor = MyDictionary.CanvasObjectBackColor(_Shape);
                FontColor = MyDictionary.CanvasObjectFontColor(_Shape);
                FontSize = MyDictionary.CanvasObjectFontSize(_Shape);

            }
        }
        public SolidBrush BackColor;
        public Color FontColor;

        //http://stackoverflow.com/questions/34582234/how-to-detect-if-mouse-has-clicked-inside-of-a-certain-shape-in-c-sharp-on-winfo
        public bool IsContain(Point location)
        {
            var contains = false;
            if (Shape==MyDictionary.Shape.Start)
            { 
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddEllipse(Rect);
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == MyDictionary.Shape.End)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddEllipse(Rect);
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == MyDictionary.Shape.Execution)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddRectangle(Rect);
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == MyDictionary.Shape.Input)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddLine(new Point(Rect.Location.X, Rect.Location.Y + Rect.Height), new Point(Rect.Location.X + 10, Rect.Location.Y));
                    gp.AddLine(new Point(Rect.Location.X + Rect.Width, Rect.Location.Y), new Point(Rect.Location.X + Rect.Width - 10, Rect.Location.Y + Rect.Height));
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == MyDictionary.Shape.Decision)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddLine(new Point(Rect.Location.X, Rect.Location.Y + Rect.Height / 2), new Point(Rect.Location.X + Rect.Width / 2, Rect.Location.Y));
                    gp.AddLine(new Point(Rect.Location.X + Rect.Width, Rect.Location.Y + Rect.Height / 2), new Point(Rect.Location.X + Rect.Width / 2, Rect.Location.Y + Rect.Height));
                    contains = gp.IsVisible(location);
                }
            }
            return contains;
        }

        #region DrawBlock with text
        public void Draw( Graphics g)
        {
            switch(Shape)
            {
                case (MyDictionary.Shape.Start):DrawStart(g);
                    break;
                case (MyDictionary.Shape.End):DrawEnd(g);
                    break;
                case (MyDictionary.Shape.Input):DrawInput(g);
                    break;
                case (MyDictionary.Shape.Execution):DrawExecution(g);
                    break;
                case (MyDictionary.Shape.Decision):DrawDecision(g);
                    break;
                default:
                    MessageBox.Show("nie znalazłem odpowiedniego kształtu i nwm co teraz mam narysować ;(");
                    break;
            }
        }
        private void DrawStart(Graphics g)
        {
            g.FillEllipse(BackColor, Rect);
            DrawText(g);
        }
        private void DrawEnd(Graphics g)
        {
            g.FillEllipse(BackColor, Rect);
            DrawText(g);
        }
        private void DrawInput(Graphics g)
        {
            var x = new System.Drawing.Drawing2D.GraphicsPath();
            x.AddLine(new Point(Rect.Location.X, Rect.Location.Y+Rect.Height), new Point(Rect.Location.X+10, Rect.Location.Y));
            x.AddLine(new Point(Rect.Location.X+Rect.Width, Rect.Location.Y), new Point(Rect.Location.X+Rect.Width - 10, Rect.Location.Y+Rect.Height));
            g.FillPath(BackColor,x );
        }
        private void DrawExecution(Graphics g)
        {
            g.FillRectangle(BackColor, Rect);
        }
        private void DrawDecision(Graphics g)
        {

            var x = new System.Drawing.Drawing2D.GraphicsPath();
            x.AddLine(new Point(Rect.Location.X, Rect.Location.Y + Rect.Height / 2), new Point(Rect.Location.X + Rect.Width / 2, Rect.Location.Y));
            x.AddLine(new Point(Rect.Location.X + Rect.Width, Rect.Location.Y + Rect.Height / 2), new Point(Rect.Location.X + Rect.Width / 2, Rect.Location.Y + Rect.Height));
            g.FillPath(BackColor, x);
        }

        //for start/end
        private void DrawText(Graphics g)
        {
            Font font;
            Size stringSize;
            int fontSize = (int)Math.Ceiling(Math.Abs(Rect.Height) / 3.0)+2;
            do
            {
                font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
                stringSize = g.MeasureString(Text, font).ToSize();
                fontSize--;
            } while ((stringSize.Width >Math.Abs(Rect.Width)  && fontSize > 2));

            g.DrawString
                (
                Text,
                font,
                new SolidBrush(MyDictionary.CanvasObjectFontColor(Shape)),
                Rect.Location.X + (Rect.Width - stringSize.Width) / 2,
                Rect.Location.Y + Rect.Height / 2 - stringSize.Height / 2
                );
        }

        #endregion
        #endregion
    }
}
