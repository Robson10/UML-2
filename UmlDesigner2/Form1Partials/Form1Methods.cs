using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmlDesigner2.Component.Workspace.CanvasArea;
using UmlDesigner2.Component.Workspace.ResultComponent;
namespace UmlDesigner2
{
    partial class Form1
    {
        private void Run()
        {
            Compile.Run(Canvas.CanvObj,Canvas.CanvLines);
        }
    }
}
