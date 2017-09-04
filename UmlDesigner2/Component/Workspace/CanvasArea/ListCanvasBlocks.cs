﻿using System;
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
                    base[i].BackColor = MyDictionary.DefaultSelectionColor;
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
                    base[i].BackColor = MyDictionary.DefaultSelectionColor;
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
                    base[i].Rect =new Rectangle( new Point((e.X - mouseDownLocation.X) + base[i].Rect.Left,
                        (e.Y - mouseDownLocation.Y) + base[i].Rect.Top), base[i].Rect.Size);
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
                    if (NewWidth >= MyDictionary.DefaultBlocksSettings[this[i].Shape].MinSize.Width)
                        base[i].Rect = new Rectangle(base[i].Rect.Location,new Size( NewWidth,base[i].Rect.Size.Height));
                    if (NewHeight >= MyDictionary.DefaultBlocksSettings[this[i].Shape].MinSize.Height)
                    base[i].Rect = new Rectangle(base[i].Rect.Location, new Size(base[i].Rect.Size.Width, NewHeight));
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
                    if (width > MyDictionary.DefaultBlocksSettings[this[i].Shape].MinSize.Width)
                    {
                        base[i].Rect = new Rectangle(base[i].Rect.Location, new Size(width, base[i].Rect.Size.Height));
                        base[i].Rect =new Rectangle( new Point(x, base[i].Rect.Location.Y), base[i].Rect.Size);
                    }
                    if (height > MyDictionary.DefaultBlocksSettings[this[i].Shape].MinSize.Height)
                    {
                        base[i].Rect = new Rectangle(base[i].Rect.Location, new Size(this[i].Rect.Size.Width, height));
                        base[i].Rect = new Rectangle(new Point(base[i].Rect.Location.X, y), base[i].Rect.Size);
                    }

                }
        }

        public void My_IsSelectedSetForAll(bool isSelected)
        {
            for (int i = 0; i < Count; i++)
            {
                base[i].BackColor = (isSelected)
                    ? (MyDictionary.DefaultSelectionColor)
                    : (this[i].BackColorStorage);
                base[i].IsSelected = isSelected;
            }
        }

        public void MyAdd(Point e, MyDictionary.Shape shapeToDraw)
        {
            Insert(0, (new MyBlock(new Rectangle(e.X - MyDictionary.DefaultBlocksSettings[shapeToDraw].BlockSize.Width / 2,
                    e.Y - MyDictionary.DefaultBlocksSettings[shapeToDraw].BlockSize.Height / 2, MyDictionary.DefaultBlocksSettings[shapeToDraw].BlockSize.Width,
                    MyDictionary.DefaultBlocksSettings[shapeToDraw].BlockSize.Height),
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
        public MyDictionary.Shape Shape { get; }
        public string Label = "";
        public string Code  = "";
        public bool IsSelected  = false;
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
                if(Shape!= MyDictionary.Shape.Start)//procz start
                    PointInput = new Point(Rect.Left + Rect.Width / 2, Rect.Top);
                if (Shape== MyDictionary.Shape.Decision)//dla decision
                {
                    PointOutput1 = new Point(Rect.Left, Rect.Top + Rect.Height / 2);
                    PointOutput2 = new Point(Rect.Right, Rect.Top + Rect.Height / 2);
                }
                else if (Shape!= MyDictionary.Shape.End)//procz koniec
                    PointOutput1 = new Point(Rect.Left + Rect.Width / 2, Rect.Bottom);

            }
        } //obszar dla figury 

        public Color BackColor { get; set; }
        public Color BackColorStorage;
        public Color FontColor;
        public int FontSize;
        public MyBlock(Rectangle rect, MyDictionary.Shape shape, int id)
        {
            Rect = rect;
            Shape = shape;
            ID = id;
            Label = MyDictionary.DefaultBlocksSettings[Shape].Label;
            BackColor = MyDictionary.DefaultBlocksSettings[Shape].BackColor;
            BackColorStorage= MyDictionary.DefaultBlocksSettings[Shape].BackColor;
            FontColor = MyDictionary.DefaultBlocksSettings[Shape].FontColor;
            FontSize = MyDictionary.DefaultBlocksSettings[Shape].FontSize;
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
                    if (Shape == MyDictionary.Shape.Decision)
                        Rect = new Rectangle(Rect.Location,
                            new Size((int) (stringSize.Width * 1.7), stringSize.Height * 2));
                    else
                        Rect = new Rectangle(Rect.Location, new Size((int)(stringSize.Width * 1.1), stringSize.Height));
                }
            }
        }


        public bool IsContain(Point location)
        {
            var contains = false;
            if (Shape == MyDictionary.Shape.Start)
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
                    gp.AddLine(new Point(Rect.Location.X, Rect.Location.Y + Rect.Height),
                        new Point(Rect.Location.X + 10, Rect.Location.Y));
                    gp.AddLine(new Point(Rect.Location.X + Rect.Width, Rect.Location.Y),
                        new Point(Rect.Location.X + Rect.Width - 10, Rect.Location.Y + Rect.Height));
                    contains = gp.IsVisible(location);
                }
            }
            else if (Shape == MyDictionary.Shape.Decision)
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
                case (MyDictionary.Shape.Start):
                    DrawStart(g);
                    break;
                case (MyDictionary.Shape.End):
                    DrawEnd(g);
                    break;
                case (MyDictionary.Shape.Input):
                    DrawInput(g);
                    break;
                case (MyDictionary.Shape.Execution):
                    DrawExecution(g);
                    break;
                case (MyDictionary.Shape.Decision):
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
            var maxWidth = Math.Abs((Shape == MyDictionary.Shape.Decision)? (int) (Rect.Width * 0.7f): Rect.Width);
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
                if (Shape == MyDictionary.Shape.Decision)
                    Rect =new Rectangle(Rect.Location, new Size((int) (stringSize.Width * 1.7), stringSize.Height * 2));
                else
                    Rect = new Rectangle(Rect.Location, new Size((int)(stringSize.Width * 1.1), stringSize.Height));
                g.DrawString(Label, font, new SolidBrush(FontColor), Rect,
                    CanvasVariables.BlockStringFormat);
            }
        }

        #endregion
        
    }
}
