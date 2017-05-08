using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UtilsHelper.HookHelper
{
    /// <summary>
    /// Captures global mouse events
    /// </summary>
    public class MouseHook : GlobalHook
    {
        #region MouseEventType Enum
        private enum MouseEventType
        {
            None,
            MouseDown,
            MouseUp,
            DoubleClick,
            MouseWheel,
            MouseMove
        }
        #endregion
        #region Events
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseWheel;
        public event EventHandler Click;
        public event EventHandler DoubleClick;
        #endregion
        #region Constructor
        public MouseHook()
        {
            HookType = WhMouseLl;
        }
        #endregion
        #region Methods
        protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode > -1 && (MouseDown != null || MouseUp != null || MouseMove != null))
            {
                MouseLlHookStruct mouseHookStruct = (MouseLlHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLlHookStruct));
                MouseButtons button = GetButton(wParam);
                MouseEventType eventType = GetEventType(wParam);
                MouseEventArgs e = new MouseEventArgs(button, (eventType == MouseEventType.DoubleClick ? 2 : 1), mouseHookStruct.pt.x, mouseHookStruct.pt.y, (eventType == MouseEventType.MouseWheel ? (short)((mouseHookStruct.mouseData >> 16) & 0xffff) : 0));
                // Prevent multiple Right Click events (this probably happens for popup menus)
                if (button == MouseButtons.Right && mouseHookStruct.flags != 0)
                {
                    eventType = MouseEventType.None;
                }
                switch (eventType)
                {
                    case MouseEventType.MouseDown:
                        if (MouseDown != null)
                        {
                            MouseDown(this, e);
                        }
                        break;
                    case MouseEventType.MouseUp:
                        if (Click != null)
                        {
                            Click(this, new EventArgs());
                        }
                        if (MouseUp != null)
                        {
                            MouseUp(this, e);
                        }
                        break;
                    case MouseEventType.DoubleClick:
                        if (DoubleClick != null)
                        {
                            DoubleClick(this, new EventArgs());
                        }
                        break;
                    case MouseEventType.MouseWheel:
                        if (MouseWheel != null)
                        {
                            MouseWheel(this, e);
                        }
                        break;
                    case MouseEventType.MouseMove:
                        if (MouseMove != null)
                        {
                            MouseMove(this, e);
                        }
                        break;
                }
            }
            //return -1;
            //屏蔽
            return CallNextHookEx(HandleToHook, nCode, wParam, lParam);
        }
        private MouseButtons GetButton(Int32 wParam)
        {
            switch (wParam)
            {
                case WmLbuttondown:
                case WmLbuttonup:
                case WmLbuttondblclk:
                    return MouseButtons.Left;
                case WmRbuttondown:
                case WmRbuttonup:
                case WmRbuttondblclk:
                    return MouseButtons.Right;
                case WmMbuttondown:
                case WmMbuttonup:
                case WmMbuttondblclk:
                    return MouseButtons.Middle;
                default:
                    return MouseButtons.None;
            }
        }
        private MouseEventType GetEventType(Int32 wParam)
        {
            switch (wParam)
            {
                case WmLbuttondown:
                case WmRbuttondown:
                case WmMbuttondown:
                    return MouseEventType.MouseDown;
                case WmLbuttonup:
                case WmRbuttonup:
                case WmMbuttonup:
                    return MouseEventType.MouseUp;
                case WmLbuttondblclk:
                case WmRbuttondblclk:
                case WmMbuttondblclk:
                    return MouseEventType.DoubleClick;
                case WmMousewheel:
                    return MouseEventType.MouseWheel;
                case WmMousemove:
                    return MouseEventType.MouseMove;
                default:
                    return MouseEventType.None;
            }
        }
        #endregion
    }
}
