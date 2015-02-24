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
                if (line.Contains('(') && line.Contains(')'))
                {
                    var split = line.Split(new char[] { '(', ')' });
                    if (split[1].Contains('"'))
                    {
                        if(split[1].Contains('+'))
                        {
                            var parsingConstantsAndVariables = split[1].Split('+');
                            string allTogetherNow = parsingConstantsAndVariables[0].Trim('"');
                            foreach(var item in parsingConstantsAndVariables)
                            {
                                string compare = item.Trim(new char[] { ' ', '"', '\'' });

                                if (compare.StartsWith("[") && compare.EndsWith("]")) //it's a variable
                                {
                                    string replaceVarName = item.Trim(new char[] { '[', ']' });
                                    allTogetherNow += BasicScriptFile.MainScriptInterepreter.RetrieveVariableValue(replaceVarName);
                                }
                                else if (compare.StartsWith("{") && compare.EndsWith("}"))
                                {
                                    allTogetherNow += Constants.ReplaceConstantsInString(item);
                                }
                            }
                        }
                        Console.WriteLine(split[1].Trim('"')); 
                    }
                    else
                        throw new System.IO.InvalidDataException("Script error on line " + lineCount + ": Missing quotes");
                }
                else
                    throw new System.IO.InvalidDataException("Script error on line " + lineCount + ": Missing parenthesis");
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
