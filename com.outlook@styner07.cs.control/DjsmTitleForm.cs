using System.ComponentModel;

namespace com.outlook_styner07.cs.control
{
    public partial class DjsmTitleForm : DjsmBaseForm
    {
        [Browsable(true)]
        public bool ShowWindowTitle
        {
            get => lblWindowTitle.Visible;
            set => lblWindowTitle.Visible = value;
        }

        [Browsable(true)]
        public bool ShowMinimizeWindowButton
        {
            get => btnMinimizeWindow.Visible;
            set => btnMinimizeWindow.Visible = value;
        }

        [Browsable(true)]
        public bool ShowMaximizeWindowButton
        {
            get => btnMaximizeWindow.Visible;
            set => btnMaximizeWindow.Visible = value;
        }

        [Browsable(true)]
        public bool ShowCloseWindowButton
        {
            get => btnCloseWindow.Visible;
            set => btnCloseWindow.Visible = value;
        }

        public DjsmTitleForm()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;

            btnMinimizeWindow.Click += btnMinimizeWindow_Click;
            btnMaximizeWindow.Click += btnMaximizeWindow_Click;
            btnCloseWindow.Click += btnCloseWindow_Click;
        }

        private void btnMinimizeWindow_Click(object? sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnMaximizeWindow_Click(object? sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void btnCloseWindow_Click(object? sender, EventArgs e)
        {
            Close();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            tlbTitle.SendToBack();
        }
    }
}
