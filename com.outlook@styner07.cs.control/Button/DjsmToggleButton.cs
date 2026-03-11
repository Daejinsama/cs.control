using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace com.outlook_styner07.cs.control.Button
{
    public class DjsmToggleButton : Control
    {
        #region Constructors
        #endregion

        #region Types
        public enum ToggleState
        {
            Left,
            Right
        }
        #endregion

        #region Fields
        private ToggleState _state = ToggleState.Left;
        private int _borderWidth = 1;
        private Color _borderColor = Color.Black;
        private Color _switchColor = Color.Orange;
        private Color _baseColor = Color.DimGray;
        private TextRenderingHint _textRenderingHint = TextRenderingHint.AntiAlias;
        private SmoothingMode _smoothMode = SmoothingMode.AntiAlias;

        public event EventHandler CheckedChanged;
        #endregion

        #region Properties
        public ToggleState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    CheckedChanged?.Invoke(this, EventArgs.Empty);
                    Invalidate();
                }
            }
        }

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

        public Color SwitchColor
        {
            get { return _switchColor; }
            set
            {
                if (_switchColor != value)
                {
                    _switchColor = value;
                    Invalidate();
                }
            }
        }

        public Color BaseColor
        {
            get { return _baseColor; }
            set
            {
                if (_baseColor != value)
                {
                    _baseColor = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public TextRenderingHint RenderingHint
        {
            get { return _textRenderingHint; }
            set
            {
                if (_textRenderingHint != value)
                {
                    _textRenderingHint = value;
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        public SmoothingMode SmoothMode
        {
            get { return _smoothMode; }
            set
            {
                if (_smoothMode != value)
                {
                    _smoothMode = value;
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        public new Color BackColor { get; set; } = Color.Transparent;
        #endregion

        #region Methods
        protected override void OnClick(EventArgs e)
        {
            State = State == ToggleState.Left ? ToggleState.Right : ToggleState.Left;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = _smoothMode;
            g.TextRenderingHint = _textRenderingHint;

            Rectangle controlBoundary = ClientRectangle;
            Rectangle drawingRect = new Rectangle();

            if (controlBoundary.Width < controlBoundary.Height * 2)
            {
                drawingRect.Width = controlBoundary.Width - _borderWidth * 2;
                drawingRect.Height = controlBoundary.Width / 2 - _borderWidth * 2;
            }
            else
            {
                drawingRect.Width = controlBoundary.Height * 2 - _borderWidth * 2;
                drawingRect.Height = controlBoundary.Height - _borderWidth * 2;
            }

            drawingRect.X = (controlBoundary.Width - drawingRect.Width) / 2;
            drawingRect.Y = (controlBoundary.Height - drawingRect.Height) / 2;

            using (Pen p = new Pen(_borderColor, _borderWidth))
            {
                Rectangle leftCircleBoundary = new Rectangle(drawingRect.X, drawingRect.Y, drawingRect.Height, drawingRect.Height);
                Rectangle rightCircleBoundary = new Rectangle(drawingRect.Height + drawingRect.X, drawingRect.Y, drawingRect.Height, drawingRect.Height);

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(leftCircleBoundary, 180, 90);
                    path.AddLine(new Point(leftCircleBoundary.X + leftCircleBoundary.Width / 2, leftCircleBoundary.Y), new Point(rightCircleBoundary.X + rightCircleBoundary.Width / 2 /*+ 1*/, rightCircleBoundary.Y));
                    path.AddArc(rightCircleBoundary, 270, 90);
                    path.AddArc(rightCircleBoundary, 0, 90);
                    path.AddLine(new Point(rightCircleBoundary.X + rightCircleBoundary.Width / 2 /*+ 1*/, rightCircleBoundary.Y + rightCircleBoundary.Height),
                        new Point(leftCircleBoundary.X + leftCircleBoundary.Width / 2, leftCircleBoundary.Y + leftCircleBoundary.Height));
                    path.AddArc(leftCircleBoundary, 90, 90);

                    g.FillPath(new SolidBrush(_baseColor), path);
                    g.DrawPath(p, path);
                }

                leftCircleBoundary.X += _borderWidth;
                leftCircleBoundary.Y += _borderWidth;
                leftCircleBoundary.Width -= _borderWidth * 2;
                leftCircleBoundary.Height -= _borderWidth * 2;

                rightCircleBoundary.X += _borderWidth;
                rightCircleBoundary.Y += _borderWidth;
                rightCircleBoundary.Width -= _borderWidth * 2;
                rightCircleBoundary.Height -= _borderWidth * 2;

                using var b = new SolidBrush(_switchColor);
                g.FillEllipse(b, _state == ToggleState.Left ? leftCircleBoundary : rightCircleBoundary);
                g.DrawEllipse(p, _state == ToggleState.Left ? leftCircleBoundary : rightCircleBoundary);
            }
            #endregion
        }
    }
}
