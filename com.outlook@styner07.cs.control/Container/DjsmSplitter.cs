using System.ComponentModel;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmSplitter : Splitter
    {
        #region Constructors
        public DjsmSplitter()
        {
            DoubleBuffered = true;
            BackColor = Color.FromArgb(0xA7, 0xA9, 0xAC);
            BorderStyle = BorderStyle.None;
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private const int DEFAULT_BAR_WIDTH = 8;

        private Color _splitterColor = DjsmColorTable.Secondary;
        private Color _splitterBorderColor = Color.White;
        private Color _splitterHandleColor = Color.White;
        #endregion

        #region Properties
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

        [Browsable(false)]
        public new Color BackColor { get; set; }

        [Browsable(false)]
        public new BorderStyle BorderStyle { get; set; }
        #endregion

        #region Methods
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.FillRectangle(new SolidBrush(SplitterColor), ClientRectangle);

            using Brush handleBrush = new SolidBrush(SplitterHandleColor);
            using (Pen p = new Pen(new SolidBrush(SplitterBorderColor), 2))
            {
                if (Dock == DockStyle.Left || Dock == DockStyle.Right)
                {
                    if (Width != DEFAULT_BAR_WIDTH)
                    {
                        Width = DEFAULT_BAR_WIDTH;
                    }

                    g.DrawLine(p, new Point(0, 0), new Point(0, Height));
                    g.DrawLine(p, new Point(Width, 0), new Point(Width, Height));

                    int diameter = Width / 2;
                    g.FillEllipse(handleBrush, new RectangleF(
                        Width / 2 - (diameter / 2), Height / 2 - Width, diameter, diameter));
                    g.FillEllipse(handleBrush, new RectangleF(
                        Width / 2 - (diameter / 2), Height / 2 - (diameter / 2), diameter, diameter));
                    g.FillEllipse(handleBrush, new RectangleF(
                        Width / 2 - (diameter / 2), Height / 2 + (Width / 2), diameter, diameter));
                }
                else
                {
                    if (Height != DEFAULT_BAR_WIDTH)
                    {
                        Height = DEFAULT_BAR_WIDTH;
                    }

                    g.DrawLine(p, new Point(0, 0), new Point(Width, 0));
                    g.DrawLine(p, new Point(0, Height), new Point(Width, Height));

                    int diameter = Height / 2;
                    g.FillEllipse(handleBrush, new RectangleF(
                        Width / 2 - Height, Height / 2 - (diameter / 2), diameter, diameter));
                    g.FillEllipse(handleBrush, new RectangleF(
                        Width / 2 - (diameter / 2), Height / 2 - (diameter / 2), diameter, diameter));
                    g.FillEllipse(handleBrush, new RectangleF(
                        Width / 2 + (Height / 2), Height / 2 - (diameter / 2), diameter, diameter));
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Margin = new System.Windows.Forms.Padding(0);
            this.ResumeLayout(false);
        }
        #endregion
    }
}
