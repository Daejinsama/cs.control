namespace com.outlook_styner07.cs.control.Gauge
{
    public class DjsmLifeTimeGauge : UserControl
    {
        #region Constructors
        #endregion

        #region Types
        #endregion

        #region Fields
        private const int FIXED_HEIGHT = 35;
        private const int MINIMUM = 0;
        private const int MAXIMUM = 100;

        private const int DEFAULT_MARGIN = 3;
        private const int H_MARGIN = 5;

        private const float LINE_WIDTH = 2f;

        private readonly Font LABEL_FONT = new Font("Arial", 7f, FontStyle.Bold);
        private readonly Color RED = Color.FromArgb(192, 222, 44, 40);
        private readonly Color ORANGE = Color.FromArgb(192, 246, 126, 4);
        private readonly Color YELLOW = Color.FromArgb(192, 255, 214, 21);
        private readonly Color GREEN = Color.FromArgb(192, 62, 187, 69);

        private int _value = 0;
        #endregion

        #region Properties
        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Methods
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = FIXED_HEIGHT;
            Invalidate();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            Height = FIXED_HEIGHT;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Graphics g = e.Graphics;

            int width = Width - (H_MARGIN * 2);
            /// draw back ground
            int blockWidth = (int)(width / 4d);
            int blockHeight = (int)(Height / 4d);

            using (var b = new SolidBrush(RED))
            {
                g.FillRectangle(b, new RectangleF(H_MARGIN, blockHeight + DEFAULT_MARGIN, blockWidth, blockHeight));
            }

            using (var b = new SolidBrush(ORANGE))
            {
                g.FillRectangle(b, new RectangleF(blockWidth + H_MARGIN, blockHeight + DEFAULT_MARGIN, blockWidth, blockHeight));
            }

            using (var b = new SolidBrush(YELLOW))
            {
                g.FillRectangle(b, new RectangleF(blockWidth * 2 + H_MARGIN, blockHeight + DEFAULT_MARGIN, blockWidth, blockHeight));
            }

            using (var b = new SolidBrush(GREEN))
            {
                g.FillRectangle(b, new RectangleF(blockWidth * 3 + H_MARGIN, blockHeight + DEFAULT_MARGIN, blockWidth, blockHeight));
            }

            /// draw lines
            int lineHeight = Height / 3;
            using (Pen defaultLine = new Pen(Brushes.Black, LINE_WIDTH))
            {
                g.DrawLine(defaultLine, new Point(H_MARGIN, lineHeight), new Point(width + H_MARGIN, lineHeight));    /// center h line

                g.DrawLine(defaultLine, new Point(H_MARGIN, lineHeight), new Point(H_MARGIN, lineHeight * 2)); /// split left v line
                
                g.DrawLine(defaultLine, new Point(blockWidth + H_MARGIN, lineHeight), new Point(blockWidth + H_MARGIN, lineHeight * 2)); /// split right v line
                
                g.DrawLine(defaultLine, new Point(blockWidth * 2 + H_MARGIN, lineHeight), new Point(blockWidth * 2 + H_MARGIN, lineHeight * 2)); /// split center v line
                
                g.DrawLine(defaultLine, new Point(blockWidth * 3 + H_MARGIN, lineHeight), new Point(blockWidth * 3 + H_MARGIN, lineHeight * 2)); /// split center v line
                
                g.DrawLine(defaultLine, new Point(width + H_MARGIN, lineHeight), new Point(width + H_MARGIN, lineHeight * 2)); /// split center v line
            }

            SizeF size1 = g.MeasureString(MINIMUM.ToString(), LABEL_FONT);
            g.DrawString(MINIMUM.ToString(), LABEL_FONT, Brushes.Black, new Point(0, (int)(Height - size1.Height)));

            SizeF size2 = g.MeasureString(MAXIMUM.ToString(), LABEL_FONT);
            g.DrawString(MAXIMUM.ToString(), LABEL_FONT, Brushes.Black, new Point((int)(Width - size2.Width), (int)(Height - size2.Height)));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int width = Width - (H_MARGIN * 2);
            /// draw gauge
            Color gaugeColor = Color.SkyBlue;
            //Color gaugeColor = Color.Green;
            //if (_value <= 25)
            //{
            //    gaugeColor = Color.Red;
            //}
            //else if (_value <= 50)
            //{
            //    gaugeColor = Color.Orange;
            //}
            //else if (_value <= 75)
            //{
            //    gaugeColor = Color.Blue;
            //}
            int gaugeHeight = Height / 3 - DEFAULT_MARGIN - 1;
            using (var b = new SolidBrush(gaugeColor))
            {
                g.FillRectangle(b, new RectangleF(H_MARGIN - 1, DEFAULT_MARGIN, (float)((width + 1) / 100d * _value), gaugeHeight));
            }
            
            using (var p = new Pen(Brushes.DimGray))
            {
                g.DrawRectangle(p, new Rectangle(H_MARGIN - 1, DEFAULT_MARGIN, width + 1, gaugeHeight));
            }
        }
        #endregion
    }
}
