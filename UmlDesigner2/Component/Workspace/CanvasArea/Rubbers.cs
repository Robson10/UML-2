using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    class Rubbers : List<UserControl>
    {
        private ListCanvasBlocks _canvasBlocks;
        private Point MouseDownLocation_Rubbers;

        public Rubbers(ref ListCanvasBlocks _listCanvasItems)
        {
            _canvasBlocks = _listCanvasItems;
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
                this[i].BackColor = BlocksData.RubberColor;
                this[i].Visible = false;
                this[i].Size = BlocksData.RubberSize;
                this[i].TabIndex = i;
                this[i].MouseDown += Rubbers_MouseDown;
                this[i].MouseMove += Rubbers_MouseMove;
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

        /// <summary>
        /// Metoda służąca do zapisania miejsca wcisniecia LPM na 1z8 gumek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rubbers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MouseDownLocation_Rubbers = e.Location;
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
                _canvasBlocks.My_ResizeSelectedObjectsByRubbers(ref MouseDownLocation_Rubbers, e.Location,
                    (sender as UserControl).TabIndex);
                if (_canvasBlocks.Count > 0) ShowRubbers(_canvasBlocks[0]);
                this[0].Parent.Invalidate();
            }
        }

        /// <summary>
        /// Metoda aktualizująca(wyliczająca) położenie gumek po kliknięciu na blok oraz po zmianie jego rozmiaru. Dodatkowo wywołuje metodę odpowiedzialną za widoczność gumek.
        /// </summary>
        public void ShowRubbers(MyBlock canvasObject)
        {

            var centerX = canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width / 2 -
                          BlocksData.RubberSize.Width / 2;
            var centerY = canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height / 2 -
                          BlocksData.RubberSize.Height / 2;

            var left = canvasObject.Rect.Location.X - BlocksData.RubberSize.Width;
            var right = canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width;
            var up = canvasObject.Rect.Location.Y - BlocksData.RubberSize.Height;
            var down = canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height;

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

        /// <summary>
        /// Metoda aktualizująca widoczność gumek zgodnie z parametrem isSelected
        /// </summary>
        public void SetRubberVisible(bool isSelected)
        {
            for (int i = 0; i < this.Count; i++)
                this[i].Visible = isSelected;
        }

        /// <summary>
        /// Metoda służąca do dodania gumek do Kontrolki (zastępuje komędę typu Controls.Add(Rubbers[i])
        /// </summary>
        /// <param name="control"></param>
        public void AddRubbersToControl(Control control)
        {
            for (int i = 0; i < this.Count; i++)
            {
                control.Controls.Add(this[i]);
            }
        }
    }
}
