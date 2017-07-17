using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.Canvas
{
    class Rubbers : List<UserControl>
    {
        public Rubbers()
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
                this[i].HandleCreated += Rubbers_HandleCreated;
            }
            RubbersPresets();
        }

        private void Rubbers_HandleCreated(object sender, EventArgs e)
        {
            //Controls.Add(Rubbers[i]); dodanie do canvasu
        }

        private void RubbersPresets()
        {
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
        private void UpdateRubbers(MyCanvasFigure canvasObject)
        {
            //po "obróceniu" sie figury wyznaczamy inne XY dla gumek

            int left, right, up, down;
            int centerX = canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width / 2 - BlockParameters.RubberSize.Width / 2;
            int centerY = canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height / 2 - BlockParameters.RubberSize.Height / 2;
            if (canvasObject.Rect.Size.Width < 0)
            {
                left = canvasObject.Rect.Location.X;
                right = canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width - BlockParameters.RubberSize.Width;
            }
            else
            {
                left = canvasObject.Rect.Location.X - BlockParameters.RubberSize.Width;
                right = canvasObject.Rect.Location.X + canvasObject.Rect.Size.Width;
            }
            if (canvasObject.Rect.Size.Height < 0)
            {
                up = canvasObject.Rect.Location.Y;
                down = canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height - BlockParameters.RubberSize.Height;
            }
            else
            {
                up = canvasObject.Rect.Location.Y - BlockParameters.RubberSize.Height;
                down = canvasObject.Rect.Location.Y + canvasObject.Rect.Size.Height;
            }
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
    }
}
