// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataGridViewProgressColumn.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The data grid view progress bar column class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Models;

/// <summary>
/// The data grid view progress bar column class.
/// </summary>
public class DataGridViewProgressColumn : DataGridViewImageColumn
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataGridViewProgressColumn"/> class.
    /// </summary>
    public DataGridViewProgressColumn()
    {
        this.CellTemplate = new DataGridViewProgressCell();
    }

    /// <summary>
    /// Gets the progress bar color.
    /// </summary>
    public static Color ProgressBarColorInternal { get; }

    /// <summary>
    /// Gets the cell template.
    /// </summary>
#pragma warning disable WFO1000 // Fehlende Codeserialisierungskonfiguration für Eigenschafteninhalt
    public override sealed DataGridViewCell? CellTemplate
#pragma warning restore WFO1000 // Fehlende Codeserialisierungskonfiguration für Eigenschafteninhalt
    {
        get => base.CellTemplate;
        set
        {
            if (value != null &&
                !value.GetType().IsAssignableFrom(typeof(DataGridViewProgressCell)))
            {
                throw new InvalidCastException("Must be a DataGridViewProgressCell");
            }

            base.CellTemplate = value;
        }
    }

    /// <summary>
    /// Gets or sets the progress bar color.
    /// </summary>
    [Browsable(true)]
#pragma warning disable WFO1000 // Fehlende Codeserialisierungskonfiguration für Eigenschafteninhalt
    public Color ProgressBarColor
#pragma warning restore WFO1000 // Fehlende Codeserialisierungskonfiguration für Eigenschafteninhalt
    {
        get
        {
            if (this.ProgressBarCellTemplate is null)
            {
                throw new InvalidOperationException(
                    "Operation cannot be completed because this DataGridViewColumn "
                    + "does not have a CellTemplate.");
            }

            return this.ProgressBarCellTemplate.ProgressBarColor;
        }

        set
        {
            if (this.ProgressBarCellTemplate is null)
            {
                throw new InvalidOperationException(
                    "Operation cannot be completed because this DataGridViewColumn "
                    + "does not have a CellTemplate.");
            }

            this.ProgressBarCellTemplate.ProgressBarColor = value;

            if (this.DataGridView is null)
            {
                return;
            }

            var dataGridViewRows = this.DataGridView.Rows;
            var rowCount = dataGridViewRows.Count;

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                var dataGridViewCell = dataGridViewRow.Cells[this.Index] as DataGridViewProgressCell;
                dataGridViewCell?.SetProgressBarColor(value);
            }

            this.DataGridView.InvalidateColumn(this.Index);
        }
    }

    /// <summary>
    /// Gets the progress bar cell template.
    /// </summary>
    private DataGridViewProgressCell? ProgressBarCellTemplate => (DataGridViewProgressCell?)this.CellTemplate;
}
