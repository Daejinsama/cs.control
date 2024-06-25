using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmCheckGroupBox : GroupBox
    {
        private const int RADIO_LEFT_MARGIN = 5;
        private CheckBox chkButton;
        
        public event EventHandler GroupCheckedChanged;

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
            base.OnPaint(e);
            chkButton.Text = base.Text;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NntRadioGroupBox
            // 
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResumeLayout(false);

        }
    }
}
