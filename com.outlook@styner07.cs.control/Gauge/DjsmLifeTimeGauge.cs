using System;
using System.Drawing;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control.Gauge
{
    public class DjsmLifeTimeGauge : UserControl
    {
        private int _value = 0;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Invalidate();
            }
        }

        private const int FIXED_HEIGHT = 35;

        private Font LABEL_FONT = new Font("Arial", 7f, FontStyle.Bold);
        private const int MINIMUM = 0;
        private const int MAXIMUM = 100;

        private const int DEFAULT_MARGIN = 3;
        private const int H_MARGIN = 5;

        private Color RED = Color.FromArgb(192, 222, 44, 40);
        private Color ORANGE = Color.FromArgb(192, 246, 126, 4);
        private Color YELLOW = Color.FromArgb(192, 255, 214, 21);
        private Color GREEN = Color.FromArgb(192, 62, 187, 69);

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
            const float LINE_WIDTH = 2f;

            int WIDTH = Width - (H_MARGIN * 2);
            /// draw back ground
            int blockWidth = (int)(WIDTH / 4d);
            int blockHeight = (int)(Height / 4d);

            g.FillRectangle(new SolidBrush(RED), new RectangleF(H_MARGIN, blockHeight + DEFAULT_MARGIN, blockWidth, blockHeight));
            g.FillRectangle(new SolidBrush(ORANGE), new RectangleF(blockWidth + H_MARGIN, blockHeight + DEFAULT_MARGIN, blockWidth, blockHeight));
            g.FillRectangle(new SolidBrush(YELLOW), new RectangleF(blockWidth * 2 + H_MARGIN, blockHeight + DEFAULT_MARGIN, blockWidth, blockHeight));
            g.FillRectangle(new SolidBrush(GREEN), new RectangleF(blockWidth * 3 + H_MARGIN, blockHeight + DEFAULT_MARGIN, blockWidth, blockHeight));

            /// draw lines
            int lineHeight = Height / 3;
            Pen defaultLine = new Pen(Brushes.Black, LINE_WIDTH);
            g.DrawLine(defaultLine, new Point(H_MARGIN, lineHeight), new Point(WIDTH + H_MARGIN, lineHeight));    /// center h line

            g.DrawLine(defaultLine, new Point(H_MARGIN, lineHeight), new Point(H_MARGIN, lineHeight * 2)); /// split left v line
            g.DrawLine(defaultLine, new Point(blockWidth + H_MARGIN, lineHeight), new Point(blockWidth + H_MARGIN, lineHeight * 2)); /// split right v line
            g.DrawLine(defaultLine, new Point(blockWidth * 2 + H_MARGIN, lineHeight), new Point(blockWidth * 2 + H_MARGIN, lineHeight * 2)); /// split center v line
            g.DrawLine(defaultLine, new Point(blockWidth * 3 + H_MARGIN, lineHeight), new Point(blockWidth * 3 + H_MARGIN, lineHeight * 2)); /// split center v line
            g.DrawLine(defaultLine, new Point(WIDTH + H_MARGIN, lineHeight), new Point(WIDTH + H_MARGIN, lineHeight * 2)); /// split center v line

            SizeF size1 = g.MeasureString(MINIMUM.ToString(), LABEL_FONT);
            g.DrawString(MINIMUM.ToString(), LABEL_FONT, Brushes.Black, new Point(0, (int)(Height - size1.Height)));

            SizeF size2 = g.MeasureString(MAXIMUM.ToString(), LABEL_FONT);
            g.DrawString(MAXIMUM.ToString(), LABEL_FONT, Brushes.Black, new Point((int)(Width - size2.Width), (int)(Height - size2.Height)));

        }

        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            int WIDTH = Width - (H_MARGIN * 2);
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
            g.FillRectangle(new SolidBrush(gaugeColor), new RectangleF(H_MARGIN - 1, DEFAULT_MARGIN, (float)((WIDTH + 1) / 100d * _value), gaugeHeight));
            g.DrawRectangle(new Pen(Brushes.DimGray), new Rectangle(H_MARGIN - 1, DEFAULT_MARGIN, WIDTH + 1, gaugeHeight));
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LifeTimeGauge
            // 
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Name = "LifeTimeGauge";
            this.ResumeLayout(false);

        }
    }
}
