﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmlDesigner2.Component.TabsArea.BlockPropertis;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    //krzywo wyrysowują się linie podczas szybkiego przesuwania
    //zaznaczanie przez rect i przesuwanie bez ctrl
    //szybkie zaznaczenie powoduje że niekiedy niektore bloczki nie zmieniają koloru(ale mają true)
    //wprowadzany tekst jako label przekracza szer bloku
    partial class Canvas
    {
        private string _blockClipboardFormat = "CopyOfBlocks";
        private string _lineClipboardFormat = "CopyOfLines";
        private Rectangle SelectRect = Rectangle.Empty;

        private void HideSelectionRect()
        {
            SelectRect = Rectangle.Empty;
            Invalidate();
        }

        public void AddObjectInstant(BlocksData.Shape shape)
        {
            ShapeToDraw = shape;
            if (shape != BlocksData.Shape.ConnectionLine)
            {
                if (ShapeToDraw == BlocksData.Shape.Start)
                {
                    if (!CheckIsStartExist())
                        _canvObj.MyAdd(new Point(Width * 10 / 100, Height * 10 / 100), ShapeToDraw);
                }
                else
                {
                    _canvObj.MyAdd(new Point(Width * 10 / 100, Height * 10 / 100), ShapeToDraw);
                }
                ShapeToDraw = BlocksData.Shape.Nothing;
                Invalidate();
            }
            else
                MessageBox.Show(
                    "Niestety linia nie może zostać dodana poprzez 2xLPM. Należy wybrać linie a następnie wskazać blok początkowy a następnie blok końcowy by powstało połączenie między blokami");
        }

        public void AddObjectAfterClick(BlocksData.Shape shape)
        {
            if (shape == BlocksData.Shape.Start)
            {
                if (!CheckIsStartExist())
                    ShapeToDraw = shape;
            }
            else
                ShapeToDraw = shape;
        }

        private bool CheckIsStartExist()
        {
            for (int i = 0; i < _canvObj.Count; i++)
                if (_canvObj[i].Shape == BlocksData.Shape.Start)
                {
                    MessageBox.Show("Każdy schemat blokowy może posiadać tylko jeden początek (blok startu)");
                    Cursor = Cursors.Default;
                    return true;
                }
            return false;
        }

        public void AbortAddingObject()
        {
            if (ShapeToDraw != BlocksData.Shape.Nothing)
            {
                ShapeToDraw = BlocksData.Shape.Nothing;
                _canvLines.MyAbortAdd(); //usuniecie 1 pkt lini (jeśli jest)
                _rubbers.MyHideRubbers();
            }
        }

        public void SetIsLockedForObject()
        {
            _canvObj.MySetIsLockedForSelectedObj();
            IsMultiSelect = false;
        }

        private void AutoResizeBlockToContent()
        {
            throw new NotImplementedException();
        }
        #region ShortcutsMethods

        public void Delete()
        {
            for (int i = _canvObj.Count - 1; i >= 0; i--)
            {
                if (_canvObj[i].IsSelected && !_canvObj[i].IsLocked)
                {
                    _canvLines.MyRemove(_canvObj[i].ID);
                    _canvObj.MyDelete(i);
                    _rubbers.MyHideRubbers();
                }
            }
            Invalidate();
        }

        public void Copy()
        {
            Clipboard.Clear();
            IDataObject clips = new DataObject();
            clips.SetData(_blockClipboardFormat, _canvObj.MyCopy(_blockClipboardFormat));
            Clipboard.SetDataObject(clips, true);
            clips.SetData(_lineClipboardFormat, _canvLines.MyCopy(_lineClipboardFormat, _blockClipboardFormat));
            Clipboard.Clear();
            Clipboard.SetDataObject(clips, true);
        }

        public void Cut()
        {
            Clipboard.Clear();
            IDataObject clips = new DataObject();
            clips.SetData(_blockClipboardFormat, _canvObj.MyCut(_blockClipboardFormat));
            Clipboard.SetDataObject(clips, true);
            clips.SetData(_lineClipboardFormat, _canvLines.MyCut(_lineClipboardFormat, _blockClipboardFormat));
            Clipboard.Clear();
            Clipboard.SetDataObject(clips, true);

            _rubbers.MyHideRubbers();
            Invalidate();
        }

        public void Paste()
        {
            _canvObj.My_IsSelectedSetForAll(false);
            _rubbers.MyHideRubbers();
            if (Clipboard.ContainsData(_blockClipboardFormat))
            {
                if (Clipboard.ContainsData(_lineClipboardFormat))
                {
                    var blockTemp = (List<MyBlock>) Clipboard.GetData(_blockClipboardFormat);
                    var lineTemp = (List<MyLine>) Clipboard.GetData(_lineClipboardFormat);
                    for (int i = 0; i < blockTemp.Count; i++)
                    {
                        var oldId = blockTemp[i].ID;
                        var newId = _canvObj.MyPaste(blockTemp[i]);
                        if (lineTemp.Count > 0)
                        {
                            lineTemp.Where(w => w.BeginId == oldId).ToList().ForEach(f => f.BeginId = newId);
                            lineTemp.Where(w => w.EndId == oldId).ToList().ForEach(f => f.EndId = newId);
                        }
                    }
                    _canvLines.MyPaste(lineTemp);
                }
                Invalidate();
            }
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void LPM_TryAddObject(Point e)
        {
            if (ShapeToDraw != BlocksData.Shape.Nothing)
            {
                if (ShapeToDraw == BlocksData.Shape.ConnectionLine)
                {
                    _canvLines.MyAdd(e, ref _shapeToDraw, ref _canvObj);
                    _canvObj.My_SelectObjectContainingPoint(e);
                }
                else
                {
                    _canvObj.MyAdd(e, ShapeToDraw);
                    ShapeToDraw = BlocksData.Shape.Nothing;
                }
                if (_canvObj.Count>0) _rubbers.ShowRubbers(_canvObj[0]);
                Invalidate();
            }
        }

        private void LPM_SelectObjectByClick(Point e)
        {
            if (ShapeToDraw == BlocksData.Shape.Nothing)
                if (_canvObj.Count > 0)
                {
                    _canvObj.My_SelectObjectContainingPoint(e);
                    _rubbers.ShowRubbers(_canvObj[0]);
                    Invalidate();
                }
        }

        private bool LPM_MoveObject(Point e)
        {
            if (_canvObj.My_MoveSelectedObjects(ref _mouseDownLocation, e))
            {
                _canvLines.MyUpdate(ref _canvObj);
                if (_canvObj.Count > 0)
                    _rubbers.ShowRubbers(_canvObj[0]); //zawsze index 0 to to ostatni zaznaczony objekt
                _mouseDownLocation = e;
                Invalidate();
                return false;
            }
            return true;
        }

        private void LPM_SelectObjectByRect(Point mouseDown, Point e)
        {
            int bufor;
            if (e.X < mouseDown.X)
            {
                bufor = e.X;
                e.X = mouseDown.X;
                mouseDown.X = bufor;
            }
            if (e.Y < mouseDown.Y)
            {
                bufor = e.Y;
                e.Y = mouseDown.Y;
                mouseDown.Y = bufor;
            }
            SelectRect = new Rectangle(mouseDown.X, mouseDown.Y, e.X - mouseDown.X, e.Y - mouseDown.Y);
            _canvObj.MySelectObjectByRect(SelectRect);
            Invalidate();
        }

        private void PPM_TryAbortAddingObject()
        {
            AbortAddingObject();
        }

        private void PPM_TryShowContextMenu(Point e)
        {
            if (_canvObj.My_IsAnyObjectContainingPoint(e))
            {
                ShowContextMenu(e);
                _rubbers.ShowRubbers(_canvObj[0]);
            }
        }

        private void PPM_SelectForResizinrOrContextMenu(Point e)
        {
            Cursor = Cursors.SizeAll;
            _canvObj.My_SelectObjectContainingPoint(e);

            _ppm = true;
            Invalidate();
        }

        private void PPM_ResizeObject(Point e)
        {
            if (ShapeToDraw != BlocksData.Shape.Nothing) return;
            _ppm = false;
            _canvObj.My_ResizeSelectedObjects(ref _mouseDownLocation, e);
            _canvLines.MyUpdate(ref _canvObj);
            if (_canvObj.Count > 0)
                _rubbers.ShowRubbers(_canvObj[0]); //zawsze index 0 to to ostatni zaznaczony objekt
            _mouseDownLocation = e;
            Invalidate();
        }
    }
}