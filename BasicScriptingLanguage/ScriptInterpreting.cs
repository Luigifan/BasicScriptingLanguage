using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

                if(IsCommand(line))
                {
                    if (line.StartsWith("echo"))
                    {
                        BasicOutput.Echo(line, lineCount);
                    }
                    else if (line.StartsWith("showQuestionDialog"))
                    {
                        QuestionDialog.ShowQuestionDialog(line, lineCount);
                    }
                    else if (line.StartsWith("runProcess"))
                        ProcessExecutor.StartProcess(line, lineCount);
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

        public string RetrieveVariableValue(string variableToRetrieve)
        {
            bool foundVar = false;
            for(int i = 0; i < DeclaredValues.Count(); i++)
            {
               if (DeclaredValues[i].Key == variableToRetrieve)
                   return DeclaredValues[i].Value;
            }
            if (foundVar == false)
                throw new KeyNotFoundException("Script error: Couldn't find the variable '" + variableToRetrieve + "'.");

            return null;
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
                    string possibleNewValue = "";
                    bool foundVar = false;
                    if(IsCommand(split[2]))
                    {
                        //showQuestionMessage("message","Yes","No","Unsure");
                        if (split[2].StartsWith("showQuestionMessage"))
                        {
                            possibleNewValue = QuestionDialog.ShowQuestionDialog(line, lineCount);
                        }
                        else
                            throw new InvalidDataException("Script error at line " + lineCount + ": Command is not allowed to be run here as it does not return a value.");
                    }
                    else
                    {
                        possibleNewValue = split[2];
                    }
                    
                    for(int i = 0; i < DeclaredValues.Count; i++)
                    {
                        if (DeclaredValues[i].Key == split[0])
                        {
                            DeclaredValues.RemoveAt(i);
                            DeclaredValues.Add(new KeyValuePair<string, string>(split[0], possibleNewValue));
                            foundVar = true;
                        }
                    }

                    if (foundVar == false)
                        throw new InvalidDataException("Script error at line " + lineCount + ": Value not previously assigned to.");
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

        private bool IsCommand(string line)
        {
            if (line.Contains("showQuestionMessage"))
                return true;
            if (line.Contains("showInfoMessage"))
                return true;
            if (line.Contains("runProcess"))
                return true;
            if (line.Contains("echo"))
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
