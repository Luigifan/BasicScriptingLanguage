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
                //echo("Hello");
                var splitIntoParams = line.Split(new char[]{'(', ')'});

                Console.Write("");
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
