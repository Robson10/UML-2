using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmlDesigner2.Component.TabsArea.BlockPropertis;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    //zaznaczanie przez rect i przesuwanie bez ctrl???
    partial class Canvas
    {
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
            //wyczyszczenie historii ctrl z/y
            Invalidate();
        }

        public void UpdatePropertiesSelectedBlock()
        {
            //Metoda służąca do zaktualizowania zaznaczonego bloku 
            //z poziomu form1 które reaguje na event z BlockProperties
            CanvObj[0].UpdateRectSizeOnAutoresize();
            CanvLines.MyUpdate(ref CanvObj);
            _rubbers.ShowRubbers(CanvObj[0], AutoScrollPosition);
            Invalidate();
        }

        private void ShowProperties()
        {
            if (CanvObj.Count > 0)
                if (!CanvObj[0].IsSelected || (CanvObj.Count > 1 && CanvObj[1].IsSelected))
                    OnHideBlockProperties();
                else
                    OnShowBlockProperties();
                
        }

        private void HideSelectionRect()
        {
            SelectRect = Rectangle.Empty;
            Invalidate();
        }

        public void AddObjectInstant(Helper.Shape shape)
        {
            ShapeToDraw = shape;
            if (shape != Helper.Shape.ConnectionLine)
            {
                if (ShapeToDraw == Helper.Shape.Start)
                {
                    if (!CheckIsStartExist())
                        CanvObj.MyAdd(new Point(Width * 10 / 100, Height * 10 / 100), ShapeToDraw);
                }
                else
                {
                    CanvObj.MyAdd(new Point(Width * 10 / 100, Height * 10 / 100), ShapeToDraw);
                }
                ShapeToDraw = Helper.Shape.Nothing;
                Invalidate();
            }
            else
                MessageBox.Show(
                    "Niestety linia nie może zostać dodana poprzez 2xLPM. Należy wybrać linie a następnie wskazać blok początkowy a następnie blok końcowy by powstało połączenie między blokami");
        }

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

        public void AbortAddingObject()
        {
            if (ShapeToDraw != Helper.Shape.Nothing)
            {
                ShapeToDraw = Helper.Shape.Nothing;
                CanvLines.MyAbortAdd(); //usuniecie 1 pkt lini (jeśli jest)
                _rubbers.MyHideRubbers();
            }
        }

        private void SetIsLockedForObject()
        {
            CanvObj.MySetIsLockedForSelectedObj();
            IsMultiSelect = false;
        }

        private void AutoResizeBlockToContent()
        {
            CanvObj.Where(x => !x.IsLocked && x.IsSelected).ToList().ForEach(x=>x.AutoResize=!x.AutoResize);
            ShowProperties();
        }

        #region ShortcutsMethods

        public void Delete()
        {
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

        public void Cut()
        {
            Clipboard.Clear();
            IDataObject clips = new DataObject();
            clips.SetData(Helper.BlockClipboardFormat, CanvObj.MyCut(Helper.BlockClipboardFormat));
            Clipboard.SetDataObject(clips, true);
            clips.SetData(Helper.LineClipboardFormat, CanvLines.MyCut(Helper.LineClipboardFormat, Helper.BlockClipboardFormat));
            Clipboard.Clear();
            Clipboard.SetDataObject(clips, true);

            _rubbers.MyHideRubbers();
            OnHideBlockProperties();
            Invalidate();
        }

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
                    if (CanvObj.Count > 1 && CanvObj[1].IsSelected)
                        OnHideBlockProperties();
                    else
                        OnShowBlockProperties();
                }
                Invalidate();
            }
        }

        public void Undo()
        {
            //dodanie wyczyszczenia historii do metody ClearCanvas() -> rozpoczecie egzaminu
            throw new NotImplementedException();
            OnHideBlockProperties();
        }

        public void Redo()
        {
            throw new NotImplementedException();
            OnHideBlockProperties();
        }

        #endregion

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

        private bool LPM_MoveObject(Point e)
        {

            var _scrolledPoint = new Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y);
            if (CanvObj.My_MoveSelectedObjects(ref _mouseDownLocation, _scrolledPoint))
            {
                CanvLines.MyUpdate(ref CanvObj);
                if (CanvObj.Count > 0)
                    _rubbers.ShowRubbers(CanvObj[0], AutoScrollPosition); //zawsze index 0 to to ostatni zaznaczony objekt
                _mouseDownLocation = _scrolledPoint;
                Invalidate();
                return false;
            }
            return true;
        }

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

        private void PPM_TryAbortAddingObject()
        {
            AbortAddingObject();
        }

        private void PPM_TryShowContextMenu(Point e)
        {
            var _scrolledPoint = new Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y);
            if (CanvObj.My_IsAnyObjectContainingPoint(_scrolledPoint))
            {
                ShowContextMenu(e);
                _rubbers.ShowRubbers(CanvObj[0], AutoScrollPosition);
            }
        }

        private void PPM_SelectForResizeOrContextMenu(Point e)
        {
            Cursor = Cursors.SizeAll;
            var _scrolledPoint = new Point(e.X - AutoScrollPosition.X, e.Y - AutoScrollPosition.Y);
            CanvObj.My_SelectObjectContainingPoint(_scrolledPoint);
            _ppm = true;
            Invalidate();
        }

        private void PPM_ResizeObject(Point e)
        {
            if (ShapeToDraw != Helper.Shape.Nothing) return;
            _ppm = false;
            CanvObj.My_ResizeSelectedObjects(ref _mouseDownLocation, e);
            CanvLines.MyUpdate(ref CanvObj);
            if (CanvObj.Count > 0)
                _rubbers.ShowRubbers(CanvObj[0], AutoScrollPosition); //zawsze index 0 to to ostatni zaznaczony objekt
            _mouseDownLocation = e;
            Invalidate();
        }
    }
}