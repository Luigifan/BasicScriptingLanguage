using BasicScriptingLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicTestingApplication
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            BasicScriptFile.ExecuteScript(Environment.CurrentDirectory + @"\test.bsl");
            //MessageBox.Show("Result of variable 'hi' is " + BasicScriptFile.MainScriptInterepreter.RetrieveVariableValue("hi"));
        }

    }
}
