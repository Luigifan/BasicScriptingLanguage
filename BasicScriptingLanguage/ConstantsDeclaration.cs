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


        public static string RetrieveConstantValue(string line)
        {
            if (line == "CURRENTDIRECTORY")
                return CURRENTDIRECTORY;

            return "ayy lmao";
        }

    }
}
