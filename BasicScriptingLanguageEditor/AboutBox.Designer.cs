namespace BasicScriptingLanguageEditor
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bslEditorVerLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.editorAuthorLabel = new System.Windows.Forms.Label();
            this.bslEditorLabel = new System.Windows.Forms.Label();
            this.editorIconPictureBox = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.bslDllVerLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bslTitleLabel = new System.Windows.Forms.Label();
            this.bslIconPictureBox = new System.Windows.Forms.PictureBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorIconPictureBox)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bslIconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(609, 390);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bslEditorVerLabel);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.editorAuthorLabel);
            this.tabPage1.Controls.Add(this.bslEditorLabel);
            this.tabPage1.Controls.Add(this.editorIconPictureBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(601, 364);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Editor";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // bslEditorVerLabel
            // 
            this.bslEditorVerLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bslEditorVerLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bslEditorVerLabel.Location = new System.Drawing.Point(28, 183);
            this.bslEditorVerLabel.Name = "bslEditorVerLabel";
            this.bslEditorVerLabel.Size = new System.Drawing.Size(545, 23);
            this.bslEditorVerLabel.TabIndex = 4;
            this.bslEditorVerLabel.Text = "v1.0.0.0";
            this.bslEditorVerLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(9, 212);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(584, 144);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // editorAuthorLabel
            // 
            this.editorAuthorLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.editorAuthorLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editorAuthorLabel.Location = new System.Drawing.Point(28, 160);
            this.editorAuthorLabel.Name = "editorAuthorLabel";
            this.editorAuthorLabel.Size = new System.Drawing.Size(545, 23);
            this.editorAuthorLabel.TabIndex = 2;
            this.editorAuthorLabel.Text = "by Mike Santiago";
            this.editorAuthorLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // bslEditorLabel
            // 
            this.bslEditorLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bslEditorLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bslEditorLabel.Location = new System.Drawing.Point(28, 137);
            this.bslEditorLabel.Name = "bslEditorLabel";
            this.bslEditorLabel.Size = new System.Drawing.Size(545, 23);
            this.bslEditorLabel.TabIndex = 1;
            this.bslEditorLabel.Text = "BasicScriptingLanguage Editor";
            this.bslEditorLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // editorIconPictureBox
            // 
            this.editorIconPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.editorIconPictureBox.Image = global::BasicScriptingLanguageEditor.Properties.Resources.Editor_Large;
            this.editorIconPictureBox.Location = new System.Drawing.Point(236, 6);
            this.editorIconPictureBox.MaximumSize = new System.Drawing.Size(128, 128);
            this.editorIconPictureBox.MinimumSize = new System.Drawing.Size(128, 128);
            this.editorIconPictureBox.Name = "editorIconPictureBox";
            this.editorIconPictureBox.Size = new System.Drawing.Size(128, 128);
            this.editorIconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.editorIconPictureBox.TabIndex = 0;
            this.editorIconPictureBox.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.bslDllVerLabel);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.bslTitleLabel);
            this.tabPage2.Controls.Add(this.bslIconPictureBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(601, 364);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "BasicScriptingLanguage";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // bslDllVerLabel
            // 
            this.bslDllVerLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bslDllVerLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bslDllVerLabel.Location = new System.Drawing.Point(29, 186);
            this.bslDllVerLabel.Name = "bslDllVerLabel";
            this.bslDllVerLabel.Size = new System.Drawing.Size(545, 23);
            this.bslDllVerLabel.TabIndex = 8;
            this.bslDllVerLabel.Text = "v1.0.0.0";
            this.bslDllVerLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(545, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "BasicScriptingLanguage.dll";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // bslTitleLabel
            // 
            this.bslTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bslTitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bslTitleLabel.Location = new System.Drawing.Point(29, 140);
            this.bslTitleLabel.Name = "bslTitleLabel";
            this.bslTitleLabel.Size = new System.Drawing.Size(545, 23);
            this.bslTitleLabel.TabIndex = 6;
            this.bslTitleLabel.Text = "BasicScriptingLanguage";
            this.bslTitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // bslIconPictureBox
            // 
            this.bslIconPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bslIconPictureBox.Image = global::BasicScriptingLanguageEditor.Properties.Resources.Dll_512;
            this.bslIconPictureBox.Location = new System.Drawing.Point(237, 9);
            this.bslIconPictureBox.MaximumSize = new System.Drawing.Size(128, 128);
            this.bslIconPictureBox.MinimumSize = new System.Drawing.Size(128, 128);
            this.bslIconPictureBox.Name = "bslIconPictureBox";
            this.bslIconPictureBox.Size = new System.Drawing.Size(128, 128);
            this.bslIconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bslIconPictureBox.TabIndex = 5;
            this.bslIconPictureBox.TabStop = false;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(8, 212);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(584, 144);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "BasicScriptingLanguage is a simple scripting language created in C# and the .NET " +
    "Framework intended for extending functionality of applications to basic extents." +
    "\r\n";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 390);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.AboutBox_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorIconPictureBox)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bslIconPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox editorIconPictureBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label editorAuthorLabel;
        private System.Windows.Forms.Label bslEditorLabel;
        private System.Windows.Forms.Label bslEditorVerLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label bslDllVerLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label bslTitleLabel;
        private System.Windows.Forms.PictureBox bslIconPictureBox;
        private System.Windows.Forms.TextBox textBox2;
    }
}