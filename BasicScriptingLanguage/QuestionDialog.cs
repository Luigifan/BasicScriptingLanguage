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
    class QuestionDialog
    {
        /// <summary>
        /// Shows a question dialog for the user
        /// </summary>
        /// <param name="line">Line to parse</param>
        /// <param name="lineCount">Line number</param>
        /// <returns> The dialog result as a string
        /// </returns>
        public static string ShowQuestionDialog(string line, int lineCount)
        {
            //(max of 3)
            //showQuestionDialog|"body","Yes","No","Unsure"
            try
            {
                var split = line.Split('|');
                //splits into 
                //showQuestionDialog
                //and
                //"body","yes","no","unsure"
                if(split[1].Contains(',') != true)
                {
                    DialogResult dr = MessageBox.Show(BasicScriptFile.MainScriptInterepreter.ReplaceWithVarsConstants(split[1].Trim('"')), 
                        ScriptMetadata.SCRIPT_TITLE);
                    return dr.ToString();
                }
                else
                {
                    //Splits into parts of args
                    //Count 2 if one button
                    //3 if two buttons
                    //4 if 3 buttons
                    //splitArgs[0] is the body
                    //var splitArgs = split[1].Split(',');
                    var splitArgs = Regex.Split(split[1], @",(?=(?:[^""]*""[^""]*"")*[^""]*$)");
                    DialogResult dr;
                    switch(splitArgs.Count())
                    {
                        case(2):
                            MessageBoxManager.OK = splitArgs[1].Trim('"').Trim('"');
                            MessageBoxManager.Register();
                            dr = MessageBox.Show(BasicScriptFile.MainScriptInterepreter.ReplaceWithVarsConstants(splitArgs[0].Trim('"')), 
                                ScriptMetadata.SCRIPT_TITLE, 
                                MessageBoxButtons.OK);
                            MessageBoxManager.Unregister();

                            return splitArgs[1];
                        case(3):
                            MessageBoxManager.OK = splitArgs[1].Trim('"');
                            MessageBoxManager.Cancel = splitArgs[2].Trim('"');
                            MessageBoxManager.Register();
                            dr = MessageBox.Show(BasicScriptFile.MainScriptInterepreter.ReplaceWithVarsConstants(splitArgs[0].Trim('"')),
                                ScriptMetadata.SCRIPT_TITLE, 
                                MessageBoxButtons.OKCancel);
                            MessageBoxManager.Unregister();
                            if (dr == DialogResult.OK)
                                return splitArgs[1];
                            else if (dr == DialogResult.Cancel)
                                return splitArgs[2];
                            return dr.ToString();
                        case(4):
                            MessageBoxManager.Yes = splitArgs[1].Trim('"');
                            MessageBoxManager.No = splitArgs[2].Trim('"');
                            MessageBoxManager.Cancel = splitArgs[3].Trim('"');
                            MessageBoxManager.Register();
                            dr = MessageBox.Show(BasicScriptFile.MainScriptInterepreter.ReplaceWithVarsConstants(splitArgs[0].Trim('"')),
                                ScriptMetadata.SCRIPT_TITLE, 
                                MessageBoxButtons.YesNoCancel);
                            MessageBoxManager.Unregister();

                            if(dr == DialogResult.Yes)
                                return splitArgs[1];
                            else if(dr == DialogResult.No)
                                return splitArgs[2];
                            else if(dr == DialogResult.Cancel)
                                return splitArgs[3];

                            return dr.ToString();
                    }
                }
            }
            catch (InvalidDataException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nERROR: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(ex.Message);
            }
            return "THISISANERROR";
        }
    }
}
