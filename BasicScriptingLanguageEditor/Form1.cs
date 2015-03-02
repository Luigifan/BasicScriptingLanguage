using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace BasicScriptingLanguageEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "BasicScriptFile|*.bsl|Everything|*.*";
            DialogResult dr = of.ShowDialog();
            if(dr == System.Windows.Forms.DialogResult.OK)
            {
                string sourceCode = File.ReadAllText(of.FileName);
                fastColoredTextBox1.Text = sourceCode;
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
