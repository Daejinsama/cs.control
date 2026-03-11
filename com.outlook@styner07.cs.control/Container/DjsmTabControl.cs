using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmTabControl : TabControl
    {
        #region Constructors
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
        #endregion

        #region Types
        public struct RECT
        {
            public int Left, Top, Right, Bottom;
        }
        #endregion

        #region Fields
        private const string CATEGORY_DESIGN = "Design";
        private const int DEFAULT_GAP = 5;
        private readonly int TCM_ADJUSTRECT = (0x1300 + 40);

        private Bitmap _bmpExpand;
        private Bitmap _bmpClose;

        private Rectangle _rectExpand;
        private Rectangle _rectClose;

        private bool _pressExpand;
        private bool _pressClose;

        private bool _drawBorder = false;

        private int _borderWidth = 0;

        private Color _borderColor = Color.FromArgb(255, 255, 255);
        private Color _selectedTabBackColor = DjsmColorTable.Primary;
        private Color _deselectedTabBackColor = DjsmColorTable.SecondaryLight;
        private Color _selectedTabForeColor = Color.White;
        private Color _deselectedTabForeColor = Color.Black;

        private bool _showTabButton = true;
        private bool _drawTabStrip;

        public event EventHandler<TabButtonEventArgs> ExpandClick;
        public event EventHandler<TabButtonEventArgs> CloseClick;
        #endregion

        #region Properties
        [Category(CATEGORY_DESIGN), Description()]
        public bool DrawBorder
        {
            get
            {
                return _drawBorder;
            }
            set
            {
                if (_drawBorder != value)
                {
                    _drawBorder = value;
                    Invalidate();
                }
            }
        }

        [Category(CATEGORY_DESIGN), Description()]
        public int BorderWidth
        {
            get
            {
                return _borderWidth;
            }
            set
            {
                if (_borderWidth != value)
                {
                    _borderWidth = value;
                    Invalidate();
                }
            }
        }

        [Category(CATEGORY_DESIGN), Description()]
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
                    Invalidate();
                }
            }
        }

        [Category(CATEGORY_DESIGN), Description()]
        public bool ShowTabButton
        {
            get
            {
                return _showTabButton;
            }
            set
            {
                if (_showTabButton != value)
                {
                    _showTabButton = value;
                    Invalidate();
                }
            }
        }

        [Category(CATEGORY_DESIGN), DefaultValue(true)]
        public bool DrawTabStrip
        {
            get
            {
                return _drawTabStrip;
            }
            set
            {
                if (_drawTabStrip != value)
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
        }

        public Color SelectedTabBackColor
        {
            get
            {
                return _selectedTabBackColor;
            }
            set
            {
                if (_selectedTabBackColor != value)
                {
                    _selectedTabBackColor = value;
                    Invalidate();
                }
            }
        }

        public Color DeselectedTabBackColor
        {
            get
            {
                return _deselectedTabBackColor;
            }
            set
            {
                if (_deselectedTabBackColor != value)
                {
                    _deselectedTabBackColor = value;
                    Invalidate();
                }
            }
        }

        public Color SelectedTabForeColor
        {
            get
            {
                return _selectedTabForeColor;
            }
            set
            {
                if (_selectedTabForeColor != value)
                {
                    _selectedTabForeColor = value;
                    Invalidate();
                }
            }
        }

        public Color DeselectedTabForeColor
        {
            get
            {
                return _deselectedTabForeColor;
            }
            set
            {
                if (_deselectedTabForeColor != value)
                {
                    _deselectedTabForeColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Methods
        private void InitializeToolBoxes()
        {
            _bmpClose = Properties.Resources.Close;
            _rectClose = new Rectangle();

            _bmpExpand = Properties.Resources.FullScreen;
            _rectExpand = new Rectangle();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (_rectExpand.Contains(PointToClient(Cursor.Position)))
            {
                _pressExpand = true;
                Invalidate(_rectExpand);
            }

            if (_rectClose.Contains(PointToClient(Cursor.Position)))
            {
                _pressClose = true;
                Invalidate(_rectClose);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_pressExpand && _rectExpand.Contains(new Point(e.X, e.Y)))
            {
                ExpandClick?.Invoke(this, new TabButtonEventArgs { Index = ((DjsmTabPage)TabPages[SelectedIndex]).Index });
            }
            else if (_pressClose && _rectClose.Contains(new Point(e.X, e.Y)))
            {
                CloseClick?.Invoke(this, new TabButtonEventArgs { Index = ((DjsmTabPage)TabPages[SelectedIndex]).Index });
                TabPages.RemoveAt(SelectedIndex);
            }

            _pressExpand = false;
            _pressClose = false;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DrawTabStrip)
            {
                for (int il = TabPages.Count, i = 0; i < il; i++)
                {
                    DrawTab(e.Graphics, i);
                }
            }

            if (DrawBorder)
            {
                using (Pen p = new Pen(BorderColor))
                {
                    Rectangle borderRect = ClientRectangle;
                    borderRect.Width = borderRect.Width - 1;
                    borderRect.Height = borderRect.Height - 1;
                    p.Width = BorderWidth;
                    e.Graphics.DrawRectangle(p, borderRect);
                }
            }
        }

        private void DrawTab(Graphics g, int index)
        {
            string text = TabPages[index].Text;

            Rectangle tabRect = GetTabRect(index);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            bool isSelectedTab = SelectedIndex == index;
            Font tabFont = isSelectedTab
                  ? new Font(Font.Name, Font.Size, FontStyle.Bold)
                  : new Font(Font.Name, Font.Size, FontStyle.Regular);

            SizeF textSize = g.MeasureString(text, tabFont);

            using (var b = new SolidBrush(isSelectedTab ? SelectedTabBackColor : DeselectedTabBackColor))
            {
                g.FillRectangle(b, tabRect);
            }

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

                    using (var b = new SolidBrush(isSelectedTab ? SelectedTabForeColor : DeselectedTabForeColor))
                    {
                        g.DrawString(TabPages[index].Text, tabFont, b, drawingPoint);
                    }

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

                using (var b = new SolidBrush(isSelectedTab ? SelectedTabForeColor : DeselectedTabForeColor))
                {
                    g.DrawString(TabPages[index].Text, tabFont, b, new Point(rect.X, rect.Y));
                }

                if (isSelectedTab && ShowTabButton)
                {
                    _rectExpand.X =
                        _pressExpand
                        ? (tabRect.X + tabRect.Width) - (_bmpExpand.Width * 2 + DEFAULT_GAP * 2) + 1
                        : (tabRect.X + tabRect.Width) - (_bmpExpand.Width * 2 + DEFAULT_GAP * 2);
                    _rectExpand.Y =
                        _pressExpand
                        ? tabRect.Y + (tabRect.Height - _bmpExpand.Height) / 2 + 1
                        : tabRect.Y + (tabRect.Height - _bmpExpand.Height) / 2;
                    _rectExpand.Width = _bmpExpand.Width;
                    _rectExpand.Height = _bmpExpand.Height;

                    _rectClose.X =
                        _pressClose
                        ? (tabRect.X + tabRect.Width) - (_bmpClose.Width + DEFAULT_GAP) + 1
                        : (tabRect.X + tabRect.Width) - (_bmpClose.Width + DEFAULT_GAP);
                    _rectClose.Y =
                        _pressClose
                        ? tabRect.Y + (tabRect.Height - _bmpClose.Height) / 2 + 1
                        : tabRect.Y + (tabRect.Height - _bmpClose.Height) / 2;
                    _rectClose.Width = _bmpClose.Width;
                    _rectClose.Height = _bmpClose.Height;

                    g.DrawImage(_bmpExpand, _rectExpand);
                    g.DrawImage(_bmpClose, _rectClose);
                }
            }
        }

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

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.ResumeLayout(false);
        }
        #endregion

        public class TabButtonEventArgs : EventArgs
        {
            public int Index { get; set; }
        }
    }

    public class DjsmTabPage : TabPage
    {
        public int Index { get; set; }
    }
}
