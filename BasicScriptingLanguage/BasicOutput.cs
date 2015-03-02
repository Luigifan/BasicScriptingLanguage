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
                //echo|"String"
                var split = line.Split('|');
                if (split[0] != "echo")
                    throw new System.IO.InvalidDataException("Invalid command");
                else
                {
                    //Console.WriteLine(split[1].Trim('"'));
                    Console.WriteLine("[OUTPUT]: {0}", BasicScriptFile.MainScriptInterepreter.ReplaceWithVarsConstants(split[1]).Trim('"'));
                }
            }
            catch(System.IO.InvalidDataException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nERROR: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(ex.Message);
            }
        }

        public static void Wait(string line, int lineCount)
        {
            try
            {
                if (line != "wait")
                    throw new System.IO.InvalidDataException("Invalid command");
                else
                { 
                    Console.WriteLine("Press enter to continue.."); 
                    Console.ReadLine(); 
                }
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
