using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UmlDesigner2.Class;
using UmlDesigner2.Components.Workspace;

namespace UmlDesigner2.Components.ResultComponent
{
    public class Compile
    {

        public static TextBox Results = new TextBox();

        public static string Run(ListCanvasBlocks blocks, ListCanvasLines lines)
        {
            if (ValidateSchema(ref blocks, ref lines))
            {
                try
                {
                    if (File.Exists(Helper.CompilePath + @"\project.cpp"))
                        File.Delete(Helper.CompilePath + @"\project.cpp");
                    if (File.Exists(Helper.CompilePath + @"\project.exe"))
                        File.Delete(Helper.CompilePath + @"\project.exe");
                    File.WriteAllText(Helper.CompilePath + @"\project.cpp", TransformBlockToCode(blocks, lines));
                    RunCMD(false);
                }
                catch
                {
                    MessageBox.Show(
                        "Odmowa dostępu! Prawdopodobnie masz uruchomione projekty w konsoli. Zamknij je i spróbuj ponownie");
                }
            }
            return "asd";
        }

        private static void RunCMD(bool debug)
        {
            CmdCompile();
            if (File.Exists(Helper.CompilePath + @"\project.exe"))
                if (!debug)
                {
                    File.WriteAllText(Helper.CompilePath + @"\run.bat","start project.exe" );
                    Process cmd = new Process();
                    cmd.StartInfo.FileName = Helper.CompilePath + @"\run.bat";
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.Start();
                    cmd.Close();
                }
                else
                {
                    File.WriteAllText(Helper.CompilePath + @"\run.bat", "g++ -g -o project.exe project.cpp" + Environment.NewLine + "gdb project.exe" );
                    Process cmd = new Process();
                    cmd.StartInfo.FileName = Helper.CompilePath + @"\run.bat";
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.Start();
                    cmd.Close();
                }

        }

        private static void CmdCompile()
        {
            File.WriteAllText(Helper.CompilePath + @"\run.bat", "g++ project.cpp -o project.exe" +Environment.NewLine+"pause");
            Process cmd = new Process();
            cmd.StartInfo.FileName = Helper.CompilePath + @"\run.bat";
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.WaitForExit();
            cmd.Close();
        }

        public static string TransformBlockToCode(ListCanvasBlocks blocks, ListCanvasLines lines)
        {
            string code = "";
            try
            {
                blocks.Sort((x, y) => x.ID.CompareTo(y.ID));
                var indexBegin = blocks.FindIndex(x => x.Shape == Helper.Shape.Start);
                int endId = lines.Find(x => x.BeginId == blocks[indexBegin].ID).EndId;
                StartToCode(ref code,blocks[indexBegin], endId);
                for (int i = 0; i < blocks.Count; i++)
                {
                    if (blocks[i].Shape == Helper.Shape.Decision)
                    {
                        DecisionToCode(ref code, blocks[i], ref lines);
                    }
                    else if (blocks[i].Shape != Helper.Shape.End && blocks[i].Shape != Helper.Shape.Start)
                    {
                        endId = lines.Find(x => x.BeginId == blocks[i].ID).EndId;
                        InputExecutionToCode(ref code, blocks[i], endId);
                    }
                    else if (blocks[i].Shape == Helper.Shape.End)
                    {
                        EndToCode(ref code, blocks[i].ID);
                    }
                    code += Environment.NewLine;
                }
                FinishCode(ref code);
                Results.Text = code;
            }
            catch
            {
            }
            return code;
        }

        private static void DecisionToCode(ref string code, MyBlock block, ref ListCanvasLines lines)
        {
            code += "          case(" + block.ID + "):" + Environment.NewLine +
                    "               " + block.Code + Environment.NewLine +
                    //"               {" + Environment.NewLine +
                    "                   ID=" + lines.Find(x => x.BeginId == block.ID && x.IsTrue).EndId + ";" +
                    Environment.NewLine +
                    //"               }" + Environment.NewLine +
                    "               else" + Environment.NewLine +
                    //"               {" + Environment.NewLine +
                    "                   ID=" + lines.Find(x => x.BeginId == block.ID && !x.IsTrue).EndId + ";" +
                    Environment.NewLine +
                    //"               }" + Environment.NewLine +
                    "              break;" + Environment.NewLine;
        }

        private static void StartToCode(ref string code, MyBlock block, int startID)
        {
            code += block.Includes + Environment.NewLine + Environment.NewLine;
            code += "int main()" + Environment.NewLine +
                    "{" + Environment.NewLine +
                    "   int ID=" + startID + ";" + Environment.NewLine;
            var temp = block.Variables.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            for (int i = 0; i < temp.Count; i++)
            {
                code += "   " + temp[i] + Environment.NewLine;
            }
            code+="   while(true)" + Environment.NewLine +
                    "   {" + Environment.NewLine +
                    "       switch(ID)" + Environment.NewLine +
                    "       {" + Environment.NewLine;
        }

        private static void EndToCode(ref string code, int ID)
        {
            code += "          case(" + ID + "):" + Environment.NewLine +
                    "              return 0;" + Environment.NewLine +
                    "              break;" + Environment.NewLine;
        }

        private static void InputExecutionToCode(ref string code, MyBlock block, int endID)
        {
            code += "          case(" + block.ID + "):" + Environment.NewLine;
            var temp = block.Code.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            for (int i = 0; i < temp.Count; i++)
            {
                code += "             " + temp[i]+Environment.NewLine;
            }
            code+=
                    "              ID=" + endID + ";" + Environment.NewLine +
                    "              break;" + Environment.NewLine;
        }

        private static void FinishCode(ref string code)
        {
            code += "       }" + Environment.NewLine +
                    "   }" + Environment.NewLine +
                    "return 0;" + Environment.NewLine +
                    "}" + Environment.NewLine;
        }


        public static void Debug(ListCanvasBlocks blocks, ListCanvasLines lines)
        {
             if (ValidateSchema(ref blocks, ref lines))
            {
                try
                {
                    if (File.Exists(Helper.CompilePath + @"\project.cpp"))
                        File.Delete(Helper.CompilePath + @"\project.cpp");
                    if (File.Exists(Helper.CompilePath + @"\project.exe"))
                        File.Delete(Helper.CompilePath + @"\project.exe");
                    File.WriteAllText(Helper.CompilePath + @"\project.cpp", TransformBlockToCode(blocks, lines));
                    RunCMD(true);
                }
                catch (UnauthorizedAccessException e)
                {
                    MessageBox.Show("Nie udało się usunąc plików projektu. Prawdopodobnie masz uruchomione projekty?");
                }
            }
        }


        

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