/********************************************************************************************
 * 文件名称:	OpenXml
 * 设计人员:	徐少年[QQ:78018999 email:xushaonian@qq.com]
 * 设计时间:	2013-10-16 9:40:41
 * 功能描述:	
 * CLR 版本:	4.0.30319.18052
 *
 * 注意事项:	
 * 版权所有:	Copyright (c) 2008-2013 by gkxsn.com
 * 修改记录: 	修改时间		人员	   修改备注
 *				----------		------	   -------------------------------------------------
 *              
 ********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using System.IO;

namespace Utils
{
    public class OpenXmlHelper : IDisposable
    {
        #region 变量
        #region 全局变量
        /// <summary>
        /// 工作簿导入数据起始行
        /// </summary>
        public int[] RowIndex { get; set; }

        /// <summary>
        /// 导入数据时从Excel中获取的图像
        /// </summary>
        public Dictionary<string, List<OpenXmlImportImages>> OpenXmlImportImages { get; set; }

        /// <summary>
        /// 导出数据时要插入的图像
        /// </summary>
        public Dictionary<string, List<OpenXmlExportImages>> OpenXmlExportImages { get; set; }
        #endregion

        #region 私有变量
        private Dictionary<string, List<string>> DicCellName;
        private Dictionary<string, List<Row>> DicRows;
        private Dictionary<string, SharedStringTablePart> DicSharedStringTables;
        private List<SheetEx> Sheets;
        #endregion
        #endregion

        #region 导入
        /// <summary>
        /// Excel转为DataSet
        /// </summary>
        /// <param name="filePath">
        /// 文件路径 如:c\temp.xlsx
        /// </param>
        public DataSet ExcelToDataSet(string filePath)
        {
            DataSet ds = new DataSet();
            DicRows = new Dictionary<string, List<Row>>();
            DicSharedStringTables = new Dictionary<string, SharedStringTablePart>();
            using (var document = SpreadsheetDocument.Open(filePath, false))
            {
                Sheets = document.GetWorksheets();
                WorkbookPart workbookPart = document.WorkbookPart;
                int i = 0;
                if (RowIndex == null)
                    RowIndex = new int[] { 0 };
                OpenXmlImportImages = new Dictionary<string, List<OpenXmlImportImages>>();
                foreach (var sheet in Sheets)
                {
                    if (i >= RowIndex.Length)
                        break;
                    sheet.RowIndex = RowIndex[i];
                    i++;
                    var rows = sheet.Worksheet.Descendants<Row>().ToList();
                    var sharedStringTable = document.GetSharedStringTable();
                    DicRows.Add(sheet.SheetName, rows);
                    DicSharedStringTables.Add(sheet.SheetName, sharedStringTable);
                    // 读取Excel中的数据
                    var data = ReadExcelData(rows, sharedStringTable, sheet.SheetName);
                    data.TableName = sheet.SheetName;
                    ds.Tables.Add(data);
                    OpenXmlImportImages.Add(sheet.SheetName, GetOpenXmlImportImages(sheet, workbookPart));
                }
            }
            return ds;
        }

        private List<OpenXmlImportImages> GetOpenXmlImportImages(SheetEx sheet, WorkbookPart workbookPart)
        {
            WorksheetPart wsPart = (WorksheetPart)workbookPart.GetPartById(sheet.SheetId);
            DrawingsPart drawingPart = wsPart.GetPartsOfType<DrawingsPart>().ToList().FirstOrDefault();
            List<OpenXmlImportImages> pictures = new List<OpenXmlImportImages>();
            if (drawingPart != null)
            {
                foreach (var part in drawingPart.Parts)
                {
                    OpenXmlImportImages pic = new OpenXmlImportImages();
                    ImagePart imgPart = (ImagePart)part.OpenXmlPart;
                    pic.Image = StreamToBytes(imgPart.GetStream());
                    pic.RefId = part.RelationshipId;
                    pictures.Add(pic);
                }

                var worksheetDrawings = drawingPart.WorksheetDrawing.Where(c => c.ChildElements.Any
                    (a => a.GetType().FullName == "DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture")).ToList();
                foreach (var worksheetDrawing in worksheetDrawings)
                {
                    if (worksheetDrawing.GetType().FullName ==
                        "DocumentFormat.OpenXml.Drawing.Spreadsheet.TwoCellAnchor")
                    {
                        TwoCellAnchor anchor = (TwoCellAnchor)worksheetDrawing;
                        DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture picDef =
                            (DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture)
                            anchor.ChildElements.FirstOrDefault(c => c.GetType().FullName ==
                            "DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture");
                        if (picDef != null)
                        {
                            var embed = picDef.BlipFill.Blip.Embed;
                            if (embed != null)
                            {
                                var picMapping = pictures.FirstOrDefault(c => c.RefId == embed.InnerText);
                                picMapping.FromCol = int.Parse(anchor.FromMarker.ColumnId.InnerText);
                                picMapping.FromRow = int.Parse(anchor.FromMarker.RowId.InnerText);
                            }
                        }
                    }
                }
            }
            return pictures;
        }

        private byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 读取Excel数据
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="sharedStringTable"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        private DataTable ReadExcelData(List<Row> rows, SharedStringTablePart sharedStringTable, string sheetName)
        {
            var dt = new DataTable();
            dt.TableName = sheetName;
            if (string.IsNullOrEmpty(sheetName))
                throw new ArgumentNullException("Excel的工作簿名称为空!");
            if (DicCellName == null)
                DicCellName = new Dictionary<string, List<string>>();

            if (!DicCellName.ContainsKey(sheetName))
            {
                DicCellName.Add(sheetName, GetCellNames(rows, Sheets.Where(w => w.SheetName == sheetName).FirstOrDefault().RowIndex));
                if (DicCellName[sheetName] == null || DicCellName[sheetName].Count < 1)
                    return dt;
            }
            foreach (var cell in DicCellName[sheetName])
            {
                dt.Columns.Add(cell, typeof(string));
            }
            for (var i = 0; i < rows.Where(x => x.RowIndex.Value > 3).GetRowsCount(); i++)
            {
                var row = dt.NewRow();
                int rowIndex = 4 + i;
                var cells = rows.GetCells(rowIndex);
                foreach (var cellName in DicCellName[sheetName])
                {
                    row[cellName] = cells.GetCellValue(cellName + rowIndex, sharedStringTable);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// 获取单元格数据
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="cellName">列名</param>
        /// <returns></returns>
        public string GetCellValue(int rowIndex, string cellName, string sheetName = null)
        {
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = DicRows.Keys.FirstOrDefault();
                if (string.IsNullOrEmpty(sheetName))
                    throw new ArgumentNullException("Excel的工作簿名称为空!");
            }
            List<Row> rows = DicRows[sheetName];
            SharedStringTablePart sharedStringTable = DicSharedStringTables[sheetName];
            return rows.GetCells(rowIndex).GetCellValue(cellName, sharedStringTable);
        }

        /// <summary>
        /// 获取列表数据列索引
        /// </summary>
        /// <returns></returns>
        private List<string> GetCellNames(List<Row> rows, int rowIndex)
        {
            List<string> listCellName = new List<string>();
            var cells = rows.GetCells(rowIndex);
            foreach (var cell in cells)
            {
                string cellName = ((DocumentFormat.OpenXml.Spreadsheet.CellType)(cell)).CellReference.Value;
                listCellName.Add(cellName.Replace(rowIndex.ToString(), string.Empty));
            }
            return listCellName;
        }
        #endregion

        #region 导出数据
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="filePath">导出的文件路径</param>
        /// <param name="fileTemplatePath">模板路径</param>
        /// <exception cref="Exception">
        /// </exception>
        public void ExcelExport(DataSet ds, string filePath, string fileTemplatePath)
        {
            try
            {
                System.IO.File.Copy(fileTemplatePath, filePath);
            }
            catch (Exception ex)
            {
                throw new Exception("复制Excel文件出错" + ex.Message);
            }

            using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, true))
            {
                var sheetExs = document.GetWorksheets();
                for (int s = 0; s < sheetExs.Count; s++)
                {
                    if (s >= ds.Tables.Count)
                        break;
                    DataTable dt = ds.Tables[s];
                    if (dt == null || dt.Rows.Count < 1)
                        continue;
                    List<string> listCol = new List<string>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        listCol.Add(dc.ColumnName);
                    }
                    var sheetEx = sheetExs[s];
                    var sheetData = sheetEx.Worksheet.GetFirstSheetData();
                    OpenXml.CellStyleIndex = 1;
                    ////写标题相关信息
                    this.UpdateTitleText(sheetData);

                    int startRowIndex = 0;
                    if (RowIndex != null && RowIndex.Length > 0)
                    {
                        startRowIndex = RowIndex[s];
                    }
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        var rowIndex = startRowIndex + i;
                        DataRow dr = dt.Rows[i];
                        for (int l = 0; l < listCol.Count; l++)
                        {
                            string cellName = IntToMoreChar(l + 1) + rowIndex;
                            sheetData.SetCellValue(cellName, dr[listCol[l]]);
                        }

                    }
                    if (OpenXmlExportImages != null && OpenXmlExportImages.Count > 0 && OpenXmlExportImages.ContainsKey(sheetEx.SheetName))
                    {
                        var exportImages = OpenXmlExportImages[sheetEx.SheetName];
                        if (exportImages != null)
                        {
                            foreach (var img in exportImages)
                            {
                                document.InsertImage(sheetEx.SheetId, img.X, img.Y, img.Width, img.Height, img.ImagePath);
                            }
                        }
                    }
                }
                // var str = OpenXmlHelper.ValidateDocument(document);验证生成的Excel
            }
        }
        #endregion

        /// <summary>
        /// 数字转字母
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string IntToMoreChar(int value)
        {
            string rtn = string.Empty;
            List<int> iList = new List<int>();

            //To single Int
            while (value / 26 != 0 || value % 26 != 0)
            {
                iList.Add(value % 26);
                value /= 26;
            }

            //Change 0 To 26
            for (int j = 0; j < iList.Count - 1; j++)
            {
                if (iList[j] == 0)
                {
                    iList[j + 1] -= 1;
                    iList[j] = 26;
                }
            }
            //Remove 0 at last
            if (iList[iList.Count - 1] == 0)
            {
                iList.Remove(iList[iList.Count - 1]);
            }

            //To String
            for (int j = iList.Count - 1; j >= 0; j--)
            {
                char c = (char)(iList[j] + 64);
                rtn += c.ToString();
            }

            return rtn;
        }

        /// <summary>
        /// 修改标头
        /// </summary>
        /// <param name="sheetData">
        /// The sheet data.
        /// </param>
        private void UpdateTitleText(SheetData sheetData)
        {
            sheetData.UpdateCellText("A1", "技术部员工信息");
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (DicCellName != null)
                DicCellName.Clear();
            if (DicRows != null)
                DicRows.Clear();
            if (DicSharedStringTables != null)
                DicSharedStringTables.Clear();
            if (Sheets != null)
                Sheets.Clear();
        }
    }
}
