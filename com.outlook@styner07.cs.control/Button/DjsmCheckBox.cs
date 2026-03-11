using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace com.outlook_styner07.cs.control.Button
{
    public class DjsmCheckBox : CheckBox
    {
        #region Constructors
        public DjsmCheckBox()
        {
            DoubleBuffered = true;

            UseVisualStyleBackColor = false;
            _checkedForeColor = _uncheckedForeColor = ForeColor;
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private const int HORIZONTAL_MARGIN = 3;

        private Color _checkedForeColor;
        private Color _uncheckedForeColor;
        private TextRenderingHint _textRenderingHint = TextRenderingHint.AntiAlias;
        private SmoothingMode _smoothMode = SmoothingMode.AntiAlias;
        #endregion

        #region Properties
        [Browsable(true)]
        public Color CheckedForeColor
        {
            get
            {
                return _checkedForeColor;
            }
            set
            {
                if (_checkedForeColor != value)
                {
                    _checkedForeColor = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public Color UncheckedForeColor
        {
            get
            {
                return _uncheckedForeColor;
            }
            set
            {
                if (_uncheckedForeColor != value)
                {
                    _uncheckedForeColor = value;
                    Invalidate();
                }
            }
        }

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

        [Browsable(false)]
        public new Color ForeColor { get; set; } = Color.Black;
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;

            g.SmoothingMode = _smoothMode;
            g.TextRenderingHint = _textRenderingHint;

            g.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

            
            int buttonSize = 11;
            float buttonMargin = ClientRectangle.Height / 2 - buttonSize / 2;

            if (Checked)
            {
                using (var b = new SolidBrush(_checkedForeColor))
                {
                    g.FillRectangle(b, new RectangleF(0, buttonMargin, buttonSize, buttonSize));
                }
            }

            using (var p = new Pen(_uncheckedForeColor))
            {
                g.DrawRectangle(p, new RectangleF(0, buttonMargin, buttonSize, buttonSize));
            }
            
            Font = new Font(Font.FontFamily, Font.Size, Checked ? FontStyle.Bold : FontStyle.Regular);
            ForeColor = Checked ? _checkedForeColor : _uncheckedForeColor;

            int textMargin = buttonSize + HORIZONTAL_MARGIN;

            Rectangle textDrawingRectangle = ClientRectangle;

            textDrawingRectangle.X += textMargin;

            using (var b = new SolidBrush(ForeColor))
            {
                g.DrawString(Text, Font, b, textDrawingRectangle, DrawingUtil.ConvertStringAlign(TextAlign));
            }
        }
        #endregion
    }
}
