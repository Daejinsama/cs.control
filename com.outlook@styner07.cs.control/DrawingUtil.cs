using System.Drawing;
using System.Drawing.Drawing2D;

namespace com.outlook_styner07.cs.control
{
    public class DrawingUtil
    {
        public static TextFormatFlags GetTextFormatFlag(ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    return TextFormatFlags.Top | TextFormatFlags.Left;
                case ContentAlignment.TopCenter:
                    return TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                case ContentAlignment.TopRight:
                    return TextFormatFlags.Top | TextFormatFlags.Right;
                case ContentAlignment.MiddleLeft:
                    return TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                case ContentAlignment.MiddleCenter:
                    return TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                case ContentAlignment.MiddleRight:
                    return TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                case ContentAlignment.BottomLeft:
                    return TextFormatFlags.Bottom | TextFormatFlags.Left;
                case ContentAlignment.BottomCenter:
                    return TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                case ContentAlignment.BottomRight:
                    return TextFormatFlags.Bottom & TextFormatFlags.Right;
                default:
                    return TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;

            }
        }
        public static GraphicsPath GetNormalRectPath(Rectangle rect, SizeF textSize, bool textAlignTop)
        {
            PointF pointLeftTop = textAlignTop
                ? new PointF(rect.X, rect.Y + (textSize.Height / 2))
                : new PointF(rect.X, rect.Y);

            PointF pointRightTop = textAlignTop
                ? new PointF(rect.X + rect.Width - 1, rect.Y + (textSize.Height / 2))
                : new PointF(rect.X + rect.Width - 1, rect.Y);

            PointF pointRightBottom = textAlignTop
                ? new PointF(rect.X + rect.Width - 1, rect.Y + rect.Height - 1)
                : new PointF(rect.X + rect.Width - 1, (rect.Y + rect.Height - 1) - (textSize.Height / 2));

            PointF pointLeftBottom = textAlignTop
                ? new PointF(rect.X, rect.Y + rect.Height - 1)
                : new PointF(rect.X, (rect.Y + rect.Height - 1) - (textSize.Height / 2));

            GraphicsPath ret = new GraphicsPath();
            ret.AddLine(pointLeftTop, pointRightTop);   // top horizon
            ret.AddLine(pointRightTop, pointRightBottom); // right vertical
            ret.AddLine(pointRightBottom, pointLeftBottom); // bottom horizon
            ret.AddLine(pointLeftBottom, pointLeftTop); // left vertical
            ret.CloseAllFigures();
            return ret;
        }

        public static GraphicsPath GetRoundRectPath(Rectangle rect, SizeF textSize, int radius, bool textAlignTop)
        {
            PointF pointLeftTop = textAlignTop
                ? new PointF(rect.X, rect.Y + (textSize.Height / 2))
                : new PointF(rect.X, rect.Y);

            PointF pointRightTop = textAlignTop
                ? new PointF(rect.X + rect.Width - radius - 1, rect.Y + (textSize.Height / 2))
                : new PointF(rect.X + rect.Width - radius - 1, rect.Y);

            PointF pointRightBottom = textAlignTop
                ? new PointF(rect.X + rect.Width - radius - 1, rect.Y + rect.Height - radius - 1)
                : new PointF(rect.X + rect.Width - radius - 1, (rect.Y + rect.Height - radius - 1) - (textSize.Height / 2));

            PointF pointLeftBottom = textAlignTop
                ? new PointF(rect.X, rect.Y + rect.Height - radius - 1)
                : new PointF(rect.X, (rect.Y + rect.Height - radius - 1) - (textSize.Height / 2));

            GraphicsPath ret = new GraphicsPath();
            ret.AddArc(pointLeftTop.X, pointLeftTop.Y, radius, radius, 180, 90);
            ret.AddArc(pointRightTop.X, pointRightTop.Y, radius, radius, 270, 90);
            ret.AddArc(pointRightBottom.X, pointRightBottom.Y, radius, radius, 0, 90);
            ret.AddArc(pointLeftBottom.X, pointLeftBottom.Y, radius, radius, 90, 90);
            ret.CloseAllFigures();
            return ret;
        }
    }
}
