using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using SbWinNew.Class;

namespace SbWinNew.Components.Workspace
{
    [Serializable]
    public class MyBlock
    {
        public Helper.Shape Shape;
        public string Label = "";
        public string Code  = "";
        public string Includes = "";
        public string Variables = "";
        public bool IsSelected = false;
        public bool IsLocked = false;
        public bool AutoResize  = false;
        public int ID;

        public Point PointInput{ get; set; }
        public Point PointOutput1, PointOutput2;

        
        private Rectangle _rect;
        public Rectangle Rect
        {
            get =>_rect;
            set
            {
                _rect = value;
                if(Shape!= Helper.Shape.Start)//procz start
                    PointInput = new Point(Rect.Left + Rect.Width / 2, Rect.Top);
                if (Shape== Helper.Shape.Decision)//dla decision
                {
                    PointOutput1 = new Point(Rect.Left, Rect.Top + Rect.Height / 2);
                    PointOutput2 = new Point(Rect.Right, Rect.Top + Rect.Height / 2);
                }
                else if (Shape!= Helper.Shape.End)//procz koniec
                    PointOutput1 = new Point(Rect.Left + Rect.Width / 2, Rect.Bottom);

            }
        } //obszar dla figury 
        [XmlIgnore]
        public Color BackColor;
        [XmlIgnore]
        public Color BackColorStorage;
        [XmlIgnore]
        public Color FontColor;

        //zmienne potrzebne tylko do zapisu i odczytu pliku. Bazują na zmiennych bez "HTML"
        [XmlAttribute]
        public string BackColorHTML
        {
            get { return ColorTranslator.ToHtml(BackColor); }
            set { BackColor = ColorTranslator.FromHtml(value); }
        }
        [XmlAttribute]
        public string BackColorStorageHTML
        {
            get { return ColorTranslator.ToHtml(BackColorStorage); }
            set { BackColorStorage = ColorTranslator.FromHtml(value); }
        }
        [XmlAttribute]
        public string FontColorHTML
        {
            get { return ColorTranslator.ToHtml(FontColor); }
            set { FontColor = ColorTranslator.FromHtml(value); }
        }
        public int FontSize;
        /// <summary>
        /// only for serialization
        /// </summary>
        public MyBlock()
        {
            
        }
        public MyBlock(Rectangle rect, Helper.Shape shape, int id)
        {
            if (shape == Helper.Shape.Start)
            {
                Includes = "#include <stdio.h>" + Environment.NewLine +
                           "#include <iostream>" + Environment.NewLine +
                           "#include <string>" + Environment.NewLine +
                           "using namespace std;" + Environment.NewLine;
            }
            Rect = rect;
            Shape = shape;
            ID = id;
            Label = Helper.DefaultBlocksSettings[Shape].Label;
            BackColor = Helper.DefaultBlocksSettings[Shape].BackColor;
            BackColorStorage= Helper.DefaultBlocksSettings[Shape].BackColor;
            FontColor = Helper.DefaultBlocksSettings[Shape].FontColor;
            FontSize = Helper.DefaultBlocksSettings[Shape].FontSize;
            AutoResize = Helper.DefaultBlockAutoresize;
            UpdateRectSizeOnAutoresize();

        }

        public void UpdateRectSizeOnAutoresize()
        {
            if (AutoResize)
            {
                using (Panel x = new Panel())
                using (Graphics g = x.CreateGraphics())
                {
                    var font = new Font(FontFamily.GenericSansSerif, FontSize, FontStyle.Bold);
                    var stringSize = g.MeasureString(Label, font).ToSize();
                    if (Shape == Helper.Shape.Decision)
                        Rect = new Rectangle(Rect.Location,
                            new Size((int) (stringSize.Width * 1.7), stringSize.Height * 3));
                    else
                        Rect = new Rectangle(Rect.Location, new Size((int)(stringSize.Width * 1.1), stringSize.Height*3));
                }
            }
        }


