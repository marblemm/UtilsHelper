//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Windows.Forms;
//using UtilsHelper.WindowsApiHelper;

//namespace TestForm
//{
//     //使用线程互斥变量. 通过定义互斥变量来判断是否已运行实例. 把program.cs文件里的Main()函数改为如下代码:
//    static class Program
//    {
//        /// <summary>
//        /// 应用程序的主入口点。
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {

//            bool ret;
//            Mutex mutex = new Mutex(true, Application.ProductName, out ret);
//            if (ret)
//            //var rst = WindowsApi.GlobalFindAtom("jiao_test");
//            //if (rst == 0)
//            {
//                //var i = WindowsApi.GlobalAddAtom("jiaao_test"); //添加原子"jiaao_test" 
//                Application.EnableVisualStyles();
//                Application.DoEvents();
//                //Application.SetCompatibleTextRenderingDefault(false);
//                Application.Run(new Form1());
//                mutex.ReleaseMutex();
//            }
//            else
//            {
//                MessageBox.Show("已经运行了一个实例了。");
//                Application.Exit();
//            }
//        }
//    }
//}


