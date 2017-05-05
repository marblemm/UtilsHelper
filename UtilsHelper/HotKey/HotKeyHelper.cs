// <copyright file="HotKeyHelper.cs" company="zondy">
//		Copyright (c) Zondy. All rights reserved.
// </copyright>
// <author>Administrator</author>
// <date>2017/5/5 16:35:14</date>
// <summary>文件功能描述</summary>
// <modify>
//		修改人:		
//		修改时间:	
//		修改描述:	
//		版本: 1.0	
// </modify>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace UtilsHelper.HotKey
{
    class HotKeyHelper
    {
        //        1、窗体热键

        //首先要设置主窗体KeyPreview为true, 可直接在属性中进行设置，
        //或者在窗体加载中设置： this.KeyPreview = true;
        //然后添加窗体KeyDown事件，如下：
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.Shift && e.Control && e.KeyCode == Keys.S)
            {
                MessageBox.Show("我按了Control +Shift +Alt +S");
            }
        }


        //        2、全局热键设置

        //定义API函数 》 注册热键 》 卸载热键

        //我这里定义了AppHotKey类，全部代码如下：
        public class AppHotKey
        {
            [DllImport("kernel32.dll")]
            public static extern uint GetLastError();
            //如果函数执行成功，返回值不为0。  
            //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。  
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool RegisterHotKey(
                IntPtr hWnd,                //要定义热键的窗口的句柄  
                int id,                     //定义热键ID（不能与其它ID重复）            
                KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效  
                Keys vk                     //定义热键的内容  
                );

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool UnregisterHotKey(
                IntPtr hWnd,                //要取消热键的窗口的句柄  
                int id                      //要取消热键的ID  
                );

            //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）  
            [Flags()]
            public enum KeyModifiers
            {
                None = 0,
                Alt = 1,
                Ctrl = 2,
                Shift = 4,
                WindowsKey = 8
            }
            /// <summary>  
            /// 注册热键  
            /// </summary>  
            /// <param name="hwnd">窗口句柄</param>  
            /// <param name="hotKey_id">热键ID</param>  
            /// <param name="keyModifiers">组合键</param>  
            /// <param name="key">热键</param>  
            public static void RegKey(IntPtr hwnd, int hotKey_id, KeyModifiers keyModifiers, Keys key)
            {
                try
                {
                    if (!RegisterHotKey(hwnd, hotKey_id, keyModifiers, key))
                    {
                        if (Marshal.GetLastWin32Error() == 1409) { MessageBox.Show("热键被占用 ！"); }
                        else
                        {
                            MessageBox.Show("注册热键失败！");
                        }
                    }
                }
                catch (Exception) { }
            }
            /// <summary>  
            /// 注销热键  
            /// </summary>  
            /// <param name="hwnd">窗口句柄</param>  
            /// <param name="hotKey_id">热键ID</param>  
            public static void UnRegKey(IntPtr hwnd, int hotKey_id)
            {
                //注销Id号为hotKey_id的热键设定  
                UnregisterHotKey(hwnd, hotKey_id);
            }
        }

        //重写窗体的WndProc函数，在窗口创建的时候注册热键，窗口销毁时销毁热键，代码如下：
        public class FomeDemo : Form
        {
            private const int WM_HOTKEY = 0x312; //窗口消息-热键  
            private const int WM_CREATE = 0x1; //窗口消息-创建  
            private const int WM_DESTROY = 0x2; //窗口消息-销毁  
            private const int Space = 0x3572; //热键ID  
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                switch (m.Msg)
                {
                    case WM_HOTKEY: //窗口消息-热键ID  
                        switch (m.WParam.ToInt32())
                        {
                            case Space: //热键ID  
                                MessageBox.Show("我按了Control +Shift +Alt +S");
                                break;
                            default:
                                break;
                        }
                        break;
                    case WM_CREATE: //窗口消息-创建  
                        AppHotKey.RegKey(Handle, Space, AppHotKey.KeyModifiers.Ctrl | AppHotKey.KeyModifiers.Shift | AppHotKey.KeyModifiers.Alt, Keys.S);
                        break;
                    case WM_DESTROY: //窗口消息-销毁  
                        AppHotKey.UnRegKey(Handle, Space); //销毁热键  
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
