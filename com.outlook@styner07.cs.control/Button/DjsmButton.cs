using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace com.outlook_styner07.cs.control.Button
{
    public class DjsmButton : ButtonBase
    {
        #region Constructors
        public DjsmButton()
        {
            _radius = Height / 2;
            _state = ButtonState.Normal;
        }
        #endregion

        #region Types
        private enum ButtonState
        {
            Normal,
            Pressed,
            MouseOver,
            Disabled
        }
        #endregion

        #region Fields
        private Color _pressedBackColor;
        private Color _mouseOverBackColor;
        private Color _disabledBackColor;

        private int _radius;
        private TextRenderingHint _textRenderingHint = TextRenderingHint.AntiAlias;
        private SmoothingMode _smoothMode = SmoothingMode.AntiAlias;
        private ButtonState _state;

        #endregion

        #region Properties
        [Browsable(true)]
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
        #endregion

        #region Methods
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

            using GraphicsPath path = DrawingUtil.GetRoundRectPath(drawingArea, _radius);

            Color brushColor = _state switch
            {
                ButtonState.Normal => BackColor,
                ButtonState.MouseOver => _mouseOverBackColor,
                ButtonState.Pressed => _pressedBackColor,
                ButtonState.Disabled => _disabledBackColor,
                _ => BackColor
            };

            using (var b = new SolidBrush(brushColor))
            {
                g.FillPath(b, path);
            }

            using (var b = new SolidBrush(ForeColor))
            {
                g.DrawString(Text, Font, b, drawingArea, DrawingUtil.ConvertStringAlign(TextAlign));
            }

            if (Image != null)
            {
                g.DrawImage(Image, (ClientRectangle.Width - Image.Width) / 2, (ClientRectangle.Height - Image.Height) / 2);
            }
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);

            _mouseOverBackColor = GetHoverColor(BackColor);
            _pressedBackColor = GetPressedColor(BackColor);
            _disabledBackColor = GetDisabledColor(BackColor);
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
            _state = ButtonState.MouseOver;
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
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _state = Enabled ? ButtonState.Normal : ButtonState.Disabled;
            Invalidate();
        }

        private static int Clamp(float value)
        {
            return Math.Min(255, Math.Max(0, (int)value));
        }

        private static Color AdjustBrightness(Color color, float factor)
        {
            return Color.FromArgb(
                color.A,
                Clamp(color.R * factor),
                Clamp(color.G * factor),
                Clamp(color.B * factor)
            );
        }

        // 1. 체감 밝기 (Luminance) 계산 (0.0 ~ 1.0)
        private static double GetLuminance(Color color)
        {
            return (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255.0;
        }

        // 2. 마우스 오버 색상 (밝은 색은 어둡게, 어두운 색은 밝게)
        public static Color GetHoverColor(Color baseColor)
        {
            double luminance = GetLuminance(baseColor);

            if (luminance > 0.7)
            {
                // 배경이 이미 너무 밝으면 검은색을 15% 섞어 살짝 어둡게
                return BlendColor(baseColor, Color.Black, 0.15);
            }
            else
            {
                // 일반적이거나 어두운 배경이면 흰색을 20% 섞어 화사하게
                return BlendColor(baseColor, Color.White, 0.20);
            }
        }

        // 3. 클릭(Pressed) 색상 (확실히 눌린 느낌을 위해 검은색 25% 섞음)
        public static Color GetPressedColor(Color baseColor)
        {
            return BlendColor(baseColor, Color.Black, 0.25);
        }

        // 4. 비활성화(Disabled) 색상 (흑백 변환 후 밝게)
        public static Color GetDisabledColor(Color baseColor)
        {
            // RGB 값을 이용해 흑백(Grayscale) 명도 추출
            int gray = (int)(baseColor.R * 0.299 + baseColor.G * 0.587 + baseColor.B * 0.114);
            Color grayscale = Color.FromArgb(baseColor.A, gray, gray, gray);

            // 너무 칙칙해지지 않도록 흰색을 40% 정도 섞어 밝은 회색 톤으로 만듦
            return BlendColor(grayscale, Color.White, 0.4);
        }

        // 핵심 헬퍼: 두 색상을 지정한 비율(amount: 0.0 ~ 1.0)로 섞어주는 메서드
        private static Color BlendColor(Color baseColor, Color targetColor, double amount)
        {
            if (amount < 0.0)
            {
                amount = 0.0;
            }

            if (amount > 1.0)
            {
                amount = 1.0;
            }

            int r = (int)(baseColor.R * (1 - amount) + targetColor.R * amount);
            int g = (int)(baseColor.G * (1 - amount) + targetColor.G * amount);
            int b = (int)(baseColor.B * (1 - amount) + targetColor.B * amount);

            return Color.FromArgb(baseColor.A, r, g, b);
        }
        #endregion
    }
}
