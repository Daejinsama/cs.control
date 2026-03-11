using System.ComponentModel;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmRadioGroupBox : GroupBox
    {
        #region Constructors
        public DjsmRadioGroupBox()
        {
            _button = new RadioButton { Location = new Point(RADIO_LEFT_MARGIN, 0), AutoSize = true, };
            _button.CheckedChanged += _button_CheckedChanged;

            Controls.Add(_button);

            base.Text = "";
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private const int RADIO_LEFT_MARGIN = 5;
        private readonly RadioButton _button;
        #endregion

        #region Properties
        [Browsable(true)]
        public bool Checked
        {
            get
            {
                return _button.Checked;
            }
            set
            {
                if (_button.Checked != value)
                {
                    _button.Checked = value;
                }
            }
        }
        #endregion

        #region Methods
        private void _button_CheckedChanged(object? sender, EventArgs e)
        {
            if (Parent != null && _button.Checked)
            {
                for (int il = Parent.Controls.Count, i = 0; i < il; i++)
                {
                    Control c = Parent.Controls[i];

                    if (c.Equals(this))
                    {
                        continue;
                    }
                    else
                    {
                        if (c is DjsmRadioGroupBox btn)
                        {
                            btn.Checked = false;
                        }
                    }
                }
            }

            bool enabled = _button.Checked;
            for (int il = Controls.Count, i = 0; i < il; i++)
            {
                Control c = Controls[i];

                if (c.Equals(_button))
                {
                    continue;
                }

                c.Enabled = enabled;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _button.Text = base.Text;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResumeLayout(false);

        }
        #endregion
    }
}
