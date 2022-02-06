// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadProgress.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The upload progress view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader
{
    using System.IO;
    using System.Windows.Forms;

    using AESCryptoUploader.UiThreadInvoke;

    using Languages.Interfaces;

    /// <summary>
    /// The upload progress view.
    /// </summary>
    public partial class UploadProgress : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadProgress"/> class.
        /// </summary>
        /// <param name="currentLanguage">The current language.</param>
        public UploadProgress(ILanguage currentLanguage)
        {
            this.InitializeComponent();
            this.CurrentLanguage = currentLanguage;
            this.Initialize();
        }

        /// <summary>
        /// Gets or sets the current language.
        /// </summary>
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        private ILanguage CurrentLanguage { get; set; }

        /// <summary>
        /// Gets the row count.
        /// </summary>
        /// <returns>The row count as <see cref="int"/>.</returns>
        public int GetRowsCount()
        {
            return this.dataGridViewFileUpload.Rows.Count;
        }

        /// <summary>
        /// Gets the last row value.
        /// </summary>
        /// <returns>The last row value.</returns>
        public double GetLastRowValue()
        {
            var lastRow = this.GetRowsCount() - 1;

            if (lastRow == -1)
            {
                return 0;
            }

            return (double)this.dataGridViewFileUpload.Rows[lastRow].Cells[2].Value;
        }

        /// <summary>
        /// Show the progress.
        /// </summary>
        /// <param name="hoster">The hoster.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="value">The value.</param>
        public void ShowProgressPublic(string hoster, string fileName, double value)
        {
            this.UiThreadInvoke(
                () =>
                {
                    foreach (DataGridViewRow row in this.dataGridViewFileUpload.Rows)
                    {
                        var fileNameWithoutPath = Path.GetFileName(fileName);

                        if (!row.Cells[0].Value.Equals(hoster) || !row.Cells[1].Value.Equals(fileNameWithoutPath))
                        {
                            continue;
                        }

                        this.dataGridViewFileUpload.Rows[row.Index].Cells[2].Value = value;
                        this.dataGridViewFileUpload.Update();
                        return;
                    }

                    this.AddFileToTable(hoster, fileName, value);
                });
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        private void Initialize()
        {
            this.InitializeHeaders();
            this.ClearColumns();
        }

        /// <summary>
        /// Clears the columns.
        /// </summary>
        private void ClearColumns()
        {
            this.UiThreadInvoke(() => { this.dataGridViewFileUpload.Rows.Clear(); });
        }

        /// <summary>
        /// Initializes the headers.
        /// </summary>
        private void InitializeHeaders()
        {
            this.UiThreadInvoke(
                () =>
                {
                    this.dataGridViewFileUpload.Columns[0].HeaderText = this.CurrentLanguage.GetWord("Hoster");
                    this.dataGridViewFileUpload.Columns[1].HeaderText = this.CurrentLanguage.GetWord("FileName");
                    this.dataGridViewFileUpload.Columns[2].HeaderText = this.CurrentLanguage.GetWord("UploadProgress");
                });
        }

        /// <summary>
        /// Adds a file to the table.
        /// </summary>
        /// <param name="hoster">The hoster.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="value">The value.</param>
        private void AddFileToTable(string hoster, string fileName, double value)
        {
            var fileNameWithoutPath = Path.GetFileName(fileName);
            this.UiThreadInvoke(
                () =>
                {
                    var row = new object[] { hoster, fileNameWithoutPath, value };
                    this.dataGridViewFileUpload.Rows.Add(row);
                });
        }
    }
}