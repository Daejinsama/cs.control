namespace com.outlook_styner07.cs.control
{
    public class DjsmToastMessage
    {
        #region Constructors
        #endregion

        #region Types
        public enum Theme
        {
            DARK,
            LIGHT,
            ALERT
        }
        #endregion

        #region Fields
        private const int V_MARGIN = 5;
        private const int H_MARGIN = 10;
        private const int B_MARGIN = 30;
        private const int SHADOW_MARGIN = 2;

        private static Theme _theme;
        private static Font _font = new Font("Arial", 9f, FontStyle.Regular);
        private static MessageControl? _control;
        private static System.Threading.Timer? _timer;
        #endregion

        #region Properties
        #endregion

        #region Methods
        public static void Show(string message, int period, Theme theme, Form owner)
        {
            if (_control != null || owner == null)
            {
                return;
            }

            if (owner.InvokeRequired)
            {
                owner.Invoke(() => Show(message, period, theme, owner));
            }
            else
            {
                _theme = theme;

                Rectangle parentRect = owner.ClientRectangle;
                Size messageSize = TextRenderer.MeasureText(message, _font);
                Rectangle border = new Rectangle((parentRect.Width - messageSize.Width) / 2 - H_MARGIN, parentRect.Height - B_MARGIN - messageSize.Height, messageSize.Width + H_MARGIN * 2, messageSize.Height + V_MARGIN * 4);

                _control = new MessageControl(message);

                owner.Controls.Add(_control);
                _control.Bounds = border;
                _control.BringToFront();

                int tickCount = 0;

                _timer = new System.Threading.Timer((state) =>
                {
                    tickCount++;
                    if (tickCount * 1000 >= period)
                    {
                        if (_control.IsHandleCreated && !_control.IsDisposed)
                        {
                            _control.Invoke((MethodInvoker)delegate
                            {
                                _control.Dispose();
                            });
                        }

                        _control = null;
                        _timer?.Dispose();
                    }
                }, null, 0, 1000);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="message"></param>
        /// <param name="period">unit: millisec</param>
        public static void Show(string message, int period, Theme theme)
        {
            Form? activeForm = Form.ActiveForm;
            if (activeForm != null)
            {
                Show(message, period, theme, activeForm);
            }
        }
        #endregion

        private class MessageControl : Control
        {
            private string _message;

            public MessageControl(string message)
            {
                _message = message;
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                Rectangle rect = new Rectangle(0, 0, Width - SHADOW_MARGIN, Height - SHADOW_MARGIN);

                g.FillRectangle(_theme == Theme.DARK ? Brushes.DarkRed : Brushes.DimGray, new Rectangle(SHADOW_MARGIN, SHADOW_MARGIN, Width + SHADOW_MARGIN, Height + SHADOW_MARGIN));
                g.FillRectangle(_theme == Theme.DARK ? Brushes.DimGray : _theme == Theme.ALERT ? Brushes.Red : Brushes.White, rect);

                SizeF messageSize = g.MeasureString(_message, _font);

                g.DrawString(_message, _font, _theme == Theme.LIGHT ? Brushes.Black : Brushes.White, rect.X + ((rect.Width - messageSize.Width) / 2), rect.Y + ((rect.Height - messageSize.Height) / 2));
            }
        }
    }
}
