﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UtilsHelper.HotKey
{
    public class SystemHotKey
    {
        //    注册系统热键，.net 类库好像没有提供现成的方法，需要使用系统提供的 DLL。
        //微软将许多常用的系统函数都封装在 user32.dll 中，注册系统热键使用到的 RegisterHotKey 函数和 UnregisterHotKey 函数也在该 DLL 文件中，所以我们需要将这两个方法映射到 C# 类中。下面代码封装了这两个方法，并做一些简单的封装，如下：

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



        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKeyId">热键ID</param>
        /// <param name="keyModifiers">组合键</param>
        /// <param name="key">热键</param>
        public static void RegHotKey(IntPtr hwnd, int hotKeyId, KeyModifiers keyModifiers, Keys key)
        {
            if (!RegisterHotKey(hwnd, hotKeyId, keyModifiers, key))
            {
                //int errorCode = Marshal.GetLastWin32Error();
                //if (errorCode == 1409)
                //{
                //    MessageBox.Show("热键被占用 ！");
                //}
                //else
                //{
                //    MessageBox.Show("注册热键失败！错误代码：" + errorCode);
                //}
            }
        }

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hotKeyId">热键ID</param>
        public static void UnRegHotKey(IntPtr hwnd, int hotKeyId)
        {
            //注销指定的热键
            UnregisterHotKey(hwnd, hotKeyId);
        }
    }

    //（这个类是网上搜到的，这里借用一下。。。在此对原作者表示感谢！）
    //　　上面这个类中，只需要使用两个静态方法 RegHotKey 和 UnRegHotKey 来注册和注销热键即可。
    //　　这里有一点需要注意一下：这两个方法需要一个窗口的句柄来绑定系统热键消息，也就是说，当用户按下注册过的热键以后，系统会将按键消息发送给指定窗口。
    //　　RegHotKey 方法有四个参数，第一个是窗口句柄，第二个是自定义的热键ID，第三个是组合键，比如Ctrl、Alt、Shift等，如果没有，就是None，第四个就是指定的热键了。
    //　　UnRegHotKey 方法只需要窗口句柄和热键ID，就可以将该热键注销。
    //　　然后，创建一个窗体，在代码视图中添加如下代码：

    //public class FormEx : Form
    //{
    //    private const int WmHotkey = 0x312; //窗口消息：热键
    //    private const int WmCreate = 0x1;   //窗口消息：创建
    //    private const int WmDestroy = 0x2;   //窗口消息：销毁

    //    private const int HotKeyId = 1; //热键ID（自定义）

    //    protected override void WndProc(ref Message msg)
    //    {
    //        base.WndProc(ref msg);
    //        switch (msg.Msg)
    //        {
    //            case WmHotkey: //窗口消息：热键
    //                int tmpWParam = msg.WParam.ToInt32();
    //                if (tmpWParam == HotKeyId)
    //                {
    //                    System.Windows.Forms.SendKeys.Send("^v");
    //                }
    //                break;
    //            case WmCreate: //窗口消息：创建
    //                SystemHotKey.RegHotKey(this.Handle, HotKeyId, SystemHotKey.KeyModifiers.None, Keys.F1);
    //                break;
    //            case WmDestroy: //窗口消息：销毁
    //                SystemHotKey.UnRegHotKey(this.Handle, HotKeyId); //销毁热键
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}

    //在上面代码中，WM_HOTKEY、WM_CREATE、WM_DESTROY 三个常量的值是系统定义，不用关心。HotKeyID 是自定义的一个数值，用于在注册了多个热键的时候使用该数值来区分不同热键处理逻辑，系统会在用户触发热键时将该值做为参数传递给处理程序。另外，上面代码中重写了一个系统方法 WndProc，这个方法叫“窗口过程”（参考百度百科），用于接收处理注册到该窗体上的所有事件，包括窗体创建、窗体销毁、系统热键等等。该方法有一个 Message 结构体参数，该参数封装了 Windows 消息的一些基本属性，比如消息ID、参数等等。上面代码在该方法接收到窗口创建消息的时候注册热键 F1，并且在接收到窗口销毁消息的时候注销该热键，并且在接收到系统热键消息的时候，根据消息参数（热键ID）来确认触发我们想要的动作，比如这里的模拟用户按下 Ctrl+V 键。
}