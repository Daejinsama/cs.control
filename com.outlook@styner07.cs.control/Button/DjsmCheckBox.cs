using System.ComponentModel;
using System.Drawing.Text;

namespace com.outlook_styner07.cs.control.Button
{
    public class DjsmCheckBox : CheckBox
    {
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

        private Color _checkedForeColor;

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

        private Color _uncheckedForeColor;
        
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

        [Browsable(false)]
        public new Color ForeColor { get; set; } = Color.Black;

        //[Browsable(false)]
        //public new bool AutoSize { get; set; } = true;

        public DjsmCheckBox()
        {
            DoubleBuffered = true;

            UseVisualStyleBackColor = false;
            _checkedForeColor
                = _uncheckedForeColor
                = ForeColor;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;

            g.TextRenderingHint = _textRenderingHint;

            g.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

            const int HORIZONTAL_MARGIN = 3;
            int buttonSize = 11;
            float buttonMargin = ClientRectangle.Height / 2 - buttonSize / 2;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (Checked)
            {
                g.FillRectangle(new SolidBrush(_checkedForeColor), new RectangleF(HORIZONTAL_MARGIN, buttonMargin, buttonSize, buttonSize));
            }
            else
            {
                g.DrawRectangle(new Pen(_uncheckedForeColor), new RectangleF(HORIZONTAL_MARGIN, buttonMargin, buttonSize, buttonSize));
            }

            Font = new Font(Font.FontFamily, Font.Size, Checked ? FontStyle.Bold : FontStyle.Regular);
            ForeColor = Checked ? _checkedForeColor : _uncheckedForeColor;
            
            int textMargin = buttonSize + HORIZONTAL_MARGIN * 2;

            Rectangle textDrawingRectangle = ClientRectangle;

            textDrawingRectangle.X += textMargin;
            //textDrawingRectangle.Width -= textMargin;

            //Width = textDrawingRectangle.X + textDrawingRectangle.Width;
            
            g.DrawString(Text, Font, new SolidBrush(ForeColor), textDrawingRectangle, DrawingUtil.ConvertStringAlign(TextAlign));
        }
    }
}
