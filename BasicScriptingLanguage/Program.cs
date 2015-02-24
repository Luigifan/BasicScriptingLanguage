using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScriptingLanguage
{
    public class BasicScriptFile
    {
        public static ScriptInterpreting MainScriptInterepreter = new ScriptInterpreting();

        public static void ExecuteScript(string _file)
        {
            MainScriptInterepreter.ReadScript(_file);
        }

    }
}
