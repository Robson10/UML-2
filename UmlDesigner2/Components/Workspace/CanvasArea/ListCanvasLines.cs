using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace UmlDesigner2.Component.Workspace.CanvasArea
{
    public class ListCanvasLines:List<MyLine>
    {
        public MyLine ToListHistory(int i)
        {

            return new MyLine()
            {
                BackColor = this[i].BackColor,
                BackColorHTML = this[i].BackColorHTML,
                BeginId = this[i].BeginId,
                BeginPoint = this[i].BeginPoint,
                EndId = this[i].EndId,
                EndPoint = this[i].EndPoint,
                IsSelected = this[i].IsSelected,
                IsTrue = this[i].IsTrue
            };
        }

        public List<MyLine> GetLineByID(int blockId)
        {
            List<MyLine> temp;
            try
            {
                temp = FindAll(x => x.BeginId == blockId || x.EndId == blockId);
                return temp;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Metoda dodająca linię do listy
        /// </summary>
        /// <param name="e"></param>
        /// <param name="shapeToDraw"></param>
        /// <param name="listBlocks"></param>
        public void MyAdd(Point e, ref Helper.Shape shapeToDraw,ref ListCanvasBlocks listBlocks)
        {
            var temp = listBlocks.TryGetElementContainingPoint(e);
            if (temp!=null)
            {
                if (Count > 0 && base[0].EndPoint == Point.Empty) //input
                {
                    if (temp.Shape == Helper.Shape.Start)//start nie może mieć wejscia
                        MessageBox.Show("Blok startu nie może mieć linii wejścia");
                    else if (this[0].BeginId == temp.ID) //wyjscie na wejscie - zapetlenie
                        MessageBox.Show("Nie możesz połączyć wyjścia bloku z jego wejściem - Zapętlenie");
                    else
                    {
                        this[0].EndPoint = temp.PointInput;
                        this[0].EndId = temp.ID;
                        UndoRedo.Push(new List<UndoRedoItem>()
                        {
                            new UndoRedoItem(MyAction.Add, null, this.ToListHistory(0))
                        });
                        shapeToDraw = Helper.Shape.Nothing;
                    }
                }
                else //output
                {
                    if (temp.Shape == Helper.Shape.End)//start nie może mieć wejscia
                        MessageBox.Show("Blok Końca nie może mieć linii wyjścia");
                    else if (temp.Shape == Helper.Shape.Decision)
                    {
                        DialogResult dialogResult =
                            MessageBox.Show("Czy ma być to linia dla prawdy (true)", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            OverrideLine(false, temp);
                            Insert(0, new MyLine(temp.PointOutput1, temp.ID, true));
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            OverrideLine(true, temp);
                            Insert(0, new MyLine(temp.PointOutput2, temp.ID, false));
                        }
                    }
                    else
                    {
                        OverrideLine(false, temp);
                        Insert(0, new MyLine(temp.PointOutput1, temp.ID));

                    }
                }
            }
        }

        private void OverrideLine(bool isFalseLine, MyBlock block)
        {
            bool isOverride = false;
            if (isFalseLine)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i].BeginId == block.ID && !this[i].IsTrue) //już istnieje taka linia
                    {
                        UndoRedo.Push(new List<UndoRedoItem>(){ new UndoRedoItem(MyAction.OverrideLine, null, ToListHistory(i))});
                        RemoveAt(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i].BeginId == block.ID&& this[i].IsTrue) //już istnieje taka linia
                    {
                        UndoRedo.Push(new List<UndoRedoItem>() { new UndoRedoItem(MyAction.OverrideLine, null, ToListHistory(i)) });
                        RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// Metoda aktualizująca rozmieszczenie każdej z linii po przemieszczeniu bloku
        /// </summary>
        /// <param name="listBlocks"></param>
        public void MyUpdate(ref ListCanvasBlocks listBlocks)
        {
            if (listBlocks.Count > 0)
            {
                for (int i = 0; i < Count; i++)
                {
                    var temp1 = listBlocks.TryGetElementWithId(this[i].BeginId);
                    var temp2 = listBlocks.TryGetElementWithId(this[i].EndId);
                    //if (temp1 == null || temp2 == null)
                    //{
                    //    this.RemoveAt(i);
                    //    i--;
                    //    continue;
                    //}
                    if (temp1.Shape == Helper.Shape.Decision)
                    {
                        this[i].BeginPoint = (this[i].IsTrue)?temp1.PointOutput1:temp1.PointOutput2;
                    }
                    else
                    {
                        this[i].BeginPoint = temp1.PointOutput1;
                    }
                    this[i].EndPoint = temp2.PointInput;
                }
            }
        }

        /// <summary>
        /// Metoda usuwająca wszystkie linie wchodzące i wychodzące z bloku o konkretnym ID
        /// </summary>
        /// <param name="id"></param>
        public void MyRemove(int id)
        {
            RemoveAll(x => x.BeginId == id || x.EndId == id);
        }
        public void MyRemove(int beginId, int endId,bool oneMatchParameter=false)
        {
            if(!oneMatchParameter)
            RemoveAll(x => x.BeginId == beginId && x.EndId == endId);
            else
                RemoveAll(x => x.BeginId == beginId || x.EndId == endId);
        }

        /// <summary>
        /// Metoda przerywająca dodawanie linii
        /// </summary>
        /// <param name="shapeToDraw"></param>
        public void MyAbortAdd()
        {
            if (Count > 0 && this[0].EndPoint == Point.Empty)
                RemoveAt(0);
        }

        public List<MyLine> MyCopy(string clipboardFormat, string clipboardBlockFormat)
        {
            var x = new List<MyLine>();
            if (Clipboard.ContainsData(clipboardBlockFormat))
            {
                var blockTemp = (List<MyBlock>)Clipboard.GetData(clipboardBlockFormat);
                for (int i = 0; i < Count; i++)
                    for (int j = 0; j < blockTemp.Count; j++)
                        if (blockTemp[j].ID == this[i].BeginId)
                            for (int k = 0; k < blockTemp.Count; k++)
                                if (blockTemp[k].ID == this[i].EndId)
                                    x.Add(this[i]);
            }
            return x;
        }

        public List<MyLine> MyCut(string clipboardFormat, string clipboardBlockFormat)
        {
            var x = new List<MyLine>();
            if (Clipboard.ContainsData(clipboardBlockFormat))
            {
                var blockTemp = (List<MyBlock>) Clipboard.GetData(clipboardBlockFormat);
                for (int i = Count - 1; i >= 0; i--)
                for (int j = 0; j < blockTemp.Count; j++)
                    if (blockTemp[j].ID == this[i].BeginId)
                        for (int k = 0; k < blockTemp.Count; k++)
                            if (blockTemp[k].ID == this[i].EndId)
                            {
                                x.Add(this[i]);
                            }
                for (int i = blockTemp.Count - 1; i >= 0; i--)
                {
                    MyRemove(blockTemp[i].ID);
                }
            }
            return x;
        }

        public void MyPaste(List<MyLine> lines)
        {
            AddRange(lines);
        }
    }
    [Serializable]
    public class MyLine
    {
        public Point BeginPoint;
        public Point EndPoint = Point.Empty;
        public int BeginId, EndId;
        public bool IsSelected = false; //czy jest zaznaczona
        [XmlIgnore]
        public Color BackColor;
        [XmlAttribute]
        public string BackColorHTML
        {
            get { return ColorTranslator.ToHtml(BackColor); }
            set { BackColor = ColorTranslator.FromHtml(value); }
        }
        public bool IsTrue = true;
        /// <summary>
        /// for serialization
        /// </summary>
        public MyLine()
        {
            
        }
        /// <summary>
        /// Konstruktor dla wszystkich lini z wyjątkiem lini wychodzących z bloku decyzyjnego
        /// </summary>
        /// <param name="beginPoint"></param>
        /// <param name="beginId"></param>
        public MyLine(Point beginPoint,int beginId)
        {
            BeginPoint = beginPoint;
            BackColor = Helper.DefaultBlocksSettings[Helper.Shape.ConnectionLine].BackColor;
            BeginId = beginId;
        }

        /// <summary>
        /// Kostruktor dla linii wychodzących z bloku decyzyjnego - True false
        /// </summary>
        /// <param name="beginPoint"></param>
        /// <param name="beginId"></param>
        /// <param name="isTrue"></param>
        public MyLine(Point beginPoint, int beginId, bool isTrue)
        {
            BeginPoint = beginPoint;
            BackColor = (isTrue)?Helper.TrueLineBackColor:Helper.FalseLineBackColor;
            BeginId = beginId;
            IsTrue = isTrue;
        }

        /// <summary>
        /// Metoda sprawdzająca czy została wybrana linia.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Metoda wyrysowująca połączenia między blokami w postaci linii.
        /// </summary>
        /// <param name="g"></param>
        public void My_DrawConnectionLine(Graphics g)
        {
            if (EndPoint != Point.Empty)
            {
                //var YBreak = Math.Abs(BeginPoint.Y - EndPoint.Y) * 50 / 100;
                //var XBreak = Math.Abs(BeginPoint.X - EndPoint.X) * 50 / 100;
                var YBreak = 10;
                var XBreak = 10;
                if (BeginPoint.Y > EndPoint.Y)
                {
                    XBreak= Canvas.CanvObj.Find(x => x.ID == this.BeginId).Rect.Width/2+10;
                    if (BeginPoint.X > EndPoint.X)
                        g.DrawLines(
                            new Pen(BackColor, 4),
                            new[]
                            {
                                BeginPoint,
                                new Point(BeginPoint.X, BeginPoint.Y + YBreak), //1
                                new Point(BeginPoint.X + XBreak, BeginPoint.Y + YBreak), //2
                                new Point(BeginPoint.X + XBreak, EndPoint.Y - YBreak), //3
                                new Point(EndPoint.X, EndPoint.Y - YBreak), //4
                                EndPoint //5
                            });
                    else
                        g.DrawLines(
                            new Pen(BackColor, 4),
                            new[]
                            {
                                BeginPoint,
                                new Point(BeginPoint.X, BeginPoint.Y + YBreak),
                                new Point(BeginPoint.X - XBreak, BeginPoint.Y + YBreak),
                                new Point(BeginPoint.X - XBreak, EndPoint.Y - YBreak),
                                new Point(EndPoint.X, EndPoint.Y - YBreak),
                                EndPoint
                            });
                }
                else
                    g.DrawLines(
                        new Pen(BackColor, 4),
                        new[]
                        {
                            BeginPoint,
                            new Point(BeginPoint.X, BeginPoint.Y + YBreak),
                            //new Point(EndPoint.X, EndPoint.Y-YBreak),//skośne linie łączące
                            new Point(EndPoint.X, BeginPoint.Y + YBreak), //kąty proste
                            EndPoint
                        });
            }
        }
        public void My_DrawConnectionLineForDecisionBlock(Graphics g)
        {
            if (EndPoint != Point.Empty)
            {
                var YBreak = 10;
                var XBreak = 10;
                if (BeginPoint.Y > EndPoint.Y)
                {
                    XBreak = (IsTrue ? -1 : 1) * XBreak;
                    YBreak += (IsTrue ? 5 : 0);
                    if (BeginPoint.X > EndPoint.X) //lewo
                    {
                        g.DrawLines(
                            new Pen(BackColor, 4),
                            new[]
                            {
                                BeginPoint,
                                new Point(BeginPoint.X + XBreak, BeginPoint.Y),
                                new Point(BeginPoint.X + XBreak, EndPoint.Y - YBreak),
                                new Point(EndPoint.X, EndPoint.Y - YBreak ),
                                EndPoint
                            });
                    }
                    else
                    {
                        g.DrawLines(
                            new Pen(BackColor, 4),
                            new[]
                            {
                                BeginPoint,
                                new Point(BeginPoint.X + XBreak, BeginPoint.Y),
                                new Point(BeginPoint.X + XBreak, EndPoint.Y - YBreak),
                                new Point(EndPoint.X, EndPoint.Y -YBreak),
                                EndPoint
                            });
                    }
                }
                else
                {
                    if ((BeginPoint.X < EndPoint.X && !this.IsTrue)|| (BeginPoint.X > EndPoint.X && this.IsTrue))
                    {
                        g.DrawLines(
                            new Pen(BackColor, 4),
                            new[]
                            {
                                BeginPoint,
                                new Point(EndPoint.X, BeginPoint.Y),
                                EndPoint
                            });
                    }
                    else
                    {
                        XBreak = (IsTrue ? -1 : 1) * XBreak;
                        YBreak = Canvas.CanvObj.Find(x => x.ID == this.BeginId).Rect.Height / 2 + 10+ (IsTrue ? 5 : 0);
                        g.DrawLines(
                            new Pen(BackColor, 4),
                            new[]
                            {
                                BeginPoint,
                                new Point(BeginPoint.X+XBreak, BeginPoint.Y),
                                new Point(BeginPoint.X+XBreak, BeginPoint.Y+YBreak),
                                new Point(EndPoint.X, BeginPoint.Y+YBreak),
                                EndPoint
                            });
                    }
                }
            }
        }
    }
}
