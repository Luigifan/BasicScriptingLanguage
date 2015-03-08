using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.IO;
using System.Text.RegularExpressions;

namespace BasicScriptingLanguageEditor
{
    public partial class EditorControl : UserControl
    {
        //Styles
        Style FirstLineStyle = new TextStyle(Brushes.Purple, Brushes.Plum, FontStyle.Bold | FontStyle.Italic);
        Style MetadataStyle = new TextStyle(Brushes.Purple, Brushes.Transparent, FontStyle.Bold | FontStyle.Underline);
        Style CommentStyle = new TextStyle(Brushes.Purple, null, FontStyle.Regular);
        Style KeywordStyle = new TextStyle(Brushes.Blue, null, FontStyle.Bold);
        Style StringStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Italic);
        //
        public bool HasChanges = false;
        public string CurrentFile = "New Document";
        private bool DuringLoadOrSave = false;

        public EditorControl()
        {
            InitializeComponent();
        }

        public void NewFile()
        {
            DuringLoadOrSave = true;
            fastColoredTextBox1.Text = "";
            CurrentFile = "New Document";
            HasChanges = false;
            DuringLoadOrSave = false;
        }

        public int SaveFile(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            try
            {
                sw.Write(fastColoredTextBox1.Text);
                sw.Flush();
                sw.Close();
                CurrentFile = path;
                HasChanges = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BasicScriptingLanguage Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1;
            }

            return 0;
        }

        public int LoadFile(string path)
        {
            DuringLoadOrSave = true;

            StreamReader sr = new StreamReader(path);
            try
            {
                fastColoredTextBox1.Text = sr.ReadToEnd();
                HasChanges = false;
                DuringLoadOrSave = false;
                CurrentFile = path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BasicScriptingLanguage Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DuringLoadOrSave = false;
                return 1;
            }
            DuringLoadOrSave = false;
            sr.Close();
            return 0;
        }

        private void fastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (DuringLoadOrSave == false)
            {
                if (Text.Contains('*') != true)
                    HasChanges = true;
                else
                    HasChanges = false;
            }

            if (String.IsNullOrEmpty(fastColoredTextBox1.Text))
            {
                HasChanges = false;
            }

            //First line
            e.ChangedRange.ClearStyle(FirstLineStyle);
            e.ChangedRange.SetStyle(FirstLineStyle, @"#BasicScriptFile", RegexOptions.Multiline);
                
            //metadata
            e.ChangedRange.ClearStyle(MetadataStyle);
            e.ChangedRange.SetStyle(MetadataStyle, @"#metadata .*$", RegexOptions.Multiline);

            //Handles comments
            e.ChangedRange.ClearStyle(CommentStyle);
            e.ChangedRange.SetStyle(CommentStyle, @"#.*$", RegexOptions.Multiline);

            //Strings
            e.ChangedRange.ClearStyle(StringStyle);
            e.ChangedRange.SetStyle(StringStyle, "\".*?\"", RegexOptions.Multiline);

            //Keywords like declare, if, etc
            e.ChangedRange.ClearStyle(KeywordStyle);
            e.ChangedRange.SetStyle(KeywordStyle, @"\b(declare|if|else|elseif|exit|return|wait|endif)", RegexOptions.Multiline);
        }

    }
}
