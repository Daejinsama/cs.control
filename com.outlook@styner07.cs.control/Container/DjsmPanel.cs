using System.ComponentModel;

namespace com.outlook_styner07.cs.control.Container
{
    public class DjsmPanel : Panel
    {
        #region Constructors
        public DjsmPanel()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            BorderStyle = BorderStyle.None;
            Padding = new Padding(1);
        }
        #endregion

        #region Types
        #endregion

        #region Fields
        private Color _borderColor = DjsmColorTable.SecondaryLight;
        private ToolStripStatusLabelBorderSides _borderSides = ToolStripStatusLabelBorderSides.Bottom;
        #endregion

        #region Properties
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
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Left) == ToolStripStatusLabelBorderSides.Left ? 1 : 0, ButtonBorderStyle.Solid,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Top) == ToolStripStatusLabelBorderSides.Top ? 1 : 0, ButtonBorderStyle.Solid,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Right) == ToolStripStatusLabelBorderSides.Right ? 1 : 0, ButtonBorderStyle.Solid,
                _borderColor, (_borderSides & ToolStripStatusLabelBorderSides.Bottom) == ToolStripStatusLabelBorderSides.Bottom ? 1 : 0, ButtonBorderStyle.Solid);
        }
        #endregion
    }
}
