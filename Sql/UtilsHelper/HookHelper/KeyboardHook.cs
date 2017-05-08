using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UtilsHelper.HookHelper
{
    /// <summary>
    /// Captures global keyboard events
    /// </summary>
    public class KeyboardHook : GlobalHook
    {
        #region Events
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        public event KeyPressEventHandler KeyPress;
        #endregion
        #region Constructor
        public KeyboardHook()
        {
            HookType = WhKeyboardLl;
        }
        #endregion
        #region Methods
        protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {
            bool handled = false;
            if (nCode > -1 && (KeyDown != null || KeyUp != null || KeyPress != null))
            {
                KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                // Is Control being held down?
                bool control = ((GetKeyState(VkLcontrol) & 0x80) != 0) || ((GetKeyState(VkRcontrol) & 0x80) != 0);
                // Is Shift being held down?
                bool shift = ((GetKeyState(VkLshift) & 0x80) != 0) || ((GetKeyState(VkRshift) & 0x80) != 0);
                // Is Alt being held down?
                bool alt = ((GetKeyState(VkLalt) & 0x80) != 0) || ((GetKeyState(VkRalt) & 0x80) != 0);
                // Is CapsLock on?
                bool capslock = GetKeyState(VkCapital) != 0;
                // Create event using keycode and control/shift/alt values found above
                KeyEventArgs e = new KeyEventArgs((Keys)(keyboardHookStruct.vkCode | (control ? (int)Keys.Control : 0) | (shift ? (int)Keys.Shift : 0) | (alt ? (int)Keys.Alt : 0)));
                // Handle KeyDown and KeyUp events
                switch (wParam)
                {
                    case WmKeydown:
                    case WmSyskeydown:
                        if (KeyDown != null)
                        {
                            KeyDown(this, e);
                            handled = e.Handled;
                        }
                        break;
                    case WmKeyup:
                    case WmSyskeyup:
                        if (KeyUp != null)
                        {
                            KeyUp(this, e);
                            handled = e.Handled;
                        }
                        break;
                }
                // Handle KeyPress event
                if (wParam == WmKeydown && !handled && !e.SuppressKeyPress && KeyPress != null)
                {
                    byte[] keyState = new byte[256];
                    byte[] inBuffer = new byte[2];
                    GetKeyboardState(keyState);
                    if (ToAscii(keyboardHookStruct.vkCode, keyboardHookStruct.scanCode, keyState, inBuffer, keyboardHookStruct.flags) == 1)
                    {
                        char key = (char)inBuffer[0];
                        if ((capslock ^ shift) && Char.IsLetter(key))
                            key = Char.ToUpper(key);
                        KeyPressEventArgs e2 = new KeyPressEventArgs(key);
                        KeyPress(this, e2);
                        handled = e.Handled;
                    }
                }
            }
            if (handled)
            {
                return 1;
            }
            //return -1;
            return CallNextHookEx(HandleToHook, nCode, wParam, lParam);
        }
        #endregion
    }
}
