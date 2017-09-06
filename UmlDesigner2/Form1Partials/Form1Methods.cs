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
        private void View()
        {
            splitContainer2.Panel2.BackColor = System.Drawing.Color.White;
            splitContainer3.Panel2.BackColor = System.Drawing.Color.Gray;
        }

        private void AddEvents()
        {
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;

            clock1.EgzamStarted += Clock1_EgzamStarted;
            clock1.EgzamEnded += Clock1_EgzamEnded;

            canvas1.HideBlockPoperites += Canvas1_HideBlockPoperites;
            canvas1.ShowBlockPoperites += Canvas1_ShowBlockPoperites;

            ToolStripAddEvents();
            TabsAddEvents();
        }


        //todo
        #region metody dla skrótów i przycisków z paska narzedzi
        //todo usunąć uchwyt do pliku by ctrl+s nie nadpisało starego pliku
        private void NewFile()
        {
            canvas1.ClearCanvas();
        }

        private void OpenFile()
        {
            
        }

        private void OpenFileFromServer()
        {
            
        }

        private void SaveFile()
        {

        }

        private void SaveFileAs()
        {

        }

        private void Debug()
        {
            
        }

        private void Run()
        {
            Compile.Run(Canvas.CanvObj, Canvas.CanvLines);
        }

        private void LogIn()
        {
            
        }

        private void Options()
        {
            
        }

        #endregion
    }
}
