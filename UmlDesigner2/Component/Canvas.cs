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


        public Canvas()
        {
            InitializeComponent();
            DoubleBuffered = true;
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
                CanvasObjects.MoveSelectedObjects(ref MouseDownLocation,  e.Location);
                MouseDownLocation = e.Location;
                Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                ppm = false;
                CanvasObjects.ResizeSelectedObjects(ref MouseDownLocation, e.Location);
                MouseDownLocation = e.Location;
                Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < CanvasObjects.Count; i++)
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
            DrawText(g);
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
