namespace com.outlook_styner07.cs.control
{
    public partial class DjsmBaseForm : Form
    {
        public DjsmBaseForm()
        {
            InitializeComponent();
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
    }
}
