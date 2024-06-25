using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmBlockTitleGroupBox : GroupBox
    {
        private const string CATEGORY_APPEARANCE = "Appearance";

        public enum TitleAlign { TOP_LEFT, TOP_CENTER, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_CENTER, BOTTOM_RIGHT };

        private const int titleEdgeMargin = 10;

        public DjsmBlockTitleGroupBox()
        {
            Padding = new Padding(1);
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

        private Color _fontColor = Color.White;
        [Browsable(true)]
        [Category(CATEGORY_APPEARANCE)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color FontColor { get { return _fontColor; } set { _fontColor = value; Invalidate(); } }

        private TitleAlign _align = TitleAlign.TOP_CENTER;
        public TitleAlign Align { get { return _align; } set { _align = value; Invalidate(); } }

        private Color _titleBarBackColor = Color.White;
        [Browsable(true)]
        public Color TitleBarBackColor { get { return _titleBarBackColor; } set { _titleBarBackColor = value; Invalidate(); } }

        private Color _titleBarForeColor = DjsmColorTable.SecondaryDark;
        [Browsable(true)]
        public Color TitleBarForeColor { get { return _titleBarForeColor; } set { _titleBarForeColor = value; Invalidate(); } }

        private bool _drawBorder = false;
        [Browsable(true)]
        public bool DrawBorder { get { return _drawBorder; } set { _drawBorder = value; Invalidate(); } }

        private Color _borderColor = DjsmColorTable.SecondaryLight;
        [Browsable(true)]
        public Color BorderColor { get { return _borderColor; } set { _borderColor = value; Invalidate(); } }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            //Font oldFont = e.Control.Font;
            //e.Control.Font = new Font(oldFont.FontFamily, 9f, oldFont.Style);

            base.OnControlAdded(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            SolidBrush backBrush = new SolidBrush(_titleBarBackColor);

            SizeF textSize = e.Graphics.MeasureString(Text, Font);
            PointF textLocation = new PointF(0, 0);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            GroupBoxRenderer.DrawParentBackground(e.Graphics, ClientRectangle, this);

            switch (Align)
            {
                case TitleAlign.BOTTOM_CENTER:
                case TitleAlign.BOTTOM_LEFT:
                case TitleAlign.BOTTOM_RIGHT:
                    // resize top padding when child docking
                    base.Font = new Font("arial", 1f, FontStyle.Regular);
                    Padding = new Padding(3, 0, 3, (int)textSize.Height + 6);
                    break;
                default:
                    base.Font = new Font("arial", Font.Size, FontStyle.Regular);
                    Padding = new Padding(3, 3 + 3, 3, 3);
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
                    textLocation.Y = 3;
                    break;
                case TitleAlign.TOP_LEFT:
                    textLocation.X = titleEdgeMargin;
                    textLocation.Y = 3;
                    break;
                case TitleAlign.TOP_RIGHT:
                    textLocation.X = (Width - textSize.Width) - titleEdgeMargin;
                    textLocation.Y = 3;
                    break;
            }

            RectangleF rectTitle = new RectangleF(ClientRectangle.X, textLocation.Y - 3, ClientRectangle.Width, textSize.Height + 3);

            e.Graphics.FillRectangle(backBrush, rectTitle);
            e.Graphics.DrawString(Text, Font, new SolidBrush(_titleBarForeColor), textLocation);
            if (_drawBorder)
            {
                e.Graphics.DrawRectangle(new Pen(_borderColor), new Rectangle(new Point(ClientRectangle.X, ClientRectangle.Y), new Size(ClientRectangle.Width - 1, ClientRectangle.Height - 1)));
            }
        }

        private GraphicsPath GetNormalPath(Rectangle rect)
        {
            PointF pointLeftTop = new PointF(rect.X, rect.Y);
            PointF pointRightTop = new PointF(rect.X + rect.Width - 1, rect.Y);
            PointF pointRightBottom = new PointF(rect.X + rect.Width - 1, (rect.Y + rect.Height - 1));
            PointF pointLeftBottom = new PointF(rect.X, (rect.Y + rect.Height - 1));

            GraphicsPath ret = new GraphicsPath();
            ret.AddLine(pointLeftTop, pointRightTop);   // top horizon
            ret.AddLine(pointRightTop, pointRightBottom); // right vertical
            ret.AddLine(pointRightBottom, pointLeftBottom); // bottom horizon
            ret.AddLine(pointLeftBottom, pointLeftTop); // left vertical
            ret.CloseAllFigures();
            return ret;
        }
    }
}
