using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    //zaznaczanie przez rect i przesuwanie bez ctrl???
    partial class Canvas
    {
        //po cofnieciu utworzenia bloku musze także sprawdzić czy linia nie straciła pkt przyłączenia jak tak usuwamy ją.(przy przywracaniu lini zebym nie miał 2 lini na raz)
        private Rectangle SelectRect = Rectangle.Empty;
        protected virtual void OnHideBlockProperties()
        {
            HideBlockPoperites?.Invoke(null, EventArgs.Empty);
        }
        public event EventHandler HideBlockPoperites;
        protected virtual void OnShowBlockProperties()
        {
            ShowBlockPoperites?.Invoke(CanvObj[0], EventArgs.Empty);
        }
        public event EventHandler ShowBlockPoperites;

        public void ClearCanvas()
        {
            // Metoda wywoływana podczas rozpoczęcia egzaminu - czyści wszystko w canvas
            // moze być uzywana do tworzenia nowego pliku
            CanvLines.Clear();
            CanvObj.Clear();
            Clipboard.Clear();
            _rubbers.MyHideRubbers();
            History.Clear();//wyczyszczenie historii ctrl z/y
            Invalidate();
        }

        //todo X - AllProperties
        public void UpdatePropertiesSelectedBlock()
        {
            //Metoda służąca do zaktualizowania zaznaczonego bloku 
            //z poziomu form1 które reaguje na event z BlockProperties
            CanvObj[0].UpdateRectSizeOnAutoresize();
            CanvLines.MyUpdate(ref CanvObj);
            _rubbers.ShowRubbers(CanvObj[0], AutoScrollPosition);
            Invalidate();
        }

        //todo VX- mozliwe tu nic
        private void ShowProperties()
        {
            if (CanvObj.Count > 0)
                if (!CanvObj[0].IsSelected || (CanvObj.Count > 1 && CanvObj[1].IsSelected))
                    OnHideBlockProperties();
                else
                    OnShowBlockProperties();
        }

        //V
        private void HideSelectionRect()
        {
            SelectRect = Rectangle.Empty;
            Invalidate();
        }

        //todo V - Add
        public void AddObjectInstant(Helper.Shape shape)
        {
            ShapeToDraw = shape;
            if (shape != Helper.Shape.ConnectionLine)
            {
                if (ShapeToDraw == Helper.Shape.Start)
                {
                    if (!CheckIsStartExist())
                    {
                        CanvObj.MyAdd(new Point(Width * 10 / 100, Height * 10 / 100), ShapeToDraw);
                        History.Push(new List<HistoryItem>(){new HistoryItem(MyAction.Add,CanvObj[0])});
                    }
                }
                else
                {
                    CanvObj.MyAdd(new Point(Width * 10 / 100, Height * 10 / 100), ShapeToDraw);
                    History.Push(new List<HistoryItem>() { new HistoryItem(MyAction.Add, CanvObj[0]) });
                }
                ShapeToDraw = Helper.Shape.Nothing;
                Invalidate();
            }
            else
                MessageBox.Show(
                    "Niestety linia nie może zostać dodana poprzez 2xLPM. Należy wybrać linie a następnie wskazać blok początkowy a następnie blok końcowy by powstało połączenie między blokami");
        }

        //V
        public void AddObjectAfterClick(Helper.Shape shape)
        {
            if (shape == Helper.Shape.Start)
            {
                if (!CheckIsStartExist())
                    ShapeToDraw = shape;
            }
            else
                ShapeToDraw = shape;
        }

        //V
        private bool CheckIsStartExist()
        {
            for (int i = 0; i < CanvObj.Count; i++)
                if (CanvObj[i].Shape == Helper.Shape.Start)
                {
                    MessageBox.Show("Każdy schemat blokowy może posiadać tylko jeden początek (blok startu)");
                    Cursor = Cursors.Default;
                    return true;
                }
            return false;
        }

        //V
        public void AbortAddingObject()
        {
            if (ShapeToDraw != Helper.Shape.Nothing)
            {
                ShapeToDraw = Helper.Shape.Nothing;
                CanvLines.MyAbortAdd(); //usuniecie 1 pkt lini (jeśli jest)
                _rubbers.MyHideRubbers();
            }
        }

        //todo X - Edit - Locked
        private void SetIsLockedForObject()
        {
            CanvObj.MySetIsLockedForSelectedObj();
            IsMultiSelect = false;
        }
        //todo X - Edit -Autoresize
        private void AutoResizeBlockToContent()
        {
            CanvObj.Where(x => !x.IsLocked && x.IsSelected).ToList().ForEach(x=>x.AutoResize=!x.AutoResize);
            for (int i = 0; i < CanvObj.Count; i++)
            {
                CanvObj[i].UpdateRectSizeOnAutoresize();
            }
            CanvLines.MyUpdate(ref CanvObj);
            _rubbers.ShowRubbers(CanvObj[0], AutoScrollPosition);

            History.Push(CanvObj.ToListHistory(MyAction.Edit));
            ShowProperties();
            Invalidate();
        }

        #region ShortcutsMethods

        //todo V
        public void Delete()
        {
            History.Push(CanvObj.ToListHistory(MyAction.Delete));
            for (int i = CanvObj.Count - 1; i >= 0; i--)
            {
                if (CanvObj[i].IsSelected && !CanvObj[i].IsLocked)
                {
                    CanvLines.MyRemove(CanvObj[i].ID);
                    CanvObj.MyDelete(i);
                    _rubbers.MyHideRubbers();
                    OnHideBlockProperties();
                }
            }
            Invalidate();
        }

        //V przy paste sie zrobilo 
        public void Copy()
        {
            Clipboard.Clear();
            IDataObject clips = new DataObject();
            clips.SetData(Helper.BlockClipboardFormat, CanvObj.MyCopy(Helper.BlockClipboardFormat));
            Clipboard.SetDataObject(clips, true);
            clips.SetData(Helper.LineClipboardFormat, CanvLines.MyCopy(Helper.LineClipboardFormat, Helper.BlockClipboardFormat));
            Clipboard.Clear();
            Clipboard.SetDataObject(clips, true);
        }

        //todo V
        public void Cut()
        {
            Clipboard.Clear();
            IDataObject clips = new DataObject();
            History.Push(CanvObj.ToListHistory(MyAction.Cut));
            clips.SetData(Helper.BlockClipboardFormat, CanvObj.MyCut(Helper.BlockClipboardFormat));
            Clipboard.SetDataObject(clips, true);

            clips.SetData(Helper.LineClipboardFormat, CanvLines.MyCut(Helper.LineClipboardFormat, Helper.BlockClipboardFormat));
            Clipboard.Clear();
            Clipboard.SetDataObject(clips, true);

            _rubbers.MyHideRubbers();
            OnHideBlockProperties();
            Invalidate();
        }

        //todo V
        public void Paste()
        {
            CanvObj.My_IsSelectedSetForAll(false);
            _rubbers.MyHideRubbers();
            if (Clipboard.ContainsData(Helper.BlockClipboardFormat))
            {
                if (Clipboard.ContainsData(Helper.LineClipboardFormat))
                {
                    var blockTemp = (List<MyBlock>) Clipboard.GetData(Helper.BlockClipboardFormat);
                    var lineTemp = (List<MyLine>) Clipboard.GetData(Helper.LineClipboardFormat);
                    if (blockTemp.Count == 0) return;//jezeli nie ma nic do wklejenia to pomiń
                    for (int i = 0; i < blockTemp.Count; i++)
                    {
                        var oldId = blockTemp[i].ID;
                        var newId = CanvObj.MyPaste(blockTemp[i]);
                        if (lineTemp.Count > 0)
                        {
                            lineTemp.Where(w => w.BeginId == oldId).ToList().ForEach(f => f.BeginId = newId);
                            lineTemp.Where(w => w.EndId == oldId).ToList().ForEach(f => f.EndId = newId);
                        }
                    }
                    CanvLines.MyPaste(lineTemp);
                    History.Push(CanvObj.ToListHistory(MyAction.Add));
                    if (CanvObj.Count > 1 && CanvObj[1].IsSelected)
                        OnHideBlockProperties();
                    else
                    {
                        OnShowBlockProperties();
                    }
                }
                Invalidate();
            }
        }

        public void Undo()
        {
            bool showProperties = false;
            var temp = History.Cofnij();
            temp = (temp?[0].MyActionType == MyAction.EditSize || 
                temp?[0].MyActionType == MyAction.Move ||
                temp?[0].MyActionType == MyAction.Edit) ? History.Cofnij():temp;//podwojny pop na EditSize.NWM czemu
            if (temp == null) return;
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].Block != null)//blok
                {
                    if (temp[i].MyActionType == MyAction.Add)//został dodany wiec usuń blok
                    {
                        CanvObj.MyDeleteByID(temp[i].Block.ID);
                    }
                    else if (temp[i].MyActionType == MyAction.EditSize)
                    {
                        var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                        CanvObj[index].Rect = temp[i].Block.Rect;
                    }
                    else if (temp[i].MyActionType == MyAction.Cut)//został dodany wiec usuń blok
                    {
                        CanvObj.Add(temp[i].Block);//wykorzystujemy metode domyslną ponieważ CanvObj.MyAdd zmieniłaby ID bloku
                    }
                    else if (temp[i].MyActionType == MyAction.Delete)
                    {
                        CanvObj.Add(temp[i].Block);
                    }
                    else if (temp[i].MyActionType == MyAction.Move)
                    {
                        var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                        CanvObj[index].Rect= temp[i].Block.Rect;
                    }
                    else if (temp[i].MyActionType==MyAction.Edit)
                    {
                        var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                        CanvObj[index].Label = temp[i].Block.Label;
                        CanvObj[index].AutoResize = temp[i].Block.AutoResize;
                        CanvObj[index].BackColor = temp[i].Block.BackColor;
                        CanvObj[index].BackColorStorage = temp[i].Block.BackColorStorage;
                        CanvObj[index].Code = temp[i].Block.Code;
                        CanvObj[index].FontColor = temp[i].Block.FontColor;
                        CanvObj[index].FontSize = temp[i].Block.FontSize;
                        CanvObj[index].IsLocked = temp[i].Block.IsLocked;
                        CanvObj[index].IsSelected = temp[i].Block.IsSelected;
                        CanvObj[index].Shape = temp[i].Block.Shape;
                        CanvObj[index].Rect = temp[i].Block.Rect;
                        showProperties = true;
                    }

                }
                else//linia
                {
                    
                }
            }
            CanvLines.MyUpdate(ref CanvObj);
            _rubbers.MyHideRubbers();
            if (!showProperties)
            {
                OnHideBlockProperties();
            }
            else
            { ShowProperties(); }
            Invalidate();
        }
        //zmiana rozmiaru nwm czemu musi zczytać 2 razy stos
        public void Redo()
        {
            bool showProperties = false;
            var temp = History.DoPrzodu();
            temp = (
                temp?[0].MyActionType == MyAction.EditSize || 
                temp?[0].MyActionType == MyAction.Move||
                temp?[0].MyActionType == MyAction.Edit) ? History.DoPrzodu() : temp;
            if (temp == null) return;
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].Block != null)//blok
                {
                    if (temp[i].MyActionType == MyAction.Add)//został dodany wiec usuń blok
                    {
                        CanvObj.Add(temp[i].Block);//wykorzystujemy metode domyslną ponieważ CanvObj.MyAdd zmieniłaby ID bloku
                    }
                    else if (temp[i].MyActionType == MyAction.EditSize)
                    {
                        var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                        CanvObj[index].Rect = temp[i].Block.Rect;
                    }
                    else if (temp[i].MyActionType == MyAction.Cut)
                    {
                        CanvObj.MyDeleteByID(temp[i].Block.ID);
                    }
                    else if (temp[i].MyActionType == MyAction.Delete)
                    {
                        CanvObj.MyDeleteByID(temp[i].Block.ID);
                    }
                    else if (temp[i].MyActionType == MyAction.Move)
                    {
                        var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                        CanvObj[index].Rect = temp[i].Block.Rect;
                    }
                    else if (temp[i].MyActionType == MyAction.Edit)
                    {
                        var index = CanvObj.FindIndex(x => x.ID == temp[i].Block.ID);
                        CanvObj[index].Label = temp[i].Block.Label;
                        CanvObj[index].AutoResize = temp[i].Block.AutoResize;
                        CanvObj[index].BackColor = temp[i].Block.BackColor;
                        CanvObj[index].BackColorStorage = temp[i].Block.BackColorStorage;
                        CanvObj[index].Code = temp[i].Block.Code;
                        CanvObj[index].FontColor = temp[i].Block.FontColor;
                        CanvObj[index].FontSize = temp[i].Block.FontSize;
                        CanvObj[index].IsLocked = temp[i].Block.IsLocked;
                        CanvObj[index].IsSelected = temp[i].Block.IsSelected;
                        CanvObj[index].Shape = temp[i].Block.Shape;
                        CanvObj[index].Rect = temp[i].Block.Rect;
                        showProperties = true;
                    }
                }
                else//linia
                {

                }
            }
            CanvLines.MyUpdate(ref CanvObj);
            if (!showProperties)
            {
                OnHideBlockProperties();
            }
            else
            { ShowProperties(); }
            Invalidate();
        }

        #endregion
        //todo V - ADD
        private void LPM_TryAddObject(Point e)
        {
            if (ShapeToDraw != Helper.Shape.Nothing)
            {
                var _scrolledPoint = new Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y);
                if (ShapeToDraw == Helper.Shape.ConnectionLine)
                {
                    CanvLines.MyAdd(_scrolledPoint, ref _shapeToDraw, ref CanvObj);
                    CanvObj.My_SelectObjectContainingPoint(_scrolledPoint);
                }
                else
                {
                    CanvObj.MyAdd(_scrolledPoint, ShapeToDraw);
                    History.Push(new List<HistoryItem>() { new HistoryItem(MyAction.Add, CanvObj[0]) });
                    ShapeToDraw = Helper.Shape.Nothing;
                }
                if (CanvObj.Count>0) _rubbers.ShowRubbers(CanvObj[0], AutoScrollPosition);
                ShowProperties();
                Invalidate();
            }
        }
        private void LPM_SelectObjectByClick(Point e)
        {
            if (ShapeToDraw == Helper.Shape.Nothing)
                if (CanvObj.Count > 0)
                {
                    var _scrolledPoint = new Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y);
                    CanvObj.My_SelectObjectContainingPoint(_scrolledPoint);
                    _rubbers.ShowRubbers(CanvObj[0], AutoScrollPosition);
                    Invalidate();
                }
        }
        bool isMoved = false;
        private bool LPM_MoveObject(Point e)
        {
            var _scrolledPoint = new Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y);
            if (CanvObj.GetSelectedItems().Count>0)
            {
               if (!isMoved)
                {
                    History.Push(CanvObj.ToListHistory(MyAction.Move));
                    isMoved = true;
                }
                CanvObj.My_MoveSelectedObjects(ref _mouseDownLocation, _scrolledPoint);
                CanvLines.MyUpdate(ref CanvObj);
                if (CanvObj.Count > 0)
                    _rubbers.ShowRubbers(CanvObj[0], AutoScrollPosition); //zawsze index 0 to to ostatni zaznaczony objekt
                _mouseDownLocation = _scrolledPoint;
                Invalidate();
                return false;
            }
            return true;
        }

        //V - reczej nic
        private void LPM_SelectObjectByRect(Point mouseDown, Point e)
        {
            var _scrolledPoint = new Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y);
            int bufor;
            if (_scrolledPoint.X < mouseDown.X)
            {
                bufor = _scrolledPoint.X;
                _scrolledPoint.X = mouseDown.X;
                mouseDown.X = bufor;
            }
            if (_scrolledPoint.Y < mouseDown.Y)
            {
                bufor = _scrolledPoint.Y;
                _scrolledPoint.Y = mouseDown.Y;
                mouseDown.Y = bufor;
            }
            SelectRect = new Rectangle(mouseDown.X, mouseDown.Y, _scrolledPoint.X - mouseDown.X, _scrolledPoint.Y - mouseDown.Y);
            CanvObj.MySelectObjectByRect(SelectRect);
            Invalidate();
        }

        //V
        private void PPM_TryAbortAddingObject()
        {
            AbortAddingObject();
        }

        //V
        //private void PPM_TryShowContextMenu(Point e)
        //{
        //    var _scrolledPoint = new Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y);
        //    if (CanvObj.My_IsAnyObjectContainingPoint(_scrolledPoint))
        //    {
        //        ShowContextMenu(e);
        //        _rubbers.ShowRubbers(CanvObj[0], AutoScrollPosition);
        //    }
        //}

        //todo - EditSize +linie
        private void PPM_SelectForResizeOrContextMenu(Point e)
        {
            Cursor = Cursors.SizeAll;
            var _scrolledPoint = new Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y);
            CanvObj.My_SelectObjectContainingPoint(_scrolledPoint);
            _ppm = true;
            History.Push(CanvObj.ToListHistory(MyAction.EditSize));
            Invalidate();
        }

        bool sizeChanged = false;// zmienna służąca do rozpoznania czy rozmiar jakieś kontrolki został zmieniony
        //V
        private void PPM_ResizeObject(Point e)
        {
            if (ShapeToDraw != Helper.Shape.Nothing) return;
            _ppm = false;
            CanvObj.My_ResizeSelectedObjects(ref _mouseDownLocation, e);
            CanvLines.MyUpdate(ref CanvObj);
            if (CanvObj.Count > 0)
                _rubbers.ShowRubbers(CanvObj[0], AutoScrollPosition); //zawsze index 0 to to ostatni zaznaczony objekt
            _mouseDownLocation = e;
            sizeChanged = true;
            Invalidate();
        }
    }
}