using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace com.outlook_styner07.cs.control.Gauge
{
    public partial class DjsmIndicator : UserControl
    {
        [Browsable(true)]
        public string Label
        {
            get { return label; }
            set
            {
                label = value;
                Invalidate();
            }
        }

        private string label = string.Empty;

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

        private byte opacity = 128;

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

        [Browsable(true), DisplayName("Blink Interval")]
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

        private const int PADDING = 3;

        public DjsmIndicator()
        {
            InitializeComponent();
        }

        private System.Threading.Timer blinkTimer;

        private void SetBlink()
        {
            if (blink)
            {
                blinkTimer?.Dispose();
                blinkTimer = new System.Threading.Timer((obj) =>
                {
                    color = color == blinkOnColor ? blinkOffColor : blinkOnColor;
                    Invalidate();
                }, null, 0, blinkInterval);
            }
            else
            {
                blinkTimer?.Dispose();
                blinkTimer = null;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectF = new Rectangle(PADDING, PADDING, Size.Width - PADDING * 2, Size.Height - PADDING * 2);
            Rectangle rect = new Rectangle(PADDING, PADDING, Size.Width - PADDING * 2, Size.Height - PADDING * 2);

            GraphicsPath path = new GraphicsPath();

            if (shape == ShapeType.Ellipse)
            {
                path.AddEllipse(rectF);
            }
            else
            {
                path.AddRectangle(rectF);
            }

            PathGradientBrush pathBrush = new PathGradientBrush(path);
            pathBrush.CenterColor = Color.FromArgb(opacity, Color);
            pathBrush.SurroundColors = new Color[] { Color };

            if (shape == ShapeType.Ellipse)
            {
                g.FillEllipse(pathBrush, rectF);
            }
            else
            {
                g.FillRectangle(pathBrush, rectF);
            }

            if (borderWidth > 0)
            {
                if (shape == ShapeType.Ellipse)
                {
                    g.DrawEllipse(new Pen(borderColor, borderWidth), rectF);
                }
                else
                {
                    g.DrawRectangle(new Pen(borderColor, borderWidth), rect);
                }
            }

            if (!string.IsNullOrEmpty(label))
            {
                Size size = TextRenderer.MeasureText(label, Font);
                TextRenderer.DrawText(g, label, Font, new Point((Size.Width - size.Width) / 2 + 1, (Size.Height - size.Height) / 2), ForeColor);
            }
        }
    }
}
