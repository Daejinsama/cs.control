using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmRadioGroupBox : GroupBox
    {
        private const int RADIO_LEFT_MARGIN = 5;
        private RadioButton radiobutton;

        [Browsable(true)]
        public bool Checked
        {
            get
            {
                return radiobutton.Checked;
            }

            set
            {
                radiobutton.Checked = value;
            }
        }

        public DjsmRadioGroupBox()
        {
            radiobutton = new RadioButton { Location = new Point(RADIO_LEFT_MARGIN, 0), AutoSize = true, };
            radiobutton.CheckedChanged += Radiobutton_CheckedChanged;
            Controls.Add(radiobutton);

            base.Text = "";
        }

        private void Radiobutton_CheckedChanged(object sender, EventArgs e)
        {
            if (radiobutton.Checked)
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
                if (c.Equals(radiobutton))
                {
                    continue;
                }
                c.Enabled = radiobutton.Checked;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            radiobutton.Text = base.Text;
        }

        //[Browsable(false)]
        //public override Font Font { get => base.Font; set => base.Font = value; }

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
