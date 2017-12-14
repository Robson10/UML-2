using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp;
using UmlDesigner2.Component.Workspace.CanvasArea;

namespace UmlDesigner2.Component.Workspace.ResultComponent
{
    public class Compile
    {
        public static string Includes = "#include <stdio.h>";
        public static TextBox Results = new System.Windows.Forms.TextBox();

        private static string OutputCode =
            "#include <stdio.h>" +
            "int main()" +
            "{" +
            @"printf(""Hello, World! This is a native C program compiled on the command line.\n"");" +
            "return 0;" +
            "}";

        public static string Run(ListCanvasBlocks blocks, ListCanvasLines lines)
        {
            if (ValidateSchema(ref blocks, ref lines))
            {
                if (TransformBlockToCode(blocks, lines))
                {
                    //zapisanie jako plik kodu i jego kompilacja wraz z uruchomieniem
                    //mozna jeszcze zapisywać projekt
                }

            }
            return "asd";

        }
        
        private static bool TransformBlockToCode(ListCanvasBlocks blocks, ListCanvasLines lines)
        {
            string code="";
            var indexBegin = blocks.FindIndex(x => x.Shape == Helper.Shape.Start);
            int endId=lines.Find(x=>x.BeginId==blocks[indexBegin].ID).EndId;
            StartToCode(ref code, blocks[indexBegin].ID);
            for (int i = 0; i < blocks.Count-1; i++)
            {
                //indexBegin = indexEnd;
                if (blocks[i].Shape == Helper.Shape.Decision)
                {
                    code += "          case(" + blocks[i].ID + "):" + Environment.NewLine +
                            "               " + blocks[i].Code + Environment.NewLine +
                            "               {" + Environment.NewLine +
                            "                   ID=" +lines.Find(x => x.BeginId == blocks[i].ID && x.IsTrue).EndId+ Environment.NewLine +
                            "               }" + Environment.NewLine +
                            "               else"+Environment.NewLine+
                            "               {" + Environment.NewLine +
                            "                   ID=" + lines.Find(x => x.BeginId == blocks[i].ID && !x.IsTrue).EndId + Environment.NewLine +
                            "               }" + Environment.NewLine +
                            "              ID=" + endId + ";" + Environment.NewLine +
                            "              break;" + Environment.NewLine;
                }
                else if (blocks[i].Shape != Helper.Shape.End)
                {
                    endId = lines.Find(x => x.BeginId == blocks[i].ID).EndId;
                    code += "          case(" + blocks[i].ID + "):" + Environment.NewLine +
                            "             "+blocks[i].Code + Environment.NewLine +
                            "              ID=" + endId + ";" + Environment.NewLine +
                            "              break;" + Environment.NewLine;
                }
                else if (blocks[i].Shape == Helper.Shape.End)
                {
                    //endId = lines.Find(x => x.BeginId == blocks[i].ID).EndId;
                    code += "          case(" + blocks[i].ID + "):" + Environment.NewLine +
                            "              return 0;" + Environment.NewLine +
                            "              break;" + Environment.NewLine;
                }
            }
            FinishCode(ref code);
            MessageBox.Show(code);
            return true;
        }

        private static void StartToCode(ref string code, int startID)
        {
            code += Includes + Environment.NewLine + Environment.NewLine;
            code += "int main()" + Environment.NewLine +
                    "{" + Environment.NewLine +
                    "   int ID=" + startID + ";" + Environment.NewLine +
                    "   while(true)" + Environment.NewLine +
                    "   {" + Environment.NewLine +
                    "       switch(ID)" + Environment.NewLine +
                    "       {" + Environment.NewLine;
        }

        private static void FinishCode(ref string code)
        {
            code +="       }" + Environment.NewLine +
                   "   }" + Environment.NewLine+
                   "return 0;" + Environment.NewLine + 
                   "}" + Environment.NewLine;
        }


        public static void Debug(ListCanvasBlocks blocks, ListCanvasLines lines)
        {
            if (ValidateSchema(ref blocks, ref lines))
            {
                
            }
        }


        //public static void LookForErrors(string Code)
        //{

        //}
        //public static void DebugNextStep()
        //{
        //}

        //public static void Build()
        //{

        //}

        //public static void Stop()
        //{

        //}

        private static bool ValidateSchema(ref ListCanvasBlocks blocks, ref ListCanvasLines lines)
        {
            ///Celem owej metody jest sprawdzenie poprawności stworzonego schematu - 
            /// czy istnieje początek algorytmu - START, koniec algorytmu - KONIEC, oraz czy wszystkie bloki są ze sobą połączone 
            string myOutput = "";
            bool isStartExist = blocks.Exists(x => x.Shape == Helper.Shape.Start);
            bool isEndExist = blocks.Exists(x => x.Shape == Helper.Shape.End);
            bool isCountOfLinesCorrect;
            if (!isStartExist)
            {
                myOutput += "Błąd: algorytm musi posiadać " +
                          Helper.DefaultBlocksSettings[Helper.Shape.Start].Label + Environment.NewLine;
            }
            if (!isEndExist)
            {
                myOutput += "Błąd: algorytm musi posiadać " +
                          Helper.DefaultBlocksSettings[Helper.Shape.End].Label + Environment.NewLine;
            }
            int count = 0;
            count += blocks.FindAll(x => x.Shape == Helper.Shape.Decision).Count * 2;
            count += blocks.FindAll(x => x.Shape != Helper.Shape.Decision && x.Shape != Helper.Shape.End).Count;
            isCountOfLinesCorrect = count == lines.Count;
            if (!isCountOfLinesCorrect)
            {
                myOutput += "Błąd: Brakuje połączeń między blokami" +
                          Environment.NewLine;
            }
            myOutput += (isCountOfLinesCorrect && isStartExist && isEndExist)
                ? "Twój kod wstępnie wygląda w porządku"
                : "";
            if (isCountOfLinesCorrect && isStartExist && isEndExist)
            {
                return true;
            }
            else
            {
                MessageBox.Show(myOutput);
                return false;
            }
        }

    }
    

}