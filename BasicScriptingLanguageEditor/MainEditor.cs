using FastColoredTextBoxNS;
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
    public partial class MainEditor : Form
    {
        //Styles
        Style FirstLineStyle = new TextStyle(Brushes.Purple, Brushes.Plum, FontStyle.Bold | FontStyle.Italic);
        Style CommentStyle = new TextStyle(Brushes.Purple, null, FontStyle.Regular);
        Style KeywordStyle = new TextStyle(Brushes.Blue, null, FontStyle.Bold);
        Style StringStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Italic);


        public MainEditor()
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



        public int SaveFile(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            try
            {
                sw.Write(fastColoredTextBox1.Text);
                sw.Flush();
                sw.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "BasicScriptingLanguage Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }

            return 0;
        }

        public int LoadFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            try
            {
                fastColoredTextBox1.Text = sr.ReadToEnd();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "BasicScriptingLanguage Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }

            return 0;
        }

        private void fastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if(Text.Contains('*') != true)
                this.Text += "*";

            //First line
            e.ChangedRange.ClearStyle(FirstLineStyle);
            e.ChangedRange.SetStyle(FirstLineStyle, @"#BasicScriptFile", RegexOptions.Multiline);

            //Handles comments
            e.ChangedRange.ClearStyle(CommentStyle);
            e.ChangedRange.SetStyle(CommentStyle, @"#.*$", RegexOptions.Multiline);

            //Strings
            e.ChangedRange.ClearStyle(StringStyle);
            e.ChangedRange.SetStyle(StringStyle, "\".*?\"", RegexOptions.Multiline);

            //Keywords like declare, if, etc
            e.ChangedRange.ClearStyle(KeywordStyle);
            e.ChangedRange.SetStyle(KeywordStyle, @"\b(declare|if|else|elseif|exit|return|wait)", RegexOptions.Multiline);
        }
    }
}
