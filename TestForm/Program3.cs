//using System;
//using System.Windows.Forms;
//using UtilsHelper.WindowsApiHelper;

//namespace TestForm
//{
//    //全局原子法，创建程序前，先检查全局原子表中看是否存在特定原子A（创建时添加的），存在时停止创建，说明该程序已运行了一个实例；不存在则运行程序并想全局原子表中添加特定原子A；退出程序时要记得释放特定的原子A哦，不然要到关机才会释放。
//    static class Program
//    {
//        /// <summary>
//        /// 应用程序的主入口点。
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            var rst = WindowsApi.GlobalFindAtom("jiao_test");
//            if (rst == 77856768)
//            {
//                WindowsApi.GlobalAddAtom("jiaao_test");
//                Application.EnableVisualStyles();
//                //Application.DoEvents();
//                Application.SetCompatibleTextRenderingDefault(false);
//                Application.Run(new Form1());
//            }
//            else
//            {
//                MessageBox.Show(@"已经运行了一个实例了。");
//            }
//        }
//    }
//}