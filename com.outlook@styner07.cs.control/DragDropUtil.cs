using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace com.outlook_styner07.cs.control
{
    public class DragDropUtil
    {
        /// Usage
        //protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        //{
        //    base.OnGiveFeedback(e);

        //    if (e.Effect == DragDropEffects.Move)
        //    {
        //        e.UseDefaultCursors = false;
        //        if (_dragCursor == null)
        //        {
        //            Bitmap bmp = new Bitmap(Width, Height);
        //            DrawToBitmap(bmp, new Rectangle(new Point(0, 0), Size));

        //            bmp = SetBitmapOpacity(bmp, 0.75f);

        //            _dragCursor = CreateCursor(bmp, new Point(0, 0));
        //            Cursor.Current = _dragCursor;
        //        }
        //    }
        //    else
        //    {
        //        e.UseDefaultCursors = true;

        //        _dragCursor?.Dispose();
        //        _dragCursor = null;
        //    }
        //}

        public static Cursor? CreateCursor(Bitmap bmp, Point hotSpot)
        {
            IconInfo iconInfo = new IconInfo();
            iconInfo.fIcon = false;
            iconInfo.xHotspot = hotSpot.X;
            iconInfo.yHotspot = hotSpot.Y;
            iconInfo.hbmMask = bmp.GetHbitmap();
            iconInfo.hbmColor = bmp.GetHbitmap();

            // WinAPI 호출로 핫스팟이 설정된 커서를 생성
            IntPtr iconPtr = CreateIconIndirect(ref iconInfo);
            if (iconPtr == IntPtr.Zero)
            {
                return null;
            }
            return new Cursor(iconPtr);
        }

        public static Bitmap SetBitmapOpacity(Image original, float opacity)
        {
            Bitmap bmp = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = opacity;

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                g.DrawImage(original,
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    0, 0, original.Width, original.Height,
                    GraphicsUnit.Pixel,
                    attributes);
            }
            return bmp;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr CreateIconIndirect(ref IconInfo iconInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    }
}
