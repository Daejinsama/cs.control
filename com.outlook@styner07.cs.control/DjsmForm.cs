using com.outlook_styner07.cs.control.Container;
using System.ComponentModel;

namespace com.outlook_styner07.cs.control
{
    public partial class DjsmForm : Form
    {
        private const int HTBOTTOMRIGHT = 17;
        private const int WM_NCHITTEST = 0x84;

        private const int _gripSize = 32;

        private DjsmToolStrip tlsTitle;

        private ToolStripLabel tslTitle;

        private ToolStripButton tsbMinimize;
        private ToolStripButton tsbMaximize;
        private ToolStripButton tsbClose;

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
        public bool Resizable { get; set; } = true;

        [Browsable(true)]
        public bool ShowTitleBar
        {
            get => _showTitleBar;
            set
            {
                _showTitleBar = value;
                tlsTitle.Visible = _showTitleBar;
            }
        }

        private bool _showTitleBar = true;

        [Browsable(true)]
        public bool ShowTitleLabel
        {
            get => _showTitleLabel;
            set
            {
                _showTitleLabel = value;
                tslTitle.Visible = _showTitleLabel;
            }
        }

        private bool _showTitleLabel = true;

        [Browsable(true)]
        public bool ShowMinimizeButton
        {
            get => _showMinimizeButton;
            set
            {
                _showMinimizeButton = value;
                tsbMinimize.Visible = _showMinimizeButton;
            }
        }

        private bool _showMinimizeButton = true;

        [Browsable(true)]
        public bool ShowMaximizeButton
        {
            get => _showMaximizeButton;
            set
            {
                _showMaximizeButton = value;
                tsbMaximize.Visible = _showMaximizeButton;
            }
        }

        private bool _showMaximizeButton = true;

        [Browsable(true)]
        public bool showCloseButton
        {
            get => _showCloseButton;
            set
            {
                _showCloseButton = value;
                tsbClose.Visible = _showCloseButton;
            }
        }

        private bool _showCloseButton = true;

        public ToolStrip TitleBar => tlsTitle;

        public ToolStripLabel TitleLabel => tslTitle;

        public ToolStripButton MinimizeButton => tsbMinimize;

        public ToolStripButton MaximizeButton => tsbMaximize;

        public ToolStripButton CloseButton => tsbClose;

        public DjsmForm()
        {
            InitializeComponent();
            InitializeDefaultTitleBar();

            DoubleBuffered = true;
            Font = new Font("Arial", 9f);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            Padding = new Padding(1);
        }

        private void InitializeDefaultTitleBar()
        {
            tlsTitle = new DjsmToolStrip
            {
                Dock = DockStyle.Top,
                GripStyle = ToolStripGripStyle.Hidden,
                RenderMode = ToolStripRenderMode.Professional,
                Renderer = new DjsmToolStripRenderer(),
                Padding = new Padding(3)
            };

            tslTitle = new ToolStripLabel
            {
                Alignment = ToolStripItemAlignment.Left,
                Text = "DjsmForm",
                Padding = new Padding(10, 0, 0, 0),
            };

            tsbMinimize = new ToolStripButton
            {
                Alignment = ToolStripItemAlignment.Right,
                ToolTipText = "Minimize",
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Image = Properties.Resources.Minimize_Window,
                ImageScaling = ToolStripItemImageScaling.None
            };

            tsbMinimize.Click += (s, e) => WindowState = FormWindowState.Minimized;

            tsbMaximize = new ToolStripButton
            {
                Alignment = ToolStripItemAlignment.Right,
                ToolTipText = "Maximize",
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Image = Properties.Resources.Maximize_Window,
                ImageScaling = ToolStripItemImageScaling.None
            };
            tsbMaximize.Click += (s, e) => WindowState = FormWindowState.Maximized;

            tsbClose = new ToolStripButton
            {
                Alignment = ToolStripItemAlignment.Right,
                ToolTipText = "Close",
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Image = Properties.Resources.Close_Window,
                ImageScaling = ToolStripItemImageScaling.None
            };

            tsbClose.Click += (s, e) => Close();

            tlsTitle.Items.AddRange([tslTitle, tsbClose, tsbMaximize, tsbMinimize]);
            Controls.Add(tlsTitle);
            tlsTitle.SendToBack();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            tlsTitle.SendToBack();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Left) == ToolStripStatusLabelBorderSides.Left ? 1 : 0, ButtonBorderStyle.Solid,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Top) == ToolStripStatusLabelBorderSides.Top ? 1 : 0, ButtonBorderStyle.Solid,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Right) == ToolStripStatusLabelBorderSides.Right ? 1 : 0, ButtonBorderStyle.Solid,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Bottom) == ToolStripStatusLabelBorderSides.Bottom ? 1 : 0, ButtonBorderStyle.Solid);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (!Resizable)
            {
                return;
            }

            if (m.Msg == WM_NCHITTEST)
            {
                Point pos = PointToClient(Cursor.Position);
                if (pos.X >= ClientSize.Width - _gripSize &&
                    pos.Y >= ClientSize.Height - _gripSize)
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                }
            }
        }
    }
}
