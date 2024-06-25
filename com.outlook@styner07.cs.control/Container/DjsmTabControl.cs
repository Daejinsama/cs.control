using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmTabControl : TabControl
    {
        private const string CATEGORY_DESIGN = "Design";

        private Bitmap bmpExpand, bmpClose;
        private Rectangle rectExpand, rectClose;
        private bool pressExpand, pressClose;

        [Category(CATEGORY_DESIGN), Description()]
        public bool DrawBorder { get; set; } = false;

        [Category(CATEGORY_DESIGN), Description()]
        public int BorderWidth { get; set; } = 0;

        [Category(CATEGORY_DESIGN), Description()]
        public Color BorderColor { get; set; } = Color.FromArgb(255, 255, 255);

        [Category(CATEGORY_DESIGN), Description()]
        public bool ShowTabButton { get; set; }

        [Category(CATEGORY_DESIGN), DefaultValue(true)]
        public bool DrawTabStrip
        {
            get
            {
                return _drawTabStrip;
            }
            set
            {
                _drawTabStrip = value;
                if (_drawTabStrip)
                {
                    ItemSize = new Size(73, 25);
                    SizeMode = TabSizeMode.Normal;
                }
                else
                {
                    ItemSize = new Size(0, 1);
                    SizeMode = TabSizeMode.Fixed;
                }
                Invalidate();
            }
        }
        private bool _drawTabStrip;

        public DjsmTabControl()
        {
            DrawTabStrip = true;

            SizeMode = TabSizeMode.Normal;

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            InitializeToolBoxes();
        }

        private void InitializeToolBoxes()
        {
            bmpClose = Properties.Resources.Close;
            rectClose = new Rectangle();

            bmpExpand = Properties.Resources.FullScreen;
            rectExpand = new Rectangle();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (rectExpand.Contains(PointToClient(Cursor.Position)))
            {
                pressExpand = true;
                Invalidate(rectExpand);
            }
            if (rectClose.Contains(PointToClient(Cursor.Position)))
            {
                pressClose = true;
                Invalidate(rectClose);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (pressExpand && rectExpand.Contains(new Point(e.X, e.Y)))
            {
                ExpandClick?.Invoke(this, new TabButtonEventArgs { Index = (TabPages[SelectedIndex] as NntTabPage).Index });
            }

            else if (pressClose && rectClose.Contains(new Point(e.X, e.Y)))
            {
                CloseClick?.Invoke(this, new TabButtonEventArgs { Index = (TabPages[SelectedIndex] as NntTabPage).Index });
                TabPages.RemoveAt(SelectedIndex);
            }

            pressExpand = false;
            pressClose = false;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DrawTabStrip)
            {
                for (int tabCount = TabPages.Count, i = 0; i < tabCount; i++)
                {
                    DrawTab(e.Graphics, i);
                }
            }

            if (DrawBorder)
            {
                Pen p = new Pen(BorderColor);
                Rectangle borderRect = ClientRectangle;
                borderRect.Width = borderRect.Width - 1;
                borderRect.Height = borderRect.Height - 1;
                p.Width = BorderWidth;
                e.Graphics.DrawRectangle(p, borderRect);
            }
        }

        public Color SelectedTabBackColor { get; set; } = DjsmColorTable.Primary;
        public Color DeselectedTabBackColor { get; set; } = DjsmColorTable.SecondaryLight;
        public Color SelectedTabForeColor { get; set; } = Color.White;
        public Color DeselectedTabForeColor { get; set; } = Color.Black;

        private Font TabFont;

        private const int DEFAULT_GAP = 5;

        private void DrawTab(Graphics g, int index)
        {
            string text = TabPages[index].Text;

            Rectangle tabRect = GetTabRect(index);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            bool isSelectedTab = SelectedIndex == index;
            TabFont = isSelectedTab
                ? new Font(Font.Name, Font.Size, FontStyle.Bold)
                : new Font(Font.Name, Font.Size, FontStyle.Regular);

            SizeF textSize = g.MeasureString(text, TabFont);

            g.FillRectangle(new SolidBrush(isSelectedTab ? SelectedTabBackColor : DeselectedTabBackColor), tabRect);

            //Debug.WriteLine($"x:{tabRect.X}, y:{tabRect.Y}, w:{tabRect.Width}, h:{tabRect.Height}");

            if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
            {
                using (Matrix matrix = new Matrix())
                {
                    Point drawingPoint = new Point(tabRect.X, tabRect.Y);
                    if (Alignment == TabAlignment.Left)
                    {
                        matrix.RotateAt(270, drawingPoint);
                        drawingPoint.X -= tabRect.Height - (tabRect.Height - (int)textSize.Width) / 2;
                        drawingPoint.Y += (tabRect.Width - (int)textSize.Height) / 2;
                    }
                    else
                    {
                        matrix.RotateAt(90, drawingPoint);

                        drawingPoint.X += (tabRect.Height - (int)textSize.Width) / 2;
                        drawingPoint.Y -= tabRect.Width - (tabRect.Width - (int)textSize.Height) / 2;
                    }

                    g.MultiplyTransform(matrix);
                    g.DrawString(TabPages[index].Text, TabFont, new SolidBrush(isSelectedTab ? SelectedTabForeColor : DeselectedTabForeColor), drawingPoint);
                    matrix.Invert();
                    g.MultiplyTransform(matrix);
                }
            }
            else
            {
                Rectangle rect = new Rectangle(
                (int)(ShowTabButton ? tabRect.X + DEFAULT_GAP : tabRect.X + (tabRect.Width - textSize.Width) / 2),
                (int)(tabRect.Y + ((tabRect.Height - textSize.Height) / 2)),
                (int)textSize.Width,
                (int)textSize.Height);

                g.DrawString(TabPages[index].Text, TabFont, new SolidBrush(isSelectedTab ? SelectedTabForeColor : DeselectedTabForeColor), new Point(rect.X, rect.Y));

                if (isSelectedTab && ShowTabButton)
                {
                    rectExpand.X =
                        pressExpand
                        ? (tabRect.X + tabRect.Width) - (bmpExpand.Width * 2 + DEFAULT_GAP * 2) + 1
                        : (tabRect.X + tabRect.Width) - (bmpExpand.Width * 2 + DEFAULT_GAP * 2);
                    rectExpand.Y =
                        pressExpand
                        ? tabRect.Y + (tabRect.Height - bmpExpand.Height) / 2 + 1
                        : tabRect.Y + (tabRect.Height - bmpExpand.Height) / 2;
                    rectExpand.Width = bmpExpand.Width;
                    rectExpand.Height = bmpExpand.Height;

                    rectClose.X =
                        pressClose
                        ? (tabRect.X + tabRect.Width) - (bmpClose.Width + DEFAULT_GAP) + 1
                        : (tabRect.X + tabRect.Width) - (bmpClose.Width + DEFAULT_GAP);
                    rectClose.Y =
                        pressClose
                        ? tabRect.Y + (tabRect.Height - bmpClose.Height) / 2 + 1
                        : tabRect.Y + (tabRect.Height - bmpClose.Height) / 2;
                    rectClose.Width = bmpClose.Width;
                    rectClose.Height = bmpClose.Height;

                    g.DrawImage(bmpExpand, rectExpand);
                    g.DrawImage(bmpClose, rectClose);
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NntTabControl
            // 
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.ResumeLayout(false);

        }

        //↓remove all margin with tab headers
        //private const int TCM_ADJUSTRECT = 0x1328;
        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //    if (m.Msg == TCM_ADJUSTRECT)
        //    {
        //        m.Result = new IntPtr(1);
        //    }
        //}

        #region remove tabcontrol margins
        private readonly int TCM_ADJUSTRECT = (0x1300 + 40);
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == TCM_ADJUSTRECT)
            {
                RECT rc = (RECT)m.GetLParam(typeof(RECT));
                rc.Left -= 7;
                rc.Right += 7;
                rc.Top -= 2;
                rc.Bottom += 7;
                Marshal.StructureToPtr(rc, m.LParam, true);
            }
            base.WndProc(ref m);
        }

        public struct RECT
        {
            public int Left, Top, Right, Bottom;
        }
        #endregion

        public event EventHandler<TabButtonEventArgs> ExpandClick;
        public event EventHandler<TabButtonEventArgs> CloseClick;
        public class TabButtonEventArgs : EventArgs
        {
            public int Index { get; set; }
        }
    }
    public class NntTabPage : TabPage
    {
        public int Index { get; set; }
    }
}
