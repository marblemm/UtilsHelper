using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace UtilsHelper.AutoBoot
{

    //如果想要将一个exe程序设置为开机自启动，其实就是在HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run注册表项中添加一个注册表变量，这个变量的值是程序的所在路径。
    //具体操作步骤是：
    //1、使用RegistryKey类的CreateSubKey方法打开HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run变量，如果不存在，这个方法会直接创建。
    //2、如果是添加一个变量的键值可以使用RegistryKey类的SetValue方法；
    //如果是删除一个变量的键值可以使用RegistryKey类的DeleteValue方法。
    //代码： 设置程序开机自启动状态


    //需要注意的是：
    //Windows中微软的注册表信息是分32位和64位的：
    //32位：HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft
    //64位：HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft

    //以下代码
    //RegistryKey rk = Registry.LocalMachine;
    //RegistryKey rk2 = rk.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
    //rk2.SetValue("MyExec", execPath);  

    //在32位机器上执行，那么没有问题，变量会创建在HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run下。
    //但是如果在64位机器上执行，会自动创建在HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Run


    public static class SetAutoBootStartHelper
    {
        /// <summary>  
        /// 在注册表中添加、删除开机自启动键值  
        /// </summary>  
        public static int SetAutoBootStatu(string executePath, bool isAutoBoot)
        {
            try
            {
                //string execPath = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                if (isAutoBoot)
                {
                    if (rk2 != null)
                    {
                        rk2.SetValue("MyExec", executePath);
                        Console.WriteLine(string.Format("[注册表操作]添加注册表键值：path = {0}, key = {1}, value = {2} 成功", rk2.Name, "TuniuAutoboot", executePath));
                    }
                }
                else
                {
                    if (rk2 != null)
                    {
                        rk2.DeleteValue("MyExec", false);
                        Console.WriteLine(string.Format("[注册表操作]删除注册表键值：path = {0}, key = {1} 成功", rk2.Name, "TuniuAutoboot"));
                    }
                }
                if (rk2 != null) rk2.Close();
                rk.Close();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("[注册表操作]向注册表写开机启动信息失败, Exception: {0}", ex.Message));
                return -1;
            }
        }
    }
}