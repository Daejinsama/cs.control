using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace com.outlook_styner07.cs.control.Button
{
    public class DjsmButton : ButtonBase
    {
        private Color _pressedBackColor;
        private Color _mouseOverBackColor;

        [Browsable(true)]
        public int Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                Invalidate();
            }
        }

        private int _radius;

        [Browsable(true)]
        public TextRenderingHint RenderingHint
        {
            get { return _textRenderingHint; }
            set
            {
                _textRenderingHint = value; Invalidate();
            }
        }

        private TextRenderingHint _textRenderingHint = TextRenderingHint.AntiAlias;

        [Browsable(true)]
        public SmoothingMode SmoothMode
        {
            get { return _smoothMode; }
            set
            {
                _smoothMode = value; Invalidate();
            }
        }

        private SmoothingMode _smoothMode = SmoothingMode.AntiAlias;

        private enum ButtonState { Normal, Pressed, MouseOver }

        private ButtonState _state;

        public DjsmButton()
        {
            _radius = Height / 2;
            _state = ButtonState.Normal;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            if (Parent != null && Parent.BackColor != Color.Transparent)
            {
                g.Clear(Parent.BackColor);
            }

            g.SmoothingMode = _smoothMode;
            g.TextRenderingHint = _textRenderingHint;

            Rectangle drawingArea = new Rectangle(ClientRectangle.X + Padding.Left,
                ClientRectangle.Y + Padding.Top,
                ClientRectangle.Width - (Padding.Right * 2),
                ClientRectangle.Height - (Padding.Bottom * 2));

            GraphicsPath path = DrawingUtil.GetRoundRectPath(drawingArea, g.MeasureString(Text, Font), _radius);

            Color brushColor = _state switch
            {
                ButtonState.Normal => BackColor,
                ButtonState.MouseOver => _mouseOverBackColor,
                ButtonState.Pressed => _pressedBackColor,
                _ => BackColor
            };

            using (SolidBrush b = new SolidBrush(brushColor))
            {
                g.FillPath(b, path);
            }

            g.DrawString(Text, Font, new SolidBrush(ForeColor), drawingArea, DrawingUtil.ConvertStringAlign(TextAlign));

            if (Image != null)
            {
                g.DrawImage(Image, (ClientRectangle.Width - Image.Width) / 2, (ClientRectangle.Height - Image.Height) / 2);
            }
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);

            if (BackColor.GetBrightness() < 0.5f)
            {
                _mouseOverBackColor = AdjustBrightness(BackColor, 1.6f);
                _pressedBackColor = AdjustBrightness(BackColor, 1.8f);
            }
            else
            {
                _mouseOverBackColor = AdjustBrightness(BackColor, 0.8f);
                _pressedBackColor = AdjustBrightness(BackColor, 0.6f);
            }
        }

        private static int Clamp(float value) => Math.Min(255, Math.Max(0, (int)value));

        private static Color AdjustBrightness(Color color, float factor)
        {
            return Color.FromArgb(
                color.A,
                Clamp(color.R * factor),
                Clamp(color.G * factor),
                Clamp(color.B * factor)
            );
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            _state = ButtonState.Pressed;
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            _state = ButtonState.MouseOver;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            _state = ButtonState.MouseOver;
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            base.OnMouseLeave(eventargs);
            _state = ButtonState.Normal;
        }
    }
}
