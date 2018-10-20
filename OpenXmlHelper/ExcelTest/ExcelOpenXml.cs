using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Data;
using System.Linq;

namespace ExcelExport
{
    public class ExcelOpenXml
    {
        /*
         * excel 对象结构
         * SpreadsheetDocument
         *   》WorkbookPart
	     *       》WorksheetPart
		 *           》Worksheet
		 *            》SheetData
	     *       》WorksheetPart
		 *          》Worksheet
		 *	            》SheetData1
	     *       》Workbook
		 *           》Sheets
		 *	            》Sheet
         */
        public static void Create(string filename, DataSet ds)
        {
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filename, SpreadsheetDocumentType.Workbook);

            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            Workbook workbook = new Workbook();
            Sheets sheets = new Sheets();

            #region 创建多个 sheet 页

            //创建多个sheet
            for (int s = 0; s < ds.Tables.Count; s++)
            {
                DataTable dt = ds.Tables[s];
                var tname = dt.TableName;

                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                Worksheet worksheet = new Worksheet();
                SheetData sheetData = new SheetData();

                //创建 sheet 页
                Sheet sheet = new Sheet()
                {
                    //页面关联的 WorksheetPart
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = UInt32Value.FromUInt32((uint)s + 1),
                    Name = tname
                };
                sheets.Append(sheet);

                #region 创建sheet 行
                Row row;
                uint rowIndex = 1;
                //添加表头
                row = new Row()
                {
                    RowIndex = UInt32Value.FromUInt32(rowIndex++)
                };
                sheetData.Append(row);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Cell newCell = new Cell();
                    newCell.CellValue = new CellValue(dt.Columns[i].ColumnName);
                    newCell.DataType = new EnumValue<CellValues>(CellValues.String);
                    row.Append(newCell);
                }
                //添加内容
                object val = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = new Row()
                    {
                        RowIndex = UInt32Value.FromUInt32(rowIndex++)
                    };
                    sheetData.Append(row);

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Cell newCell = new Cell();
                        val = dt.Rows[i][j];
                        newCell.CellValue = new CellValue(val.ToString());
                        newCell.DataType = new EnumValue<CellValues>(CellValues.String);

                        row.Append(newCell);
                    }

                }
                #endregion

                worksheet.Append(sheetData);
                worksheetPart.Worksheet = worksheet;
                worksheetPart.Worksheet.Save();
            }
            #endregion

            workbook.Append(sheets);
            workbookpart.Workbook = workbook;

            workbookpart.Workbook.Save();
            spreadsheetDocument.Close();
        }

        public static DataTable GetSheet(string filename, string sheetName)
        {
            DataTable dt = new DataTable();
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(filename, false))
            {
                WorkbookPart wbPart = document.WorkbookPart;
                //通过sheet名查找 sheet页
                Sheet sheet = wbPart
                    .Workbook
                    .Descendants<Sheet>()
                    .Where(s => s.Name == sheetName)
                    .FirstOrDefault();

                if (sheet == null)
                {
                    throw new ArgumentException("未能找到" + sheetName + " sheet 页");
                }

                //获取Excel中共享表
                SharedStringTablePart sharedStringTablePart = wbPart
                    .GetPartsOfType<SharedStringTablePart>()
                    .FirstOrDefault();
                SharedStringTable sharedStringTable = null;
                if (sharedStringTablePart != null)
                    sharedStringTable = sharedStringTablePart.SharedStringTable;
                #region 构建datatable

                //添加talbe列,返回列数
                Func<Row, int> addTabColumn = (r) =>
                {
                    //遍历单元格
                    foreach (Cell c in r.Elements<Cell>())
                    {
                        dt.Columns.Add(GetCellVal(c, sharedStringTable));
                    }
                    return dt.Columns.Count;
                };
                //添加行
                Action<Row> addTabRow = (r) =>
                {
                    DataRow dr = dt.NewRow();
                    int colIndex = 0;
                    int colCount = dt.Columns.Count;
                    //遍历单元格
                    foreach (Cell c in r.Elements<Cell>())
                    {
                        if (colIndex >= colCount)
                            break;
                        dr[colIndex++] = GetCellVal(c, sharedStringTable);
                    }
                    dt.Rows.Add(dr);
                };
                #endregion


                //通过 sheet.id 查找 WorksheetPart 
                WorksheetPart worksheetPart
                    = wbPart.GetPartById(sheet.Id) as WorksheetPart;
                //查找 sheetdata
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                //遍历行
                foreach (Row r in sheetData.Elements<Row>())
                {
                    //构建table列
                    if (r.RowIndex == 1)
                    {
                        addTabColumn(r);
                        continue;
                    }
                    //构建table行
                    addTabRow(r);
                }

            }
            return dt;
        }

        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="sharedStringTable"></param>
        /// <returns></returns>
        static string GetCellVal(Cell cell, SharedStringTable sharedStringTable)
        {
            var val = cell.InnerText;

            if (cell.DataType != null)
            {
                switch (cell.DataType.Value)
                {
                    //从共享表中获取值
                    case CellValues.SharedString:
                        if (sharedStringTable != null)
                            val = sharedStringTable
                                .ElementAt(int.Parse(val))
                                .InnerText;
                        break;
                    default:
                        val = string.Empty;
                        break;
                }

            }
            return val;
        }
    }
}
