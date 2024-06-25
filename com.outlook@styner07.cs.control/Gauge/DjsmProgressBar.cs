using System.ComponentModel;
using System.Timers;

namespace com.outlook_styner07.cs.control.Gauge
{
    public class DjsmProgressBar : ProgressBar
    {
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
        private Font _progressFont = new Font("arial", 9f, FontStyle.Regular);


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
        private Color _progressFontColor = Color.Black;

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

        private Color _progressBarColor = Color.Blue;


        [Browsable(true)]
        public bool LabelDrawing { get; set; } = true;

        [Browsable(true)]
        public bool IsFixedLabel { get; set; } = false;

        [Browsable(true)]
        public string LabelText { get; set; } = string.Empty;

        [Browsable(false)]
        public new Color ForeColor { get; set; } 

        /// <summary>
        /// not support marquee style.
        /// </summary>
        public DjsmProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private System.Timers.Timer marqueeTimer;

        private bool updateMarquee = false;
        public void StartMarquee()
        {
            if (marqueeTimer == null)
            {
                marqueeTimer = new System.Timers.Timer(MarqueeAnimationSpeed);
                marqueeTimer.Elapsed += (object sender, ElapsedEventArgs e) =>
                {
                    Invoke((MethodInvoker)delegate
                    {
                        updateMarquee = true;
                        Invalidate();
                        marqueePos += Step;
                    });
                };
                marqueeTimer.Start();
            }
        }

        public void StopMarquee()
        {
            if (marqueeTimer != null)
            {
                marqueeTimer.Stop();
                marqueeTimer.Dispose();
                marqueeTimer = null;
            }
            marqueePos = ClientRectangle.Width;
            Invalidate();
        }

        private int marqueePos = int.MinValue;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            {
                if (ProgressBarRenderer.IsSupported)
                {
                    ProgressBarRenderer.DrawHorizontalBar(g, e.ClipRectangle);
                }

                if (Style == ProgressBarStyle.Marquee && updateMarquee)
                {
                    Rectangle rect = e.ClipRectangle;
                    Rectangle newRect = e.ClipRectangle;
                    newRect.Width = (int)(newRect.Width * 0.35);

                    if (marqueePos < -newRect.Width)
                    {
                        marqueePos = -newRect.Width;
                    }

                    if (marqueePos >= rect.Width)
                    {
                        marqueePos = -newRect.Width;
                    }

                    g.FillRectangle(new SolidBrush(ProgressBarColor), marqueePos, 0, newRect.Width, newRect.Height);

                    if (LabelDrawing)
                    {
                        SizeF stringSize = TextRenderer.MeasureText(g, LabelText, ProgressFont);
                        TextRenderer.DrawText(g, LabelText, ProgressFont, newRect, ProgressFontColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                    updateMarquee = false;
                }
                else
                {
                    Rectangle rect = e.ClipRectangle;

                    g.FillRectangle(new SolidBrush(BackColor), rect);

                    rect.Width = (int)(rect.Width * ((double)Value / Maximum)) - 4;
                    rect.Height = rect.Height - 4;

                    g.FillRectangle(new SolidBrush(ProgressBarColor), 2, 2, rect.Width, rect.Height);

                    if (LabelDrawing)
                    {
                        SizeF stringSize = TextRenderer.MeasureText(g, LabelText, ProgressFont);
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        if (IsFixedLabel)
                        {
                            TextRenderer.DrawText(g, LabelText, ProgressFont, e.ClipRectangle, ProgressFontColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        }
                        else
                        {
                            string percentage = string.Format("{0:0.0}%", ((double)Value / Maximum) * 100);
                            TextRenderer.DrawText(g, percentage, ProgressFont, rect, ProgressFontColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                        }
                    }
                }
            }
        }
    }
}
