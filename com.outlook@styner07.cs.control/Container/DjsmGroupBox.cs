using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmGroupBox : GroupBox
    {
        #region Constructors
        public DjsmGroupBox()
        {
            DoubleBuffered = true;
            base.Text = string.Empty;
            base.Font = new Font("arial", 1f, FontStyle.Regular);   //not use
        }
        #endregion

        #region Types
        public enum TitleAlign
        {
            TOP_LEFT,
            TOP_CENTER,
            TOP_RIGHT,
            BOTTOM_LEFT,
            BOTTOM_CENTER,
            BOTTOM_RIGHT
        };
        #endregion

        #region Fields
        private const string CATEGORY_APPEARANCE = "Appearance";
        private const int TITLE_EDGE_MARGIN = 10;

        private Font _font = new Font("arial", 9f, FontStyle.Regular);
        private string _text;
        private TitleAlign _align = TitleAlign.TOP_CENTER;
        private bool _drawRoundRect = false;
        private int _radius = 25;
        #endregion

        #region Properties
        [Browsable(true)]
        [Category(CATEGORY_APPEARANCE)]
        [DefaultValue("GroupBoxFont")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Font Font
        {
            get { return _font; }
            set
            {
                if (_font != value)
                {
                    _font = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Category(CATEGORY_APPEARANCE)]
        [DefaultValue("GroupBoxText")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    Invalidate();
                }
            }
        }

        public TitleAlign Align
        {
            get { return _align; }
            set
            {
                if (_align != value)
                {
                    _align = value;
                    Invalidate();
                }
            }
        }

        public bool DrawRoundRect
        {
            get { return _drawRoundRect; }
            set
            {
                if (_drawRoundRect != value)
                {
                    _drawRoundRect = value;
                    Invalidate();
                }
            }
        }

        public int Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    _radius = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Methods
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            SizeF textSize = e.Graphics.MeasureString(Text, Font);
            PointF textLocation = new PointF(0, 0);

            e.Graphics.SmoothingMode = SmoothingMode.None;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            GroupBoxRenderer.DrawParentBackground(e.Graphics, ClientRectangle, this);

            using (Pen pathPen = new Pen(Color.LightGray, 1f))
            {
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
            }

            switch (Align)
            {
                case TitleAlign.BOTTOM_CENTER:
                    textLocation.X = (Width - textSize.Width) / 2;
                    textLocation.Y = Height - (textSize.Height);
                    break;
                case TitleAlign.BOTTOM_LEFT:
                    textLocation.X = TITLE_EDGE_MARGIN;
                    textLocation.Y = Height - (textSize.Height);
                    break;
                case TitleAlign.BOTTOM_RIGHT:
                    textLocation.X = (Width - textSize.Width) - TITLE_EDGE_MARGIN;
                    textLocation.Y = Height - (textSize.Height);
                    break;
                case TitleAlign.TOP_CENTER:
                    textLocation.X = (Width - textSize.Width) / 2;
                    textLocation.Y = 0;
                    break;
                case TitleAlign.TOP_LEFT:
                    textLocation.X = TITLE_EDGE_MARGIN;
                    textLocation.Y = 0;
                    break;
                case TitleAlign.TOP_RIGHT:
                    textLocation.X = (Width - textSize.Width) - TITLE_EDGE_MARGIN;
                    textLocation.Y = 0;
                    break;
            }

            using (SolidBrush foreBrush = new SolidBrush(ForeColor), backBrush = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(backBrush, textLocation.X, textLocation.Y, textSize.Width, textSize.Height);
                e.Graphics.DrawString(Text, Font, foreBrush, textLocation);
            }
        }
        #endregion
    }
}
