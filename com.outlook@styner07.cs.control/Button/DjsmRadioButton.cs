using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace com.outlook_styner07.cs.control.Button
{
    public class DjsmRadioButton : RadioButton
    {
        #region Constructors
        public DjsmRadioButton()
        {
            DoubleBuffered = true;

            Font = new Font(Font.FontFamily, Font.Size, FontStyle.Regular);
            UseVisualStyleBackColor = false;

            _selectedForeColor = _deselectedForeColor = ForeColor;

            AppearanceChanged += djsmRadioButton_AppearanceChanged;
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private const int HORIZONTAL_MARGIN = 3;

        private Color _selectedForeColor = Color.White;
        private Color _deselectedForeColor = Color.DimGray;
        private ToolStripStatusLabelBorderSides _borderSides = ToolStripStatusLabelBorderSides.Bottom;
        private TextRenderingHint _textRenderingHint = TextRenderingHint.AntiAlias;
        private SmoothingMode _smoothMode = SmoothingMode.AntiAlias;
        #endregion

        #region Properties
        [Browsable(true)]
        public Color SelectedForeColor
        {
            get { return _selectedForeColor; }
            set
            {
                if (_selectedForeColor != value)
                {
                    _selectedForeColor = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public Color DeselectedForeColor
        {
            get { return _deselectedForeColor; }
            set
            {
                if (_deselectedForeColor != value)
                {
                    _deselectedForeColor = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public ToolStripStatusLabelBorderSides BorderSides
        {
            get { return _borderSides; }
            set
            {
                if (_borderSides != value)
                {
                    _borderSides = value;
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
        private void djsmRadioButton_AppearanceChanged(object? sender, EventArgs e)
        {
            if (Appearance == Appearance.Button)
            {
                FlatStyle = FlatStyle.Flat;
                FlatAppearance.BorderSize = 0;
                FlatAppearance.CheckedBackColor = Color.Transparent;
                FlatAppearance.MouseDownBackColor = Color.Transparent;
                FlatAppearance.MouseOverBackColor = Color.Transparent;
            }
            else
            {

            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
            g.SmoothingMode = _smoothMode;
            g.TextRenderingHint = _textRenderingHint;

            using (var b = new SolidBrush(BackColor))
            {
                g.FillRectangle(b, ClientRectangle);
            }

            Rectangle textDrawingRectangle = ClientRectangle;

            Font = new Font(Font.FontFamily, Font.Size, Checked ? FontStyle.Bold : FontStyle.Regular);
            ForeColor = Checked ? _selectedForeColor : _deselectedForeColor;

            if (Appearance == Appearance.Button)
            {
                if (Checked)
                {
                    ControlPaint.DrawBorder(g, ClientRectangle,
                    ForeColor, (_borderSides & ToolStripStatusLabelBorderSides.Left) == ToolStripStatusLabelBorderSides.Left ? 3 : 0, ButtonBorderStyle.Solid,
                    ForeColor, (_borderSides & ToolStripStatusLabelBorderSides.Top) == ToolStripStatusLabelBorderSides.Top ? 3 : 0, ButtonBorderStyle.Solid,
                    ForeColor, (_borderSides & ToolStripStatusLabelBorderSides.Right) == ToolStripStatusLabelBorderSides.Right ? 3 : 0, ButtonBorderStyle.Solid,
                    ForeColor, (_borderSides & ToolStripStatusLabelBorderSides.Bottom) == ToolStripStatusLabelBorderSides.Bottom ? 3 : 0, ButtonBorderStyle.Solid);
                }
            }
            else
            {
                int buttonSize = 11;
                float buttonMargin = ClientRectangle.Height / 2 - buttonSize / 2;

                if (Checked)
                {
                    using (var b = new SolidBrush(_selectedForeColor))
                    {
                        g.FillEllipse(b, new RectangleF(0, buttonMargin, buttonSize, buttonSize));
                    }
                }

                using (var p = new Pen(_deselectedForeColor))
                {
                    g.DrawEllipse(p, new RectangleF(0, buttonMargin, buttonSize, buttonSize));
                }
                
                int textMargin = buttonSize + HORIZONTAL_MARGIN;

                textDrawingRectangle.X += textMargin;
            }

            using (var b = new SolidBrush(ForeColor))
            {
                g.DrawString(Text, Font, b, textDrawingRectangle, DrawingUtil.ConvertStringAlign(TextAlign));
            }
        }
        #endregion
    }
}
