using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SbWinNew.Class;

namespace SbWinNew.Components.Workspace
{
    public class ListCanvasBlocks : List<MyBlock>
    {
        private static int _id = 0;

        public List<UndoRedoItem> ToListHistory(MyAction action)
        {
            List<UndoRedoItem> temp = new List<UndoRedoItem>();
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].IsSelected)
                {
                    temp.Add(new UndoRedoItem(action, new MyBlock()
                    {
                        AutoResize = this[i].AutoResize,
                        Code = this[i].Code,
                        Includes = this[i].Includes,
                        Variables = this[i].Variables,
                        FontColor = this[i].FontColor,
                        FontColorHTML = this[i].FontColorHTML,
                        FontSize = this[i].FontSize,
                        IsLocked = this[i].IsLocked,
                        IsSelected = this[i].IsSelected,
                        Label = this[i].Label,
                        BackColor = this[i].BackColor,
                        BackColorHTML = this[i].BackColorHTML,
                        BackColorStorage = this[i].BackColorStorage,
                        BackColorStorageHTML = this[i].BackColorStorageHTML,
                        PointOutput1 = this[i].PointOutput1,
                        PointOutput2 = this[i].PointOutput2,
                        PointInput = this[i].PointInput,
                        Shape = this[i].Shape,
                        Rect = this[i].Rect,
                        ID = this[i].ID
                    }, null));
                }
            }
            return temp;
        }

        public void My_SelectObjectContainingPoint(Point location)
        {
            for (int i = 0; i < Count; i++)
                if (base[i].IsContain(location))
                {
                    base[i].IsSelected = true;
                    base[i].BackColor = Helper.DefaultSelectionColor;
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
                    base[i].BackColor = Helper.DefaultSelectionColor;
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
                    if (NewWidth >= Helper.DefaultBlocksSettings[this[i].Shape].MinSize.Width)
                        base[i].Rect = new Rectangle(base[i].Rect.Location,new Size( NewWidth,base[i].Rect.Size.Height));
                    if (NewHeight >= Helper.DefaultBlocksSettings[this[i].Shape].MinSize.Height)
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
                    if (width > Helper.DefaultBlocksSettings[this[i].Shape].MinSize.Width)
                    {
                        base[i].Rect = new Rectangle(base[i].Rect.Location, new Size(width, base[i].Rect.Size.Height));
                        base[i].Rect =new Rectangle( new Point(x, base[i].Rect.Location.Y), base[i].Rect.Size);
                    }
                    if (height > Helper.DefaultBlocksSettings[this[i].Shape].MinSize.Height)
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
                    ? (Helper.DefaultSelectionColor)
                    : (this[i].BackColorStorage);
                base[i].IsSelected = isSelected;
            }
        }

        public void MyAdd(Point e, Helper.Shape shapeToDraw)
        {
            for (int i = 0; i < int.MaxValue; i++)
            {
                if (!this.Exists(x => x.ID == i))
                {
                    _id = i;
                    break;
                }
            }
            Insert(0, 
                new MyBlock(new Rectangle(e.X - Helper.DefaultBlocksSettings[shapeToDraw].BlockSize.Width / 2,
                    e.Y - Helper.DefaultBlocksSettings[shapeToDraw].BlockSize.Height / 2, Helper.DefaultBlocksSettings[shapeToDraw].BlockSize.Width,
                    Helper.DefaultBlocksSettings[shapeToDraw].BlockSize.Height),
                shapeToDraw, _id));
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

        public void MyDelete(int index)
        {
            this.RemoveAt(index);
        }

        public void MyDeleteByID(int id)
        {
            var index= this.FindIndex(x => x.ID == id);
            MyDelete(index);
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
            for (int i = 0; i < int.MaxValue; i++)
            {
                if (!this.Exists(x => x.ID == i))
                {
                    _id = i;
                    break;
                }
            }
            block.ID = _id;
            _id++;
            Insert(0, block);
            return _id - 1;
        }

        public List<MyBlock> GetSelectedItems()
        {
            return this.FindAll(x => x.IsSelected);
        }
    }
}
