using System.IO;
using System.Windows.Forms;
using Languages.Implementation;
using UiThreadInvoke;

public partial class UploadProgress : Form
{
    public UploadProgress(Language currentLanguage)
    {
        InitializeComponent();
        CurrentLanguage = currentLanguage;
        Initialize();
    }

    private void Initialize()
    {
        InitializeHeaders();
        ClearColumns();
    }

    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    private Language CurrentLanguage { get; set; }

    private void ClearColumns()
    {
        this.UiThreadInvoke(() => { dataGridViewFileUpload.Rows.Clear();});
    }

    private void InitializeHeaders()
    {
        this.UiThreadInvoke(() => {
            dataGridViewFileUpload.Columns[0].HeaderText = CurrentLanguage.GetWord("Hoster");
            dataGridViewFileUpload.Columns[1].HeaderText = CurrentLanguage.GetWord("FileName");
            dataGridViewFileUpload.Columns[2].HeaderText = CurrentLanguage.GetWord("UploadProgress");
        });
    }

    public int GetRowsCount()
    {
        return dataGridViewFileUpload.Rows.Count;
    }

    public double GetLastRowValue()
    {
        var lastRow = GetRowsCount() - 1;
        if (lastRow == -1) return 0;
        return (double)dataGridViewFileUpload.Rows[lastRow].Cells[2].Value;
    }

    public void ShowProgressPublic(string hoster, string fileName, double value)
    {
        this.UiThreadInvoke(() =>
        {
            foreach (DataGridViewRow row in dataGridViewFileUpload.Rows)
            {
                var fileNameWithoutPath = Path.GetFileName(fileName);
                if (!row.Cells[0].Value.Equals(hoster) || !row.Cells[1].Value.Equals(fileNameWithoutPath)) continue;
                dataGridViewFileUpload.Rows[row.Index].Cells[2].Value = value;
                dataGridViewFileUpload.Update();
                return;
            }
            AddFileToTable(hoster, fileName, value);
        });
    }

    private void AddFileToTable(string hoster, string fileName, double value)
    {
        var fileNameWithoutPath = Path.GetFileName(fileName);
        this.UiThreadInvoke(() => {
            var row = new object[] { hoster, fileNameWithoutPath, value };
            dataGridViewFileUpload.Rows.Add(row);
        });
    }
}