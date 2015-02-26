using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BasicScriptingLanguage
{
    class Constants
    {
        public static string CURRENTDIRECTORY = Environment.CurrentDirectory;


        public static string RetrieveConstantValue(string line)
        {
            string pattern = "\\{(.*?)\\}";
            Regex regx = new Regex(pattern, RegexOptions.None);
            string[] splitToViewInsides = regx.Split(line);
            if (splitToViewInsides.Contains("CURRENTDIRECTORY"))
                return CURRENTDIRECTORY;

            return "ayy lmao";
        }

    }
}
