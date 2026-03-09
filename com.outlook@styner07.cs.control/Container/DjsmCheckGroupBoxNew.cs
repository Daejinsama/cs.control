using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmCheckGroupBoxNew : GroupBox
    {
        #region Constructors
        public DjsmCheckGroupBoxNew()
        {
            Padding = new Padding(1);
            DoubleBuffered = true;

            base.Text = string.Empty;
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private const int PADDING = 5;
        private RectangleF _titleArea;
        private string _title = string.Empty;
        #endregion

        #region Properties
        public bool Checked { get; set; } = false;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public new string Text { get; set; }
        #endregion

        #region Methods
        protected override void OnControlAdded(ControlEventArgs e)
        {
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

            _titleArea = new RectangleF(PADDING, -3, textSize.Width + PADDING + checkBoxSize.Width, textSize.Height + 3);

            g.FillRectangle(new SolidBrush(BackColor), _titleArea);
            g.DrawString(_title, Font, new SolidBrush(ForeColor), textLocation);

            CheckBoxRenderer.DrawCheckBox(g, new Point(PADDING + 3, 0), Checked ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_titleArea.Contains(e.Location))
            {
                Checked = !Checked;
                Invalidate();
            }
        }
        #endregion
    }
}
