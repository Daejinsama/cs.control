using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace com.outlook_styner07.cs.control.Button
{
    public class DjsmToggleButton : System.Windows.Forms.Control
    {
        private ToggleState _state = ToggleState.Left;

        public enum ToggleState { Left, Right }

        public ToggleState State
        {
            get { return _state; }
            set
            {
                _state = value;
                CheckedChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }

        public int BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                _borderWidth = value;
                Invalidate();
            }
        }

        private int _borderWidth = 1;

        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        private Color _borderColor = Color.Black;

        public Color SwitchColor
        {
            get { return _switchColor; }
            set
            {
                _switchColor = value;
                Invalidate();
            }
        }

        private Color _switchColor = Color.Orange;

        public Color BaseColor
        {
            get { return _baseColor; }
            set
            {
                _baseColor = value;
                Invalidate();
            }
        }

        private Color _baseColor = Color.DimGray;

        [Browsable(false)]
        public new Color BackColor { get; set; } = Color.Transparent;

        protected override void OnClick(EventArgs e)
        {
            State = State == ToggleState.Left ? ToggleState.Right : ToggleState.Left;
        }

        public event EventHandler CheckedChanged;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

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

                GraphicsPath path = new GraphicsPath();
                path.AddArc(leftCircleBoundary, 180, 90);
                path.AddLine(new Point(leftCircleBoundary.X + leftCircleBoundary.Width / 2, leftCircleBoundary.Y), new Point(rightCircleBoundary.X + rightCircleBoundary.Width / 2 /*+ 1*/, rightCircleBoundary.Y));
                path.AddArc(rightCircleBoundary, 270, 90);
                path.AddArc(rightCircleBoundary, 0, 90);
                path.AddLine(new Point(rightCircleBoundary.X + rightCircleBoundary.Width / 2 /*+ 1*/, rightCircleBoundary.Y + rightCircleBoundary.Height),
                    new Point(leftCircleBoundary.X + leftCircleBoundary.Width / 2, leftCircleBoundary.Y + leftCircleBoundary.Height));
                path.AddArc(leftCircleBoundary, 90, 90);

                g.FillPath(new SolidBrush(_baseColor), path);
                g.DrawPath(p, path);

                leftCircleBoundary.X += _borderWidth;
                leftCircleBoundary.Y += _borderWidth;
                leftCircleBoundary.Width -= _borderWidth * 2;
                leftCircleBoundary.Height -= _borderWidth * 2;

                rightCircleBoundary.X += _borderWidth;
                rightCircleBoundary.Y += _borderWidth;
                rightCircleBoundary.Width -= _borderWidth * 2;
                rightCircleBoundary.Height -= _borderWidth * 2;

                g.FillEllipse(new SolidBrush(_switchColor), _state == ToggleState.Left ? leftCircleBoundary : rightCircleBoundary);
                g.DrawEllipse(p, _state == ToggleState.Left ? leftCircleBoundary : rightCircleBoundary);
            }
        }
    }
}
