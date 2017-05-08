using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UtilsHelper.HookHelper
{
    /// <summary>
    /// Abstract base class for Mouse and Keyboard hooks
    /// </summary>
    public abstract class GlobalHook
    {
        #region Windows API Code
        [StructLayout(LayoutKind.Sequential)]
        protected class PointStruct
        {
            public int x;
            public int y;
        }
        [StructLayout(LayoutKind.Sequential)]
        protected class MouseHookStruct
        {
            public PointStruct pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        protected class MouseLlHookStruct
        {
            public PointStruct pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        protected class KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        protected static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        protected static extern int UnhookWindowsHookEx(int idHook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);
        [DllImport("user32")]
        protected static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);
        [DllImport("user32")]
        protected static extern int GetKeyboardState(byte[] pbKeyState);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern short GetKeyState(int vKey);
        protected delegate int HookProc(int nCode, int wParam, IntPtr lParam);
        //1.WH_MOUSE只能监控钩子所在模块的鼠标事件。
        //2.WH_MOUSE_LL可以截获整个系统所有模块的鼠标事件。
        protected const int WhMouseLl = 14;
        protected const int WhKeyboardLl = 13;
        protected const int WhMouse = 7;
        protected const int WhKeyboard = 2;
        protected const int WmMousemove = 0x200;
        protected const int WmLbuttondown = 0x201;
        protected const int WmRbuttondown = 0x204;
        protected const int WmMbuttondown = 0x207;
        protected const int WmLbuttonup = 0x202;
        protected const int WmRbuttonup = 0x205;
        protected const int WmMbuttonup = 0x208;
        protected const int WmLbuttondblclk = 0x203;
        protected const int WmRbuttondblclk = 0x206;
        protected const int WmMbuttondblclk = 0x209;
        protected const int WmMousewheel = 0x020A;
        protected const int WmKeydown = 0x100;
        protected const int WmKeyup = 0x101;
        protected const int WmSyskeydown = 0x104;
        protected const int WmSyskeyup = 0x105;
        protected const byte VkShift = 0x10;
        protected const byte VkCapital = 0x14;
        protected const byte VkNumlock = 0x90;
        protected const byte VkLshift = 0xA0;
        protected const byte VkRshift = 0xA1;
        protected const byte VkLcontrol = 0xA2;
        protected const byte VkRcontrol = 0x3;
        protected const byte VkLalt = 0xA4;
        protected const byte VkRalt = 0xA5;
        protected const byte LlkhfAltdown = 0x20;
        #endregion
        #region Private Variables
        protected int HookType;
        protected int HandleToHook;
        protected HookProc HookCallback;
        #endregion
        #region Properties
        public bool IsStarted { get; protected set; }

        #endregion
        #region Constructor

        protected GlobalHook()
        {
            Application.ApplicationExit += Application_ApplicationExit;
        }
        #endregion
        #region Methods
        public void Start()
        {
            if (!IsStarted && HookType != 0)
            {
                // Make sure we keep a reference to this delegate!
                // If not, GC randomly collects it, and a NullReference exception is thrown
                HookCallback = HookCallbackProcedure;
                HandleToHook = SetWindowsHookEx(HookType, HookCallback,//Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                    IntPtr.Zero,
                    0);
                //IntPtr.Zero;
                // Were we able to sucessfully start hook?
                if (HandleToHook != 0)
                {
                    IsStarted = true;
                }
            }
        }
        public void Stop()
        {
            if (IsStarted)
            {
                UnhookWindowsHookEx(HandleToHook);
                IsStarted = false;
            }
        }
        protected virtual int HookCallbackProcedure(int nCode, Int32 wParam, IntPtr lParam)
        {
            // This method must be overriden by each extending hook
            return 0;
        }
        protected void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (IsStarted)
            {
                Stop();
            }
        }
        #endregion
    }
}
