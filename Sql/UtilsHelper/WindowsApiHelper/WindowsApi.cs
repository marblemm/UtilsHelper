using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UtilsHelper.HotKey;

namespace UtilsHelper.WindowsApiHelper
{
    public class WindowsApi
    {
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);

        [DllImport("user32.dll")]
        // ReSharper disable once UnusedMember.Local
        public static extern uint DestroyCursor(IntPtr cursorHandle);




        [DllImport("User32.dll")]

        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);//设定焦点

        [DllImport("User32.dll")]

        public static extern bool SetForegroundWindow(IntPtr hWnd);




        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalAddAtom(string lpString); //添加原子 
        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalFindAtom(string lpString); //查找原子 
        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalDeleteAtom(UInt32 nAtom); //删除原子


        /// <summary>
        /// 如果函数执行成功，返回值不为0。
        /// 如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        /// </summary>
        /// <param name="hWnd">要定义热键的窗口的句柄</param>
        /// <param name="id">定义热键ID（不能与其它ID重复）</param>
        /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="vk">定义热键的内容</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hWnd">要取消热键的窗口的句柄</param>
        /// <param name="id">要取消热键的ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}