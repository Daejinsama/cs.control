using System.ComponentModel;

namespace com.outlook_styner07.cs.control.Gauge
{
    public class DjsmProgressBar : ProgressBar
    {
        #region Constructors
        /// <summary>
        /// not support marquee style.
        /// </summary>
        public DjsmProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private Font _progressFont = new Font("arial", 9f, FontStyle.Regular);
        private Color _progressFontColor = Color.Black;
        private Color _progressBarColor = Color.Blue;

        private System.Timers.Timer? _marqueeTimer;
        private bool _updateMarquee = false;
        private float _marqueePos = float.MinValue;
        #endregion

        #region Properties
        [Browsable(true)]
        public Font ProgressFont
        {
            get { return _progressFont; }
            set
            {
                if (_progressFont != value)
                {
                    _progressFont = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public Color ProgressFontColor
        {
            get { return _progressFontColor; }
            set
            {
                if (_progressFontColor != value)
                {
                    _progressFontColor = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public Color ProgressBarColor
        {
            get { return _progressBarColor; }
            set
            {
                if (_progressBarColor != value)
                {
                    _progressBarColor = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public bool LabelDrawing { get; set; } = true;

        [Browsable(true)]
        public bool IsFixedLabel { get; set; } = false;

        [Browsable(true)]
        public string LabelText { get; set; } = string.Empty;

        [Browsable(false)]
        public new Color ForeColor { get; set; }
        #endregion

        #region Methods
        public void StartMarquee()
        {
            if (_marqueeTimer == null)
            {
                _marqueeTimer = new System.Timers.Timer(MarqueeAnimationSpeed);
                _marqueeTimer.Elapsed += (sender, e) =>
                {
                    Invoke((MethodInvoker)delegate
                    {
                        _updateMarquee = true;
                        Invalidate();
                        _marqueePos += Step;
                    });
                };
                _marqueeTimer.Start();
            }
        }

        public void StopMarquee()
        {
            if (_marqueeTimer != null)
            {
                _marqueeTimer.Stop();
                _marqueeTimer.Dispose();
                _marqueeTimer = null;
            }

            _marqueePos = ClientRectangle.Width;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            if (ProgressBarRenderer.IsSupported)
            {
                ProgressBarRenderer.DrawHorizontalBar(g, e.ClipRectangle);
            }

            using var fontBrush = new SolidBrush(ProgressFontColor);
            using var backBrush = new SolidBrush(BackColor);
            using var barBrush = new SolidBrush(ProgressBarColor);

            if (Style == ProgressBarStyle.Marquee && _updateMarquee)
            {
                RectangleF newRect = e.ClipRectangle;
                newRect.Width = (int)(newRect.Width * 0.35);

                if (_marqueePos < -newRect.Width)
                {
                    _marqueePos = -newRect.Width;
                }

                if (_marqueePos >= newRect.Width)
                {
                    _marqueePos = -newRect.Width;
                }

                g.FillRectangle(barBrush, _marqueePos, 0, newRect.Width, newRect.Height);

                if (LabelDrawing)
                {
                    SizeF stringSize = g.MeasureString(LabelText, ProgressFont);

                    g.DrawString(LabelText, ProgressFont, fontBrush, newRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }

                _updateMarquee = false;
            }
            else
            {
                Rectangle rect = e.ClipRectangle;

                g.FillRectangle(backBrush, rect);

                rect.Width = (int)(rect.Width * ((double)Value / Maximum)) - 4;
                rect.Height = rect.Height - 4;

                g.FillRectangle(barBrush, 2, 2, rect.Width, rect.Height);

                if (LabelDrawing)
                {
                    SizeF stringSize = g.MeasureString(LabelText, ProgressFont);

                    if (IsFixedLabel)
                    {
                        g.DrawString(LabelText, ProgressFont, fontBrush, e.ClipRectangle, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    else
                    {
                        string percentage = string.Format("{0:0.0}%", ((double)Value / Maximum) * 100);

                        g.DrawString(percentage, ProgressFont, fontBrush, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                }
            }
        }
        #endregion
    }
}
