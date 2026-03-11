using System.Drawing.Drawing2D;
using System.Timers;

namespace com.outlook_styner07.cs.control.Data.CellTemplates
{
    public class DataGridViewAlertCell : DataGridViewTextBoxCell
    {
        #region Constructors
        public DataGridViewAlertCell()
        {
            _timer = new System.Timers.Timer();
            _timer.Elapsed += _timer_Elapsed; 
            _timer.AutoReset = true;
            _timer.Start();

            SetStatus(Alerts.NORMAL);
        }
        #endregion

        #region Types
        public enum Alerts
        {
            NORMAL,
            WARNING,
            DANGER
        }
        #endregion

        #region Fields
        private const int ALERT_DURATION = 5000;
        private const int NORMAL_ANI_INTERVAL = 300;
        private const int OTHERS_ANI_INTERVAL = 50;
        private const int MARGIN = 3;

        private Color _alertColor = DjsmColorTable.IndicatorGreen;
        
        private bool _counter = false;
        private int _normalAniCounter = 0;
        private int _normalAniFlag = 0;
        private Font _font = new Font("Arial", 8f, FontStyle.Bold);

        private System.Timers.Timer _timer;
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void _timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            _counter = !_counter;
            if (_normalAniCounter > 10)
            {
                _normalAniFlag = 1;
            }
            if (_normalAniCounter < 1)
            {
                _normalAniFlag = 0;
            }
            _normalAniCounter = _normalAniFlag == 0 ? _normalAniCounter + 1 : _normalAniCounter - 1;
            if (DataGridView != null)
            {
                DataGridView.InvalidateCell(this);
            }
        }

        public void SetStatus(Alerts alert)
        {
            _timer.Interval = alert == Alerts.NORMAL ? NORMAL_ANI_INTERVAL : OTHERS_ANI_INTERVAL;

            _alertColor = alert == Alerts.NORMAL ? DjsmColorTable.IndicatorGreen : alert == Alerts.WARNING ? DjsmColorTable.IndicatorOrange : DjsmColorTable.IndicatorRed;

            if (DataGridView != null)
            {
                DataGridView.InvalidateCell(this);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }

            base.Dispose(disposing);
        }


        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            IntPtr hdc = graphics.GetHdc();
            using (Graphics g = Graphics.FromHdc(hdc))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                /// draw indicator

                float diameter = cellBounds.Height / 2f;
                RectangleF rect = new RectangleF(
                        cellBounds.X + ((diameter + diameter) / 2),
                        cellBounds.Y + ((diameter) / 2),
                        diameter, diameter);

                Color color = Color.FromArgb(20 * _normalAniCounter, _alertColor);

                using LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    Color.FromArgb(64, color),
                    color,
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
