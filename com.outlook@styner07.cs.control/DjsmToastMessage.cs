using System.Drawing;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control
{
    public class DjsmToastMessage
    {
        public enum Theme { DARK, LIGHT, ALERT }

        private const int V_MARGIN = 5;
        private const int H_MARGIN = 10;
        private const int B_MARGIN = 30;
        private const int SHADOW_MARGIN = 2;

        private static Theme theme;
        private static Font font = new Font("Arial", 9f, FontStyle.Regular);
        private static MessageControl control;
        private static System.Threading.Timer timer;

        public static void Show(string message, int period, Theme theme, Form owner)
        {
            if (control != null || owner == null)
            {
                return;
            }

            if (owner.InvokeRequired)
            {
                owner.Invoke((MethodInvoker)delegate
                {
                    Show(message, period, theme, owner);
                });
            }
            else
            {
                DjsmToastMessage.theme = theme;

                Rectangle parentRect = owner.ClientRectangle;
                Size messageSize = TextRenderer.MeasureText(message, font);
                Rectangle border = new Rectangle(
                    (parentRect.Width - messageSize.Width) / 2 - H_MARGIN,
                    parentRect.Height - B_MARGIN - messageSize.Height,
                    messageSize.Width + H_MARGIN * 2,
                    messageSize.Height + V_MARGIN * 4);

                control = new MessageControl(message);

                owner.Controls.Add(control);
                control.Bounds = border;
                control.BringToFront();

                int tickCount = 0;
                timer = new System.Threading.Timer((object state) =>
                {
                    tickCount++;
                    if (tickCount * 1000 >= period)
                    {
                        if (control.IsHandleCreated && !control.IsDisposed)
                        {
                            control.Invoke((MethodInvoker)delegate
                            {
                                control.Dispose();
                            });
                        }
                        control = null;
                        timer.Dispose();
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
            Show(message, period, theme, Form.ActiveForm);
        }

        internal class MessageControl : System.Windows.Forms.Control
        {
            string message;

            public MessageControl(string message)
            {
                this.message = message;
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                Rectangle rect = new Rectangle(0, 0, Width - SHADOW_MARGIN, Height - SHADOW_MARGIN);
                g.FillRectangle(
                    theme == Theme.DARK ? Brushes.DarkRed : Brushes.DimGray,
                    new Rectangle(SHADOW_MARGIN, SHADOW_MARGIN, Width + SHADOW_MARGIN, Height + SHADOW_MARGIN));
                g.FillRectangle(
                    theme == Theme.DARK ? Brushes.DimGray : theme == Theme.ALERT ? Brushes.Red : Brushes.White, rect);

                SizeF messageSize = g.MeasureString(message, font);
                g.DrawString(message, font,
                    theme == Theme.LIGHT ? Brushes.Black : Brushes.White,
                    rect.X + ((rect.Width - messageSize.Width) / 2),
                    rect.Y + ((rect.Height - messageSize.Height) / 2));
            }
        }
    }
}
