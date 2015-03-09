using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicScriptingLanguageEditor
{
    class SingleInstanceController : WindowsFormsApplicationBase
    {
        private TabbedUI mainForm = new TabbedUI();

        public SingleInstanceController()
        {
            this.IsSingleInstance = true;

            this.StartupNextInstance += new
              StartupNextInstanceEventHandler(this_StartupNextInstance);
        }

        public SingleInstanceController(string fileToOpen)
        {
            this.IsSingleInstance = true;

            this.StartupNextInstance += new
              StartupNextInstanceEventHandler(this_StartupNextInstance);

            mainForm = new TabbedUI(fileToOpen);
        }

        public SingleInstanceController(string[] filesToOpen)
        {
            this.IsSingleInstance = true;

            this.StartupNextInstance += new
              StartupNextInstanceEventHandler(this_StartupNextInstance);

            mainForm = new TabbedUI(filesToOpen);
        }

        private void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            if(e.CommandLine != null)
            {
                string[] trueArgs = e.CommandLine.ToArray<string>();
                if(trueArgs.Length > 0)
                {
                    if (trueArgs.Length == 2)
                        mainForm.OpenFile(trueArgs[1]);
                    else
                        mainForm.OpenMultipleFiles(trueArgs);
                }
            }
        }

        protected override void OnCreateMainForm()
        {
            if (mainForm == null)
                mainForm = new TabbedUI();
            this.MainForm = mainForm;
        }
    }
}
