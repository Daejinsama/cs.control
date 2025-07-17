using System.ComponentModel;

namespace com.outlook_styner07.cs.control
{
    public partial class DjsmBaseForm : Form
    {
        [Browsable(false)]
        private new FormBorderStyle FormBorderStyle;

        public DjsmBaseForm()
        {
            InitializeComponent();
            Padding = new Padding(1);
            FormBorderStyle = FormBorderStyle.None;
        }

        public const int WS_CAPTION = 0x00c00000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.Style = param.Style & ~WS_CAPTION;
                return param;
            }
        }

        [Browsable(true)]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
                    Invalidate();
                }
            }
        }
        private Color _borderColor = DjsmColorTable.SecondaryLight;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Left) == ToolStripStatusLabelBorderSides.Left ? 1 : 0, ButtonBorderStyle.Solid,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Top) == ToolStripStatusLabelBorderSides.Top ? 1 : 0, ButtonBorderStyle.Solid,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Right) == ToolStripStatusLabelBorderSides.Right ? 1 : 0, ButtonBorderStyle.Solid,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Bottom) == ToolStripStatusLabelBorderSides.Bottom ? 1 : 0, ButtonBorderStyle.Solid);
        }
    }
}
