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
        List<MyCanvasObject> CanvasObjects = new List<MyCanvasObject>();
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
        private Keys IsMultiSelect
        {
            get { return _IsMultiSelect; }
            set
            {
                _IsMultiSelect = value;
                if (value == Keys.Escape)
                {
                    ShapeToDraw = MyDictionary.Shape.Nothing;
                    CanvasObjectsIsSelected(false);
                }

            }
        }
        private void CanvasObjectsIsSelected(bool isSelected)
        {
            for (int i = 0; i < CanvasObjects.Count; i++)
            {
                CanvasObjects[i].BackColor = (isSelected)?new SolidBrush(Color.Yellow):new SolidBrush(Color.Gray);//-----------------------------------
                CanvasObjects[i].IsSelected = isSelected;
            }
            Invalidate();
        }
        private void CanvasObjectsIsSelected(MyCanvasObject myCanvasObject,bool isSelected, int i)
        {
            myCanvasObject.IsSelected = isSelected; //(IsMultiSelect == Keys.ControlKey) ? !myCanvasObject.IsSelected : true; 


            myCanvasObject.BackColor = (myCanvasObject.IsSelected) ? new SolidBrush(Color.Yellow) : new SolidBrush(Color.Gray);
            CanvasObjects.Insert(0,myCanvasObject);
            CanvasObjects.RemoveAt(i+1);
            Invalidate();
        }
        protected override void OnKeyDown( KeyEventArgs e)
        {
            IsMultiSelect = e.KeyCode;
        }
        protected override void OnKeyUp( KeyEventArgs e)
        {
            IsMultiSelect = Keys.None;
        }

        bool ppm = true;
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (IsMultiSelect != Keys.ControlKey) CanvasObjectsIsSelected(false);

            if (e.Button == MouseButtons.Left)
            {
                if (ShapeToDraw != MyDictionary.Shape.Nothing)
                {
                    CanvasObjects.Add(new MyCanvasObject(new Rectangle(e.Location.X - MyDictionary.defaultCanvasControlSize.Width / 2, e.Location.Y - MyDictionary.defaultCanvasControlSize.Height / 2, MyDictionary.defaultCanvasControlSize.Width, MyDictionary.defaultCanvasControlSize.Height),ShapeToDraw));
                    ShapeToDraw = MyDictionary.Shape.Nothing;
                }
                else
                {
                    for (int i = 0; i < CanvasObjects.Count; i++)
                        if (CanvasObjects[i].IsContain(e.Location))
                        {
                            CanvasObjectsIsSelected(CanvasObjects[i], true, i);
                            break;
                        }
                }
            }
            else if (e.Button == MouseButtons.Right&& ppm==true)
            {
                for (int i = 0; i < CanvasObjects.Count; i++)
                    if (CanvasObjects[i].IsContain(e.Location))
                        MessageBox.Show("menu kontekstowe poszło i do tej pory nie wrociło");
            }
            Invalidate();
        }
        
        private Point MouseDownLocation;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (IsMultiSelect != Keys.ControlKey) CanvasObjectsIsSelected(false);

            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < CanvasObjects.Count; i++)
                    if (CanvasObjects[i].IsContain(e.Location))
                    {
                        CanvasObjectsIsSelected(CanvasObjects[i],true, i);
                        break;
                    }
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
                ppm = false;
                for (int i = 0; i < CanvasObjects.Count; i++)
                    if (CanvasObjects[i].IsSelected && !CanvasObjects[i].IsLocked)
                    {
                        CanvasObjects[i].Rect.Location = new Point((e.X - MouseDownLocation.X) + CanvasObjects[i].Rect.Left, (e.Y - MouseDownLocation.Y) + CanvasObjects[i].Rect.Top);
                    }
                MouseDownLocation = e.Location;
                Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                ppm = false;
                for (int i = 0; i < CanvasObjects.Count; i++)
                {
                    if (CanvasObjects[i].IsSelected && !CanvasObjects[i].IsLocked)
                    {
                        CanvasObjects[i].Rect.Width = e.X - CanvasObjects[i].Rect.X + (CanvasObjects[i].Rect.X + CanvasObjects[i].Rect.Width - MouseDownLocation.X);
                        CanvasObjects[i].Rect.Height = e.Y - CanvasObjects[i].Rect.Y + (CanvasObjects[i].Rect.Y + CanvasObjects[i].Rect.Height - MouseDownLocation.Y);
                    }
                }
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
        public void IsAnyObjectContainPoint(Point location)
        {
            for (int i = 0; i < base.Count; i++)
                if (base[i].IsContain(location))
                {
                    CanvasObjectsIsSelected(base[i], true, i);
                    break;
                }
        }
        private void CanvasObjectsIsSelected(MyCanvasObject myCanvasObject, bool isSelected, int i)
        {
            myCanvasObject.IsSelected = isSelected; //(IsMultiSelect == Keys.ControlKey) ? !myCanvasObject.IsSelected : true; 


            myCanvasObject.BackColor = (myCanvasObject.IsSelected) ? new SolidBrush(Color.Yellow) : new SolidBrush(Color.Gray);
            base.Insert(0, myCanvasObject);
            base.RemoveAt(i + 1);
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
