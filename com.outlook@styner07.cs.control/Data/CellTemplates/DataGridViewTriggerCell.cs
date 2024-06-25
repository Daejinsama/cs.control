using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control.Data.CellTemplates
{
    public class DataGridViewTriggerCell : DataGridViewTextBoxCell
    {
        public enum Trigger { RUN, STOP, NA }
        private Color indicatorColor = DjsmColorTable.IndicatorDisabled;

        public DataGridViewTriggerCell()
        {
            SetStatus(Trigger.NA);
        }

        public void SetStatus(Trigger status)
        {
            indicatorColor = status == Trigger.RUN ? DjsmColorTable.IndicatorGreen : status == Trigger.STOP ? DjsmColorTable.IndicatorRed : DjsmColorTable.IndicatorDisabled;

            if (DataGridView != null)
            {
                DataGridView.InvalidateCell(this);
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            value = string.Empty;
            formattedValue = string.Empty;

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            IntPtr hdc = graphics.GetHdc();
            using (Graphics g = Graphics.FromHdc(hdc))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                /// draw indicator

                float diameter = cellBounds.Height / 2f;
                RectangleF rect = new RectangleF(
                        cellBounds.X + ((cellBounds.Width - diameter) / 2),
                        cellBounds.Y + ((cellBounds.Height - diameter) / 2),
                        diameter, diameter);

                LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    Color.FromArgb(64, indicatorColor),
                    indicatorColor,
                    LinearGradientMode.ForwardDiagonal)
                { GammaCorrection = true };

                //g.FillEllipse(new SolidBrush(indicatorColor), rect);
                g.FillEllipse(brush, rect);
                g.DrawEllipse(new Pen(Brushes.DimGray), rect);
            }
            graphics.ReleaseHdc(hdc);
        }
    }
}
