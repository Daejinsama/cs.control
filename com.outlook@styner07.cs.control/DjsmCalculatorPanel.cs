using System.Drawing.Drawing2D;

namespace com.outlook_styner07.cs.control
{
    public partial class DjsmCalculatorPanel : UserControl
    {
        #region Constructors
        public DjsmCalculatorPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            InitializeComponent();
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        public const string BACKSPACE = "BS";
        //public const string NEGATE = "±";
        public const string PLUS = "+";
        public const string MINUS = "-";
        public const string MULTIPLY = "*";
        public const string DIVIDE = "/";
        public const string CLEAR = "C";
        public const string LB = "(";
        public const string RB = ")";
        public const string DOT = ".";
        private const int COLUMN_COUNT = 4;
        private const int ROW_COUNT = 5;

        private const int GAP = 3;

        private readonly string[,] CHAR_SET = {
            { LB, RB, CLEAR, BACKSPACE},
            { "7", "8", "9", DIVIDE },
            { "4", "5", "6", MULTIPLY},
            { "1", "2", "3", MINUS },
            { "", "0", DOT, PLUS }
        };

        private Rectangle[,] _cells = new Rectangle[ROW_COUNT, COLUMN_COUNT];

        private bool _isPressed = false;
        private Point _currentPosition;

        public event EventHandler<KeyPadEventArgs> KeyPadClick;
        #endregion

        #region Properties
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font f = new Font("Arial", 11f, FontStyle.Bold);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.FillRectangle(new SolidBrush(SystemColors.Control), ClientRectangle);

            int cellWidth = (Width - (COLUMN_COUNT) * GAP) / COLUMN_COUNT;
            int cellHeight = (Height - (ROW_COUNT) * GAP) / ROW_COUNT;

            for (int r = 0; r < ROW_COUNT; r++)
            {
                for (int c = 0; c < COLUMN_COUNT; c++)
                {
                    string padValue = CHAR_SET[r, c];
                    SizeF charSize = g.MeasureString(padValue, f);

                    _cells[r, c] = new Rectangle(
                        c * cellWidth + GAP * c,
                        r * cellHeight + GAP * r,
                        cellWidth, cellHeight);

                    float charPosX = _cells[r, c].X + (_cells[r, c].Width - charSize.Width) / 2;
                    float charPosY = _cells[r, c].Y + (_cells[r, c].Height - charSize.Height) / 2;

                    Color tempBackColor = BackColor;
                    Color tempForeColor = ForeColor;

                    if (!string.IsNullOrEmpty(padValue) && _isPressed && _cells[r, c].Contains(_currentPosition))
                    {
                        tempBackColor = ForeColor;
                        tempForeColor = BackColor;

                        KeyPadClick?.Invoke(null, new KeyPadEventArgs { Value = padValue });
                    }

                    using (var backBrush = new SolidBrush(tempBackColor))
                    {
                        g.FillRectangle(backBrush, _cells[r, c]);
                    }

                    using (Pen borderPen = new Pen(Brushes.DimGray, 1))
                    {
                        g.DrawRectangle(borderPen, _cells[r, c]);
                    }

                    if (padValue.Equals(BACKSPACE))
                    {
                        charSize.Width = charSize.Width / 2;
                        charSize.Height = charSize.Height / 2;

                        DrawBackspace(g, charPosX + (charSize.Width / 2), charPosY + (charSize.Height / 2), charSize, tempForeColor);
                    }
                    else
                    {
                        using (var foreBrush = new SolidBrush(tempForeColor))
                        {
                            g.DrawString(padValue, f, foreBrush, new PointF(charPosX, charPosY));
                        }
                    }
                }
            }

            base.OnPaint(e);
        }

        private void DrawBackspace(Graphics g, float x, float y, SizeF size, Color color)
        {
            using Pen p = new Pen(new SolidBrush(color), size.Height / 6);

            using GraphicsPath borderPath = new GraphicsPath();
            borderPath.AddLines(new PointF[] {
                new PointF(x, y + size.Height/2),
                new PointF(x + (size.Width / 3), y),
                new PointF(x + size.Width, y),
                new PointF(x + size.Width, y + size.Height),
                new PointF(x + (size.Width / 3), y + size.Height),
            });

            borderPath.CloseAllFigures();

            g.DrawPath(p, borderPath);

            g.DrawLine(p, new PointF(x + (size.Width / 5) * 2, y + (size.Height / 5)), new PointF(x + (size.Width / 5) * 4, y + (size.Height / 5) * 4));
            g.DrawLine(p, new PointF(x + (size.Width / 5) * 4, y + (size.Height / 5)), new PointF(x + (size.Width / 5) * 2, y + (size.Height / 5) * 4));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _isPressed = true;
            _currentPosition = e.Location;

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isPressed = false;

            Invalidate();
        }

        public class KeyPadEventArgs : EventArgs
        {
            public string Value { get; set; } = string.Empty;
        }
        #endregion
    }
}
