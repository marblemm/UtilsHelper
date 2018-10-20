using System;
using System.Data;
using System.IO;
using ExcelExport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utils;

namespace TextExcelExport
{
    [TestClass]
    public class TestCreateExcel
    {
        #region npoi 
        [TestMethod]
        public void TestNpoiCrate()
        {
            var fname = TestData.GetNewExcelFileName("TestNpoiCrate.xlsx");

            var dt1 = TestData.GetDataTable(tabName: "tab1");
            var dt2 = TestData.GetDataTable(tabName: "tab2");
            DataSet ds = new DataSet();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            ExcelNpoi.Create(fname, ds);
            Assert.IsTrue(File.Exists(fname));
        }
        [TestMethod]
        public void TestNpoiRead()
        {
            var fname = TestData.GetFileName("TestNpoiCrate.xlsx");
            var dt = ExcelNpoi.GetSheet(fname, "tab1");
            Assert.IsTrue(File.Exists(fname));
        }
        #endregion

        #region openxml
        [TestMethod]
        public void TestOpenXmlCrate()
        {
            var fname = TestData.GetNewExcelFileName("TestOpenXmlCrate.xlsx");

            var dt1 = TestData.GetDataTable(tabName: "tab1");
            var dt2 = TestData.GetDataTable(tabName: "tab2");
            DataSet ds = new DataSet();
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);

            ExcelOpenXml.Create(fname, ds);

            Assert.IsTrue(File.Exists(fname));
        }
        [TestMethod]
        public void TestOpenXmlRead()
        {
            var fname = TestData.GetFileName("TestOpenXmlCrate.xlsx");
            var dt = ExcelOpenXml.GetSheet(fname, "tab1");

            Assert.IsTrue(File.Exists(fname));
        }
        #endregion

        [TestMethod]
        public void TestImportImage()
        {
            var fileTemplatePath = "..\\..\\Template\\TestTemplate.xlsx";
            if(!File.Exists(fileTemplatePath))
            {
                return;
            }
            var filePath = "..\\..\\Template\\data1.xlsx";
            OpenXmlHelper ox = new OpenXmlHelper();
            ox.RowIndex = new int[] { 4 };
            System.Data.DataSet ds = new System.Data.DataSet();
            DataTable dt1 = GetData();
            ds.Tables.Add(dt1);
            ox.OpenXmlExportImages = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<OpenXmlExportImages>>();
            System.Collections.Generic.List<OpenXmlExportImages> openXmlExportImages = new System.Collections.Generic.List<OpenXmlExportImages>();
            OpenXmlExportImages oximg1 = new OpenXmlExportImages();
            oximg1.ImagePath = "..\\..\\Template\\aa.png";
            oximg1.X = 100;
            oximg1.Y = 100;
            openXmlExportImages.Add(oximg1);
            OpenXmlExportImages oximg2 = new OpenXmlExportImages();
            oximg2.ImagePath = "..\\..\\Template\\bb.png";
            oximg2.X = 300;
            oximg2.Y = 400;
            openXmlExportImages.Add(oximg2);
            ox.OpenXmlExportImages.Add("数据", openXmlExportImages);
            ox.ExcelExport(ds, filePath, fileTemplatePath);
        }

        DataTable GetData()
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("TheID", typeof(Int32)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));
            data.Columns.Add(new DataColumn("TimeZone", typeof(string)));
            data.Columns.Add(new DataColumn("sheet", typeof(string)));
            data.Columns.Add(new DataColumn("fun", typeof(string)));

            DataRow dr;
            dr = data.NewRow();
            dr[0] = 1; dr[1] = "Washington"; dr[2] = "Pacific"; dr[3] = "1111"; dr[4] = "fff1";
            data.Rows.Add(dr);
            dr = data.NewRow();
            dr[0] = 2; dr[1] = "Utah"; dr[2] = "Mountain"; dr[3] = "2222"; dr[4] = "fff3";
            data.Rows.Add(dr);
            dr = data.NewRow();
            dr[0] = 3; dr[1] = "Wisconsin3"; dr[2] = "Central3"; dr[3] = "3333"; dr[4] = "fff3";
            data.Rows.Add(dr);
            dr = data.NewRow();
            dr[0] = 4; dr[1] = "Wisconsin4"; dr[2] = "Central4"; dr[3] = "4444"; dr[4] = "fff4";
            data.Rows.Add(dr);
            return data;
        }
    }
}
