using BasicScriptingLanguageEditor.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicScriptingLanguageEditor
{
    public partial class TabbedUI : Form
    {
        SettingsManager MainSettingsManager = new SettingsManager(Environment.CurrentDirectory + @"\config.ini");

        public TabbedUI()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            AddTab();
            fileStatusLabel.Text = "";
        }

        public TabbedUI(string fileToOpen)
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            AddTab();

            EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];
            tabControlsEditor.LoadFile(fileToOpen);
            tabControl1.TabPages[tabControl1.SelectedIndex].Text = Path.GetFileName(fileToOpen);
            this.Text = "BasicScriptingLanuage Editor - " + Path.GetFileName(fileToOpen);
            fileStatusLabel.Text = "Path: " + fileToOpen;
        }

        public TabbedUI(string[] filesToOpen)
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();

            OpenMultipleFiles(filesToOpen);
        }

        private void AddTab()
        {
            tabControl1.Visible = true;

            tabControl1.TabPages.Add("New Document");
            EditorControl newEditorControl = new EditorControl();
            newEditorControl.Dock = DockStyle.Fill;
            newEditorControl.Name = "EDITORCONTROL";

            newEditorControl.fastColoredTextBox1.TextChanged += HandleTextChangedEvent;

            tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(newEditorControl);
            tabControl1.TabPages[tabControl1.TabPages.Count - 1].ImageIndex = 0;
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
            tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Find("EDITORCONTROL", false)[0].Select();
            tabControl1.TabPages[tabControl1.TabPages.Count - 1].UseVisualStyleBackColor = true;
        }

        private void OpenMultipleFiles(string[] files)
        {
            for(int i = 0; i < files.Count(); i++)
            {
                AddTab();
                try
                {
                    EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];
                    tabControlsEditor.LoadFile(files[i]);
                    tabControl1.TabPages[tabControl1.SelectedIndex].Text = Path.GetFileName(files[i]);
                    this.Text = "BasicScriptingLanuage Editor - " + Path.GetFileName(files[i]);
                    fileStatusLabel.Text = "Path: " + files[i];
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error loading file: " + ex.Message,
                        "BasicScriptingLanguage Editor",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                }
            }
        }

        private DialogResult SaveAs()
        {
            EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];

            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "BasicScriptFile|*.bsl|Other (Everything)|*.*";
            DialogResult dr = sf.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (tabControlsEditor.SaveFile(sf.FileName) == 0)
                {
                    fileStatusLabel.Text = string.Format("Saved to {0} successfully!", Path.GetFileName(sf.FileName));
                    tabControl1.TabPages[tabControl1.SelectedIndex].Text = Path.GetFileName(sf.FileName);
                    this.Text = "BasicScriptingLanuage Editor - " + Path.GetFileName(sf.FileName);
                    return DialogResult.OK;
                }
                else
                    return System.Windows.Forms.DialogResult.Cancel;
            }
            else if (dr == System.Windows.Forms.DialogResult.Cancel)
                return DialogResult.Cancel;

            return DialogResult.Cancel;
        }

        private void Save()
        {
            EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];
            if (File.Exists(tabControlsEditor.CurrentFile))
            {
                if (tabControlsEditor.SaveFile(tabControlsEditor.CurrentFile) == 0)
                {
                    fileStatusLabel.Text = string.Format("Saved to {0} successfully!", Path.GetFileName(tabControlsEditor.CurrentFile));
                    tabControl1.TabPages[tabControl1.SelectedIndex].Text = tabControl1.TabPages[tabControl1.SelectedIndex].Text.Trim('*');
                }
            }
            else
                SaveAs();
        }

        private void HandleTextChangedEvent(object sender, EventArgs e)
        {
            FastColoredTextBoxNS.FastColoredTextBox castAsTextBox = (FastColoredTextBoxNS.FastColoredTextBox)sender;
            EditorControl castAsParent = (EditorControl)castAsTextBox.Parent;

            if (castAsParent.HasChanges)
                if (castAsParent.Parent.Text.Contains('*') != true)
                    castAsParent.Parent.Text += "*";
                else
                { }
            else
                castAsParent.Parent.Text = castAsParent.Parent.Text.Trim('*');
        }

        #region Event handlers..

        //Add tab
        private void menuItem17_Click(object sender, EventArgs e)
        {
            AddTab();
        }

        //close tab
        private void menuItem18_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != -1)
            {
                int newSelected = tabControl1.SelectedIndex - 1;

                if (newSelected == -1)
                    newSelected = 0;

                EditorControl editor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", false)[0];
                bool isCancelling = false;

                if(editor.HasChanges)
                {
                    DialogResult dr = MessageBox.Show(Path.GetFileName(editor.CurrentFile) + " has unsaved changes, do you wish to save them before exiting?", 
                        "BasicScriptingLanguage Editor", 
                        MessageBoxButtons.YesNoCancel, 
                        MessageBoxIcon.Information);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (editor.CurrentFile == "New Document")
                        {
                            if (SaveAs() == System.Windows.Forms.DialogResult.Cancel)
                                isCancelling = true;
                            else
                                isCancelling = false;
                        }
                        else
                        {
                            if (File.Exists(editor.CurrentFile))
                                editor.SaveFile(editor.CurrentFile);
                        }
                    }
                    else if (dr == System.Windows.Forms.DialogResult.No)
                    { isCancelling = false; }
                    else if (dr == System.Windows.Forms.DialogResult.Cancel)
                        isCancelling = true; ;
                }

                if (!isCancelling)
                {
                    tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                    try
                    {
                        tabControl1.SelectedIndex = newSelected;
                    }
                    catch
                    { /*nothing*/ }
                }

                if (tabControl1.TabPages.Count == 0)
                    tabControl1.Visible = false;
            }
        }

        
        //Openfile
        private void menuItem7_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "BasicScriptFiles|*.bsl|Everything|*.*";
            if(of.ShowDialog() == DialogResult.OK)
            {
                if (tabControl1.SelectedIndex == -1)
                    AddTab();
                foreach(var item in tabControl1.TabPages[tabControl1.SelectedIndex].Controls)
                {
                    Control itemm = (Control)item;
                    if(itemm.Name == "EDITORCONTROL")
                    {
                        EditorControl castControlAsEditor = (EditorControl)itemm;

                        if (castControlAsEditor.CurrentFile == "New Document" && castControlAsEditor.HasChanges != true)
                        {
                            EditorControl toOpenFileIn = (EditorControl)itemm;
                            toOpenFileIn.LoadFile(of.FileName);
                            tabControl1.TabPages[tabControl1.SelectedIndex].Text = Path.GetFileName(of.FileName);
                            this.Text = "BasicScriptingLanguage Editor - " + Path.GetFileName(of.FileName);
                            fileStatusLabel.Text = "Path: " + of.FileName;
                        }
                        else if(castControlAsEditor.HasChanges == true | castControlAsEditor.CurrentFile != "New Document")
                        {
                            AddTab();
                            EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];
                            tabControlsEditor.LoadFile(of.FileName);
                            tabControl1.TabPages[tabControl1.SelectedIndex].Text = Path.GetFileName(of.FileName);
                            this.Text = "BasicScriptingLanguage Editor - " + Path.GetFileName(of.FileName);
                            fileStatusLabel.Text = "Path: " + of.FileName;
                        }
                    }
                }
            }
        }

        //Exit
        private void menuItem11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Save
        private void menuItem8_Click(object sender, EventArgs e)
        {
            Save();
        }

        //Save As
        private void menuItem9_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != -1)
            {
                EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];
                if (tabControlsEditor.CurrentFile != "New Document")
                    fileStatusLabel.Text = "Path: " + tabControlsEditor.CurrentFile;
                else
                    fileStatusLabel.Text = "";
            }
        }

        private bool cancelling = false;
        private void TabbedUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancelling = false;
            if (!cancelling)
            {
                int index = 0;
                foreach (TabPage page in tabControl1.TabPages)
                {
                    tabControl1.SelectedIndex = index;
                    foreach (Control control in page.Controls)
                    {
                        if (control.Name == "EDITORCONTROL")
                        {
                            EditorControl castAsEditor = (EditorControl)control;
                            if (castAsEditor.HasChanges == true)
                            {
                                DialogResult dr = MessageBox.Show(string.Format("{0} has unsaved changes. Would you like to save them?", Path.GetFileName(castAsEditor.CurrentFile)),
                                    "BasicScriptingLanguage Editor",
                                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                if (dr == System.Windows.Forms.DialogResult.Yes)
                                {
                                    //save
                                    if (castAsEditor.CurrentFile == "New Document")
                                    {
                                        if (SaveAs() == DialogResult.Cancel)
                                            e.Cancel = true;
                                        else
                                            e.Cancel = false;
                                    }
                                    else
                                    {
                                        if (File.Exists(castAsEditor.CurrentFile))
                                        {
                                            castAsEditor.SaveFile(castAsEditor.CurrentFile);
                                            e.Cancel = false;
                                        }
                                        else
                                            if (SaveAs() == DialogResult.Cancel)
                                                e.Cancel = true;
                                            else
                                                e.Cancel = false;
                                    }
                                    
                                }
                                else if (dr == System.Windows.Forms.DialogResult.No)
                                {
                                    e.Cancel = false;
                                }
                                else if (dr == System.Windows.Forms.DialogResult.Cancel)
                                {
                                    e.Cancel = true;
                                    cancelling = true;
                                }
                            }
                            //
                        }
                        //
                    }
                    index++;
                }
            }
            int index2 = 0;
            List<string> FilesToBeReopened = new List<string>();
            foreach(var page in tabControl1.TabPages)
            {
                tabControl1.SelectedIndex = index2;
                EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];

                if (tabControlsEditor.CurrentFile != "New Document")
                    FilesToBeReopened.Add(tabControlsEditor.CurrentFile);

                index2++;
            }
            string built = "";
            for(int i = 0; i < FilesToBeReopened.Count(); i++)
            {
                if(i == (FilesToBeReopened.Count() - 1))
                    built += "\"" + FilesToBeReopened[i] + "\"";
                else
                    built += "\"" + FilesToBeReopened[i] + "\",";
            }
            if (built != "")
                MainSettingsManager.MainIniFile.WriteValue("Settings", "AlreadyOpenFiles", built);
            //
        }

        //New
        private void menuItem5_Click(object sender, EventArgs e)
        {
            EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];
            if (tabControlsEditor.HasChanges)
                AddTab();
            else
            { 
                tabControlsEditor.NewFile();
                tabControl1.TabPages[tabControl1.SelectedIndex].Text = "New Document";
                this.Text = "BasicScriptingLanuage Editor - " + "New Document";
                fileStatusLabel.Text = "";
            }
        }

        //Register
        private void menuItem20_Click(object sender, EventArgs e)
        {
            try
            {
                Program.RegisterFileAssociations();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: \nTry running the program as administrator." + ex.Message, "BasicScriptingLanguage Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Test script
        private void menuItem22_Click(object sender, EventArgs e)
        {
            EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];
            TestScript ts;

            if(tabControlsEditor.CurrentFile != "New Document")
            {
                if(tabControlsEditor.HasChanges)
                {
                    tabControlsEditor.SaveFile(tabControlsEditor.CurrentFile);
                    fileStatusLabel.Text = string.Format("Saved to {0} successfully!", Path.GetFileName(tabControlsEditor.CurrentFile));
                    tabControl1.TabPages[tabControl1.SelectedIndex].Text = tabControl1.TabPages[tabControl1.SelectedIndex].Text.Trim('*');
                }

                ts = new TestScript(tabControlsEditor.CurrentFile);
                ts.ShowDialog();
            }
            else
            {
                DialogResult dr = MessageBox.Show("The current file must be saved to the disk before testing the script. Continue with saving?", 
                    "BasicScriptingLanguage Editor",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                if(dr == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveAs();
                    tabControlsEditor.SaveFile(tabControlsEditor.CurrentFile);
                    fileStatusLabel.Text = string.Format("Saved to {0} successfully!", Path.GetFileName(tabControlsEditor.CurrentFile));
                    tabControl1.TabPages[tabControl1.SelectedIndex].Text = tabControl1.TabPages[tabControl1.SelectedIndex].Text.Trim('*');
                    ts = new TestScript(tabControlsEditor.CurrentFile);
                    ts.ShowDialog();
                }
            }
        }

        //Select All
        private void menuItem15_Click(object sender, EventArgs e)
        {
            EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];
            tabControlsEditor.fastColoredTextBox1.SelectAll();
        }

        //Paste
        private void menuItem14_Click(object sender, EventArgs e)
        {
            EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];

            if(Clipboard.ContainsText(TextDataFormat.Text))
                tabControlsEditor.fastColoredTextBox1.InsertText(Clipboard.GetData(DataFormats.StringFormat).ToString());
        }

        //Copy
        private void menuItem13_Click(object sender, EventArgs e)
        {
            EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];

            tabControlsEditor.fastColoredTextBox1.Copy();
        }

        //Cut
        private void menuItem12_Click(object sender, EventArgs e)
        {
            EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];
            tabControlsEditor.fastColoredTextBox1.Cut();
        }

        private void TabbedUI_Load(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(this.Width - (pictureBox1.Size.Width - (pictureBox1.Size.Width/2/2/2)), this.Height - pictureBox1.Size.Height);

            tabControl1.Visible = true;
            tabControl1.Dock = DockStyle.Fill;

            if(MainSettingsManager.LoadRecentFilesList() != 1)
            {
                OpenMultipleFiles(MainSettingsManager.AlreadyOpenFiles.ToArray<string>());
                foreach (TabPage page in tabControl1.TabPages)
                    if (page.Text == "New Document")
                        page.Dispose();
            }
        }

        //About
        private void menuItem16_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }
        //end of class
        #endregion
    }
}
