using System.ComponentModel;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmPanel : System.Windows.Forms.Panel
    {
        [Browsable(true)]
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
        private Color _borderColor = DjsmColorTable.SecondaryLight;

        //[System.Runtime.InteropServices.ComVisible(true),
        //Editor(typeof(BorderSidesEditor), typeof(UITypeEditor)), Flags]
        //public enum ToolStripStatusLabelBorderSides { Left = 0x0001, Top = 0x0010, Right = 0x0100, Bottom = 0x1000 }

        [Browsable(true)]
        public ToolStripStatusLabelBorderSides BorderSides
        {
            get { return _borderSides; }
            set
            {
                if (_borderSides != value)
                {
                    _borderSides = value;
                    Invalidate();
                }
            }
        }
        private ToolStripStatusLabelBorderSides _borderSides = ToolStripStatusLabelBorderSides.Bottom;

        public DjsmPanel()
        {
            BorderStyle = BorderStyle.None;
            Padding = new Padding(1);
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
    }
}
