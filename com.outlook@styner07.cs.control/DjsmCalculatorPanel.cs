using System.Drawing.Drawing2D;

namespace com.outlook_styner07.cs.control
{
    public partial class DjsmCalculatorPanel : UserControl
    {
        public DjsmCalculatorPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            InitializeComponent();
        }

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

        private Rectangle[,] cells = new Rectangle[ROW_COUNT, COLUMN_COUNT];

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font f = new Font("Arial", 11f, FontStyle.Bold);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.FillRectangle(new SolidBrush(SystemColors.Control), ClientRectangle);

            int cellWidth = (Width - (COLUMN_COUNT) * GAP) / COLUMN_COUNT;
            int cellHeight = (Height - (ROW_COUNT) * GAP) / ROW_COUNT;

            Pen borderPen = new Pen(Brushes.DimGray) { Width = 1 };

            for (int r = 0; r < ROW_COUNT; r++)
            {
                for (int c = 0; c < COLUMN_COUNT; c++)
                {
                    string padValue = CHAR_SET[r, c];
                    Size charSize = TextRenderer.MeasureText(padValue, f);

                    cells[r, c] = new Rectangle(
                        c * cellWidth + GAP * c,
                        r * cellHeight + GAP * r,
                        cellWidth, cellHeight);

                    int charPosX = cells[r, c].X + (cells[r, c].Width - charSize.Width) / 2;
                    int charPosY = cells[r, c].Y + (cells[r, c].Height - charSize.Height) / 2;

                    Color tempBackColor = BackColor;
                    Color tempForeColor = ForeColor;

                    if (!string.IsNullOrEmpty(padValue) && isPressed && cells[r, c].Contains(currentPosition))
                    {
                        tempBackColor = ForeColor;
                        tempForeColor = BackColor;

                        KeyPadClick?.Invoke(null, new KeyPadEventArgs { Value = padValue });
                    }

                    g.FillRectangle(new SolidBrush(tempBackColor), cells[r, c]);
                    g.DrawRectangle(borderPen, cells[r, c]);

                    if (padValue.Equals(BACKSPACE))
                    {
                        charSize.Width = charSize.Width / 2;
                        charSize.Height = charSize.Height / 2;

                        DrawBackspace(g, charPosX + (charSize.Width / 2), charPosY + (charSize.Height / 2), charSize, tempForeColor);
                    }
                    else
                    {
                        TextRenderer.DrawText(g, padValue, f, new Point(charPosX, charPosY), tempForeColor);
                    }
                }
            }

            base.OnPaint(e);
        }

        private void DrawBackspace(Graphics g, float x, float y, SizeF size, Color color)
        {
            Pen p = new Pen(new SolidBrush(color)) { Width = size.Height / 6 };

            GraphicsPath borderPath = new GraphicsPath();
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

        private bool isPressed = false;
        private Point currentPosition;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isPressed = true;
            currentPosition = e.Location;

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isPressed = false;

            Invalidate();
        }

        public event EventHandler<KeyPadEventArgs> KeyPadClick;
        public class KeyPadEventArgs : EventArgs
        {
            public string Value { get; set; }
        }
    }
}
