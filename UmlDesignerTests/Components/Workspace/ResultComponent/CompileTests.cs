using Microsoft.VisualStudio.TestTools.UnitTesting;
using UmlDesigner2.Component.Workspace.ResultComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmlDesigner2.Component.Workspace.CanvasArea;
using System.Drawing;

namespace UmlDesigner2.Component.Workspace.ResultComponent.Tests
{
    [TestClass()]
    public class CompileTests
    {
        [TestMethod()]
        public void RunTest()
        {
            var canv=CreateExampleOnCanvas();
            string expected =
                "#include <stdio.h>" + Environment.NewLine+
                "int main(){" + Environment.NewLine +
                "1" + Environment.NewLine +
                "if(2)" + Environment.NewLine +
                "{" + Environment.NewLine +
                "4" + Environment.NewLine +
                "5" + Environment.NewLine +
                "6" + Environment.NewLine +
                "return 0" + Environment.NewLine +
                "}" + Environment.NewLine +
                "else" + Environment.NewLine +
                "{" + Environment.NewLine +
                "3" + Environment.NewLine +
                "}" + Environment.NewLine +
                "return 0;" + Environment.NewLine +
                "}";
            string result = Compile.Run(Canvas.CanvObj,Canvas.CanvLines);
            Assert.AreEqual(expected,result);
            System.Windows.Forms.MessageBox.Show(result);
            //Assert.Fail();
        }

        private Canvas CreateExampleOnCanvas()
        {
            Canvas temp = new Canvas();
            Canvas.CanvObj.MyAdd(new Point(500, 100), Helper.Shape.Start);
            Canvas.CanvObj.MyAdd(new Point(500, 200), Helper.Shape.Input);
            Canvas.CanvObj.MyAdd(new Point(500, 300), Helper.Shape.Decision);
            Canvas.CanvObj.MyAdd(new Point(750, 400), Helper.Shape.Execution);
            Canvas.CanvObj.MyAdd(new Point(250, 400), Helper.Shape.Execution);
            Canvas.CanvObj.MyAdd(new Point(250, 500), Helper.Shape.Input);
            Canvas.CanvObj.MyAdd(new Point(250, 600), Helper.Shape.Execution);
            Canvas.CanvObj.MyAdd(new Point(500, 700), Helper.Shape.End);
            for (int i = Canvas.CanvObj.Count - 1; i >= 1; i--)
            {
                if (i != 4 && i != 5)
                {
                    var shape = Helper.Shape.ConnectionLine;
                    Canvas.CanvLines.MyAdd(
                        new Point(Canvas.CanvObj[i].Rect.Location.X + Canvas.CanvObj[i].Rect.Width / 2,
                            Canvas.CanvObj[i].Rect.Location.Y + Canvas.CanvObj[i].Rect.Height / 2), ref shape,
                        ref Canvas.CanvObj);

                    Canvas.CanvLines.MyAdd(
                        new Point(Canvas.CanvObj[i - 1].Rect.Location.X + Canvas.CanvObj[i - 1].Rect.Width / 2,
                            Canvas.CanvObj[i - 1].Rect.Location.Y + Canvas.CanvObj[i - 1].Rect.Height / 2), ref shape,
                        ref Canvas.CanvObj);
                }
                else
                {
                    if (i == 4)
                    {
                        Canvas.CanvLines.Add(new MyLine(Canvas.CanvObj[i].PointOutput1, Canvas.CanvObj[i].ID) { EndId = Canvas.CanvObj[i + 1].ID, EndPoint = Canvas.CanvObj[i + 1].PointInput });
                    }
                    else if (i == 5)
                    {
                        Canvas.CanvLines.Add(new MyLine(Canvas.CanvObj[i].PointOutput1, Canvas.CanvObj[i].ID, false) { EndId = Canvas.CanvObj[i - 1].ID, EndPoint = Canvas.CanvObj[i - 1].PointInput });
                        Canvas.CanvLines.Add(new MyLine(Canvas.CanvObj[i].PointOutput1, Canvas.CanvObj[i].ID, true) { EndId = Canvas.CanvObj[i - 2].ID, EndPoint = Canvas.CanvObj[i - 2].PointInput });
                    }
                }
            }
            for (int i = 0; i < Canvas.CanvObj.Count; i++)
            {
                Canvas.CanvObj[i].Label = Canvas.CanvObj[Canvas.CanvObj.Count-i-1].ID.ToString();
                Canvas.CanvObj[i].Code = Canvas.CanvObj[Canvas.CanvObj.Count-i-1].ID.ToString();
            }
            return temp;
        }
    }
}