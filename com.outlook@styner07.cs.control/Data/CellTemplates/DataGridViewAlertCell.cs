using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control.Data.CellTemplates
{
    public class DataGridViewAlertCell : DataGridViewTextBoxCell
    {
        public enum Alerts { NORMAL, WARNING, DANGER }

        private const int ALERT_DURATION = 5000;

        private const int NORMAL_ANI_INTERVAL = 300;
        private const int OTHERS_ANI_INTERVAL = 50;

        private Color alertColor = DjsmColorTable.IndicatorGreen;
        private const int MARGIN = 3;
        private bool counter = false;
        private int normalAniCounter = 0;
        private int normalAniFlag = 0;
        private Font font = new Font("Arial", 8f, FontStyle.Bold);

        private System.Timers.Timer timer;

        public DataGridViewAlertCell()
        {
            timer = new System.Timers.Timer();
            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                counter = !counter;
                if (normalAniCounter > 10)
                {
                    normalAniFlag = 1;
                }
                if (normalAniCounter < 1)
                {
                    normalAniFlag = 0;
                }
                normalAniCounter = normalAniFlag == 0 ? normalAniCounter + 1 : normalAniCounter - 1;
                if (DataGridView != null)
                {
                    DataGridView.InvalidateCell(this);
                }
            };
            timer.AutoReset = true;
            timer.Start();

            SetStatus(Alerts.NORMAL);
        }

        protected override void Dispose(bool disposing)
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
            base.Dispose(disposing);
        }

        public void SetStatus(Alerts alert)
        {
            timer.Interval = alert == Alerts.NORMAL ? NORMAL_ANI_INTERVAL : OTHERS_ANI_INTERVAL;

            alertColor = alert == Alerts.NORMAL ? DjsmColorTable.IndicatorGreen : alert == Alerts.WARNING ? DjsmColorTable.IndicatorOrange : DjsmColorTable.IndicatorRed;

            if (DataGridView != null)
            {
                DataGridView.InvalidateCell(this);
            }
        }

        //public void NotifyAlert(Alerts alert)
        //{
        //    new Thread(new ThreadStart(() =>
        //    {
        //        SetStatus(alert);
        //        int counter = 0;
        //        while (counter < ALERT_DURATION / 100)
        //        {
        //            counter++;
        //            Thread.Sleep(100);
        //        }

        //        SetStatus(Alerts.NORMAL);
        //    })).Start();
        //}

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

                Color color = Color.FromArgb(20 * normalAniCounter, alertColor);

                LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    Color.FromArgb(64, color),
                    color,
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
