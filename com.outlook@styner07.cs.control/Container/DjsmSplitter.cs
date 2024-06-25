using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmSplitter : Splitter
    {
        private Color _splitterColor = DjsmColorTable.Secondary;
        public Color SplitterColor { get { return _splitterColor; } set { _splitterColor = value; Invalidate(); } }

        private Color _splitterBorderColor = Color.White;
        public Color SplitterBorderColor { get { return _splitterBorderColor; } set { _splitterBorderColor = value; Invalidate(); } }

        private Color _splitterHandleColor = Color.White;
        public Color SplitterHandleColor { get { return _splitterHandleColor; } set { _splitterHandleColor = value; Invalidate(); } }

        public DjsmSplitter()
        {
            DoubleBuffered = true;
            BackColor = Color.FromArgb(0xA7, 0xA9, 0xAC);
            BorderStyle = BorderStyle.None;
        }

        [Browsable(false)]
        public new Color BackColor { get; set; }

        [Browsable(false)]
        public new BorderStyle BorderStyle { get; set; }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        private const int DEFAULT_BAR_WIDTH = 8;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.FillRectangle(new SolidBrush(SplitterColor), ClientRectangle);
            Brush handleBrush = new SolidBrush(SplitterHandleColor);
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
            // 
            // NntSplitter
            // 
            this.Margin = new System.Windows.Forms.Padding(0);
            this.ResumeLayout(false);
        }
    }
}
