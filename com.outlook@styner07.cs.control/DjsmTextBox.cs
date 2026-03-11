using System.ComponentModel;

namespace com.outlook_styner07.cs.control
{
    public class DjsmTextBox : UserControl
    {
        #region Constructors
        public DjsmTextBox()
        {
            Controls.Add(_txtBox = new TextBox { AutoSize = true, BorderStyle = BorderStyle.None, Dock = DockStyle.Fill });
            Padding = new Padding(3);
            BackColor = Color.White;
            ForeColor = DjsmColorTable.SecondaryDark;
            Text = string.Empty;

            _txtBox.GotFocus += delegate { Invalidate(); };
            _txtBox.LostFocus += delegate { Invalidate(); };

            _txtBox.KeyUp += (obj, e) => { KeyUp?.Invoke(obj, e); };
            _txtBox.KeyDown += (obj, e) => { KeyDown?.Invoke(obj, e); };
            _txtBox.KeyPress += (obj, e) => { KeyPress?.Invoke(obj, e); };
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private TextBox _txtBox;

        public new EventHandler<KeyEventArgs> KeyUp;
        public new EventHandler<KeyEventArgs> KeyDown;
        public new EventHandler<KeyPressEventArgs> KeyPress;
        #endregion

        #region Properties
        [Browsable(true)]
        public override string Text
        {
            get { return _txtBox.Text; }
            set
            {
                _txtBox.Text = value;
            }
        }

        [Browsable(true)]
        public HorizontalAlignment TextAlign
        {
            get { return _txtBox.TextAlign; }
            set
            {
                _txtBox.TextAlign = value;
            }
        }

        [Browsable(true)]
        public char PasswordChar
        {
            get { return _txtBox.PasswordChar; }
            set
            {
                _txtBox.PasswordChar = value;
            }
        }

        [Browsable(true)]
        public bool UseSystemPasswordChar
        {
            get { return _txtBox.UseSystemPasswordChar; }
            set
            {
                _txtBox.UseSystemPasswordChar = value;
            }
        }

        [Browsable(true)]
        public int MaxLength
        {
            get { return _txtBox.MaxLength; }
            set
            {
                _txtBox.MaxLength = value;
            }
        }

        [Browsable(true)]
        public bool ReadOnly
        {
            get { return _txtBox.ReadOnly; }
            set
            {
                _txtBox.ReadOnly = value;
            }
        }

        public int TextLength { get { return _txtBox.TextLength; } }
        #endregion

        #region Methods
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);

            _txtBox.BackColor = BackColor;
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);

            _txtBox.ForeColor = ForeColor;
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            Size = new Size(Size.Width, _txtBox.Height + 1 + Padding.Top + Padding.Bottom);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new Size(Size.Width, _txtBox.Height + 1 + Padding.Top + Padding.Bottom);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Graphics g = e.Graphics)
            {
                Color borderColor = !_txtBox.ReadOnly && _txtBox.Focused ? DjsmColorTable.Primary : DjsmColorTable.Secondary;
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
        #endregion
    }
}
