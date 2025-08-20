using System.ComponentModel;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmRadioGroupBox : GroupBox
    {
        private const int RADIO_LEFT_MARGIN = 5;
        private readonly RadioButton _radiobutton;

        [Browsable(true)]
        public bool Checked
        {
            get
            {
                return _radiobutton.Checked;
            }

            set
            {
                _radiobutton.Checked = value;
            }
        }

        public DjsmRadioGroupBox()
        {
            _radiobutton = new RadioButton { Location = new Point(RADIO_LEFT_MARGIN, 0), AutoSize = true, };
            _radiobutton.CheckedChanged += Radiobutton_CheckedChanged;
            Controls.Add(_radiobutton);

            base.Text = "";
        }

        private void Radiobutton_CheckedChanged(object sender, EventArgs e)
        {
            if (_radiobutton.Checked)
            {
                for (int len = Parent.Controls.Count, i = 0; i < len; i++)
                {
                    System.Windows.Forms. Control c = Parent.Controls[i];

                    if (c.Equals(this))
                    {
                        continue;
                    }
                    else
                    {
                        if (c is DjsmRadioGroupBox)
                        {
                            (c as DjsmRadioGroupBox).Checked = false;
                        }
                    }
                }
            }

            for (int len = Controls.Count, i = 0; i < len; i++)
            {
                System.Windows.Forms.Control c = Controls[i];
                if (c.Equals(_radiobutton))
                {
                    continue;
                }
                c.Enabled = _radiobutton.Checked;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _radiobutton.Text = base.Text;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResumeLayout(false);

        }
    }
}
