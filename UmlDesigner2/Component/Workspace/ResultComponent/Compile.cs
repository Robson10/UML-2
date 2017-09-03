using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
        public static void LookForErrors()
        {
            string language = "C++";
            CodeDomProvider provider;

            // Check for a provider corresponding to the input language.   
            if (CodeDomProvider.IsDefinedLanguage(language))
            {
                provider = CodeDomProvider.CreateProvider(language);

                // Display information about this language provider.

                MessageBox.Show(provider.ToString());
                MessageBox.Show(provider.FileExtension);

                // Get the compiler settings for this language.

                CompilerInfo langCompilerInfo = CodeDomProvider.GetCompilerInfo(language);
                CompilerParameters langCompilerConfig = langCompilerInfo.CreateDefaultCompilerParameters();

                MessageBox.Show(langCompilerConfig.CompilerOptions);
                MessageBox.Show(langCompilerConfig.WarningLevel.ToString());
            }
            else
            {
                // Tell the user that the language provider was not found.
                MessageBox.Show("There is no provider configured for input language "+language);
            }
        }
        //System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
        //parameters.GenerateExecutable = true;
        //parameters.OutputAssembly = Output;
        //CompilerResults results = icc.CompileAssemblyFromSource(parameters, SourceString);
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

        public static void Debug()
        {

        }

        public static void DebugNextStep()
        {
        }

        public static void Run()
        {
            LookForErrors();
        }

        public static void Build()
        {

        }

        public static void Stop()
        {
            
        }

    }
}
