using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    class Rubbers : List<UserControl>
    {
        public Rubbers()
        {
            RubbersPresets();
        }

        private void RubbersPresets()
        {
            for (int i = 0; i < 8; i++)
            {
                this.Add(new UserControl());
                this[i].BackColor = System.Drawing.Color.Silver;
                this[i].Visible = false;
                this[i].Size = BlockParameters.RubberSize;
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
        private Point MouseDownLocation_Rubbers;
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
                //CanvasObjects.ResizeSelectedObjectsByRubbers(ref MouseDownLocation_Rubbers, e.Location, (sender as Label).TabIndex);
                //if (CanvasObjects.Count > 0) UpdateRubbers(CanvasObjects[0]);
                //Invalidate();
            }
        }

        /// <summary>
        /// Metoda aktualizująca położenie gumek wywoływana przez event OnResize();
        /// </summary>
        public void ShowRubbers(MyCanvasFigure canvasObject)
        {
            //po "obróceniu" sie figury wyznaczamy inne XY dla gumek

            var centerX = canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width / 2 -
                          BlockParameters.RubberSize.Width / 2;
            var centerY = canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height / 2 -
                          BlockParameters.RubberSize.Height / 2;

            var left = (canvasObject.Rect.Size.Width < 0)
                ? canvasObject.Rect.Location.X
                : canvasObject.Rect.Location.X - BlockParameters.RubberSize.Width;
            var right = (canvasObject.Rect.Size.Width < 0)
                ? canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width - BlockParameters.RubberSize.Width
                : canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width;
            var up = (canvasObject.Rect.Size.Height < 0)
                ? canvasObject.Rect.Location.Y
                : canvasObject.Rect.Location.Y - BlockParameters.RubberSize.Height;
            var down = (canvasObject.Rect.Size.Height < 0)
                ? canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height - BlockParameters.RubberSize.Height
                : canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height;

            Point topLeft = new Point(left, up);
            Point topCenter = new Point(centerX, up);
            Point topRight = new Point(right, up);
            Point centerLeft = new Point(left, centerY);
            Point centerRight = new Point(right, centerY);
            Point bottomLeft = new Point(left, down);
            Point bottomCenter = new Point(centerX, down);
            Point bottomRight = new Point(right, down);

            this[0].Location = topLeft;
            this[1].Location = topCenter;
            this[2].Location = topRight;
            this[3].Location = centerRight;
            this[4].Location = bottomRight;
            this[5].Location = bottomCenter;
            this[6].Location = bottomLeft;
            this[7].Location = centerLeft;
            UpdateRubberVisible(canvasObject.IsSelected);

        }

        /// <summary>
        /// Metoda aktualizująca widoczność gumek wywoływana w geterze IsSelected
        /// </summary>
        private void UpdateRubberVisible(bool isSelected)
        {
            for (int i = 0; i < this.Count; i++)
                this[i].Visible = isSelected;
        }

        public void AddRubbersToControl(Control control)
        {
            for (int i = 0; i < this.Count; i++)
            {
                control.Controls.Add(this[i]);
            }
        }
    }
}
