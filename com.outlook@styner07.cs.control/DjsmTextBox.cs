using System.ComponentModel;

namespace com.outlook_styner07.cs.control
{
    public class DjsmTextBox : UserControl
    {
        private TextBox txtbox;

        [Browsable(true)]
        public override string Text { get { return txtbox.Text; } set { txtbox.Text = value; } }

        [Browsable(true)]
        public HorizontalAlignment TextAlign { get { return txtbox.TextAlign; } set { txtbox.TextAlign = value; } }

        [Browsable(true)]
        public char PasswordChar { get { return txtbox.PasswordChar; } set { txtbox.PasswordChar = value; } }

        [Browsable(true)]
        public bool UseSystemPasswordChar { get { return txtbox.UseSystemPasswordChar; } set { txtbox.UseSystemPasswordChar = value; } }

        [Browsable(true)]
        public int MaxLength { get { return txtbox.MaxLength; } set { txtbox.MaxLength = value; } }

        [Browsable(true)]
        public bool ReadOnly { get { return txtbox.ReadOnly; } set { txtbox.ReadOnly = value; } }

        //[Browsable(true)]
        //public string Description { get { return _description; } set { _description = value; Invalidate(); } }
        //private string _description = string.Empty;

        public int TextLength { get { return txtbox.TextLength; } }

        public new EventHandler<KeyEventArgs> KeyUp;
        public new EventHandler<KeyEventArgs> KeyDown;
        public new EventHandler<KeyPressEventArgs> KeyPress;

        public DjsmTextBox()
        {
            Controls.Add(txtbox = new TextBox { AutoSize = true, BorderStyle = BorderStyle.None, Dock = DockStyle.Fill });
            Padding = new Padding(3);
            BackColor = Color.White;
            ForeColor = DjsmColorTable.SecondaryDark;
            Text = string.Empty;

            txtbox.GotFocus += delegate { Invalidate(); };
            txtbox.LostFocus += delegate { Invalidate(); };

            txtbox.KeyUp += (obj, e) => { KeyUp?.Invoke(obj, e); };
            txtbox.KeyDown += (obj, e) => { KeyDown?.Invoke(obj, e); };
            txtbox.KeyPress += (obj, e) => { KeyPress?.Invoke(obj, e); };
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);

            txtbox.BackColor = BackColor;
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);

            txtbox.ForeColor = ForeColor;
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            Size = new Size(Size.Width, txtbox.Height + 1 + Padding.Top + Padding.Bottom);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new Size(Size.Width, txtbox.Height + 1 + Padding.Top + Padding.Bottom);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Graphics g = e.Graphics)
            {
                Color borderColor = !txtbox.ReadOnly && txtbox.Focused ? DjsmColorTable.Primary : DjsmColorTable.Secondary;
                ControlPaint.DrawBorder(g, this.ClientRectangle,
                borderColor, 0, ButtonBorderStyle.None,
                borderColor, 0, ButtonBorderStyle.None,
                borderColor, 0, ButtonBorderStyle.None,
                borderColor, 1, ButtonBorderStyle.Solid);

            }
        }

        private void DrawBorder()
        {
            Graphics g = Graphics.FromHwnd(this.Handle);

            ControlPaint.DrawBorder(g, DisplayRectangle,
                DjsmColorTable.DEFAULT_TOOLSTRIP_BORDER_COLOR, 0, ButtonBorderStyle.Solid,
                DjsmColorTable.DEFAULT_TOOLSTRIP_BORDER_COLOR, 0, ButtonBorderStyle.Solid,
                DjsmColorTable.DEFAULT_TOOLSTRIP_BORDER_COLOR, 0, ButtonBorderStyle.Solid,
                DjsmColorTable.DEFAULT_TOOLSTRIP_BORDER_COLOR, 1, ButtonBorderStyle.Solid);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Size = new System.Drawing.Size(390, 150);
            this.ResumeLayout(false);

        }
    }
}
