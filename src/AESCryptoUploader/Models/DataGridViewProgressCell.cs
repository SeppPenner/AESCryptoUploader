// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataGridViewProgressCell.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The data grid view progress cell class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AESCryptoUploader.Models;

/// <summary>
/// The data grid view progress cell class.
/// </summary>
public class DataGridViewProgressCell : DataGridViewImageCell
{
    /// <summary>
    /// The empty image.
    /// </summary>
    private static readonly Bitmap EmptyImage;

    /// <summary>
    /// The progress bar color.
    /// </summary>
    private static Color progressBarColor;

    /// <summary>
    /// Initializes static members of the <see cref="DataGridViewProgressCell"/> class.
    /// </summary>
    static DataGridViewProgressCell()
    {
        EmptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataGridViewProgressCell"/> class.
    /// </summary>
    public DataGridViewProgressCell()
    {
        this.ValueType = typeof(int);
    }

    /// <summary>
    /// Gets or sets the progress bar color.
    /// </summary>
    public Color ProgressBarColor
    {
        get => progressBarColor;
        set => progressBarColor = value;
    }

    /// <summary>
    /// Gets or sets the value type.
    /// </summary>
    public override sealed Type ValueType
    {
        get => base.ValueType;
        set => base.ValueType = value;
    }

    /// <summary>
    /// Clones the object to a new once.
    /// </summary>
    /// <returns>The cloned <see cref="object"/>.</returns>
    public override object Clone()
    {
        var dataGridViewCell = base.Clone() as DataGridViewProgressCell;

        if (dataGridViewCell is not null)
        {
            dataGridViewCell.ProgressBarColor = this.ProgressBarColor;
        }

        return dataGridViewCell ?? new object();
    }

    /// <summary>
    /// Sets the progress bar color.
    /// </summary>
    /// <param name="value">The color value.</param>
    internal void SetProgressBarColor(Color value)
    {
        this.ProgressBarColor = value;
    }

    /// <summary>
    /// Gets the formatted value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="rowIndex">The row index.</param>
    /// <param name="cellStyle">The cell style.</param>
    /// <param name="valueTypeConverter">The value type converter.</param>
    /// <param name="formattedValueTypeConverter">The formatted value type converter.</param>
    /// <param name="context">The context.</param>
    /// <returns>The formatted value as <see cref="object"/>.</returns>
    protected override object GetFormattedValue(
        object value,
        int rowIndex,
        ref DataGridViewCellStyle cellStyle,
        TypeConverter valueTypeConverter,
        TypeConverter formattedValueTypeConverter,
        DataGridViewDataErrorContexts context)
    {
        return EmptyImage;
    }

    /// <summary>
    /// Paints to the graphics.
    /// </summary>
    /// <param name="g">The graphics.</param>
    /// <param name="clipBounds">The clip bounds.</param>
    /// <param name="cellBounds">The cell bounds.</param>
    /// <param name="rowIndex">The row index.</param>
    /// <param name="cellState">The cell state.</param>
    /// <param name="value">The value.</param>
    /// <param name="formattedValue">The formatted value.</param>
    /// <param name="errorText">The error text.</param>
    /// <param name="cellStyle">The cell style.</param>
    /// <param name="advancedBorderStyle">The advanced border style.</param>
    /// <param name="paintParts">The paint parts.</param>
    protected override void Paint(
        Graphics g,
          Rectangle clipBounds,
          Rectangle cellBounds,
          int rowIndex,
          DataGridViewElementStates cellState,
          object value,
          object formattedValue,
          string errorText,
          DataGridViewCellStyle cellStyle,
          DataGridViewAdvancedBorderStyle advancedBorderStyle,
          DataGridViewPaintParts paintParts)
    {
        if (Convert.ToInt16(value) == 0 || value == null)
        {
            value = 0;
        }

        var progressVal = Convert.ToInt32(value);

        var percentage = progressVal / 100.0f;
        _ = new SolidBrush(cellStyle.BackColor);
        Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);

        // Draws the cell grid
        base.Paint(
            g,
            clipBounds,
            cellBounds,
            rowIndex,
            cellState,
            value,
            formattedValue,
            errorText,
            cellStyle,
            advancedBorderStyle,
            paintParts & ~DataGridViewPaintParts.ContentForeground);

        float posX = cellBounds.X;
        float posY = cellBounds.Y;

        float textWidth = TextRenderer.MeasureText(progressVal + "%", cellStyle.Font).Width;
        float textHeight = TextRenderer.MeasureText(progressVal + "%", cellStyle.Font).Height;

        // valuating text position according to alignment
        switch (cellStyle.Alignment)
        {
            case DataGridViewContentAlignment.BottomCenter:
                posX = cellBounds.X + (cellBounds.Width / 2) - (textWidth / 2);
                posY = cellBounds.Y + cellBounds.Height - textHeight;
                break;

            case DataGridViewContentAlignment.BottomLeft:
                posX = cellBounds.X;
                posY = cellBounds.Y + cellBounds.Height - textHeight;
                break;

            case DataGridViewContentAlignment.BottomRight:
                posX = cellBounds.X + cellBounds.Width - textWidth;
                posY = cellBounds.Y + cellBounds.Height - textHeight;
                break;

            case DataGridViewContentAlignment.MiddleCenter:
                posX = cellBounds.X + (cellBounds.Width / 2) - (textWidth / 2);
                posY = cellBounds.Y + (cellBounds.Height / 2) - (textWidth / 2);
                break;

            case DataGridViewContentAlignment.MiddleLeft:
                posX = cellBounds.X;
                posY = cellBounds.Y + (cellBounds.Height / 2) - (textWidth / 2);
                break;

            case DataGridViewContentAlignment.MiddleRight:
                posX = cellBounds.X + cellBounds.Width - textWidth;
                posY = cellBounds.Y + (cellBounds.Height / 2) - (textWidth / 2);
                break;

            case DataGridViewContentAlignment.TopCenter:
                posX = cellBounds.X + (cellBounds.Width / 2) - (textWidth / 2);
                posY = cellBounds.Y;
                break;

            case DataGridViewContentAlignment.TopLeft:
                posX = cellBounds.X;
                posY = cellBounds.Y;
                break;

            case DataGridViewContentAlignment.TopRight:
                posX = cellBounds.X + cellBounds.Width - textWidth;
                posY = cellBounds.Y;
                break;
        }

        if (percentage >= 0.0)
        {
            g.FillRectangle(
                new SolidBrush(progressBarColor),
                cellBounds.X + 2,
                cellBounds.Y + 2,
                Convert.ToInt32(percentage * cellBounds.Width * 0.8),
                (cellBounds.Height / 1) - 5);
            g.DrawString(progressVal + "%", cellStyle.Font, foreColorBrush, posX, posY);
        }
        else
        {
            if (this.DataGridView?.CurrentRow != null && this.DataGridView.CurrentRow.Index == rowIndex)
            {
                g.DrawString(
                    progressVal + "%",
                    cellStyle.Font,
                    new SolidBrush(cellStyle.SelectionForeColor),
                    posX,
                    posX);
            }
            else
            {
                g.DrawString(progressVal + "%", cellStyle.Font, foreColorBrush, posX, posY);
            }
        }
    }
}
