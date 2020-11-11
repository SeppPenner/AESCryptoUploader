namespace AESCryptoUploader
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.buttonChooseFiles = new System.Windows.Forms.Button();
            this.buttonChooseOutputFolder = new System.Windows.Forms.Button();
            this.labelOutputFolder = new System.Windows.Forms.Label();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.tableLayoutPanelLeft = new System.Windows.Forms.TableLayoutPanel();
            this.labelFiles = new System.Windows.Forms.Label();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxOutputFolder = new System.Windows.Forms.ListBox();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelBottomRight = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelBottom = new System.Windows.Forms.TableLayoutPanel();
            this.buttonEncryptAndUpload = new System.Windows.Forms.Button();
            this.menuStripAccountInfos = new System.Windows.Forms.MenuStrip();
            this.AccountInfosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelLeft.SuspendLayout();
            this.tableLayoutPanelRight.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelBottom.SuspendLayout();
            this.menuStripAccountInfos.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonChooseFiles
            // 
            this.buttonChooseFiles.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonChooseFiles.Location = new System.Drawing.Point(3, 33);
            this.buttonChooseFiles.Name = "buttonChooseFiles";
            this.buttonChooseFiles.Size = new System.Drawing.Size(111, 24);
            this.buttonChooseFiles.TabIndex = 0;
            this.buttonChooseFiles.Text = "Dateien auswählen";
            this.buttonChooseFiles.UseVisualStyleBackColor = true;
            this.buttonChooseFiles.Click += new System.EventHandler(this.ButtonChooseFilesClick);
            // 
            // buttonChooseOutputFolder
            // 
            this.buttonChooseOutputFolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonChooseOutputFolder.Location = new System.Drawing.Point(3, 33);
            this.buttonChooseOutputFolder.Name = "buttonChooseOutputFolder";
            this.buttonChooseOutputFolder.Size = new System.Drawing.Size(151, 24);
            this.buttonChooseOutputFolder.TabIndex = 1;
            this.buttonChooseOutputFolder.Text = "Ausgabeordner auswählen";
            this.buttonChooseOutputFolder.UseVisualStyleBackColor = true;
            this.buttonChooseOutputFolder.Click += new System.EventHandler(this.ButtonChooseFolderClick);
            // 
            // labelOutputFolder
            // 
            this.labelOutputFolder.AutoSize = true;
            this.labelOutputFolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelOutputFolder.Location = new System.Drawing.Point(3, 60);
            this.labelOutputFolder.Name = "labelOutputFolder";
            this.labelOutputFolder.Size = new System.Drawing.Size(82, 20);
            this.labelOutputFolder.TabIndex = 3;
            this.labelOutputFolder.Text = "Ausgabeordner:";
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.Location = new System.Drawing.Point(3, 83);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(293, 110);
            this.listBoxFiles.TabIndex = 7;
            // 
            // tableLayoutPanelLeft
            // 
            this.tableLayoutPanelLeft.ColumnCount = 1;
            this.tableLayoutPanelLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLeft.Controls.Add(this.listBoxFiles, 0, 3);
            this.tableLayoutPanelLeft.Controls.Add(this.labelFiles, 0, 2);
            this.tableLayoutPanelLeft.Controls.Add(this.buttonChooseFiles, 0, 1);
            this.tableLayoutPanelLeft.Controls.Add(this.labelLanguage, 0, 0);
            this.tableLayoutPanelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLeft.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            this.tableLayoutPanelLeft.RowCount = 4;
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelLeft.Size = new System.Drawing.Size(299, 196);
            this.tableLayoutPanelLeft.TabIndex = 8;
            // 
            // labelFiles
            // 
            this.labelFiles.AutoSize = true;
            this.labelFiles.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelFiles.Location = new System.Drawing.Point(3, 60);
            this.labelFiles.Name = "labelFiles";
            this.labelFiles.Size = new System.Drawing.Size(47, 20);
            this.labelFiles.TabIndex = 10;
            this.labelFiles.Text = "Dateien:";
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelLanguage.Location = new System.Drawing.Point(3, 0);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(0, 30);
            this.labelLanguage.TabIndex = 11;
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRight.Controls.Add(this.listBoxOutputFolder, 0, 3);
            this.tableLayoutPanelRight.Controls.Add(this.labelOutputFolder, 0, 2);
            this.tableLayoutPanelRight.Controls.Add(this.buttonChooseOutputFolder, 0, 1);
            this.tableLayoutPanelRight.Controls.Add(this.comboBoxLanguage, 0, 0);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(308, 3);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 4;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(300, 196);
            this.tableLayoutPanelRight.TabIndex = 9;
            // 
            // listBoxOutputFolder
            // 
            this.listBoxOutputFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxOutputFolder.FormattingEnabled = true;
            this.listBoxOutputFolder.Location = new System.Drawing.Point(3, 83);
            this.listBoxOutputFolder.Name = "listBoxOutputFolder";
            this.listBoxOutputFolder.Size = new System.Drawing.Size(294, 110);
            this.listBoxOutputFolder.TabIndex = 11;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(3, 3);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(151, 21);
            this.comboBoxLanguage.TabIndex = 12;
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLanguageSelectedIndex);
            // 
            // tableLayoutPanelBottomRight
            // 
            this.tableLayoutPanelBottomRight.ColumnCount = 1;
            this.tableLayoutPanelBottomRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelBottomRight.Location = new System.Drawing.Point(308, 205);
            this.tableLayoutPanelBottomRight.Name = "tableLayoutPanelBottomRight";
            this.tableLayoutPanelBottomRight.RowCount = 1;
            this.tableLayoutPanelBottomRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelBottomRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanelBottomRight.Size = new System.Drawing.Size(300, 29);
            this.tableLayoutPanelBottomRight.TabIndex = 10;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelBottom, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelLeft, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelBottomRight, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelRight, 1, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(611, 237);
            this.tableLayoutPanelMain.TabIndex = 11;
            // 
            // tableLayoutPanelBottom
            // 
            this.tableLayoutPanelBottom.ColumnCount = 1;
            this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelBottom.Controls.Add(this.buttonEncryptAndUpload, 0, 0);
            this.tableLayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelBottom.Location = new System.Drawing.Point(3, 205);
            this.tableLayoutPanelBottom.Name = "tableLayoutPanelBottom";
            this.tableLayoutPanelBottom.RowCount = 1;
            this.tableLayoutPanelBottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelBottom.Size = new System.Drawing.Size(299, 29);
            this.tableLayoutPanelBottom.TabIndex = 11;
            // 
            // buttonEncryptAndUpload
            // 
            this.buttonEncryptAndUpload.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonEncryptAndUpload.Location = new System.Drawing.Point(3, 3);
            this.buttonEncryptAndUpload.Name = "buttonEncryptAndUpload";
            this.buttonEncryptAndUpload.Size = new System.Drawing.Size(173, 24);
            this.buttonEncryptAndUpload.TabIndex = 5;
            this.buttonEncryptAndUpload.Text = "Verschlüsseln und hochladen";
            this.buttonEncryptAndUpload.UseVisualStyleBackColor = true;
            this.buttonEncryptAndUpload.Click += new System.EventHandler(this.ButtonEncryptAndUploadClick);
            // 
            // menuStripAccountInfos
            // 
            this.menuStripAccountInfos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.AccountInfosToolStripMenuItem});
            this.menuStripAccountInfos.Location = new System.Drawing.Point(0, 0);
            this.menuStripAccountInfos.Name = "menuStripAccountInfos";
            this.menuStripAccountInfos.Size = new System.Drawing.Size(611, 24);
            this.menuStripAccountInfos.TabIndex = 12;
            this.menuStripAccountInfos.Text = "Menu";
            // 
            // AccountInfosToolStripMenuItem
            // 
            this.AccountInfosToolStripMenuItem.Name = "AccountInfosToolStripMenuItem";
            this.AccountInfosToolStripMenuItem.Size = new System.Drawing.Size(140, 20);
            this.AccountInfosToolStripMenuItem.Text = "Accountinformationen";
            this.AccountInfosToolStripMenuItem.Click += new System.EventHandler(this.AccountInfosToolStripMenuItemClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 261);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.menuStripAccountInfos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripAccountInfos;
            this.Name = "Main";
            this.tableLayoutPanelLeft.ResumeLayout(false);
            this.tableLayoutPanelLeft.PerformLayout();
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.tableLayoutPanelRight.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelBottom.ResumeLayout(false);
            this.menuStripAccountInfos.ResumeLayout(false);
            this.menuStripAccountInfos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonChooseFiles;
        private System.Windows.Forms.Button buttonChooseOutputFolder;
        private System.Windows.Forms.Label labelOutputFolder;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLeft;
        private System.Windows.Forms.Label labelFiles;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottomRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.ListBox listBoxOutputFolder;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottom;
        private System.Windows.Forms.Button buttonEncryptAndUpload;
        private System.Windows.Forms.MenuStrip menuStripAccountInfos;
        private System.Windows.Forms.ToolStripMenuItem AccountInfosToolStripMenuItem;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
    }
}