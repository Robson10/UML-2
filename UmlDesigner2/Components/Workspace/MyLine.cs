using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SbWinNew.Class;

namespace SbWinNew.Components.Workspace
{
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
        public MyLine(Point beginPoint, int beginId)
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
            BackColor = (isTrue) ? Helper.TrueLineBackColor : Helper.FalseLineBackColor;
            BeginId = beginId;
            IsTrue = isTrue;
        }

        /// <summary>
        /// Metoda wyrysowująca połączenia między blokami w postaci linii.
        /// </summary>
        /// <param name="g"></param>
        public void My_DrawConnectionLine(Graphics g)
        {
            if (EndPoint != Point.Empty)
            {
                var YBreak = 10;
                var XBreak = 10;
                if (BeginPoint.Y > EndPoint.Y)
                {
                    XBreak = Canvas.CanvObj.Find(x => x.ID == this.BeginId).Rect.Width / 2 + 10;
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
                    if ((BeginPoint.X < EndPoint.X && !this.IsTrue) || (BeginPoint.X > EndPoint.X && this.IsTrue))
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
                        YBreak = Canvas.CanvObj.Find(x => x.ID == this.BeginId).Rect.Height / 2 + 10 + (IsTrue ? 5 : 0);
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
