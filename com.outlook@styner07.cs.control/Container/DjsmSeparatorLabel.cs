using System.Drawing;
using System.Windows.Forms;

namespace com.outlook_styner07.cs.control.Container
{
    public partial class DjsmSeparatorLabel : Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Size size = TextRenderer.MeasureText(Text, Font);

            TextRenderer.DrawText(g, Text, Font, new Point(0, (Height - size.Height) / 2), ForeColor);

            int lineY = Height / 2 + 1;

            g.DrawLine(new Pen(new SolidBrush(Color.LightGray), 1), new Point(size.Width + 3, lineY), new Point(Width, lineY));
        }
    }
}
