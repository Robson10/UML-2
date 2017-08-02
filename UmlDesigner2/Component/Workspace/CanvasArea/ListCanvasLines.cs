using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    class ListCanvasLines:List<MyLine>
    {
        //jak sprawdzić czy juz mogę przyłączyć?
        public void My_AddLine(Point e, ref BlocksData.Shape shapeToDraw,ref ListCanvasBlocks listBlocks)
        {
            var temp = listBlocks.TryGetElementContainingPoint(e);
            if (temp!=null)
            {
                if (base.Count > 0 && base[0].EndPoint == Point.Empty) //input
                {
                    if (temp.Shape == BlocksData.Shape.Start)//start nie może mieć wejscia
                    {
                        MessageBox.Show("Blok startu nie może mieć linii wejścia");
                    }
                    else if (this[0].BeginId == temp.ID) //wyjscie na wejscie - zapetlenie
                    {
                        MessageBox.Show("Nie możesz połączyć wyjścia bloku z jego wejściem - Zapętlenie");
                    }
                    else
                    {
                        this[0].EndPoint = temp.PointInput;
                        this[0].EndId = temp.ID;
                        shapeToDraw = BlocksData.Shape.Nothing;
                    }
                }
                else //output
                {
                    if (temp.Shape == BlocksData.Shape.Decision)
                    {
                        DialogResult dialogResult =
                            MessageBox.Show("Czy ma być to linia dla prawdy (true)", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            for (int i = 0; i < Count; i++)
                            {
                                if (this[i].BeginId == temp.ID && this[i].IsTrue) //już istnieje taka linia
                                {
                                    //MessageBox.Show("Ten blok nie może mieć więcej niż 1 wyjście Prawdy");
                                    this.RemoveAt(i);
                                    //return;
                                }
                            }
                            Insert(0, new MyLine(temp.PointOutput1, temp.ID, true));
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            for (int i = 0; i < Count; i++)
                            {
                                if (this[i].BeginId == temp.ID && !this[i].IsTrue) //już istnieje taka linia
                                {
                                    //MessageBox.Show("Ten blok nie może mieć więcej niż 1 wyjście Fałszu");
                                    this.RemoveAt(i);
                                    //return;
                                }
                            }
                            Insert(0, new MyLine(temp.PointOutput2, temp.ID, false));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < Count; i++)
                        {
                            if (this[i].BeginId == temp.ID) //już istnieje taka linia
                            {
                                //MessageBox.Show("ten blok nie może mieć więcej niż 1 wyjście");
                                this.RemoveAt(i);
                                //return;
                            }
                        }
                        Insert(0, new MyLine(temp.PointOutput1, temp.ID));
                    }
                }
            }
        }

        public void UpdateConnectionsPoints(ref ListCanvasBlocks listBlocks)
        {
            if (listBlocks.Count > 0)
            {
                for (int i = 0; i < Count; i++)
                {
                    var temp1 = listBlocks.TryGetElementWithId(this[i].BeginId);
                    if (temp1.Shape == BlocksData.Shape.Decision)
                    {
                        if (this[i].IsTrue)
                            this[i].BeginPoint = temp1.PointOutput1;
                        else
                            this[i].BeginPoint = temp1.PointOutput2;
                    }
                    else
                    {
                        this[i].BeginPoint = temp1.PointOutput1;
                    }
                    var temp2 = listBlocks.TryGetElementWithId(this[i].EndId);
                    this[i].EndPoint = temp2.PointInput;
                }
            }
        }

        public void My_AbortAddingLine(ref BlocksData.Shape shapeToDraw)
        {
            if (this.Count > 0 && this[0].EndPoint == Point.Empty)
                base.RemoveAt(0);
            shapeToDraw = BlocksData.Shape.Nothing;
        }
    }

    public class MyLine
    {
        public Point BeginPoint;
        public Point EndPoint = Point.Empty;
        public int BeginId, EndId;
        public bool IsSelected = false; //czy jest zaznaczona
        public SolidBrush BackColor;
        public bool IsTrue = true;
        public MyLine(Point beginPoint,int beginId)
        {
            BeginPoint = beginPoint;
            BackColor = BlocksData.BackColor(BlocksData.Shape.ConnectionLine);
            BeginId = beginId;
        }

        public MyLine(Point beginPoint, int beginId, bool isTrue)
        {
            BeginPoint = beginPoint;
            BackColor = (isTrue)?BlocksData.TrueLineBackColor:BlocksData.FalseLineBackColor;
            BeginId = beginId;
            IsTrue = isTrue;
        }

        public bool My_IsContain()
        {
            var contains = false;
           
                //    using (var gp = new System.Drawing.Drawing2D.GraphicsPath())
                //    {
                //        if (Rect.Width != 0)
                //        {
                //            var YBreak = Math.Abs(Rect.Y - Rect.Height) * 50 / 100;
                //            var XBreak = Math.Abs(Rect.X - Rect.Width) * 50 / 100;
                //            if (Rect.Location.Y > Rect.Height)
                //                if (Rect.Location.X > Rect.Width)
                //                    gp.AddLines(

                //                        new Point[]
                //                        {
                //                            Rect.Location,
                //                            new Point(Rect.Location.X, Rect.Location.Y+YBreak),
                //                            new Point(Rect.Location.X-XBreak,Rect.Location.Y+YBreak),
                //                            new Point(Rect.Width+XBreak,Rect.Height-YBreak),
                //                            new Point(Rect.Width, Rect.Height-YBreak),
                //                            new Point(Rect.Width, Rect.Height)
                //                        });
                //                else
                //                    gp.AddLines(
                //                        new Point[]
                //                        {
                //                            Rect.Location,
                //                            new Point(Rect.Location.X, Rect.Location.Y+YBreak),
                //                            new Point(Rect.Location.X+XBreak,Rect.Location.Y+YBreak),
                //                            new Point(Rect.Width-XBreak,Rect.Height-YBreak),
                //                            new Point(Rect.Width, Rect.Height-YBreak),
                //                            new Point(Rect.Width, Rect.Height)
                //                        });
                //            else
                //                gp.AddLines(
                //                    new Point[]
                //                    {
                //                        Rect.Location,
                //                        new Point(Rect.Location.X, Rect.Location.Y+YBreak),
                //                        new Point(Rect.Width, Rect.Height-YBreak),
                //                        new Point(Rect.Width, Rect.Height)
                //                    });
                //        }
                //        contains = gp.IsVisible(location);
                //    }
           
            return contains;
        }
        public void My_DrawConnectionLine(Graphics g)
        {
            if (EndPoint != Point.Empty)
            {
                var YBreak = Math.Abs(BeginPoint.Y - EndPoint.Y) * 50 / 100;
                var XBreak = Math.Abs(BeginPoint.X - EndPoint.X) * 50 / 100;
                if (BeginPoint.Y > EndPoint.Y)
                    if (BeginPoint.X > EndPoint.X)
                        g.DrawLines(
                            new Pen(BackColor, 4),
                            new []
                            {
                                BeginPoint,
                                new Point(BeginPoint.X, BeginPoint.Y+YBreak),
                                new Point(BeginPoint.X-XBreak,BeginPoint.Y+YBreak),
                                new Point(EndPoint.X+XBreak,EndPoint.Y-YBreak),
                                new Point(EndPoint.X, EndPoint.Y-YBreak),
                                new Point(EndPoint.X, EndPoint.Y)
                            });
                    else
                        g.DrawLines(
                            new Pen(BackColor, 4),
                            new []
                            {
                                BeginPoint,
                                new Point(BeginPoint.X, BeginPoint.Y+YBreak),
                                new Point(BeginPoint.X+XBreak,BeginPoint.Y+YBreak),
                                new Point(EndPoint.X-XBreak,EndPoint.Y-YBreak),
                                new Point(EndPoint.X, EndPoint.Y-YBreak),
                                new Point(EndPoint.X, EndPoint.Y)
                            });
                else
                    g.DrawLines(
                        new Pen(BackColor, 4),
                        new []
                        {
                            BeginPoint,
                            new Point(BeginPoint.X, BeginPoint.Y+YBreak),
                            new Point(EndPoint.X, EndPoint.Y-YBreak),
                            new Point(EndPoint.X, EndPoint.Y)
                        });
            }
        }

    }
}
