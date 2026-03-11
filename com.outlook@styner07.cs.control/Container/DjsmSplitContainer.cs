using System.ComponentModel;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmSplitContainer : SplitContainer
    {
        #region Constructors
        public DjsmSplitContainer()
        {
            DoubleBuffered = true;
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private Color _splitterColor = DjsmColorTable.Secondary;
        private Color _splitterBorderColor = Color.White;
        private Color _splitterHandleColor = Color.White;
        private Color _borderColor = DjsmColorTable.SecondaryLight;
        #endregion

        #region Properties
        [Browsable(false)]
        public new int SplitterWidth { get; set; }

        public Color SplitterColor
        {
            get { return _splitterColor; }
            set
            {
                if (_splitterColor != value)
                {
                    _splitterColor = value;
                    Invalidate();
                }
            }
        }

        public Color SplitterBorderColor
        {
            get { return _splitterBorderColor; }
            set
            {
                if (_splitterBorderColor != value)
                {
                    _splitterBorderColor = value;
                    Invalidate();
                }
            }
        }

        public Color SplitterHandleColor
        {
            get { return _splitterHandleColor; }
            set
            {
                if (_splitterHandleColor != value)
                {
                    _splitterHandleColor = value;
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
        #endregion

        #region Methods
        protected override void OnResize(EventArgs e)
        {
            try
            {
                base.OnResize(e);
                Invalidate();
            }
            catch (ArgumentException) { }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            base.SplitterWidth = 8;
            Rectangle rect = SplitterRectangle;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.FillRectangle(new SolidBrush(SplitterColor), SplitterRectangle);

            using (Pen p = new Pen(new SolidBrush(SplitterBorderColor), 2))
            {
                using (SolidBrush b = new SolidBrush(SplitterHandleColor))
                {
                    if (Orientation == Orientation.Vertical)
                    {
                        g.DrawLine(p, new Point(rect.X, rect.Y), new Point(rect.X, rect.Y + rect.Height));
                        g.DrawLine(p, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));

                        int diameter = rect.Width / 2;
                        g.FillEllipse(b, new RectangleF(
                            rect.X + rect.Width / 2 - (diameter / 2),
                            rect.Y + rect.Height / 2 - rect.Width,
                            diameter, diameter));
                        g.FillEllipse(b, new RectangleF(
                            rect.X + rect.Width / 2 - (diameter / 2),
                            rect.Y + rect.Height / 2 - (diameter / 2),
                            diameter, diameter));
                        g.FillEllipse(b, new RectangleF(
                            rect.X + rect.Width / 2 - (diameter / 2),
                            rect.Y + rect.Height / 2 + (rect.Width / 2),
                            diameter, diameter));
                    }
                    else
                    {
                        g.DrawLine(p, new Point(rect.X, rect.Y), new Point(rect.X + rect.Width, rect.Y));
                        g.DrawLine(p, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));

                        int diameter = rect.Height / 2;
                        g.FillEllipse(b, new RectangleF(
                            rect.X + rect.Width / 2 - rect.Height,
                            rect.Y + rect.Height / 2 - (diameter / 2),
                            diameter, diameter));
                        g.FillEllipse(b, new RectangleF(
                            rect.X + rect.Width / 2 - (diameter / 2),
                            rect.Y + rect.Height / 2 - (diameter / 2),
                            diameter, diameter));
                        g.FillEllipse(b, new RectangleF(
                            rect.X + rect.Width / 2 + (rect.Height / 2),
                            rect.Y + rect.Height / 2 - (diameter / 2),
                            diameter, diameter));
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();

            this.Font = new System.Drawing.Font("Arial", 9F);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
    }
}
