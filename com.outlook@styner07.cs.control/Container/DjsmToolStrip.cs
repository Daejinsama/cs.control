using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control.Container
{
    /// <summary>
    /// 툴스트립이 포함되어 있는 윈도우가 비활성 상태일 때 
    /// 원 클릭으로 툴스트립 아이템의 클릭 이벤트 호출.
    /// https://stackoverflow.com/questions/6947163/activate-a-form-and-process-button-click-at-the-same-time
    /// </summary>
    public class DjsmToolStrip : ToolStrip
    {
        public bool WindowDragEnabled { get; set; } = true;

        [Browsable(true)]
        public Color BorderColor { get { return _borderColor; } set { _borderColor = value; Invalidate(); } }
        private Color _borderColor = DjsmColorTable.SecondaryLight;

        [Browsable(true)]
        public ToolStripStatusLabelBorderSides BorderSides
        {
            get { return _borderSides; }
            set
            {

                _borderSides = value; Invalidate();
            }
        }
        private ToolStripStatusLabelBorderSides _borderSides = ToolStripStatusLabelBorderSides.Bottom;

        public DjsmToolStrip()
        {
            InitializeComponent();

            Font = new System.Drawing.Font("Arial", 9f);
            Padding = new Padding(0);
            RenderMode = ToolStripRenderMode.Professional;
            Renderer = new DjsmToolStripRenderer();
        }

        private bool readyToDrag = false;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Parent is Form && WindowDragEnabled)
                {
                    if (!(GetItemAt(e.Location) is ToolStripButton))
                    {
                        readyToDrag = true;
                    }
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs mea)
        {
            if (readyToDrag)
            {
                DraggableWindow.DoDragWindow(Parent.Handle);
            }
            base.OnMouseMove(mea);
        }

        protected override void OnMouseUp(MouseEventArgs mea)
        {
            readyToDrag = false;

            base.OnMouseUp(mea);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Parent is Form && WindowDragEnabled)
                {
                    if (!(GetItemAt(e.Location) is ToolStripButton))
                    {
                        Form frm = Parent as Form;
                        frm.WindowState = frm.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
                    }
                }
            }

            base.OnMouseDoubleClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Left) == ToolStripStatusLabelBorderSides.Left ? 1 : 0, ButtonBorderStyle.Solid,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Top) == ToolStripStatusLabelBorderSides.Top ? 1 : 0, ButtonBorderStyle.Solid,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Right) == ToolStripStatusLabelBorderSides.Right ? 1 : 0, ButtonBorderStyle.Solid,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Bottom) == ToolStripStatusLabelBorderSides.Bottom ? 1 : 0, ButtonBorderStyle.Solid);
        }

        private const uint WM_MOUSEACTIVATE = 0x21;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_MOUSEACTIVATE && CanFocus && !Focused)
            {
                Focus();
            }

            base.WndProc(ref m);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NntToolStrip
            // 
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.ResumeLayout(false);
        }
    }
}
