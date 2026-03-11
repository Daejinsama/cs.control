using System.Drawing.Drawing2D;

namespace com.outlook_styner07.cs.control.Data.CellTemplates
{
    public class DataGridViewTriggerCell : DataGridViewTextBoxCell
    {
        #region Constructors
        public DataGridViewTriggerCell()
        {
            SetStatus(Trigger.NA);
        }
        #endregion

        #region Types
        public enum Trigger
        {
            RUN,
            STOP,
            NA
        }
        #endregion

        #region Fields
        private Color _indicatorColor = DjsmColorTable.IndicatorDisabled;
        #endregion

        #region Properties
        #endregion

        #region Methods
        public void SetStatus(Trigger status)
        {
            _indicatorColor = status == Trigger.RUN ? DjsmColorTable.IndicatorGreen : status == Trigger.STOP ? DjsmColorTable.IndicatorRed : DjsmColorTable.IndicatorDisabled;

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

                using LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    Color.FromArgb(64, _indicatorColor),
                    _indicatorColor,
                    LinearGradientMode.ForwardDiagonal)
                { GammaCorrection = true };

                g.FillEllipse(brush, rect);
                using (var p = new Pen(Brushes.DimGray))
                {
                    g.DrawEllipse(p, rect);
                }
            }

            graphics.ReleaseHdc(hdc);
        }
        #endregion
    }
}
