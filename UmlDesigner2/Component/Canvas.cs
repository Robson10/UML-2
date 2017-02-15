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
        private MyDictionary.Shape _ShapeToDraw = MyDictionary.Shape.Start;
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
            BackColor = Color.Blue;
        }
        public void ResizeAll( Size clientSize)
        {
            Size = clientSize;
        }
        //lewy utworzenie controlki, zaznaczenie, ppm menu kontekstowe
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (ShapeToDraw != MyDictionary.Shape.Nothing)
                {
                    CanvasObjects.Add(
                        new MyCanvasObject(
                            new Rectangle(e.Location.X-MyDictionary.defaultCanvasControlSize.Width/2,e.Location.Y- MyDictionary.defaultCanvasControlSize.Height/ 2, MyDictionary.defaultCanvasControlSize.Width, MyDictionary.defaultCanvasControlSize.Height), 
                            ShapeToDraw));
                    ShapeToDraw = MyDictionary.Shape.Nothing;
                }
                else
                    for (int i = 0; i < CanvasObjects.Count; i++)
                        if (CanvasObjects[i].IsContain(e.Location))
                            CanvasObjects[i].IsSelected = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < CanvasObjects.Count; i++)
                    if (CanvasObjects[i].IsContain(e.Location))
                        MessageBox.Show("o to tu pojawić powinno sie menu kontekstowe dla ppm ale na chwile obecną poszło na piwo i do tej pory nie wrociło");
            }
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < CanvasObjects.Count; i++)
                CanvasObjects[i].Draw( e.Graphics);
        }
    }
   

    public class MyCanvasObject
    {
        public MyCanvasObject(Rectangle rect,MyDictionary.Shape shape)
        {
            Rect = rect;
            Shape = shape;
            Text = "";
        }

        public Rectangle Rect;//obszar dla figury - point , size

        private MyDictionary.Shape _Shape;
        public MyDictionary.Shape Shape//jaką kontrolke rysujemy
        {
            get { return _Shape; }
            set
            {
                _Shape = value;
                BackColor =MyDictionary.CanvasObjectBackColor(_Shape);
                FontColor = MyDictionary.CanvasObjectFontColor(_Shape);
                FontSize = MyDictionary.CanvasObjectFontSize(_Shape);

            }
        }
        public SolidBrush BackColor;
        public Color FontColor;
        public int FontSize;
        public string Text; //zawartosc tekstowa kontrolki
        public bool IsSelected=false;//czy jest zaznaczona
        public bool IsLocked = false;
        public uint ID;
        public int In, Out, Out2;
        public bool IsContain(Point location)//http://stackoverflow.com/questions/34582234/how-to-detect-if-mouse-has-clicked-inside-of-a-certain-shape-in-c-sharp-on-winfo
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
                    gp.AddEllipse(Rect);
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == MyDictionary.Shape.Decision)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddEllipse(Rect);
                    contains = gp.IsVisible(location);
                }
            }
            return contains;
        }


        string Start = "start";
        public  void Draw( Graphics g)
        {
            switch(Shape)
            {
                case (MyDictionary.Shape.Start):
                    g.FillEllipse(BackColor, Rect);
                    break;
                case (MyDictionary.Shape.End):
                    g.FillEllipse(BackColor, Rect);
                    break;
                case (MyDictionary.Shape.Input):
                    g.FillEllipse(BackColor, Rect);
                    break;
                case (MyDictionary.Shape.Execution):
                    g.FillEllipse(BackColor, Rect);
                    break;
                case (MyDictionary.Shape.Decision):
                    g.FillEllipse(BackColor, Rect);
                    break;
                default:
                    MessageBox.Show("nie znalazłem odpowiedniego kształtu i nwm co teraz mam narysować ;(");
                    break;
            }
        }

    }
}
