

////方法二：禁止多个进程运行,并当重复运行时激活以前的进程,采用判断进程的方式，我们在运行程序前,查找进程中是否有同名的进程,同时运行位置也相同程,如是没有运行该程序,如果有就就不运行.

//using System;
//using System.Diagnostics;
//using System.Reflection;
//using UtilsHelper.WindowsApiHelper;

//namespace TestForm
//{
//    static class Program
//    {
//        /// <summary>
//        /// 应用程序的主入口点。
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            //Get   the   running   instance.   
//            Process instance = RunningInstance();
//            if (instance == null)
//            {
//                System.Windows.Forms.Application.EnableVisualStyles();   //这两行实现   XP   可视风格   
//                System.Windows.Forms.Application.DoEvents();
//                //There   isn't   another   instance,   show   our   form.   
//                System.Windows.Forms.Application.Run(new Form1());
//            }
//            else
//            {
//                //There   is   another   instance   of   this   process.   
//                HandleRunningInstance(instance);
//            }
//        }


//        public static Process RunningInstance()
//        {

//            Process current = Process.GetCurrentProcess();
//            Process[] processes = Process.GetProcessesByName(current.ProcessName);
//            //Loop   through   the   running   processes   in   with   the   same   name   
//            foreach (Process process in processes)
//            {
//                //Ignore   the   current   process   
//                if (process.Id != current.Id)
//                {
//                    //Make   sure   that   the   process   is   running   from   the   exe   file.   

//                    var location = Assembly.GetExecutingAssembly().Location;
//                    if (location != null && location.Replace("/", "\\") == current.MainModule.FileName)
//                    {
//                        //Return   the   other   process   instance.   
//                        return process;
//                    }
//                }
//            }
//            //No   other   instance   was   found,   return   null. 
//            return null;
//        }
//        public static void HandleRunningInstance(Process instance)
//        {
//            //Make   sure   the   window   is   not   minimized   or   maximized   
//            WindowsApi.ShowWindowAsync(instance.MainWindowHandle, WsShownormal);
//            //Set   the   real   intance   to   foreground   window
//            WindowsApi.SetForegroundWindow(instance.MainWindowHandle);
//        }

//        private const int WsShownormal = 1;
//    }
//}
