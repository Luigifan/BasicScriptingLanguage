using BasicScriptingLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTestingApplication
{
    class Program
    {
        public static void Main(string[] args)
        {
            BasicScriptFile.ExecuteScript(Environment.CurrentDirectory + @"\test.bsl");

            Console.ReadLine();
        }
    }
}
