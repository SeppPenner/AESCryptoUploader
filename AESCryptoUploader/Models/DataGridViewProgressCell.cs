using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Models
{
    public class DataGridViewProgressCell : DataGridViewImageCell
    {
        private static readonly Image EmptyImage;
        private static Color _progressBarColor;

        public Color ProgressBarColor
        {
            get => _progressBarColor;
            set => _progressBarColor = value;
        }

        static DataGridViewProgressCell()
        {
            EmptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        public DataGridViewProgressCell()
        {
            ValueType = typeof(int);
        }

        public sealed override Type ValueType
        {
            get => base.ValueType;
            set => base.ValueType = value;
        }

        protected override object GetFormattedValue(object value,
            int rowIndex, ref DataGridViewCellStyle cellStyle,
            TypeConverter valueTypeConverter,
            TypeConverter formattedValueTypeConverter,
            DataGridViewDataErrorContexts context)
        {
            return EmptyImage;
        }

        protected override void Paint(Graphics g,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value, object formattedValue,
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
            Brush unused = new SolidBrush(cellStyle.BackColor);
            Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);

            // Draws the cell grid
            base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, 
                cellStyle, advancedBorderStyle, paintParts & ~DataGridViewPaintParts.ContentForeground);

            float posX = cellBounds.X;
            float posY = cellBounds.Y;

            float textWidth = TextRenderer.MeasureText(progressVal + "%", cellStyle.Font).Width;
            float textHeight = TextRenderer.MeasureText(progressVal + "%", cellStyle.Font).Height;

            //evaluating text position according to alignment
            switch (cellStyle.Alignment)
            {
                case DataGridViewContentAlignment.BottomCenter:
                    posX = cellBounds.X + (cellBounds.Width / 2) - textWidth / 2;
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
                    posX = cellBounds.X + (cellBounds.Width / 2) - textWidth / 2;
                    posY = cellBounds.Y + (cellBounds.Height / 2) - textHeight / 2;
                    break;
                case DataGridViewContentAlignment.MiddleLeft:
                    posX = cellBounds.X;
                    posY = cellBounds.Y + (cellBounds.Height / 2) - textHeight / 2;
                    break;
                case DataGridViewContentAlignment.MiddleRight:
                    posX = cellBounds.X + cellBounds.Width - textWidth;
                    posY = cellBounds.Y + (cellBounds.Height / 2) - textHeight / 2;
                    break;
                case DataGridViewContentAlignment.TopCenter:
                    posX = cellBounds.X + (cellBounds.Width / 2) - textWidth / 2;
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
                g.FillRectangle(new SolidBrush(_progressBarColor), cellBounds.X + 2, cellBounds.Y + 2, 
                    Convert.ToInt32((percentage * cellBounds.Width * 0.8)), cellBounds.Height / 1 - 5);
                g.DrawString(progressVal + "%", cellStyle.Font, foreColorBrush, posX, posY);
            }
            else
            {
                if (DataGridView.CurrentRow != null && DataGridView.CurrentRow.Index == rowIndex)
                {
                    g.DrawString(progressVal + "%", cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), posX, posX);
                }
                else
                {
                    g.DrawString(progressVal + "%", cellStyle.Font, foreColorBrush, posX, posY);
                }
            }
        }

        public override object Clone()
        {
            var dataGridViewCell = base.Clone() as DataGridViewProgressCell;
            if (dataGridViewCell != null)
            {
                dataGridViewCell.ProgressBarColor = ProgressBarColor;
            }
            return dataGridViewCell ?? new object();
        }

        internal void SetProgressBarColor(Color value)
        {
            ProgressBarColor = value;
        }
    }
}
