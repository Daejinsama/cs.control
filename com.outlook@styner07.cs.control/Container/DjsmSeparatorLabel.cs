using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace com.outlook_styner07.cs.control.Container
{
    [ToolboxItem(true)]
    public partial class DjsmSeparatorLabel : Label
    {
        [Browsable(true)]
        public TextRenderingHint RenderingHint
        {
            get { return _textRenderingHint; }
            set
            {
                _textRenderingHint = value; Invalidate();
            }
        }

        private TextRenderingHint _textRenderingHint = TextRenderingHint.AntiAlias;

        [Browsable(true)]
        public SmoothingMode SmoothMode
        {
            get { return _smoothMode; }
            set
            {
                _smoothMode = value; Invalidate();
            }
        }

        private SmoothingMode _smoothMode = SmoothingMode.AntiAlias;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            g.SmoothingMode = _smoothMode;
            g.TextRenderingHint = _textRenderingHint;

            SizeF size = g.MeasureString(Text, Font);

            g.DrawString(Text, Font, new SolidBrush(ForeColor),0, (Height - size.Height) / 2);

            int lineY = Height / 2 + 1;

            g.DrawLine(new Pen(new SolidBrush(Color.LightGray), 1), new PointF(size.Width + 3, lineY), new Point(Width, lineY));
        }
    }
}
