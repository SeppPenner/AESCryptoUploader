    partial class UploadProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadProgress));
            this.dataGridViewFileUpload = new System.Windows.Forms.DataGridView();
            this.HosterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UploadProgressColumn = new Models.DataGridViewProgressColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFileUpload)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewFileUpload
            // 
            this.dataGridViewFileUpload.AllowUserToAddRows = false;
            this.dataGridViewFileUpload.AllowUserToDeleteRows = false;
            this.dataGridViewFileUpload.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFileUpload.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewFileUpload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFileUpload.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HosterColumn,
            this.FileNameColumn,
            this.UploadProgressColumn});
            this.dataGridViewFileUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFileUpload.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFileUpload.Name = "dataGridViewFileUpload";
            this.dataGridViewFileUpload.ReadOnly = true;
            this.dataGridViewFileUpload.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewFileUpload.Size = new System.Drawing.Size(561, 206);
            this.dataGridViewFileUpload.TabIndex = 0;
            // 
            // HosterColumn
            // 
            this.HosterColumn.HeaderText = "Hoster";
            this.HosterColumn.Name = "HosterColumn";
            this.HosterColumn.ReadOnly = true;
            // 
            // FileNameColumn
            // 
            this.FileNameColumn.HeaderText = "File name";
            this.FileNameColumn.Name = "FileNameColumn";
            this.FileNameColumn.ReadOnly = true;
            // 
            // UploadProgressColumn
            // 
            this.UploadProgressColumn.HeaderText = "Upload progress";
            this.UploadProgressColumn.Name = "UploadProgressColumn";
            this.UploadProgressColumn.ProgressBarColor = System.Drawing.Color.Lime;
            this.UploadProgressColumn.ReadOnly = true;
            this.UploadProgressColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UploadProgressColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // UploadProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 206);
            this.Controls.Add(this.dataGridViewFileUpload);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UploadProgress";
            this.Text = "UploadProgress";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFileUpload)).EndInit();
            this.ResumeLayout(false);

        }

    #endregion

    private System.Windows.Forms.DataGridView dataGridViewFileUpload;
    private System.Windows.Forms.DataGridViewTextBoxColumn HosterColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn FileNameColumn;
    private Models.DataGridViewProgressColumn UploadProgressColumn;
}