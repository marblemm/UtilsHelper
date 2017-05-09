using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using UtilsHelper.Control.TipForm;
using UtilsHelper.WindowsApiHelper;

namespace ScreenShot
{
    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var dir = Environment.CurrentDirectory;
            //string xmlPath = Path.Combine(dir, "config.xml");

            //bool runone;
            //Mutex run = new Mutex(true, "screenshot", out runone);
            //if (runone)
            //{
            //    run.ReleaseMutex();
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    var frm = new MainForm();
            //    int hdc = frm.Handle.ToInt32();
            //    var xmlDoc = XmlHelper.CreateXmlDocument(xmlPath, "config");
            //    var dic = new Dictionary<string, string>();
            //    //dic.Add();
            //    XmlHelper.AppendNode(xmlPath, "frmHandle",
            //        new Dictionary<string, string> { { "handle", hdc.ToString() } }
            //        );
            //    Application.Run(frm);
            //}
            //else
            //{
            //    MessageBox.Show("aa");
            //    var dic = XmlHelper.ReadXmlNode(xmlPath, "frmHandle");
            //    var value = dic["handle"];
            //    IntPtr hdc = new IntPtr(Convert.ToInt32(value));

            //    //通过句柄得到窗口实例不能跨进程
            //    //var frm = Form.FromHandle(hdc) as MainForm;
            //    //frm.Show();
            //    //frm.WindowState = FormWindowState.Normal;


            //    //WindowsApi.FlashWindow(hdc, true);
            //    //WindowsApi.SetFocus(hdc);
            //    WindowsApi.ShowWindowAsync(hdc, 1);
            //    WindowsApi.SetForegroundWindow(hdc);
            //}


            Process instance = RunningInstance();
            if (instance == null)
            {
                Application.EnableVisualStyles();   //这两行实现   XP   可视风格   
                Application.DoEvents();
                Application.Run(new MainForm());
            }
            else
            {
                HandleRunningInstance(instance);
            }
        }

        public static Process RunningInstance()
        {

            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    var location = Assembly.GetExecutingAssembly().Location;
                    if (location != null && location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        public static void HandleRunningInstance(Process instance)
        {
            WindowsApi.ShowWindowAsync(instance.MainWindowHandle, 1);
            WindowsApi.ShowWindow(instance.MainWindowHandle, 1);
            WindowsApi.SetForegroundWindow(instance.MainWindowHandle);

            //string afTitle = "aa";
            //string afContent = "bb";//;txtContent.Text;
            //if (string.IsNullOrEmpty(afContent))
            //{
            //    MessageBox.Show("内容不能为空"); return;
            //}
            //AlertForm.ShowWay showWay = AlertForm.ShowWay.Fade;

            //int afShowInTime = 200, afShowTime = 3000, afShowOutTime = 500;
            //int afWidth=400, afHeigth= 180;
            //var frm = new AlertForm();
            //frm.Show(afContent, afTitle, showWay, afWidth, afHeigth, afShowInTime, afShowTime, afShowOutTime);


            //TipMessageBoxForm.Show( LoadMode.Prompt,  "aa");
        }
    }
}