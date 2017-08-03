using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    partial class Canvas
    {

        public void AddObjectInstant(BlocksData.Shape shape)
        {
            ShapeToDraw = shape;
            if (shape != BlocksData.Shape.ConnectionLine)
            {
                if (ShapeToDraw == BlocksData.Shape.Start)
                {
                    if (!checkIsStartExist())
                        _canvObj.My_AddObj(new Point(Width * 10 / 100, Height * 10 / 100), ref _shapeToDraw);
                }
                else
                {
                    _canvObj.My_AddObj(new Point(Width * 10 / 100, Height * 10 / 100), ref _shapeToDraw);
                }
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
                if (!checkIsStartExist())
                    ShapeToDraw = shape;
            }
            else
            ShapeToDraw = shape;
        }

        private bool checkIsStartExist()
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
                _canvLines.MyAbortAddingLine(ref _shapeToDraw);
                _rubbers.SetRubberVisible(false); //odznaczenie figury ktora była zaznaczona przed próbą(ktora została przerwana) dodania bloku-w przeciwnym razie zostawały gumki
            }
        }

        public void Delete()
        {
            for (int i = 0; i < _canvObj.Count; i++)
            {
                if (_canvObj[i].IsSelected && !_canvObj[i].IsLocked)
                {
                    _canvLines.MyDeleteLine(_canvObj[i].ID);
                    _canvObj.DeleteObj(i);
                    _rubbers.SetRubberVisible(false);
                }
            }
            Invalidate();
        }

        public void SetIsLockedForObject()
        {
            _canvObj[0].IsLocked = !_canvObj[0].IsLocked;
        }
        private void LPM_TryAddObject(Point e)
        {
            if (ShapeToDraw != BlocksData.Shape.Nothing)
            {
                if (ShapeToDraw == BlocksData.Shape.ConnectionLine)
                    _canvLines.MyAddLine(e, ref _shapeToDraw,ref _canvObj);
                else
                    _canvObj.My_AddObj(e, ref _shapeToDraw);
            }
        }

        private void LPM_TrySelectObject(Point e)
        {
            if (ShapeToDraw == BlocksData.Shape.Nothing)
                if (_canvObj.Count > 0)
                {
                    _canvObj.My_SelectObjectContainingPoint(e);
                    _rubbers.ShowRubbers(_canvObj[0]);
                }
        }

        private void LPM_MoveObject(Point e)
        {
            _canvObj.My_MoveSelectedObjects(ref MouseDownLocation, e);
            _canvLines.MyUpdateConnectionsPoints(ref _canvObj);
            if (_canvObj.Count > 0)
                _rubbers.ShowRubbers(_canvObj[0]); //zawsze index 0 to to ostatni zaznaczony objekt
            MouseDownLocation = e;
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
            ppm = true;
        }

        private void PPM_ResizeObject(Point e)
        {
            if (ShapeToDraw != BlocksData.Shape.Nothing) return;
            ppm = false;
            _canvObj.My_ResizeSelectedObjects(ref MouseDownLocation, e);
            if (_canvObj.Count > 0)
                _rubbers.ShowRubbers(_canvObj[0]); //zawsze index 0 to to ostatni zaznaczony objekt
            MouseDownLocation = e;
            Invalidate();
        }
    }
}
