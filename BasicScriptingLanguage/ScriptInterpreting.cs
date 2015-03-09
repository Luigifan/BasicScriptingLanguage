using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        string[] currentScript;
        int lineCount;
        /// <summary>
        /// Begin the script handling
        /// </summary>
        /// <param name="file">File to read</param>
        
        [STAThread]
        public void ReadScript(string file)
        {
            currentScript = File.ReadAllLines(file);
            ScriptMetadata.SetMetadata("BasicScriptingLanguage", METADATATYPE.AUTHOR);
            for (lineCount = 0; lineCount < currentScript.Count(); lineCount++ )
            {
                string actualLine = Regex.Replace(currentScript[lineCount], " {2,}", "\t");
                InterpretLine(actualLine.Trim('\t'), lineCount);
            }
        }

        bool ifStatementIsFalse = false;

        private void InterpretLine(string line, int lineCount)
        {
            if (lineCount == 0)
            {
                if (line != "#BasicScriptFile")
                    throw new InvalidDataException("First line is not '#BasicScriptFile' or is not a BasicScriptFile!");
                lineCount++;
                return;
            }

            if (line == "\n")
            {
                lineCount++;
                return;
            }

            if (line.StartsWith("#"))
            {
                if (line.StartsWith("#metadata"))
                {
                    ParseMetadata(line, lineCount);
                    lineCount++;
                    return;
                }
                else
                { /*Comment, ignore*/ }
            }

            if (line.StartsWith("declare"))
            {
                ParseDeclareVariable(line, lineCount);
                lineCount++;
                return;
            }

            if (StartsWithVariable(line)) //useful for checking for variable assining/updating variables
            {
                if (!line.StartsWith("if"))
                {
                    if (IsAssigningVariable(line))
                    {
                        AssignVariable(line, lineCount);
                        lineCount++;
                        return;
                    }
                }
            }

            if (line.StartsWith("endif"))
            { ifStatementIsFalse = false; lineCount++; return; }

            if (line.StartsWith("if"))
            {
                bool ifStatementEvaluate = ParseIfStatement(line, lineCount);

                if (ifStatementEvaluate)
                {
                    lineCount++;
                    return;
                }
                else
                {
                    ifStatementIsFalse = true;
                    lineCount++;
                    return;
                }
            }

            if (ifStatementIsFalse == false)
            {
                if (IsCommand(line))
                {
                    if (line.StartsWith("echo"))
                        BasicOutput.Echo(line, lineCount);
                    else if (line.StartsWith("showQuestionDialog"))
                        QuestionDialog.ShowQuestionDialog(line, lineCount);
                    else if (line.StartsWith("runProcess"))
                        ProcessExecutor.StartProcess(line, lineCount);
                    else if (line.StartsWith("wait"))
                        BasicOutput.Wait(line, lineCount);

                    lineCount++;
                    return;
                }
            }
            lineCount++;
        }

        private void ParseMetadata(string line, int lineCount)
        {
            try
            {
                //#metadata [whatsbeingset] "value"
                var split = line.Split(new char[]{' '}, 3);
                //so now
                //split[0] is metadata
                //split[1] is what we're assigning
                //split[2] is the value

                if (split[1] == "title")
                    ScriptMetadata.SetMetadata(split[2].Trim('"'), METADATATYPE.SCRIPT_TITLE);
                else
                    throw new InvalidDataException("Metadata error at line " + lineCount + ": Metadata type not defined");
            }
            catch(InvalidDataException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nERROR: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(ex.Message);
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
            if (variableToRetrieve.Contains('[') || variableToRetrieve.Contains(']'))
                variableToRetrieve = variableToRetrieve.Trim('[', ']');
            bool foundVar = false;
            for(int i = 0; i < DeclaredValues.Count(); i++)
            {
               if (DeclaredValues[i].Key == variableToRetrieve)
                   return DeclaredValues[i].Value.Trim('"');
            }
            if (foundVar == false)
                throw new KeyNotFoundException("Script error: Couldn't find the variable '" + variableToRetrieve + "'.");

            return null;
        }


        public string ReplaceWithVarsConstants(string replaceWith)
        {
            string finalStringReplace = replaceWith;
            string patternConstants = @"{(.*?)}(.*?)";
            string patternVariables = @"\[(.*?)\](.*?)";

            Regex matchConstants = new Regex(patternConstants, RegexOptions.None);
            Regex matchVariables = new Regex(patternVariables, RegexOptions.None);

            //Console.WriteLine(matchConstants.IsMatch(finalStringReplace));
            //Console.WriteLine(matchVariables.IsMatch(finalStringReplace));

            foreach(Match testMatch in matchConstants.Matches(finalStringReplace))
            {
                if(testMatch.Value.Contains("CURRENTDIRECTORY"))
                    finalStringReplace = matchConstants.Replace(finalStringReplace, Constants.CURRENTDIRECTORY);
                else if(testMatch.Value.Contains("SYSDIR"))
                    finalStringReplace = matchConstants.Replace(finalStringReplace, Constants.SYSDIR);
                else if (testMatch.Value.Contains("WINDIR"))
                    finalStringReplace = matchConstants.Replace(finalStringReplace, Constants.WINDIR);
            }

            foreach(Match varMatch in matchVariables.Matches(finalStringReplace))
            {
                int matchAt = varMatch.Index;
                string variableName = varMatch.Value;
                finalStringReplace = matchVariables.Replace(finalStringReplace, BasicScriptFile.MainScriptInterepreter.RetrieveVariableValue(variableName));
            }

            return finalStringReplace;
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
                //if (split[1] != "|")
                //    throw new InvalidDataException("Script error at line " + lineCount + ": Expected equals sign, didn't get.");
                    string possibleNewValue = "";
                    bool foundVar = false;
                    if(IsCommand(split[2]))
                    {
                        //showQuestionMessage("message","Yes","No","Unsure");
                        if (split[2].StartsWith("showQuestionDialog"))
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
            if (line.Contains("showQuestionDialog"))
                return true;
            if (line.Contains("showInfoMessage"))
                return true;
            if (line.Contains("runProcess"))
                return true;
            if (line.Contains("echo"))
                return true;
            if (line.Contains("wait"))
                return true;

            return false;
        }

        private bool ParseIfStatement(string line, int lineCount)
        {
            try
            {
                //if diagResult|"Hi"
                var split = line.Split(new char[] { ' ', '|' }, 3);
                string varValue = RetrieveVariableValue(split[1]);
                if (varValue != null)
                {
                    if (varValue.Equals(split[2].Trim('"')))
                        return true;
                    else
                        return false;
                }
                else
                    throw new InvalidDataException("Variable doesn't exist!");
                //endif
            }
            catch(InvalidDataException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nERROR: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(ex.Message);
            }
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
                var split = line.Split(new char[] { ' ', '|' }, 3);
                if(split[2].Contains('"') == false)
                {
                    throw new InvalidDataException("Script error at line " + lineCount + ": Variable was not assigned a value");
                }
                var searchedResult = DeclaredValues.Where(kvp => kvp.Key == split[1]);
                if (searchedResult != null)
                    DeclaredValues.Add(new KeyValuePair<string, string>(split[1], split[2]));
                else
                {
                    for(int i = 0; i < DeclaredValues.Count; i++)
                    {
                        if (DeclaredValues[i].Key == split[1])
                            DeclaredValues.RemoveAt(i);
                    }

                    DeclaredValues.Add(new KeyValuePair<string, string>(split[1], split[2]));
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
