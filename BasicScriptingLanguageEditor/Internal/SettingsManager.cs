using Setting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScriptingLanguageEditor.Internal
{
    class SettingsManager
    {
        public IniFile MainIniFile;
        public List<string> AlreadyOpenFiles = new List<string>();
        //private static List<string> RecentFilesList = new List<string>();

        public SettingsManager(string iniFile)
        {
            if(File.Exists(iniFile) != true)
            {
                File.CreateText(Environment.CurrentDirectory + @"\config.ini");
            }
            MainIniFile = new IniFile(iniFile);
        }

        //Recent files list will be long, it'll look like this
        //AlreadyOpenFiles="C:\\Users\\Mike\\Desktop\\test.bsl","C:\\Users\\Mike\\Documents\\GitHub\\BasicScriptingLanguage\\another file.bsl"
        public int LoadRecentFilesList()
        {
            try
            {
                string AlreadyOpenFilesValue = MainIniFile.ReadValue("Settings", "AlreadyOpenFiles");
                var split = AlreadyOpenFilesValue.Split(',');

                if (split[0] == "")
                    return 1;

                for (int i = 0; i < split.Count(); i++)
                    AlreadyOpenFiles.Add(split[i].Trim('"'));

                return 0;
            }
            catch (Exception ex)
            { return 1; }
        }

    }
}
