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

        [Browsable(false)]
        private new FormBorderStyle FormBorderStyle;

        [Browsable(true)]
        public bool Resizable { get; set; } = true;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(true)]
        public bool ShowTitleBar
        {
            get => tlsTitle.Visible;
            set => tlsTitle.Visible = value;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(true)]
        public bool ShowTitleLabel
        {
            get => tslTitle.Visible;
            set => tslTitle.Visible = value;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(true)]
        public bool ShowMinimizeButton
        {
            get => tsbMinimize.Visible;
            set => tsbMinimize.Visible = value;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(true)]
        public bool ShowMaximizeButton
        {
            get => tsbMaximize.Visible;
            set => tsbMaximize.Visible = value;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(true)]
        public bool showCloseButton
        {
            get => tsbClose.Visible;
            set => tsbClose.Visible = value;
        }

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
