using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace com.outlook_styner07.cs.control.Gauge
{
    public class DjsmIndicator : Control
    {
        [Browsable(true)]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        private Color borderColor = Color.Gray;

        [Browsable(true)]
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                borderWidth = value; Invalidate();
            }
        }

        private int borderWidth = 1;

        public event EventHandler ColorChanged;

        [Browsable(true)]
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                Invalidate();

                ColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private Color color = Color.Lime;

        [Browsable(true)]
        public byte Opacity
        {
            get
            {
                return opacity;
            }
            set
            {
                opacity = value;
                Invalidate();
            }
        }

        private byte opacity = 10;

        public enum ShapeType { Ellipse, Rectangle }

        public ShapeType Shape
        {
            get { return shape; }
            set
            {
                shape = value;
                Invalidate();
            }
        }

        private ShapeType shape = ShapeType.Ellipse;

        public bool Blink
        {
            get { return blink; }
            set
            {
                blink = value;
                SetBlink();
            }
        }

        private bool blink = false;

        [Browsable(true)]
        public long BlinkInterval
        {
            get { return blinkInterval; }
            set
            {
                blinkInterval = value;
                SetBlink();
            }
        }

        private long blinkInterval = 500;

        public Color BlinkOnColor
        {
            get { return blinkOnColor; }
            set
            {
                blinkOnColor = value;
                SetBlink();
            }
        }

        private Color blinkOnColor = Color.Lime;

        public Color BlinkOffColor
        {
            get { return blinkOffColor; }
            set
            {
                blinkOffColor = value;
                SetBlink();
            }
        }

        private Color blinkOffColor = Color.Red;

        [Browsable(false)]
        public new Color BackColor { get; } = Color.Transparent;

        private const int PADDING = 1;

        private System.Threading.Timer? blinkTimer;

        //public DjsmIndicator()
        //{
        //    SetStyle(ControlStyles.UserPaint, true);
        //}

        private void SetBlink()
        {
            blinkTimer?.Dispose();
            blinkTimer = null;

            if (blink)
            {
                blinkTimer = new System.Threading.Timer((obj) =>
                {
                    color = color == blinkOnColor ? blinkOffColor : blinkOnColor;
                    Invalidate();
                }, null, 0, blinkInterval);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            /// 20240806_
            /// region 설정 및 출력 시 동작 및 출력 불안정
            /// 왜 그럴까? 
            Graphics g = e.Graphics;
            g.Clear(Parent.BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectF;

            GraphicsPath path = new GraphicsPath();

            if (shape == ShapeType.Ellipse)
            {
                int diameter = Math.Min(Size.Width, Size.Height);

                rectF = new Rectangle((Width - diameter) / 2 + PADDING, (Height - diameter) / 2 + PADDING, diameter - PADDING * 2, diameter - PADDING * 2);

                path.AddEllipse(rectF);
            }
            else
            {
                rectF = new Rectangle(PADDING, PADDING, Size.Width - PADDING * 2, Size.Height - PADDING * 2);

                path.AddRectangle(rectF);
            }
            
            Blend blender = new Blend(2);
            blender.Factors = new[] { 1.0f, 1.0f };
            blender.Positions = new[] { 0.0f, 1.0f };

            PathGradientBrush pathBrush = new PathGradientBrush(path)
            {
                CenterColor = Color.FromArgb(opacity, Color),
                SurroundColors = [Color],
            };

            if (shape == ShapeType.Ellipse)
            {
                g.FillEllipse(pathBrush, rectF);

                if (borderWidth > 0)
                {
                    g.DrawEllipse(new Pen(borderColor, borderWidth), rectF);
                }
            }
            else
            {
                g.FillRectangle(pathBrush, rectF);

                if (borderWidth > 0)
                {
                    g.DrawRectangle(new Pen(borderColor, borderWidth), rectF);
                }
            }

            if (!string.IsNullOrEmpty(Text))
            {
                SizeF size = g.MeasureString(Text, Font);
                g.DrawString(Text, Font, new SolidBrush(ForeColor), new PointF((Size.Width - size.Width) / 2 + 1, (Size.Height - size.Height) / 2));
            }
        }
    }
}
