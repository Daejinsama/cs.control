using System.ComponentModel;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmCheckGroupBox : GroupBox
    {
        #region Constructors
        public DjsmCheckGroupBox()
        {
            chkButton = new CheckBox { Location = new Point(RADIO_LEFT_MARGIN, 0), AutoSize = true, };
            chkButton.CheckedChanged += delegate
            {
                GroupCheckedChanged?.Invoke(this, EventArgs.Empty);
                SetEnabled(chkButton.Checked);
            };
            Controls.Add(chkButton);

            base.Text = "";
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private const int RADIO_LEFT_MARGIN = 5;
        protected CheckBox chkButton;

        public event EventHandler GroupCheckedChanged;

        #endregion

        #region Properties
        [Browsable(true)]
        public bool Checked
        {
            get
            {
                return chkButton.Checked;
            }
            set
            {
                chkButton.Checked = value;
                SetEnabled(value);
            }
        }
        #endregion

        #region Methods
        public void SetEnabled(bool enabled)
        {
            for (int len = Controls.Count, i = 0; i < len; i++)
            {
                if (Controls[i].Equals(chkButton))
                {
                    continue;
                }

                Controls[i].Enabled = enabled;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Parent != null)
            {
                BackColor = Parent.BackColor;
            }

            chkButton.Text = base.Text;

            base.OnPaint(e);
        }
        #endregion
    }
}
