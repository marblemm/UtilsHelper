// <copyright file="Class1.cs" company="zondy">
//		Copyright (c) Zondy. All rights reserved.
// </copyright>
// <author>Administrator</author>
// <date>2017/5/3 17:52:54</date>
// <summary>文件功能描述</summary>
// <modify>
//		修改人:		
//		修改时间:	
//		修改描述:	
//		版本: 1.0	
// </modify>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ControlHelper.CommonForm
{
    class Class1
    {
        private void SaveFileDialog()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = path + "\\PowerPlant\\DataExamples\\blankexample\\B1后果评价-事故源项.xlsx";
            if (!File.Exists(path))
                return;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = @"xls(*.xls)|*.xls";
            dialog.Title = "模板下载";
            dialog.FilterIndex = 1;
            dialog.FileName = "B1后果评价-事故源项";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string destiPath = dialog.FileName;
                if (File.Exists(destiPath))
                    File.Delete(destiPath);
                File.Copy(path, destiPath);
            }
        }

        private void OpenFiledialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = @"Excel File(*.xls;*.xlsx)|*.xls;*.xlsx";
            ofd.Title = @"请选择要导入的数据文件";
            ofd.Multiselect = false;
            DialogResult dialogResult = ofd.ShowDialog();
            if (DialogResult.OK == dialogResult)
            {
            }
        }

        private void OpenFolderBrowserDialog()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "请选择输出目录";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                
            }
        }
    }
}
