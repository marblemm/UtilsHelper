// <copyright file="Class1.cs" company="zondy">
//		Copyright (c) Zondy. All rights reserved.
// </copyright>
// <author>Administrator</author>
// <date>2017/5/8 9:56:35</date>
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
using System.Text;
using Microsoft.Win32;

namespace UtilsHelper.HookHelper
{
    class DiableTaskMgrHelper
    {
        //通过修改注册表来屏蔽任务管理器 
        public void DiableTaskMgrByRegEdit()
        {
            var key = Registry.CurrentUser;
            var url = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
            try
            {
                RegistryKey r = key.OpenSubKey(url, true);
                if (r != null)
                {
                    r.SetValue("DisableTaskMgr", "1");  //屏蔽任务管理器 
                }
                else
                {
                    var syskey = key.CreateSubKey(url);
                    if (syskey != null)
                        syskey.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
                }
            }
            catch
            {
                RegistryKey r = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
                if (r != null) r.DeleteValue("DisableTaskMgr");
            }
        }

    }
}
