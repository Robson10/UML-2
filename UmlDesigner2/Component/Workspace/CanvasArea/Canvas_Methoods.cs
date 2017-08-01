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
            if (shape != BlocksData.Shape.ConnectionLine)
            {
                _canvObj.Insert(0,
                    (new MyCanvasElements(
                        new Rectangle(Width * 10 / 100, Height * 10 / 100, BlocksData.DefaultSize.Width,
                            BlocksData.DefaultSize.Height), shape)));
                ShapeToDraw = BlocksData.Shape.Nothing;
                Invalidate();
            }
            else
                MessageBox.Show(
                    "Niestety linia nie może zostać dodana poprzez 2xLPM. Należy wybrać linie a następnie wskazać blok początkowy a następnie blok końcowy by powstało połączenie między blokami");
        }

        public void AddObjectAfterClick(BlocksData.Shape shape)
        {
            ShapeToDraw = shape;
        }

        public void AbortAddingObject()
        {
            if (ShapeToDraw != BlocksData.Shape.Nothing)
            {
                _canvObj.My_AbortAddingObj(ref _shapeToDraw);
                _rubbers.ShowRubbers(_canvObj[0]);//odznaczenie figury ktora była zaznaczona przed próbą(ktora została przerwana) dodania bloku-w przeciwnym razie zostawały gumki
            }
        }

        private void LPM_TryAddObject(Point e)
        {
            if (ShapeToDraw != BlocksData.Shape.Nothing)
            {
                if (ShapeToDraw == BlocksData.Shape.ConnectionLine)
                    _canvObj.My_AddLine(e, ref _shapeToDraw);
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
