using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Models
{
    public class DataGridViewProgressColumn : DataGridViewImageColumn
    {
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once InconsistentNaming
        public static Color _ProgressBarColor;

        public DataGridViewProgressColumn()
        {
            CellTemplate = new DataGridViewProgressCell();
        }

        public sealed override DataGridViewCell CellTemplate
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

        [Browsable(true)]
        public Color ProgressBarColor
        {
            // ReSharper disable once UnusedMember.Global
            get
            {
                if (ProgressBarCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn " +
                                                        "does not have a CellTemplate.");
                }
                return ProgressBarCellTemplate.ProgressBarColor;
            }
            set
            {

                if (ProgressBarCellTemplate == null)
                {
                    throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn " +
                                                        "does not have a CellTemplate.");
                }
                ProgressBarCellTemplate.ProgressBarColor = value;
                if (DataGridView == null) return;
                var dataGridViewRows = DataGridView.Rows;
                var rowCount = dataGridViewRows.Count;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    var dataGridViewRow = dataGridViewRows.SharedRow(rowIndex);
                    var dataGridViewCell = dataGridViewRow.Cells[Index] as DataGridViewProgressCell;
                    dataGridViewCell?.SetProgressBarColor(value);
                }
                DataGridView.InvalidateColumn(Index);
            }
        }

        private DataGridViewProgressCell ProgressBarCellTemplate => (DataGridViewProgressCell)CellTemplate;
    }
}