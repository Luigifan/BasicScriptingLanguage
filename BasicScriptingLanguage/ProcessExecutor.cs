using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScriptingLanguage
{
    class ProcessExecutor
    {
        public static void StartProcess(string line, int lineCount)
        {
            try
            {
                //Constants are supported here and are defined with {}
                //Example, if I wanted to use the currentdirectory constant, I'd do so like this
                //runProcess({CURRENTDIRECTORY} + "\cmd.exe")
                //runProcess(process);
                if (line.Contains('(') && line.EndsWith(")"))
                {
                    //splits into
                    //runProcess & whatever is inside of the parenthesis
                    var split = line.Split(new char[] { '(', ')' });
                    if(split[1].Contains('"'))
                    {
                        //We need to combine strings
                        if(split[1].Contains('+'))
                        {
                            //string replacedConstants = Constants.ReplaceConstantsInString(line);
                            //var parseParams = replacedConstants.Split('+');
                            //string fullParams = "";
                            //for(int i = 0; i < parseParams.Count(); i++)
                            //{
                            //    fullParams += parseParams[i];
                            //}
                            //Process.Start(fullParams);
                        }
                        else
                        {
                            //string replacedConstants = Constants.ReplaceConstantsInString(line);
                            //Process.Start(replacedConstants);
                        }

                    }
                }
                else
                    throw new System.IO.InvalidDataException("Script error at line " + lineCount + ": Missing parenthesis");
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
