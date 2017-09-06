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
    class Compile
    {
        public static TextBox Results = new System.Windows.Forms.TextBox();

        private static string TestCode =
            "#include <stdio.h>" +
            "int main()" +
            "{" +
            @"printf(""Hello, World! This is a native C program compiled on the command line.\n"");"+
            "return 0;"+
            "}";
   

    public static void LookForErrors(string Code)
        {

        }

        public static void Debug(ListCanvasBlocks blocks, ListCanvasLines lines)
        {
            ReadCodeFromBlocks(ref blocks, ref lines);
            LookForErrors(TestCode);
        }

        public static void DebugNextStep()
        {
        }

        public static void Run(ListCanvasBlocks blocks, ListCanvasLines lines)
        {
            //ReadCodeFromBlocks(ref blocks,ref lines);
            LookForErrors(TestCode);
            //LookForErrors(TestCode);

        }

        public static void Build()
        {

        }

        public static void Stop()
        {
            
        }

        private static void ReadCodeFromBlocks(ref ListCanvasBlocks blocks, ref ListCanvasLines lines)
        {
            Results.Text = "";
            var myBlock = blocks[0];
            if (blocks.Count > 0 && lines.Count > 0)
            {
                myBlock = blocks.Find(x => x.Shape == Helper.Shape.Start);
                if (myBlock == null) //sprawdzenie czy algorytm ma blok start
                {
                    Results.Text = "Błąd: algorytm musi posiadać " +
                                   Helper.DefaultBlocksSettings[Helper.Shape.Start].Label;
                    return;
                }
                Results.Text += myBlock.Label;

                for (int i = 0; i < lines.Count; i++)
                {
                    var id = lines.Find(x => x.BeginId == myBlock.ID).EndId;
                    myBlock = blocks.Find(x => x.ID == id);
                    Results.Text += myBlock.Label;
                    //sprawdzenie czy nie probujemy dołączać do algorytmu obiektów dodanych gdzieś na boku
                    //nie będące połączone ze startem
                    if (myBlock.Shape == Helper.Shape.End)
                        break;
                }

                //jeżeli algorytm nie kończy się blokiem end jest on niepełny
                if (myBlock.Shape == Helper.Shape.End)
                    Results.Text += "Powodzenie";
                else
                    Results.Text += "Błąd: algorytm musi posiadać " +
                                    Helper.DefaultBlocksSettings[Helper.Shape.End].Label;

            }

        }


    }
}
