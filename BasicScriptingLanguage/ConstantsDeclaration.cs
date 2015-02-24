using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScriptingLanguage
{
    class Constants
    {
        public static string CURRENTDIRECTORY = Environment.CurrentDirectory;


        public static string ReplaceConstantsInString(string line)
        {
            string replaced = "";
            try
            {
                replaced = line.Replace("{CURRENTDIRECTORY}", CURRENTDIRECTORY);
            }
            catch
            { /*Nothing more to replace!*/ }
            return replaced;
        }

    }
}
