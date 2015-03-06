using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicScriptingLanguageEditor
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            bslEditorVerLabel.Text = String.Format("v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            FileVersionInfo dllInfo = FileVersionInfo.GetVersionInfo(Program.ExecutableLocation + @"\BasicScriptingLanguage.dll");

            bslDllVerLabel.Text = dllInfo.ProductVersion;
        }
    }
}
