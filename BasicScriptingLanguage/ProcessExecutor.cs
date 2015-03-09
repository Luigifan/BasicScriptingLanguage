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
                //runProcess|"{CURRENTDIRECTORY}\\editor.exe"
                var split = line.Split(new char[]{'|', ','}, 3);

                string whatToExecute = BasicScriptFile.MainScriptInterepreter.ReplaceWithVarsConstants(split[1]);
                
                    Process p = new Process();
                    p.StartInfo.FileName = whatToExecute.Trim('"');
                    if (line.Contains(','))
                        p.StartInfo.Arguments = BasicScriptFile.MainScriptInterepreter.ReplaceWithVarsConstants(split[2]).Trim('"');
                    //p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.UseShellExecute = false;
                    p.OutputDataReceived += new DataReceivedEventHandler
                    (
                        (s, e) =>
                            {
                                if(e.Data != null && e.Data != "")
                                    Console.WriteLine("> {0}", e.Data);
                            }
                    );
                    p.ErrorDataReceived += new DataReceivedEventHandler
                    (
                        (s, e) =>
                        {
                            if (e.Data != null && e.Data != "")
                                Console.WriteLine("> {0}", e.Data);
                        }
                    );
                    p.Start();
                    p.BeginOutputReadLine();
                    p.BeginErrorReadLine();
                    p.WaitForExit();
            }
            catch(System.IO.InvalidDataException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nERROR: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(ex.Message + "\n");
            }
        }
    }
}
