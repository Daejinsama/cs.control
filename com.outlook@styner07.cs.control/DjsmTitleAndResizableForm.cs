namespace com.outlook_styner07.cs.control
{
    public partial class DjsmTitleAndResizableForm : DjsmTitleForm
    {
        private const int HTBOTTOMRIGHT = 17;
        private const int WM_NCHITTEST = 0x84;
        
        private const int _gripSize = 32;

        public DjsmTitleAndResizableForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            FormBorderStyle = FormBorderStyle.None;

        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

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
