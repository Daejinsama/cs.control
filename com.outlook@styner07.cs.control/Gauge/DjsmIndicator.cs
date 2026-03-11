using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace com.outlook_styner07.cs.control.Gauge
{
    public class DjsmIndicator : Control
    {
        #region Constructors
        #endregion

        #region Types
        public enum ShapeType
        {
            Ellipse,
            Rectangle
        }
        #endregion

        #region Fields
        private const int PADDING = 1;

        private Color _color = Color.Lime;
        private Color _borderColor = Color.Gray;
        private int _borderWidth = 1;
        private byte _opacity = 10;

        private ShapeType _shape = ShapeType.Ellipse;

        private bool _blink = false;
        private long _blinkInterval = 500;
        private Color _blinkOnColor = Color.Lime;
        private Color _blinkOffColor = Color.Red;

        private System.Threading.Timer? _blinkTimer;

        public event EventHandler? ColorChanged;
        #endregion

        #region Properties
        [Browsable(true)]
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                if (_borderWidth != value)
                {
                    _borderWidth = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    Invalidate();

                    ColorChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [Browsable(true)]
        public byte Opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                if (_opacity != value)
                {
                    _opacity = value;
                    Invalidate();
                }
            }
        }

        public ShapeType Shape
        {
            get { return _shape; }
            set
            {
                if (_shape != value)
                {
                    _shape = value;
                    Invalidate();
                }
            }
        }

        public bool Blink
        {
            get { return _blink; }
            set
            {
                if (_blink != value)
                {
                    _blink = value;
                    SetBlink();
                }
            }
        }

        [Browsable(true)]
        public long BlinkInterval
        {
            get { return _blinkInterval; }
            set
            {
                if (_blinkInterval != value)
                {
                    _blinkInterval = value;
                    SetBlink();
                }
            }
        }

        public Color BlinkOnColor
        {
            get { return _blinkOnColor; }
            set
            {
                if (_blinkOnColor != value)
                {
                    _blinkOnColor = value;
                    SetBlink();
                }
            }
        }

        public Color BlinkOffColor
        {
            get { return _blinkOffColor; }
            set
            {
                if (_blinkOffColor != value)
                {
                    _blinkOffColor = value;
                    SetBlink();
                }
            }
        }

        [Browsable(false)]
        public new Color BackColor { get; } = Color.Transparent;
        #endregion

        #region Methods
        private void SetBlink()
        {
            _blinkTimer?.Dispose();
            _blinkTimer = null;

            if (_blink)
            {
                _blinkTimer = new System.Threading.Timer((obj) =>
                {
                    _color = _color == _blinkOnColor ? _blinkOffColor : _blinkOnColor;
                    Invalidate();
                }, null, 0, _blinkInterval);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(Parent.BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectF;

            using (GraphicsPath path = new GraphicsPath())
            {
                if (_shape == ShapeType.Ellipse)
                {
                    int diameter = Math.Min(Size.Width, Size.Height);

                    rectF = new Rectangle((Width - diameter) / 2 + PADDING, (Height - diameter) / 2 + PADDING, diameter - PADDING * 2, diameter - PADDING * 2);

                    path.AddEllipse(rectF);

                    using (PathGradientBrush pathBrush = new PathGradientBrush(path)
                    {
                        CenterColor = Color.FromArgb(_opacity, Color),
                        SurroundColors = [Color],
                    })
                    {
                        g.FillEllipse(pathBrush, rectF);

                        if (_borderWidth > 0)
                        {
                            g.DrawEllipse(new Pen(_borderColor, _borderWidth), rectF);
                        }
                    }
                }
                else
                {
                    rectF = new Rectangle(PADDING, PADDING, Size.Width - PADDING * 2, Size.Height - PADDING * 2);

                    path.AddRectangle(rectF);

                    using (LinearGradientBrush pathBrush = new LinearGradientBrush(new Point(rectF.X, rectF.Y), new Point(rectF.Width, rectF.Height), Color.FromArgb(_opacity, Color), Color))
                    {
                        g.FillRectangle(pathBrush, rectF);

                        if (_borderWidth > 0)
                        {
                            g.DrawRectangle(new Pen(_borderColor, _borderWidth), rectF);
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(Text))
            {
                SizeF size = g.MeasureString(Text, Font);
                using (var b = new SolidBrush(ForeColor))
                {
                    g.DrawString(Text, Font, b, new PointF((Size.Width - size.Width) / 2 + 1, (Size.Height - size.Height) / 2));
                }
            }
        }
        #endregion
    }
}
