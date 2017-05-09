using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UtilsHelper.HotKey;

namespace UtilsHelper.WindowsApiHelper
{
    public enum WindowShowStatus
    {
        /// <summary>
        /// 隐藏窗口
        /// </summary>
        SwHide = 0,
        /// <summary>
        /// 最大化窗口
        /// </summary>
        SwMaximize = 3,
        /// <summary>
        /// 最小化窗口
        /// </summary>
        SwMinimize = 6,
        /// <summary>
        /// 用原来的大小和位置显示一个窗口，同时令其进入活动状态
        /// </summary>
        SwRestore = 9,
        /// <summary>
        /// 用当前的大小和位置显示一个窗口，同时令其进入活动状态
        /// </summary>
        SwShow = 5,
        /// <summary>
        /// 最大化窗口，并将其激活
        /// </summary>
        SwShowmaximized = 3,
        /// <summary>
        /// 最小化窗口，并将其激活
        /// </summary>
        SwShowminimized = 2,
        /// <summary>
        /// 最小化一个窗口，同时不改变活动窗口
        /// </summary>
        SwShowminnoactive = 7,
        /// <summary>
        /// 用当前的大小和位置显示一个窗口，不改变活动窗口
        /// </summary>
        SwShowna = 8,
        /// <summary>
        /// 用最近的大小和位置显示一个窗口，同时不改变活动窗口
        /// </summary>
        SwShownoactivate = 4,
        /// <summary>
        /// 用原来的大小和位置显示一个窗口，同时令其进入活动状态，与SW_RESTORE 相同
        /// </summary>
        SwShownormal = 1,
        /// <summary>
        /// 关闭窗体
        /// </summary>
        WmClose = 0x10
    }


    public class WindowsApi
    {
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);

        [DllImport("user32.dll")]
        // ReSharper disable once UnusedMember.Local
        public static extern uint DestroyCursor(IntPtr cursorHandle);


        #region 窗体操作

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="hWnd">窗体句柄</param>
        /// <param name="cmdShow">指定的命令 0:关闭窗口 1:正常大小显示窗口 2:最小化窗口3:最大化窗口</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]

        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);//设定焦点

        [DllImport("User32.dll")]

        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion




        #region 原子操作

        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalAddAtom(string lpString); //添加原子 
        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalFindAtom(string lpString); //查找原子 
        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalDeleteAtom(UInt32 nAtom); //删除原子

        #endregion

        #region 注册热键

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

        #endregion

        public class AnimateWindowState
        {
            /// <summary>  
            /// 自左向右显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略   
            /// </summary>  
            public const Int32 AwHorPositive = 0x00000001;
            /// <summary>  
            ///  自右向左显示窗口。当使用了 AW_CENTER 标志时该标志被忽略  
            /// </summary>  
            public const Int32 AwHorNegative = 0x00000002;
            /// <summary>  
            /// 自顶向下显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略  
            /// </summary>  
            public const Int32 AwVerPositive = 0x00000004;   
            /// <summary>  
            /// 自下向上显示窗口。该标志可以在滚动动画和滑动动画中使用。当使用AW_CENTER标志时，该标志将被忽略  
            /// </summary>  
            public const Int32 AwVerNegative = 0x00000008;   
            /// <summary>  
            /// 若使用了AW_HIDE标志，则使窗口向内重叠；若未使用AW_HIDE标志，则使窗口向外扩展  
            /// </summary>  
            public const Int32 AwCenter = 0x00000010;   
            /// <summary>  
            /// 隐藏窗口，缺省则显示窗口  
            /// </summary>  
            public const Int32 AwHide = 0x00010000;   
            /// <summary>  
            /// 激活窗口。在使用了AW_HIDE标志后不要使用这个标志  
            /// </summary>  
            public const Int32 AwActivate = 0x00020000;   
            /// <summary>  
            /// 使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略  
            /// </summary>  
            public const Int32 AwSlide = 0x00040000;   
            /// <summary>  
            /// 使用淡入效果。只有当hWnd为顶层窗口的时候才可以使用此标志  
            /// </summary>  
            public const Int32 AwBlend = 0x00080000;
        }

        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);   // 该函数可以实现窗体的动画效果 


        /// <summary>
        /// 指定句柄的窗口发送消息
        /// </summary>
        /// <param name="hWnd">接收消息窗体的句柄</param>
        /// <param name="msg">消息标示符</param>
        /// <param name="wParam">消息</param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);
    }
}