        public bool IsContain(Point location)
        {
            var contains = false;
            if (Shape == Helper.Shape.Start)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddEllipse(Rect);
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == Helper.Shape.End)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddEllipse(Rect);
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == Helper.Shape.Execution)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddRectangle(Rect);
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == Helper.Shape.Input)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddLine(new Point(Rect.Location.X, Rect.Location.Y + Rect.Height),
                        new Point(Rect.Location.X + 10, Rect.Location.Y));
                    gp.AddLine(new Point(Rect.Location.X + Rect.Width, Rect.Location.Y),
                        new Point(Rect.Location.X + Rect.Width - 10, Rect.Location.Y + Rect.Height));
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == Helper.Shape.Decision)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddLine(new Point(Rect.Location.X, Rect.Location.Y + Rect.Height / 2),
                        new Point(Rect.Location.X + Rect.Width / 2, Rect.Location.Y));
                    gp.AddLine(new Point(Rect.Location.X + Rect.Width, Rect.Location.Y + Rect.Height / 2),
                        new Point(Rect.Location.X + Rect.Width / 2, Rect.Location.Y + Rect.Height));
                    contains = gp.IsVisible(location);
                }
            }
            return contains;
        }

        #region DrawBlock with text

        public void Draw(Graphics g)
        {
            switch (Shape)
            {
                case (Helper.Shape.Start):
                    DrawStart(g);
                    break;
                case (Helper.Shape.End):
                    DrawEnd(g);
                    break;
                case (Helper.Shape.Input):
                    DrawInput(g);
                    break;
                case (Helper.Shape.Execution):
                    DrawExecution(g);
                    break;
                case (Helper.Shape.Decision):
                    DrawDecision(g);
                    break;
                default:
                    MessageBox.Show("nie znalazłem odpowiedniego kształtu i nwm co teraz mam narysować ;(");
                    break;
            }
        }

        private void DrawStart(Graphics g)
        {
            g.FillEllipse(new SolidBrush(BackColor), Rect);
            DrawLabel(g);
        }

        private void DrawEnd(Graphics g)
        {
            g.FillEllipse(new SolidBrush(BackColor), Rect);
            DrawLabel(g);
        }

        private void DrawInput(Graphics g)
        {
            var x = new System.Drawing.Drawing2D.GraphicsPath();
            x.AddLine(new Point(Rect.Location.X, Rect.Location.Y + Rect.Height),
                new Point(Rect.Location.X + 10, Rect.Location.Y));
            x.AddLine(new Point(Rect.Location.X + Rect.Width, Rect.Location.Y),
                new Point(Rect.Location.X + Rect.Width - 10, Rect.Location.Y + Rect.Height));
            g.FillPath(new SolidBrush(BackColor), x);
            DrawLabel(g);
        }

        private void DrawExecution(Graphics g)
        {
            g.FillRectangle(new SolidBrush(BackColor), Rect);
            DrawLabel(g);
        }

        private void DrawDecision(Graphics g)
        {
            var x = new System.Drawing.Drawing2D.GraphicsPath();
            x.AddLine(new Point(Rect.Location.X, Rect.Location.Y + Rect.Height / 2),
                new Point(Rect.Location.X + Rect.Width / 2, Rect.Location.Y));
            x.AddLine(new Point(Rect.Location.X + Rect.Width, Rect.Location.Y + Rect.Height / 2),
                new Point(Rect.Location.X + Rect.Width / 2, Rect.Location.Y + Rect.Height));
            g.FillPath(new SolidBrush(BackColor), x);
            DrawLabel(g);
        }
        
        private void DrawLabel(Graphics g)
        {
            Font font;
            Size stringSize;
            StringFormat strFormat = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
            var maxWidth = Math.Abs((Shape == Helper.Shape.Decision)? (int) (Rect.Width * 0.7f): Rect.Width);
            if (!AutoResize)
            {
                int fontSize = (int) Math.Ceiling(Math.Abs(Rect.Height) / 3.0) + 2;
                do
                {
                    font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
                    stringSize = g.MeasureString(Label, font).ToSize();
                    fontSize--;
                } while ((stringSize.Width > maxWidth && fontSize > 2));
            }
            else
            {
                font = new Font(FontFamily.GenericSansSerif, FontSize, FontStyle.Bold);
                stringSize = g.MeasureString(Label, font).ToSize();
                if (Shape == Helper.Shape.Decision)
                    Rect = new Rectangle(Rect.Location, new Size((int)(stringSize.Width * 1.7), stringSize.Height * 3));
                else
                    Rect = new Rectangle(Rect.Location, new Size((int)(stringSize.Width * 1.1), stringSize.Height * 3));
            }
          
            g.DrawString(Label, font, new SolidBrush(FontColor), Rect, strFormat);
        }

        #endregion
        
    }
}