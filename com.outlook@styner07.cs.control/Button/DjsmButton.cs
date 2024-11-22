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
            g.Clear(Parent.BackColor);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = _textRenderingHint;

            Rectangle drawingArea = new Rectangle(ClientRectangle.X + Padding.Left,
                ClientRectangle.Y + Padding.Top,
                ClientRectangle.Width - (Padding.Right * 2),
                ClientRectangle.Height - (Padding.Bottom * 2));

            GraphicsPath path = DrawingUtil.GetRoundRectPath(drawingArea, g.MeasureString(Text, Font), _radius);
            
            using (SolidBrush b = new SolidBrush(_state == ButtonState.Normal
                ? BackColor : _state == ButtonState.MouseOver
                ? _mouseOverBackColor : _pressedBackColor))
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
            _mouseOverBackColor = Color.FromArgb(192, BackColor);
            _pressedBackColor = Color.FromArgb(128, BackColor);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            _state = ButtonState.Pressed;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            _state = ButtonState.Normal;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);
            _state = ButtonState.MouseOver;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            base.OnMouseLeave(eventargs);
            _state = ButtonState.Normal;
        }
    }
}
