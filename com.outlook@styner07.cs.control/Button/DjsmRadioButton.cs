using System.ComponentModel;

namespace com.outlook_styner07.cs.control.Button
{
    public class DjsmRadioButton : RadioButton
    {
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

        private Color _selectedForeColor = Color.White;

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

        private Color _deselectedForeColor = Color.DimGray;

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

        private ToolStripStatusLabelBorderSides _borderSides = ToolStripStatusLabelBorderSides.Bottom;

        [Browsable(false)]
        public new Color ForeColor { get; set; } = Color.Black;

        [Browsable(false)]
        public new bool AutoSize { get; set; } = false;

        public DjsmRadioButton()
        {
            //SetStyle(ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            
            Font = new Font(Font.FontFamily, Font.Size, FontStyle.Regular);
            UseVisualStyleBackColor = false;

            _selectedForeColor
                = _deselectedForeColor
                = ForeColor;

            AppearanceChanged += delegate
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
            };
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Graphics g = pevent.Graphics;

            g.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
            
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
                const int HORIZONTAL_MARGIN = 6;

                int buttonSize = 11;
                float buttonMargin = ClientRectangle.Height / 2 - buttonSize / 2;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                if (Checked)
                {
                    g.FillEllipse(new SolidBrush(_selectedForeColor), new RectangleF(HORIZONTAL_MARGIN, buttonMargin, buttonSize, buttonSize));
                }
                else
                {
                    g.DrawEllipse(new Pen(_deselectedForeColor), new RectangleF(HORIZONTAL_MARGIN, buttonMargin, buttonSize, buttonSize));
                }

                int textMargin = buttonSize + HORIZONTAL_MARGIN * 2;

                textDrawingRectangle.X += textMargin;
                textDrawingRectangle.Width -= textMargin;
            }

            TextRenderer.DrawText(g, Text, Font, textDrawingRectangle, ForeColor, DrawingUtil.GetTextFormatFlag(TextAlign));
        }
    }
}
