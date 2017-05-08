using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using UtilsHelper.xmlHelper;

namespace ScreenShot
{
    static class Program
    {

        [DllImport("User32.dll")]

        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hWnd, bool bInvert);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);//设定焦点

        [DllImport("User32.dll")]

        public static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            var dir = Environment.CurrentDirectory;
            string xmlPath = Path.Combine(dir, "config.xml");

            bool runone;
            Mutex run = new Mutex(true, "screenshot", out runone);
            if (runone)
            {
                run.ReleaseMutex();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var frm = new MainForm();
                int hdc = frm.Handle.ToInt32();
                var xmlDoc = XmlHelper.CreateXmlDocument(xmlPath, "config");
                var dic = new Dictionary<string, string>();
                //dic.Add();
                XmlHelper.AppendNode(xmlPath, "frmHandle",
                    new Dictionary<string, string> { { "handle", hdc.ToString() } }
                    );
                Application.Run(frm);
            }
            else
            {
                var dic = XmlHelper.ReadXmlNode(xmlPath, "frmHandle");
                var value = dic["handle"];
                IntPtr hdc = new IntPtr(Convert.ToInt32(value));

                //通过句柄得到窗口实例不能跨进程
                //var frm = Form.FromHandle(hdc) as MainForm;
                //frm.Show();
                //frm.WindowState = FormWindowState.Normal;


                FlashWindow(hdc, true);
                SetFocus(hdc);
                ShowWindowAsync(hdc, 1);
                SetForegroundWindow(hdc);
            }
        }
    }
}