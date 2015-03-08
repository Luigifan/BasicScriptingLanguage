using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScriptingLanguageDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (System.IO.File.Exists(args[0].Trim('"')))
                {
                    try
                    {
                        BasicScriptingLanguage.BasicScriptFile.ExecuteScript(args[0].Trim('"'));
                        Console.WriteLine("\nEND OF SCRIPT");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("ERROR EXECUTING SCRIPT");
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                    Console.WriteLine("File doesn't exist: " + args[0]);
            }
            else
                Console.WriteLine("Please pass a file as an argument");
        }
    }
}
