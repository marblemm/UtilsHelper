using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScreenShot
{
    class HotKey
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModiffiers keyModiffiers, Keys keys);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnRegisterHotKey(IntPtr hWnd, int id);
    }
}