using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScriptingLanguage
{
    class BasicOutput
    {
        public static void Echo(string line, int lineCount)
        {
            try
            {
                //string paramsReplaced = BasicScriptFile.MainScriptInterepreter.ReplaceWithVarsConstants(line);
                Console.WriteLine(BasicScriptFile.MainScriptInterepreter.ReplaceWithVarsConstants(line));
            }
            catch(System.IO.InvalidDataException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nERROR: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(ex.Message);
            }
        }

    }
}
