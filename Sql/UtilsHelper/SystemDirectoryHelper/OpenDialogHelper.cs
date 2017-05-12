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
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace UtilsHelper.SystemDirectoryHelper
{
    public class OpenDialogHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string OpenFolderBrowserDialog(string title)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = title;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="title"></param>
        /// <param name="filter">@"Excel File(*.xls;*.xlsx)|*.xls;*.xlsx"</param>
        /// <param name="isMulti"></param>
        /// <returns></returns>
        public static T OpenFiledialog<T>(string title, string filter, bool isMulti)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filter;
            ofd.Title = title;
            ofd.Multiselect = isMulti;
            DialogResult dialogResult = ofd.ShowDialog();
            if (DialogResult.OK == dialogResult)
            {
                return (T)Convert.ChangeType(ofd.FileName, typeof(T));
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        public static void OpenFolder(string directory)
        {
            Process.Start("Explorer.exe", directory);
        }
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
