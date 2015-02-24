using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            //showQuestionDialog("body of message","Yes","No","Unsure");
            try
            {
                var split = line.Split(new char[] { '(', ')' });
                if (split[0] == "showQuestionDialog")
                {
                    //split[0] is 'showQuestionDialog'
                    //split[1] is "body of message","yes","no","unsure"
                    var parsingParams = split[1].Split(new char[] { ',' });
                    //should be no more than 4 (body and 3 options)
                    if (parsingParams.Count() > 4)
                        throw new InvalidDataException("Script error at line " + lineCount + ": Too many arguments (no more than 4, got " + parsingParams.Count() + ")");
                    else
                    {
                        if (parsingParams.Count() == 2)
                        {
                            MessageBoxManager.OK = parsingParams[1].Trim('"');
                            DialogResult dr = MessageBox.Show(parsingParams[0], "", MessageBoxButtons.OK);
                            switch (dr)
                            {
                                case (DialogResult.OK):
                                    return parsingParams[1];
                            }
                        }
                        if (parsingParams.Count() == 3)
                        {
                            MessageBoxManager.OK = parsingParams[1].Trim('"');
                            MessageBoxManager.Cancel = parsingParams[2].Trim('"');
                            DialogResult dr = MessageBox.Show(parsingParams[0], "", MessageBoxButtons.OKCancel);
                            switch (dr)
                            {
                                case (DialogResult.OK):
                                    return parsingParams[1];
                                case (DialogResult.Cancel):
                                    return parsingParams[2];
                            }
                        }
                        if (parsingParams.Count() == 4)
                        {
                            MessageBoxManager.Yes = parsingParams[1].Trim('"');
                            MessageBoxManager.No = parsingParams[2].Trim('"');
                            MessageBoxManager.Cancel = parsingParams[3].Trim('"');
                            DialogResult dr = MessageBox.Show(parsingParams[0], "", MessageBoxButtons.YesNoCancel);
                            switch (dr)
                            {
                                case (DialogResult.Yes):
                                    return parsingParams[1];
                                case (DialogResult.No):
                                    return parsingParams[2];
                                case (DialogResult.Cancel):
                                    return parsingParams[3];
                            }
                        }
                    }
                }
                else
                {
                    throw new InvalidDataException("Script error at line " + lineCount + ": Invalid");
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
