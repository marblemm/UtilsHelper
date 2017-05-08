using System;
using System.Runtime.InteropServices;

namespace UtilsHelper.WindowsApiHelper
{
    public class WindowsApi
    {
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);

        [DllImport("user32.dll")]
        // ReSharper disable once UnusedMember.Local
        public static extern uint DestroyCursor(IntPtr cursorHandle);
    }
}