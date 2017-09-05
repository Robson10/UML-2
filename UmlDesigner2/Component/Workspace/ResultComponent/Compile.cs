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
            //string language = "C++";
            //CodeDomProvider provider= CodeDomProvider.CreateProvider(language);
            //Process.Start("gcc.exe", "a.cpp");
            //provider.CompileAssemblyFromSource(new CompilerParameters(), TestCode);

            //// Check for a provider corresponding to the input language.   
            //if (CodeDomProvider.IsDefinedLanguage(language))
            //{
            //    CompilerInfo langCompilerInfo = CodeDomProvider.GetCompilerInfo(language);
            //    CompilerParameters langCompilerConfig = langCompilerInfo.CreateDefaultCompilerParameters();
            //}
            //else
            //{
            //    // Tell the user that the language provider was not found.
            //    MessageBox.Show("There is no provider configured for input language " + language);
            //}

            //System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            //parameters.GenerateExecutable = true;
            //parameters.OutputAssembly = Output;
            //System.CodeDom.Compiler.ICodeCompiler icc = codeProvider.CreateCompiler();
            //CompilerResults results = icc.CompileAssemblyFromSource(parameters, Code);
            //if (results.Errors.Count > 0)
            //{
            //    foreach (CompilerError CompErr in results.Errors)
            //    {
            //        Results.Text = Results.Text +
            //                       "Line number " + CompErr.Line +
            //                       ", Error Number: " + CompErr.ErrorNumber +
            //                       ", '" + CompErr.ErrorText + ";" +
            //                       Environment.NewLine + Environment.NewLine;
            //    }
            //}
            //else
            //{
            //    Results.Text = "No Errors";
            //    System.Diagnostics.Process.Start(Output);
            //}
        }

        public static void Debug()
        {

        }

        public static void DebugNextStep()
        {
        }

        public static void Run()
        {
            LookForErrors(TestCode);
        }

        public static void Build()
        {

        }

        public static void Stop()
        {
            
        }
        

  
    }
}
