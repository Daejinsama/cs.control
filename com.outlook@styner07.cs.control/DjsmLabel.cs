using System.ComponentModel;

namespace com.outlook_styner07.cs.control
{
    public class DjsmLabel : Label
    {
        public enum TextAngles { Rotate_0 = 0, Rotate_90 = 90, Rotate_180 = 180, Rotate_270 = 270 }

        [Browsable(true)]
        public TextAngles TextAngle
        {
            get
            {
                return textAngle;
            }
            set
            {
                textAngle = value;
                Invalidate();
            }
        }

        public TextAngles textAngle = TextAngles.Rotate_0;

        [Browsable(false)]
        public override ContentAlignment TextAlign { get; set; } = ContentAlignment.MiddleCenter;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle rect = Bounds;

            SizeF textSize = g.MeasureString(Text, Font);

            int x = 0;
            int y = 0;

            switch (textAngle)
            {
                case TextAngles.Rotate_90:
                    x = x + rect.Height / 2 - (int)(textSize.Width / 2);
                    y = y - rect.Width / 2 - (int)(textSize.Height / 2);
                    break;
                case TextAngles.Rotate_180:
                    x = x - rect.Width / 2 - (int)(textSize.Width / 2);
                    y = y - rect.Height / 2 - (int)(textSize.Height / 2);
                    break;
                case TextAngles.Rotate_270:
                    x = x - rect.Height + (rect.Height / 2) - (int)(textSize.Width / 2);
                    y = y + rect.Width / 2 - (int)(textSize.Height / 2);
                    break;
                default:
                    x = (int)((rect.Width - textSize.Width) / 2);
                    y = (int)((rect.Height - textSize.Height) / 2);
                    break;
            }
            
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            g.RotateTransform((float)textAngle);
            g.DrawString(Text, Font, new SolidBrush(ForeColor), x, y);
            g.ResetTransform();
        }
    }
}
