    namespace AESCryptoUploader
    {
        partial class AccountInfos
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
                if (disposing && (this.components != null))
                {
                    this.components.Dispose();
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
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountInfos));
                this.dataGridViewAccounts = new System.Windows.Forms.DataGridView();
                this.AccountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.UsernameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.FreeSpaceTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.TotalSpaceTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.UsedSpaceTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.UsedSpaceColumn = new Models.DataGridViewProgressColumn();
                ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccounts)).BeginInit();
                this.SuspendLayout();
                // 
                // dataGridViewAccounts
                // 
                this.dataGridViewAccounts.AllowUserToAddRows = false;
                this.dataGridViewAccounts.AllowUserToDeleteRows = false;
                this.dataGridViewAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                this.dataGridViewAccounts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
                this.dataGridViewAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                this.dataGridViewAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                    this.AccountColumn,
                    this.UsernameColumn,
                    this.FreeSpaceTextColumn,
                    this.TotalSpaceTextColumn,
                    this.UsedSpaceTextColumn,
                    this.UsedSpaceColumn});
                this.dataGridViewAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
                this.dataGridViewAccounts.Location = new System.Drawing.Point(0, 0);
                this.dataGridViewAccounts.Name = "dataGridViewAccounts";
                this.dataGridViewAccounts.ReadOnly = true;
                this.dataGridViewAccounts.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                this.dataGridViewAccounts.Size = new System.Drawing.Size(970, 414);
                this.dataGridViewAccounts.TabIndex = 0;
                // 
                // AccountColumn
                // 
                this.AccountColumn.HeaderText = "Account";
                this.AccountColumn.Name = "AccountColumn";
                this.AccountColumn.ReadOnly = true;
                // 
                // UsernameColumn
                // 
                this.UsernameColumn.HeaderText = "Username";
                this.UsernameColumn.Name = "UsernameColumn";
                this.UsernameColumn.ReadOnly = true;
                // 
                // FreeSpaceTextColumn
                // 
                this.FreeSpaceTextColumn.HeaderText = "Free space in MB";
                this.FreeSpaceTextColumn.Name = "FreeSpaceTextColumn";
                this.FreeSpaceTextColumn.ReadOnly = true;
                // 
                // TotalSpaceTextColumn
                // 
                this.TotalSpaceTextColumn.HeaderText = "Total space in MB";
                this.TotalSpaceTextColumn.Name = "TotalSpaceTextColumn";
                this.TotalSpaceTextColumn.ReadOnly = true;
                // 
                // UsedSpaceTextColumn
                // 
                this.UsedSpaceTextColumn.HeaderText = "Used space in MB";
                this.UsedSpaceTextColumn.Name = "UsedSpaceTextColumn";
                this.UsedSpaceTextColumn.ReadOnly = true;
                // 
                // UsedSpaceColumn
                // 
                this.UsedSpaceColumn.HeaderText = "Used space";
                this.UsedSpaceColumn.Name = "UsedSpaceColumn";
                this.UsedSpaceColumn.ProgressBarColor = System.Drawing.Color.Lime;
                this.UsedSpaceColumn.ReadOnly = true;
                this.UsedSpaceColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                this.UsedSpaceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
                // 
                // AccountInfos
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(970, 414);
                this.Controls.Add(this.dataGridViewAccounts);
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.Name = "AccountInfos";
                this.Text = "AccountInfos";
                ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccounts)).EndInit();
                this.ResumeLayout(false);

            }

            #endregion

            private System.Windows.Forms.DataGridView dataGridViewAccounts;
            private System.Windows.Forms.DataGridViewTextBoxColumn AccountColumn;
            private System.Windows.Forms.DataGridViewTextBoxColumn UsernameColumn;
            private System.Windows.Forms.DataGridViewTextBoxColumn FreeSpaceTextColumn;
            private System.Windows.Forms.DataGridViewTextBoxColumn TotalSpaceTextColumn;
            private System.Windows.Forms.DataGridViewTextBoxColumn UsedSpaceTextColumn;
            private Models.DataGridViewProgressColumn UsedSpaceColumn;
        }
    }