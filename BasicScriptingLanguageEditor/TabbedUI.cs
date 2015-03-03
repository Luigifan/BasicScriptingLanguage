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
        public TabbedUI()
        {
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            AddTab();
            fileStatusLabel.Text = "";
        }

        private void AddTab()
        {
            tabControl1.TabPages.Add("New Document");
            EditorControl newEditorControl = new EditorControl();
            newEditorControl.Dock = DockStyle.Fill;
            newEditorControl.Name = "EDITORCONTROL";

            newEditorControl.fastColoredTextBox1.TextChanged += HandleTextChangedEvent;

            tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(newEditorControl);
            tabControl1.TabPages[tabControl1.TabPages.Count - 1].ImageIndex = 0;
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
            tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Find("EDITORCONTROL", false)[0].Select();
        }

        //Handlers

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

                EditorControl editor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", false)[0];
                bool isCancelling = false;

                if(editor.HasChanges)
                {
                    DialogResult dr = MessageBox.Show(Path.GetFileName(editor.CurrentFile) + " has unsaved changes, are you sure you wish to close this file?", 
                        "BasicScriptingLanguage Editor", 
                        MessageBoxButtons.YesNoCancel, 
                        MessageBoxIcon.Information);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (editor.CurrentFile == "New Document")
                        {
                            SaveAs();
                        }
                        else
                        {
                            if (File.Exists(editor.CurrentFile))
                                editor.SaveFile(editor.CurrentFile);
                        }
                    }
                    else if (dr == System.Windows.Forms.DialogResult.No)
                    { isCancelling = true; }
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
            }
        }

        private void SaveAs()
        {
            EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];

            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "BasicScriptFile|*.bsl|Other (Everything)|*.*";
            DialogResult dr = sf.ShowDialog();
            if(dr == System.Windows.Forms.DialogResult.OK)
            {
                if(tabControlsEditor.SaveFile(sf.FileName) == 0)
                {
                    fileStatusLabel.Text = string.Format("Saved to {0} successfully!", Path.GetFileName(sf.FileName));
                    tabControl1.TabPages[tabControl1.SelectedIndex].Text = Path.GetFileName(sf.FileName);
                    this.Text = "BasicScriptingLanuage Editor - " + Path.GetFileName(sf.FileName);
                }
            }
        }

        private void Save()
        {
            EditorControl tabControlsEditor = (EditorControl)tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Find("EDITORCONTROL", true)[0];
            if (File.Exists(tabControlsEditor.CurrentFile))
            {
                if(tabControlsEditor.SaveFile(tabControlsEditor.CurrentFile) == 0)
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
                        EditorControl toOpenFileIn = (EditorControl)itemm;
                        toOpenFileIn.LoadFile(of.FileName);
                        tabControl1.TabPages[tabControl1.SelectedIndex].Text = Path.GetFileName(of.FileName);
                        this.Text = "BasicScriptingLanuage Editor - " + Path.GetFileName(of.FileName);
                        fileStatusLabel.Text = "Path: " + of.FileName;
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
                                        SaveAs();
                                    }
                                    else
                                    {
                                        if (File.Exists(castAsEditor.CurrentFile))
                                            castAsEditor.SaveFile(castAsEditor.CurrentFile);
                                    }
                                    e.Cancel = false;
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
        //end of class
    }
}
