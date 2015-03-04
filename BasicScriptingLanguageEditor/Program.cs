using BasicScriptingLanguageEditor.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicScriptingLanguageEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if(args.Length > 0)
            {
                Application.Run(new TabbedUI(args[0]));
            }
            else
                Application.Run(new TabbedUI());
        }

        public static void RegisterFileAssociations()
        {
            Icon FileFormatIcon = BasicScriptingLanguageEditor.Properties.Resources.Format_BSL;

            using (FileStream fs = new FileStream(Environment.CurrentDirectory + @"\format.ico", FileMode.Create))
                FileFormatIcon.Save(fs);
            /*
            // Initializes a new AF_FileAssociator to associate the .ABC file extension.
            if (!FileAssociation.IsAssociated(".bsl"))
            {
                FileAssociation.Associate(".bsl",
                    "BSL_File",
                    "BasicScriptingLanguage script source file",
                    Environment.CurrentDirectory + @"\format.ico",
                    Assembly.GetExecutingAssembly().Location);
                MessageBox.Show("Registered file types successfully!", "BasicScriptingLanguage Editor");
            }
            else
                MessageBox.Show("File associations already registered.", "BasicScriptingLanguage Editor");*/
            string path = Environment.CurrentDirectory + @"\format.ico";
            Create_abc_FileAssociation(path);
            MessageBox.Show("Attempted file assosciation successfully", "BasicScriptingLanguage Editor");
        }

        private static void Create_abc_FileAssociation(string iconPath)
        {
            /***********************************/
            /**** Key1: Create ".abc" entry ****/
            /***********************************/
            Microsoft.Win32.RegistryKey key1 = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);

            key1.CreateSubKey("Classes");
            key1 = key1.OpenSubKey("Classes", true);

            key1.CreateSubKey(".bsl");
            key1 = key1.OpenSubKey(".bsl", true);
            key1.SetValue("", "BSLFile"); // Set default key value

            key1.Close();

            /*******************************************************/
            /**** Key2: Create "DemoKeyValue\DefaultIcon" entry ****/
            /*******************************************************/
            Microsoft.Win32.RegistryKey key2 = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);

            key2.CreateSubKey("Classes");
            key2 = key2.OpenSubKey("Classes", true);

            key2.CreateSubKey("BSLFile");
            key2 = key2.OpenSubKey("BSLFile", true);

            key2.CreateSubKey("DefaultIcon");
            key2 = key2.OpenSubKey("DefaultIcon", true);
            key2.SetValue("", "\"" + iconPath + "\""); // Set default key value

            key2.Close();

            /**************************************************************/
            /**** Key3: Create "DemoKeyValue\shell\open\command" entry ****/
            /**************************************************************/
            Microsoft.Win32.RegistryKey key3 = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true);

            key3.CreateSubKey("Classes");
            key3 = key3.OpenSubKey("Classes", true);

            key3.CreateSubKey("BSLFile");
            key3 = key3.OpenSubKey("BSLFile", true);

            key3.CreateSubKey("shell");
            key3 = key3.OpenSubKey("shell", true);

            key3.CreateSubKey("open");
            key3 = key3.OpenSubKey("open", true);

            key3.CreateSubKey("command");
            key3 = key3.OpenSubKey("command", true);
            key3.SetValue("", "\"" + Assembly.GetExecutingAssembly().Location + "\"" + " \"%1\""); // Set default key value

            key3.Close();
        }

    }
}
