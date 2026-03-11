using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace com.outlook_styner07.cs.control.Container
{
    public partial class DjsmSeparatorLabel : Label
    {
        #region Constructors
        #endregion

        #region Types
        #endregion

        #region Fields
        private TextRenderingHint _textRenderingHint = TextRenderingHint.AntiAlias;
        private SmoothingMode _smoothMode = SmoothingMode.AntiAlias;
        #endregion

        #region Properties
        [Browsable(true)]
        public TextRenderingHint RenderingHint
        {
            get { return _textRenderingHint; }
            set
            {
                if (_textRenderingHint != value)
                {
                    _textRenderingHint = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public SmoothingMode SmoothMode
        {
            get { return _smoothMode; }
            set
            {
                if (_smoothMode != value)
                {
                    _smoothMode = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = _smoothMode;
            g.TextRenderingHint = _textRenderingHint;

            SizeF size = g.MeasureString(Text, Font);

            using (var b = new SolidBrush(ForeColor))
            {
                g.DrawString(Text, Font, b, 0, (Height - size.Height) / 2);
            }
            
            int lineY = Height / 2 + 1;

            using (var p = new Pen(new SolidBrush(Color.LightGray), 1))
            {
                g.DrawLine(p, new PointF(size.Width + 3, lineY), new Point(Width, lineY));
            }
        }
        #endregion
    }
}
