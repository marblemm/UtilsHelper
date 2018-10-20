using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace TextExcelExport
{
    public class TestData
    {
        private static string _exportDir = @"D:\temp\excel";

        public static string GetNewExcelFileName(string name)
        {
            //return Path.Combine(_exportDir, DateTime.Now.ToString("yyMMdd-HHmmss") + suffix);
            return Path.Combine(_exportDir, name);
        }
        public static string GetFileName(string fileName)
        {
            return Path.Combine(_exportDir
                , fileName);
        }

        public static DataTable GetDataTable(int cols = 100, int rows = 1000, string tabName = "mytable")
        {
            DataTable dt = new DataTable(tabName);
            for (int i = 0; i < cols; i++)
            {
                dt.Columns.Add("col" + i.ToString("D3"));
            }

            DataRow dr = null;
            for (int i = 0; i < rows; i++)
            {
                dr = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dr[j] = "val-" + i + "-" + j;
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
