using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmGroupBox : GroupBox
    {
        private const string CATEGORY_APPEARANCE = "Appearance";

        public enum TitleAlign { TOP_LEFT, TOP_CENTER, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_CENTER, BOTTOM_RIGHT };

        private const int titleEdgeMargin = 10;

        public DjsmGroupBox()
        {
            DoubleBuffered = true;
            base.Text = string.Empty;
            base.Font = new Font("arial", 1f, FontStyle.Regular);   //not use
        }


        private Font _font = new Font("arial", 9f, FontStyle.Regular);

        [Browsable(true)]
        [Category(CATEGORY_APPEARANCE)]
        [DefaultValue("GroupBoxFont")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Font Font { get { return _font; } set { _font = value; Invalidate(); } }

        private string _text;
        [Browsable(true)]
        [Category(CATEGORY_APPEARANCE)]
        [DefaultValue("GroupBoxText")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new string Text { get { return _text; } set { _text = value; Invalidate(); } }

        private TitleAlign _align = TitleAlign.TOP_CENTER;
        public TitleAlign Align { get { return _align; } set { _align = value; Invalidate(); } }

        private bool _drawRoundRect = false;
        public bool DrawRoundRect { get { return _drawRoundRect; } set { _drawRoundRect = value; Invalidate(); } }

        private int _radius = 25;
        public int Radius { get { return _radius; } set { _radius = value; Invalidate(); } }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            Font oldFont = e.Control.Font;
            e.Control.Font = new Font(oldFont.FontFamily, 9f, oldFont.Style);

            base.OnControlAdded(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            SolidBrush foreBrush = new SolidBrush(ForeColor);
            SolidBrush backBrush = new SolidBrush(BackColor);

            SizeF textSize = e.Graphics.MeasureString(Text, Font);
            PointF textLocation = new PointF(0, 0);

            e.Graphics.SmoothingMode = SmoothingMode.None;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            GroupBoxRenderer.DrawParentBackground(e.Graphics, ClientRectangle, this);

            Pen pathPen = new Pen(Color.LightGray, 1f);

            switch (Align)
            {
                case TitleAlign.BOTTOM_CENTER:
                case TitleAlign.BOTTOM_LEFT:
                case TitleAlign.BOTTOM_RIGHT:
                    e.Graphics.DrawPath(pathPen, DrawRoundRect ? DrawingUtil.GetRoundRectPath(ClientRectangle, textSize, Radius, false) : DrawingUtil.GetNormalRectPath(ClientRectangle, textSize, false));
                    // resize top padding when child docking
                    base.Font = new Font("arial", 1f, FontStyle.Regular);
                    Padding = new Padding(3, 0, 3, (int)textSize.Height);
                    break;
                default:
                    e.Graphics.DrawPath(pathPen, DrawRoundRect ? DrawingUtil.GetRoundRectPath(ClientRectangle, textSize, Radius, true) : DrawingUtil.GetNormalRectPath(ClientRectangle, textSize, true));
                    base.Font = new Font("arial", Font.Size, FontStyle.Regular);
                    Padding = new Padding(3, 3, 3, 3);
                    break;
            }

            switch (Align)
            {
                case TitleAlign.BOTTOM_CENTER:
                    textLocation.X = (Width - textSize.Width) / 2;
                    textLocation.Y = Height - (textSize.Height);
                    break;
                case TitleAlign.BOTTOM_LEFT:
                    textLocation.X = titleEdgeMargin;
                    textLocation.Y = Height - (textSize.Height);
                    break;
                case TitleAlign.BOTTOM_RIGHT:
                    textLocation.X = (Width - textSize.Width) - titleEdgeMargin;
                    textLocation.Y = Height - (textSize.Height);
                    break;
                case TitleAlign.TOP_CENTER:
                    textLocation.X = (Width - textSize.Width) / 2;
                    textLocation.Y = 0;
                    break;
                case TitleAlign.TOP_LEFT:
                    textLocation.X = titleEdgeMargin;
                    textLocation.Y = 0;
                    break;
                case TitleAlign.TOP_RIGHT:
                    textLocation.X = (Width - textSize.Width) - titleEdgeMargin;
                    textLocation.Y = 0;
                    break;
            }

            e.Graphics.FillRectangle(backBrush, textLocation.X, textLocation.Y, textSize.Width, textSize.Height);
            e.Graphics.DrawString(Text, Font, foreBrush, textLocation);
        }
    }
}
