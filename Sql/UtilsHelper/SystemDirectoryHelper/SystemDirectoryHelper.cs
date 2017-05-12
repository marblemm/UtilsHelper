using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace UtilsHelper.SystemDirectoryHelper
{
    public class SystemDirectoryHelper
    {
        /// <summary>
        /// 获取当前文件的路径
        /// </summary>
        public static readonly string CurExecuteFilePath = Process.GetCurrentProcess().MainModule.FileName;
        /// <summary>
        /// 获取模块的完整路径，包括文件名。
        /// </summary>
        public static readonly string CurExecuteDirectoryPath = Environment.CurrentDirectory;
        //     获取和设置当前目录(该进程从中启动的目录)的完全限定目录。
        //3.   System.IO.Directory.GetCurrentDirectory()
        //     获取应用程序的当前工作目录。这个不一定是程序从中启动的目录啊，有可能程序放在C:\www里,这个函数有可能返回C:\Documents and Settings\ZYB\,或者C:\Program Files\Adobe\,有时不一定返回什么东东，这是任何应用程序最后一次操作过的目录，比如你用Word打开了E:\doc\my.doc这个文件，此时执行这个方法就返回了E:\doc了。
        //4. System.AppDomain.CurrentDomain.BaseDirectory
        //     获取程序的基目录。
        //二、操作环境变量
        //利用System.Environment.GetEnvironmentVariable()方法可以很方便地取得系统环境变量，如：
        //System.Environment.GetEnvironmentVariable("windir")就可以取得windows系统目录的路径。
        //以下是一些常用的环境变量取值：
        //System.Environment.GetEnvironmentVariable("windir");
        //System.Environment.GetEnvironmentVariable("INCLUDE");
        //System.Environment.GetEnvironmentVariable("TMP");
        //System.Environment.GetEnvironmentVariable("TEMP");
        //System.Environment.GetEnvironmentVariable("Path");
        // System.Environment.SystemDirectory ;C:/windows/system32目录
        //最后贴出我进行上面操作获得的变量值，事先说明，本人是编写了一个WinForm程序，项目文件存放于D:\Visual Studio Projects\MyApplication\LifeAssistant，编译后的文件位于D:\Visual Studio Projects\MyApplication\LifeAssistant\bin\Debug，最后的结果如下：
        //1、System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName=D:\Visual Studio Projects\MyApplication\LifeAssistant\bin\Debug\LifeAssistant.exe
        //2、System.Environment.CurrentDirectory=D:\Visual Studio Projects\MyApplication\LifeAssistant\bin\Debug
        //3、System.IO.Directory.GetCurrentDirectory()=D:\Visual Studio Projects\MyApplication\LifeAssistant\bin\Debug

        /// <summary>
        /// 利用System.Environment.GetEnvironmentVariable()方法可以很方便地取得系统环境变量，如：
        //System.Environment.GetEnvironmentVariable("windir")就可以取得windows系统目录的路径。
        /// </summary>
        /// <param name="filter">"windir"  "INCLUDE"   "TMP"  "Path"  </param>
        /// <returns></returns>
        public static string GetEnviromentDirectory(string filter)
        {
            return Environment.GetEnvironmentVariable(filter);
        }

        /// <summary>
        /// C:/windows/system32目录
        /// </summary>
        public static string GetSystemDirectory { get { return Environment.SystemDirectory; } }

        /// <summary>
        /// 获取当前进程的完整路径，包含文件名(进程名)。
        /// result: X:\xxx\xxx\xxx.exe (.exe文件所在的目录+.exe文件名)
        /// 不能通过静态调用
        /// </summary>
        public string CurAssemblyLocation { get { return GetType().Assembly.Location; } }

        /// <summary>
        /// 获取新的 Process 组件并将其与当前活动的进程关联的主模块的完整路径，包含文件名(进程名)。
        /// result: X:\xxx\xxx\xxx.exe (.exe文件所在的目录+.exe文件名)
        /// </summary>
        public static readonly string CurProcessMainModuleFileName = Process.GetCurrentProcess().MainModule.FileName;

        /// <summary>
        /// 获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
        /// result: X:\xxx\xxx(.exe文件所在的目录)
        /// </summary>
        public static readonly string CurExcuteDirectory = Environment.CurrentDirectory;

        //获取当前 Thread 的当前应用程序域的基目录，它由程序集冲突解决程序用来探测程序集。
        //string str = System.AppDomain.CurrentDomain.BaseDirectory;
        //result: X:\xxx\xxx\ (.exe文件所在的目录+"\")

        /// <summary>
        /// 获取和设置包含该应用程序的目录的名称。(推荐)
        /// result: X:\xxx\xxx\ (.exe文件所在的目录+"\")
        /// </summary>
        public static readonly string CurApplicationDirctory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        /// <summary>
        /// 获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称。
        /// result: X:\xxx\xxx(.exe文件所在的目录)
        /// </summary>
        public static readonly string CurApplicationStartupPath = Application.StartupPath;

        /// <summary>
        /// 获取启动了应用程序的可执行文件的路径，包括可执行文件的名称。
        /// result: X:\xxx\xxx\xxx.exe (.exe文件所在的目录+.exe文件名)
        /// </summary>
        public static readonly string CurExecutePath = Application.ExecutablePath;

        //获取应用程序的当前工作目录(不可靠)。
        //string str = System.IO.Directory.GetCurrentDirectory();
        //result: X:\xxx\xxx(.exe文件所在的目录)
    }
}
