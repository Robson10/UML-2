using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    public class ListCanvasBlocks : List<MyBlock>
    {
        private static int _id = 0;

        public void My_SelectObjectContainingPoint(Point location)
        {
            for (int i = 0; i < Count; i++)
                if (base[i].IsContain(location))
                {
                    base[i].IsSelected = true;
                    base[i].BackColor = BlocksData.DefaultSelectionColor;
                    Insert(0, base[i]);
                    RemoveAt(i + 1);
                    break;
                }
        }

        public void MySelectObjectByRect(Rectangle rect)
        {
            for (int i = 0; i < Count; i++)
            {
                if (base[i].Rect.IntersectsWith(rect))
                {
                    base[i].IsSelected = true;
                    base[i].BackColor = BlocksData.DefaultSelectionColor;
                    Insert(0, base[i]);
                    RemoveAt(i + 1);
                }
                else
                {
                    base[i].IsSelected = false;
                    base[i].BackColor = base[i].BackColorStorage;
                }
            }
        }

        public bool My_IsAnyObjectContainingPoint(Point location)
        {
            for (int i = 0; i < Count; i++)
                if (base[i].IsContain(location))
                    return true;
            return false;
        }

        public MyBlock TryGetElementContainingPoint(Point location)
        {
            for (int i = 0; i < base.Count; i++)
                if (base[i].IsContain(location))
                    return base[i];
            return null;
        }

        public MyBlock TryGetElementWithId(int id)
        {
            for (int i = 0; i < base.Count; i++)
                if (base[i].ID == id)
                    return base[i];
            return null;
        }

        public bool My_MoveSelectedObjects(ref Point mouseDownLocation, Point e)
        {
            var isAnyBlockMoved = false;
            for (int i = 0; i < Count; i++)
                if (base[i].IsSelected && !base[i].IsLocked)
                {
                    base[i].Rect.Location = new Point((e.X - mouseDownLocation.X) + base[i].Rect.Left,
                        (e.Y - mouseDownLocation.Y) + base[i].Rect.Top);
                    isAnyBlockMoved = true;
                }
            return isAnyBlockMoved;
        }

        public void My_ResizeSelectedObjects(ref Point mouseDownLocation, Point e)
        {
            for (int i = 0; i < Count; i++)
            {
                if (base[i].IsSelected && !base[i].IsLocked)
                {
                    int NewWidth = e.X - base[i].Rect.X + (base[i].Rect.X + base[i].Rect.Width - mouseDownLocation.X);
                    int NewHeight = e.Y - base[i].Rect.Y + (base[i].Rect.Y + base[i].Rect.Height - mouseDownLocation.Y);
                    if (NewWidth >= BlocksData.MinBlockSize.Width)
                        base[i].Rect.Width = NewWidth;
                    if (NewHeight >= BlocksData.MinBlockSize.Height)
                        base[i].Rect.Height = NewHeight;
                }
            }
        }

        public void My_ResizeSelectedObjectsByRubbers(ref Point mouseDownLocation, Point e, int rubberId)
        {
            for (int i = 0; i < base.Count; i++)
                if (base[i].IsSelected && !base[i].IsLocked)
                {
                    int x = base[i].Rect.Location.X,
                        y = base[i].Rect.Location.Y,
                        width = base[i].Rect.Width,
                        height = base[i].Rect.Height;

                    switch (rubberId)
                    {
                        case 0:
                            x = e.X + base[i].Rect.Location.X - mouseDownLocation.X;
                            y = e.Y + base[i].Rect.Location.Y - mouseDownLocation.Y;
                            width -= e.X - mouseDownLocation.X;
                            height -= e.Y - mouseDownLocation.Y;
                            break;

                        case 1:
                            y = e.Y + base[i].Rect.Location.Y - mouseDownLocation.Y;
                            height -= e.Y - mouseDownLocation.Y;
                            break;

                        case 2:
                            y = e.Y + base[i].Rect.Location.Y - mouseDownLocation.Y;
                            height -= e.Y - mouseDownLocation.Y;
                            width += e.X;
                            break;

                        case 3:
                            width = e.X - base[i].Rect.X + (base[i].Rect.X + width - mouseDownLocation.X);
                            break;

                        case 4:
                            width = e.X - base[i].Rect.X + (base[i].Rect.X + width - mouseDownLocation.X);
                            height = e.Y - base[i].Rect.Y + (base[i].Rect.Y + height - mouseDownLocation.Y);
                            break;

                        case 5:
                            height = e.Y - base[i].Rect.Y + (base[i].Rect.Y + height - mouseDownLocation.Y);
                            break;

                        case 6:
                            x = e.X + base[i].Rect.Location.X - mouseDownLocation.X;
                            width -= e.X - mouseDownLocation.X;
                            height += e.Y;
                            break;

                        case 7:
                            x = e.X + base[i].Rect.Location.X - mouseDownLocation.X;
                            width -= e.X - mouseDownLocation.X;
                            break;
                    }
                    if (width > BlocksData.MinBlockSize.Width)
                    {
                        base[i].Rect.Width = width;
                        base[i].Rect.Location = new Point(x, base[i].Rect.Location.Y);
                    }
                    if (height > BlocksData.MinBlockSize.Height)
                    {
                        base[i].Rect.Height = height;
                        base[i].Rect.Location = new Point(base[i].Rect.Location.X, y);
                    }

                }
        }

        public void My_IsSelectedSetForAll(bool isSelected)
        {
            for (int i = 0; i < Count; i++)
            {
                base[i].BackColor = (isSelected)
                    ? (BlocksData.DefaultSelectionColor)
                    : (this[i].BackColorStorage);
                base[i].IsSelected = isSelected;
            }
        }

        public void MyAdd(Point e, BlocksData.Shape shapeToDraw)
        {
            Insert(0, (new MyBlock(new Rectangle(e.X - BlocksData.DefaultBlockSize.Width / 2,
                    e.Y - BlocksData.DefaultBlockSize.Height / 2, BlocksData.DefaultBlockSize.Width,
                    BlocksData.DefaultBlockSize.Height),
                shapeToDraw, _id)));
            _id++;
        }

        public void MyAbortAdd()
        {
            if (Count > 0)
                RemoveAt(0);
        }

        public void MySetIsLockedForSelectedObj()
        {
            for (int i = 0; i < Count; i++)
                if (this[i].IsSelected)
                    this[i].IsLocked = !this[i].IsLocked;
        }

        public void MyDelete(int i)
        {
            this.RemoveAt(i);
        }

        public List<MyBlock> MyCopy(string clipboardFormat)
        {
            var x = new List<MyBlock>();
            x.AddRange(this.Where(z => z.IsSelected));
            return x;
        }

        public List<MyBlock> MyCut(string clipboardFormat)
        {
            var x = new List<MyBlock>();
            x.AddRange(this.Where(z => z.IsSelected));
            RemoveAll(z => z.IsSelected);
            return x;
        }

        public int MyPaste(MyBlock block)
        {
            block.ID = _id;
            _id++;
            Insert(0, block);
            return _id - 1;
        }
    }

    [Serializable]
    public class MyBlock
    {
        public BlocksData.Shape Shape { get; }
        public string Label = "";
        public string Code  = "";
        public bool IsSelected  = false;
        public bool IsLocked = false;
        public bool AutoResize  = false;
        public int ID;

        public Point PointInput;
        public Point PointOutput1, PointOutput2;
        public Rectangle Rect; //obszar dla figury 

        public Color BackColor { get; set; }
        public Color BackColorStorage;
        public Color FontColor;
        public int FontSize;
        public MyBlock(Rectangle rect, BlocksData.Shape shape, int id)
        {
            Rect = rect;
            Shape = shape;
            ID = id;
            Label = BlocksData.DefaultLabel(Shape);
            BackColor = BlocksData.DefaultBackColor(Shape);
            BackColorStorage= BlocksData.DefaultBackColor(Shape);
            FontColor = BlocksData.DefaultFontColor(Shape);
            FontSize = BlocksData.DefaultFontSize(Shape);
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
                default:
                    MessageBox.Show("nie znalazłem odpowiedniego kształtu i nwm co teraz mam narysować ;(");
                    break;
            }
        }

        private void DrawStart(Graphics g)
        {
            g.FillEllipse(new SolidBrush(BackColor), Rect);
            PointOutput1 = new Point(Rect.Left + Rect.Width / 2, Rect.Bottom);
            DrawLabel(g);
        }

        private void DrawEnd(Graphics g)
        {
            g.FillEllipse(new SolidBrush(BackColor), Rect);
            PointInput = new Point(Rect.Left + Rect.Width / 2, Rect.Top);
            DrawLabel(g);
        }

        private void DrawInput(Graphics g)
        {
            var x = new System.Drawing.Drawing2D.GraphicsPath();
            x.AddLine(new Point(Rect.Location.X, Rect.Location.Y + Rect.Height),
                new Point(Rect.Location.X + 10, Rect.Location.Y));
            x.AddLine(new Point(Rect.Location.X + Rect.Width, Rect.Location.Y),
                new Point(Rect.Location.X + Rect.Width - 10, Rect.Location.Y + Rect.Height));

            PointOutput1 = new Point(Rect.Left + Rect.Width / 2, Rect.Bottom);
            PointInput = new Point(Rect.Left + Rect.Width / 2, Rect.Top);
            g.FillPath(new SolidBrush(BackColor), x);
            DrawLabel(g);
        }

        private void DrawExecution(Graphics g)
        {
            PointOutput1 = new Point(Rect.Left + Rect.Width / 2, Rect.Bottom);
            PointInput = new Point(Rect.Left + Rect.Width / 2, Rect.Top);
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

            PointOutput1 = new Point(Rect.Left, Rect.Top + Rect.Height / 2);
            PointOutput2 = new Point(Rect.Right, Rect.Top + Rect.Height / 2);
            PointInput = new Point(Rect.Left + Rect.Width / 2, Rect.Top);
            g.FillPath(new SolidBrush(BackColor), x);
            DrawLabel(g);
        }

        private void DrawLabel(Graphics g)
        {
            Font font;
            Size stringSize;
            var maxWidth = Math.Abs((Shape == BlocksData.Shape.Decision)? (int) (Rect.Width * 0.7f): Rect.Width);
            if (!AutoResize)
            {
                int fontSize = (int) Math.Ceiling(Math.Abs(Rect.Height) / 3.0) + 2;
                do
                {
                    font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
                    stringSize = g.MeasureString(Label, font).ToSize();
                    fontSize--;
                } while ((stringSize.Width > maxWidth && fontSize > 2));
                g.DrawString(Label, font, new SolidBrush(FontColor), Rect,
                    CanvasVariables.BlockStringFormat);
            }
            else
            {
                font = new Font(FontFamily.GenericSansSerif, FontSize, FontStyle.Bold);
                stringSize = g.MeasureString(Label, font).ToSize();
                if (Shape == BlocksData.Shape.Decision)
                    Rect.Size = new Size((int) (stringSize.Width * 1.7), stringSize.Height * 2);
                else
                    Rect.Size = new Size(stringSize.Width, stringSize.Height);
                g.DrawString(Label, font, new SolidBrush(FontColor), Rect,
                    CanvasVariables.BlockStringFormat);
            }
        }

        #endregion
        
    }
}
