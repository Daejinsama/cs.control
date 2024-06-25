using System.Runtime.InteropServices;

namespace com.outlook_styner07.cs.control
{
    public class DraggableWindow
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int WM_CLOSE = 0x10,
            SW_NORMAL = 1,
            SW_MAXIMIZE = 3,
            SW_MINIMIZE = 6;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int SW_MAXIMIZE);

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        public static void DoDragWindow(IntPtr handle)
        {
            ReleaseCapture();
            SendMessage(handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        public static void CloseWindow(IntPtr handle)
        {
            SendMessage(handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        public static void MaximizedWindow(IntPtr handle)
        {
            ShowWindowAsync(handle, SW_MAXIMIZE);
        }

        public static void NormalWindow(IntPtr handle)
        {
            ShowWindowAsync(handle, SW_NORMAL);
        }

        public static void MinimizedWindow(IntPtr handle)
        {
            ShowWindowAsync(handle, SW_MINIMIZE);
        }
    }
}
