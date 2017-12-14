﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    class Rubbers : List<UserControl>
    {
        private ListCanvasBlocks _canvasBlocks;
        private Point MouseDownLocation_Rubbers;
        private Point AutoScrollPosition;

        public Rubbers(ref ListCanvasBlocks _listCanvasBlocks)
        {
            _canvasBlocks = _listCanvasBlocks;
            RubbersPresets();
        }
      
        /// <summary>
        /// Metoda ustawiająca gumki.Wywoływana w konstruktorze.
        /// </summary>
        private void RubbersPresets()
        {
            for (int i = 0; i < 8; i++)
            {
                Add(new UserControl());
                this[i].BackColor = Helper.RubberColor;
                this[i].Visible = false;
                this[i].Size = Helper.RubberSize;
                this[i].TabIndex = i;
                this[i].MouseDown += Rubbers_MouseDown;
                this[i].MouseMove += Rubbers_MouseMove;
                this[i].MouseUp += Rubbers_MouseUp;
            }
            this[0].Cursor = Cursors.SizeNWSE;
            this[1].Cursor = Cursors.SizeNS;
            this[2].Cursor = Cursors.SizeNESW;
            this[3].Cursor = Cursors.SizeWE;
            this[4].Cursor = Cursors.SizeNWSE;
            this[5].Cursor = Cursors.SizeNS;
            this[6].Cursor = Cursors.SizeNESW;
            this[7].Cursor = Cursors.SizeWE;
        }

        private void Rubbers_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            UndoRedo.Push(_canvasBlocks.ToListHistory(MyAction.EditSize));
        }


        /// <summary>
        /// Metoda służąca do zapisania miejsca wcisniecia LPM na 1z8 gumek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rubbers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                UndoRedo.Push(_canvasBlocks.ToListHistory(MyAction.EditSize));
                MouseDownLocation_Rubbers = e.Location;
            }
        }

        /// <summary>
        /// Metoda służąca do zmiany rozmiaru kontrolki w wyniku ciągnięcia jednej z gumek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rubbers_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var _scrolledPoint = e.Location;
                _canvasBlocks.My_ResizeSelectedObjectsByRubbers(ref MouseDownLocation_Rubbers, _scrolledPoint,
                    (sender as UserControl).TabIndex);
                if (_canvasBlocks.Count > 0)
                    ShowRubbers(_canvasBlocks[0],AutoScrollPosition);
                this[0].Parent.Invalidate();
            }
        }

        /// <summary>
        /// Metoda aktualizująca(wyliczająca) położenie gumek po kliknięciu na blok oraz po zmianie jego rozmiaru. Dodatkowo wywołuje metodę odpowiedzialną za widoczność gumek.
        /// </summary>
        public void ShowRubbers(MyBlock canvasObject,Point autoScrollPosition)
        {
            AutoScrollPosition = autoScrollPosition;
            if (canvasObject.IsSelected)
            {
                var centerX = canvasObject.Rect.Location.X+ autoScrollPosition.X + canvasObject.Rect.Size.Width / 2 -
                              Helper.RubberSize.Width / 2;
                var centerY = canvasObject.Rect.Location.Y+ autoScrollPosition.Y + canvasObject.Rect.Size.Height / 2 -
                              Helper.RubberSize.Height / 2;

                var left = canvasObject.Rect.Location.X - Helper.RubberSize.Width+ autoScrollPosition.X;
                var right = canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width+ autoScrollPosition.X;
                var up = canvasObject.Rect.Location.Y - Helper.RubberSize.Height+ autoScrollPosition.Y;
                var down = canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height+ autoScrollPosition.Y;

                var topLeft = new Point(left, up);
                var topCenter = new Point(centerX, up);
                var topRight = new Point(right, up);
                var centerLeft = new Point(left, centerY);
                var centerRight = new Point(right, centerY);
                var bottomLeft = new Point(left, down);
                var bottomCenter = new Point(centerX, down);
                var bottomRight = new Point(right, down);

                this[0].Location = topLeft;
                this[1].Location = topCenter;
                this[2].Location = topRight;
                this[3].Location = centerRight;
                this[4].Location = bottomRight;
                this[5].Location = bottomCenter;
                this[6].Location = bottomLeft;
                this[7].Location = centerLeft;
                SetRubberVisible(canvasObject.IsSelected);
            }
            else
                SetRubberVisible(false);
        }

        /// <summary>
        /// Metoda aktualizująca widoczność gumek zgodnie z parametrem isSelected
        /// </summary>
        private void SetRubberVisible(bool isSelected)
        {
            for (int i = 0; i < Count; i++)
                this[i].Visible = isSelected;
        }

        /// <summary>
        /// Metoda ukrywająca gumki
        /// </summary>
        public void MyHideRubbers()
        {
            for (int i = 0; i < Count; i++)
                this[i].Visible = false;
        }

        /// <summary>
        /// Metoda służąca do dodania gumek do Kontrolki (zastępuje komędę typu Controls.Add(Rubbers[i])
        /// </summary>
        /// <param name="control"></param>
        public void AddRubbersToControl(Control control)
        {
            for (int i = 0; i < Count; i++)
            {
                control.Controls.Add(this[i]);
            }
        }
    }

}