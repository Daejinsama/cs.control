using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmBlockTitleGroupBox : GroupBox
    {
        #region Constructors
        public DjsmBlockTitleGroupBox()
        {
            Padding = new Padding(1);
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

        private Color _fontColor = Color.White;
        private Color _titleBarBackColor = Color.White;
        private Color _titleBarForeColor = DjsmColorTable.SecondaryDark;
        private Color _borderColor = DjsmColorTable.SecondaryLight;

        private TitleAlign _align = TitleAlign.TOP_CENTER;

        private string _text;
        private bool _drawBorder = false;

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

        [Browsable(true)]
        [Category(CATEGORY_APPEARANCE)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color FontColor
        {
            get { return _fontColor; }
            set
            {
                if (_fontColor != value)
                {
                    _fontColor = value;
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

        [Browsable(true)]
        public Color TitleBarBackColor
        {
            get { return _titleBarBackColor; }
            set
            {
                if (_titleBarBackColor != value)
                {
                    _titleBarBackColor = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public Color TitleBarForeColor
        {
            get { return _titleBarForeColor; }
            set
            {
                if (_titleBarForeColor != value)
                {
                    _titleBarForeColor = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public bool DrawBorder
        {
            get { return _drawBorder; }
            set
            {
                if (_drawBorder != value)
                {
                    _drawBorder = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
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

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            SizeF textSize = e.Graphics.MeasureString(Text, Font);
            PointF textLocation = new PointF(0, 0);

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
                    textLocation.X = TITLE_EDGE_MARGIN;
                    textLocation.Y = Height - (textSize.Height);
                    break;
                case TitleAlign.BOTTOM_RIGHT:
                    textLocation.X = (Width - textSize.Width) - TITLE_EDGE_MARGIN;
                    textLocation.Y = Height - (textSize.Height);
                    break;
                case TitleAlign.TOP_CENTER:
                    textLocation.X = (Width - textSize.Width) / 2;
                    textLocation.Y = 3;
                    break;
                case TitleAlign.TOP_LEFT:
                    textLocation.X = TITLE_EDGE_MARGIN;
                    textLocation.Y = 3;
                    break;
                case TitleAlign.TOP_RIGHT:
                    textLocation.X = (Width - textSize.Width) - TITLE_EDGE_MARGIN;
                    textLocation.Y = 3;
                    break;
            }

            RectangleF rectTitle = new RectangleF(ClientRectangle.X, textLocation.Y - 3, ClientRectangle.Width, textSize.Height + 3);

            using (SolidBrush backBrush = new SolidBrush(_titleBarBackColor), foreBrush = new SolidBrush(_titleBarForeColor))
            {
                e.Graphics.FillRectangle(backBrush, rectTitle);
                e.Graphics.DrawString(Text, Font, foreBrush, textLocation);
            }

            if (_drawBorder)
            {
                using (var p = new Pen(_borderColor))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(new Point(ClientRectangle.X, ClientRectangle.Y), new Size(ClientRectangle.Width - 1, ClientRectangle.Height - 1)));
                }
            }
        }
        #endregion
    }
}
