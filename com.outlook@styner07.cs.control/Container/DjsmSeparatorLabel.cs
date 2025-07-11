using System.ComponentModel;

namespace com.outlook_styner07.cs.control.Container
{
    [ToolboxItem(true)]
    public partial class DjsmSeparatorLabel : Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            SizeF size = g.MeasureString(Text, Font);

            g.DrawString(Text, Font, new SolidBrush(ForeColor),0, (Height - size.Height) / 2);

            int lineY = Height / 2 + 1;

            g.DrawLine(new Pen(new SolidBrush(Color.LightGray), 1), new PointF(size.Width + 3, lineY), new Point(Width, lineY));
        }
    }
}
