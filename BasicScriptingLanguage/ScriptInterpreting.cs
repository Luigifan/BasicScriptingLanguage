using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScriptingLanguage
{
    public class ScriptInterpreting
    {
        /// <summary>
        /// A key value pair that holds basic strings. The key is the name of the variable and the value is the value of the variable.
        /// </summary>
        List<KeyValuePair<string, string>> DeclaredValues = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Begin the script handling
        /// </summary>
        /// <param name="file">File to read</param>
        public void ReadScript(string file)
        {
            StreamReader sr = new StreamReader(file);
            int lineCount = 0; string line;
            while((line = sr.ReadLine()) != null)
            {
                if(lineCount == 0)
                {
                    if (line != "#BasicScriptFile")
                        throw new InvalidDataException("First line is not '#BasicScriptFile' or is not a BasicScriptFile!");
                }

                if(line.StartsWith("#"))
                {/*Comment, ignore*/}

                if(line.StartsWith("declare"))
                {
                    ParseDeclareVariable(line, lineCount);   
                }

                if(StartsWithVariable(line)) //useful for checking for variable assining/updating variables
                {
                    if(IsAssigningVariable(line))
                    {
                        AssignVariable(line, lineCount);
                    }
                }

                lineCount++;
            }
        }

        private bool StartsWithVariable(string line)
        {
            //returns true if line has a variable at the beginning
            var split = line.Split(new char[] { ' ' });
            foreach(var item in DeclaredValues)
            {
                if (item.Key == split[0])
                    return true;
                else
                    return false;
            }
            return false;
        }

        private bool IsAssigningVariable(string line)
        {
            var split = line.Split(new char[] { ' ' });
            //1      2 3
            //result = blah;
            if (split[1] == "=")
                return true;
            else
                return false;
        }

        private void AssignVariable(string line, int lineCount)
        {
            try
            {
                var split = line.Split(new char[] { ' ' }, 3);
                if (split[1] != "=")
                    throw new InvalidDataException("Script error at line " + lineCount + ": Expected equals sign, didn't get.");
                else
                {
                    string possibleNewValue;
                    if(IsCommand(split[2]))
                    {
                        //execute command and receive input if necessary
                    }
                    bool foundVar = false;
                    for(int i = 0; i < DeclaredValues.Count; i++)
                    {

                    }
                }
            }
            catch(InvalidDataException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nERROR: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(ex.Message);
            }
        }

        private void ExecuteCommand(string p)
        {
            throw new NotImplementedException();
        }

        private bool IsCommand(string line)
        {
            if (line.Contains("showQuestionMessage"))
                return true;
            if (line.Contains("showInfoMessage"))
                return true;
            if (line.Contains("runProcess"))
                return true;


            return false;
        }

        /// <summary>
        /// Parse a variable declaration
        /// </summary>
        /// <param name="line">The line to parse</param>
        /// <param name="lineCount">Current line count for error messages</param>
        private void ParseDeclareVariable(string line, int lineCount)
        {
            try
            {
                //1       2      3 4
                //declare result = ""
                var split = line.Split(new char[] { ' ' }, 4);
                if(split[2] != "=" || split[3].Contains('"') == false)
                {
                    throw new InvalidDataException("Script error at line " + lineCount + ": Variable was not assigned a value");
                }
                var searchedResult = DeclaredValues.Where(kvp => kvp.Key == split[1]);
                if (searchedResult != null)
                    DeclaredValues.Add(new KeyValuePair<string, string>(split[1], split[3]));
                else
                {
                    for(int i = 0; i < DeclaredValues.Count; i++)
                    {
                        if (DeclaredValues[i].Key == split[1])
                            DeclaredValues.RemoveAt(i);
                    }

                    DeclaredValues.Add(new KeyValuePair<string, string>(split[1], split[3]));
                }
            }
            catch (InvalidDataException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nERROR: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(ex.Message);
            }
        }
    }

}
