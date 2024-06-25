namespace com.outlook_styner07.cs.control.Data.CellTemplates
{
    public class DataGridViewLifeTimeCell : DataGridViewTextBoxCell
    {
        private Font LABEL_FONT = new Font("Arial", 7.5f, FontStyle.Bold);
        private const int MINIMUM = 0;
        private const int MAXIMUM = 100;

        private const int V_MARGIN = 3;
        private const int H_MARGIN = 5;

        public new int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (DataGridView != null)
                {
                    DataGridView.InvalidateCell(this);
                }
            }
        }
        public int _value = 0;
        public DataGridViewLifeTimeCell()
        {
            Value = 25;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            IntPtr hdc = graphics.GetHdc();

            Graphics g = Graphics.FromHdc(hdc);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            const float LINE_WIDTH = 2f;
            Pen linePen = new Pen(Brushes.DimGray, LINE_WIDTH);

            int rulerWidth = cellBounds.Width - (H_MARGIN * 2);
            int rulerHeight = cellBounds.Height / 3 * 2;
            int rulerLeft = cellBounds.X + H_MARGIN;
            int rulerTop = cellBounds.Y + V_MARGIN;
            int rulerRight = rulerLeft + rulerWidth;
            int rulerBottom = rulerTop + rulerHeight;

            int rulerBlockWidth = (int)(rulerWidth / 4d);

            /// draw gauge
            Color gaugeColor = Color.DimGray;
            if (Value <= 25)
            {
                gaugeColor = DjsmColorTable.IndicatorRed;
            }
            else if (Value <= 50)
            {
                gaugeColor = DjsmColorTable.IndicatorOrange;
            }
            else if (Value <= 75)
            {
                gaugeColor = DjsmColorTable.IndicatorYellow;
            }
            else
            {
                gaugeColor = DjsmColorTable.IndicatorGreen;
            }

            int gaugeWidth = (int)((rulerWidth / 100d) * Value);
            if (Value == 100)
            {
                gaugeWidth -= 1;    /// 1px 튀어나옴.
            }

            Rectangle gaugeRect = new Rectangle((int)(rulerLeft - 1), (int)(rulerTop + V_MARGIN), gaugeWidth, (int)(rulerHeight - LINE_WIDTH * 2));
            g.FillRectangle(new SolidBrush(gaugeColor), gaugeRect);
            g.DrawRectangle(new Pen(Brushes.DimGray), gaugeRect);

            /// draw ruler
            g.DrawLine(linePen, new Point(rulerLeft - 1, rulerBottom), new Point(rulerRight - 2, rulerBottom)); /// baseline
            int blockLeft;
            for (int i = 0; i < 5; i++)
            {
                blockLeft = rulerLeft + (rulerBlockWidth * i);
                g.DrawLine(linePen, new Point(blockLeft, rulerTop), new Point(blockLeft, rulerBottom));
            }

            SizeF textSize = g.MeasureString(Value.ToString(), LABEL_FONT);
            Point textLocation;
            if (gaugeRect.Width < textSize.Width)
            {
                textLocation = new Point((int)(gaugeRect.Left + gaugeRect.Width), (int)(rulerBottom - textSize.Height + LINE_WIDTH));
            }
            else
            {
                textLocation = new Point((int)(gaugeRect.Left + gaugeRect.Width - textSize.Width), (int)(rulerBottom - textSize.Height + LINE_WIDTH));
            }
            g.DrawString(Value.ToString(), LABEL_FONT, Brushes.White, textLocation);
            graphics.ReleaseHdc(hdc);
            g.Dispose();
        }
    }
}
