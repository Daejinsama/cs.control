using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmCheckGroupBoxNew : GroupBox
    {
        public bool Checked { get; set; } = false;

        private const int PADDING = 5;
        private RectangleF titleArea;

        private string _title = string.Empty;
        public string Title { get { return _title; } set { _title = value; Invalidate(); } }

        [Browsable(false)]
        public new string Text { get; set; }

        public DjsmCheckGroupBoxNew()
        {
            Padding = new Padding(1);
            DoubleBuffered = true;

            base.Text = string.Empty;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            Font oldFont = e.Control.Font;
            e.Control.Font = new Font(oldFont.FontFamily, 9f, oldFont.Style);

            base.OnControlAdded(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            Graphics g = e.Graphics;

            Size checkBoxSize = CheckBoxRenderer.GetGlyphSize(g, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);

            SizeF textSize = g.MeasureString(_title, Font);
            PointF textLocation = new PointF(PADDING + checkBoxSize.Width + 3, 0);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            titleArea = new RectangleF(PADDING, -3, textSize.Width + PADDING + checkBoxSize.Width, textSize.Height + 3);

            //g.SetClip (titleArea);
            //g.Clear(BackColor);
            ////g.Save();
            //g.ResetClip();

            g.FillRectangle(new SolidBrush(BackColor), titleArea);
            g.DrawString(_title, Font, new SolidBrush(ForeColor), textLocation);

            CheckBoxRenderer.DrawCheckBox(g, new Point(PADDING + 3, 0), Checked ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (titleArea.Contains(e.Location))
            {
                Checked = !Checked;
                Invalidate();
            }
        }
    }
}
