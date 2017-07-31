using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    public class ListCanvasObjects : List<MyCanvasFigure>
    {
        public void SelectObjectContainingPoint(Point location)
        {
            for (int i = 0; i < base.Count; i++)
                if (base[i].IsContain(location))
                {
                    base[i].IsSelected = true;
                    base[i].BackColor = (base[i].IsSelected)
                        ? new SolidBrush(CanvasVariables.SelectionBgColor)
                        : new SolidBrush(CanvasVariables.DefaultBgColor);
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
                    base[i].Rect.Location = new Point((e.X - MouseDownLocation.X) + base[i].Rect.Left,
                        (e.Y - MouseDownLocation.Y) + base[i].Rect.Top);
                }
        }

        public void ResizeSelectedObjects(ref Point MouseDownLocation, Point e)
        {
            for (int i = 0; i < base.Count; i++)
            {
                if (base[i].IsSelected && !base[i].IsLocked)
                {
                    int NewWidth = e.X - base[i].Rect.X + (base[i].Rect.X + base[i].Rect.Width - MouseDownLocation.X);
                    int NewHeight = e.Y - base[i].Rect.Y + (base[i].Rect.Y + base[i].Rect.Height - MouseDownLocation.Y);
                    if (NewWidth >= BlocksData.MinSize.Width)
                        base[i].Rect.Width = NewWidth;
                    if (NewHeight >= BlocksData.MinSize.Height)
                        base[i].Rect.Height = NewHeight;
                }
            }
        }

        public void ResizeSelectedObjectsByRubbers(ref Point MouseDownLocation, Point e, int RubberID)
        {
            for (int i = 0; i < base.Count; i++)
                if (base[i].IsSelected && !base[i].IsLocked)
                {
                    int x = base[i].Rect.Location.X,
                        y = base[i].Rect.Location.Y,
                        width = base[i].Rect.Width,
                        height = base[i].Rect.Height;

                    switch (RubberID)
                    {
                        case 0:
                            x = e.X + base[i].Rect.Location.X - MouseDownLocation.X;
                            y = e.Y + base[i].Rect.Location.Y - MouseDownLocation.Y;
                            width -= e.X - MouseDownLocation.X;
                            height -= e.Y - MouseDownLocation.Y;
                            break;

                        case 1:
                            y = e.Y + base[i].Rect.Location.Y - MouseDownLocation.Y;
                            height -= e.Y - MouseDownLocation.Y;
                            break;

                        case 2:
                            y = e.Y + base[i].Rect.Location.Y - MouseDownLocation.Y;
                            height -= e.Y - MouseDownLocation.Y;
                            width += e.X;
                            break;

                        case 3:
                            width = e.X - base[i].Rect.X + (base[i].Rect.X + width - MouseDownLocation.X);
                            break;

                        case 4:
                            width = e.X - base[i].Rect.X + (base[i].Rect.X + width - MouseDownLocation.X);
                            height = e.Y - base[i].Rect.Y + (base[i].Rect.Y + height - MouseDownLocation.Y);
                            break;

                        case 5:
                            height = e.Y - base[i].Rect.Y + (base[i].Rect.Y + height - MouseDownLocation.Y);
                            break;

                        case 6:
                            x = e.X + base[i].Rect.Location.X - MouseDownLocation.X;
                            width -= e.X - MouseDownLocation.X;
                            height += e.Y;
                            break;

                        case 7:
                            x = e.X + base[i].Rect.Location.X - MouseDownLocation.X;
                            width -= e.X - MouseDownLocation.X;
                            break;
                    }
                    if (width > BlocksData.MinSize.Width)
                    {
                        base[i].Rect.Width = width;
                        base[i].Rect.Location = new Point(x, base[i].Rect.Location.Y);
                    }
                    if (height > BlocksData.MinSize.Height)
                    {
                        base[i].Rect.Height = height;
                        base[i].Rect.Location = new Point(base[i].Rect.Location.X, y);
                    }

                }
        }

        public void IsSelectedSetForAll(bool isSelected)
        {
            for (int i = 0; i < base.Count; i++)
            {
                base[i].BackColor =
                    (isSelected)
                        ? new SolidBrush(CanvasVariables.SelectionBgColor)
                        : new SolidBrush(CanvasVariables.DefaultBgColor);
                base[i].IsSelected = isSelected;
            }
        }
    }

    public class MyCanvasFigure
    {
        public MyCanvasFigure(Rectangle rect, BlocksData.Shape shape)
        {
            Rect = rect;
            Shape = shape;
            Text = BlocksData.Text(Shape);
        }


        public string Text; //zawartosc tekstowa kontrolki
        public bool IsSelected { get; set; } = false; //czy jest zaznaczona
        public bool IsLocked = false;
        public uint ID;
        public int In, Out, Out2;

        #region Done

        public Rectangle Rect; //obszar dla figury - point , size - dodatkowo wyliczane są punkty dla linii

        public SolidBrush BackColor;
        public Color FontColor;
        public int FontSize;
        private BlocksData.Shape _shape;
        public BlocksData.Shape Shape //jaką kontrolke rysujemy
        {
            get => _shape;
            set
            {
                _shape = value;
                BackColor = BlocksData.BackColor(_shape);
                FontColor = BlocksData.FontColor(_shape);
                FontSize = BlocksData.FontSize(_shape);
            }
        }


        public bool IsContain(Point location)
        {
            var contains = false;
            if (Shape == BlocksData.Shape.Start)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddEllipse(Rect);
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == BlocksData.Shape.End)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddEllipse(Rect);
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == BlocksData.Shape.Execution)
            {
                using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    gp.AddRectangle(Rect);
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == BlocksData.Shape.Input)
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
            else if (Shape == BlocksData.Shape.Decision)
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
            else if (Shape == BlocksData.Shape.ConnectionLine)
            {
            //    using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
            //    {
            //        if (Rect.Width != 0)
            //        {
            //            var YBreak = Math.Abs(Rect.Y - Rect.Height) * 50 / 100;
            //            var XBreak = Math.Abs(Rect.X - Rect.Width) * 50 / 100;
            //            if (Rect.Location.Y > Rect.Height)
            //                if (Rect.Location.X > Rect.Width)
            //                    gp.AddLines(
                                    
            //                        new Point[]
            //                        {
            //                            Rect.Location,
            //                            new Point(Rect.Location.X, Rect.Location.Y+YBreak),
            //                            new Point(Rect.Location.X-XBreak,Rect.Location.Y+YBreak),
            //                            new Point(Rect.Width+XBreak,Rect.Height-YBreak),
            //                            new Point(Rect.Width, Rect.Height-YBreak),
            //                            new Point(Rect.Width, Rect.Height)
            //                        });
            //                else
            //                    gp.AddLines(
            //                        new Point[]
            //                        {
            //                            Rect.Location,
            //                            new Point(Rect.Location.X, Rect.Location.Y+YBreak),
            //                            new Point(Rect.Location.X+XBreak,Rect.Location.Y+YBreak),
            //                            new Point(Rect.Width-XBreak,Rect.Height-YBreak),
            //                            new Point(Rect.Width, Rect.Height-YBreak),
            //                            new Point(Rect.Width, Rect.Height)
            //                        });
            //            else
            //                gp.AddLines(
            //                    new Point[]
            //                    {
            //                        Rect.Location,
            //                        new Point(Rect.Location.X, Rect.Location.Y+YBreak),
            //                        new Point(Rect.Width, Rect.Height-YBreak),
            //                        new Point(Rect.Width, Rect.Height)
            //                    });
            //        }
            //        contains = gp.IsVisible(location);
            //    }
            }
            return contains;
        }

        #region DrawBlock with text

        public void Draw(Graphics g)
        {
            switch (Shape)
            {
                case (BlocksData.Shape.Start):
                    DrawStart(g);
                    break;
                case (BlocksData.Shape.End):
                    DrawEnd(g);
                    break;
                case (BlocksData.Shape.Input):
                    DrawInput(g);
                    break;
                case (BlocksData.Shape.Execution):
                    DrawExecution(g);
                    break;
                case (BlocksData.Shape.Decision):
                    DrawDecision(g);
                    break;
                case (BlocksData.Shape.ConnectionLine):
                    DrawConnectionLine(g);
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
            x.AddLine(new Point(Rect.Location.X, Rect.Location.Y + Rect.Height),
                new Point(Rect.Location.X + 10, Rect.Location.Y));
            x.AddLine(new Point(Rect.Location.X + Rect.Width, Rect.Location.Y),
                new Point(Rect.Location.X + Rect.Width - 10, Rect.Location.Y + Rect.Height));
            g.FillPath(BackColor, x);
        }

        private void DrawExecution(Graphics g)
        {
            g.FillRectangle(BackColor, Rect);
        }

        private void DrawDecision(Graphics g)
        {

            var x = new System.Drawing.Drawing2D.GraphicsPath();
            x.AddLine(new Point(Rect.Location.X, Rect.Location.Y + Rect.Height / 2),
                new Point(Rect.Location.X + Rect.Width / 2, Rect.Location.Y));
            x.AddLine(new Point(Rect.Location.X + Rect.Width, Rect.Location.Y + Rect.Height / 2),
                new Point(Rect.Location.X + Rect.Width / 2, Rect.Location.Y + Rect.Height));
            g.FillPath(BackColor, x);
        }

        public void DrawConnectionLine(Graphics g)
        {
            if (Rect.Width != 0)
            {
                var YBreak = Math.Abs(Rect.Y - Rect.Height) * 50 / 100;
                var XBreak = Math.Abs(Rect.X - Rect.Width) * 50 / 100;
                if (Rect.Location.Y > Rect.Height)
                    if (Rect.Location.X>Rect.Width)
                    g.DrawLines(
                        new Pen(BlocksData.BackColor(BlocksData.Shape.ConnectionLine), 4),
                        new Point[]
                        {
                            Rect.Location,
                            new Point(Rect.Location.X, Rect.Location.Y+YBreak),
                            new Point(Rect.Location.X-XBreak,Rect.Location.Y+YBreak),
                            new Point(Rect.Width+XBreak,Rect.Height-YBreak),
                            new Point(Rect.Width, Rect.Height-YBreak),
                            new Point(Rect.Width, Rect.Height)
                        });
                    else
                        g.DrawLines(
                            new Pen(BlocksData.BackColor(BlocksData.Shape.ConnectionLine), 4),
                            new Point[]
                            {
                                Rect.Location,
                                new Point(Rect.Location.X, Rect.Location.Y+YBreak),
                                new Point(Rect.Location.X+XBreak,Rect.Location.Y+YBreak),
                                new Point(Rect.Width-XBreak,Rect.Height-YBreak),
                                new Point(Rect.Width, Rect.Height-YBreak),
                                new Point(Rect.Width, Rect.Height)
                            });
                else
                    g.DrawLines(
                    new Pen(BlocksData.BackColor(BlocksData.Shape.ConnectionLine), 4),
                    new Point[]
                    {
                        Rect.Location,
                        new Point(Rect.Location.X, Rect.Location.Y+YBreak),
                        new Point(Rect.Width, Rect.Height-YBreak),
                        new Point(Rect.Width, Rect.Height)
                    });
            }
        }

        //for start/end
        private void DrawText(Graphics g)
        {
            Font font;
            Size stringSize;
            int fontSize = (int) Math.Ceiling(Math.Abs(Rect.Height) / 3.0) + 2;
            do
            {
                font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
                stringSize = g.MeasureString(Text, font).ToSize();
                fontSize--;
            } while ((stringSize.Width > Math.Abs(Rect.Width) && fontSize > 2));

            g.DrawString
            (
                Text,
                font,
                new SolidBrush(BlocksData.FontColor(Shape)),
                Rect.Location.X + (Rect.Width - stringSize.Width) / 2,
                Rect.Location.Y + Rect.Height / 2 - stringSize.Height / 2
            );
        }

        #endregion

        #endregion
    }
}
