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
namespace Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Drawing.Spreadsheet;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using DocumentFormat.OpenXml.Validation;

    /// <summary>
    /// 其于OpenXml SDK写的帮助类
    /// </summary>
    internal static class OpenXml
    {
        /// <summary>
        /// 单元格样式
        /// </summary>
        public static uint CellStyleIndex { get; set; }

        /// <summary>
        /// 获取Worksheet
        /// </summary>
        /// <param name="document">document对象</param>
        /// <param name="sheetName">sheetName可空</param>
        /// <returns>Worksheet对象</returns>
        public static SheetEx GetWorksheet(this SpreadsheetDocument document, string sheetName = null)
        {
            var sheets = document.WorkbookPart.Workbook.Descendants<Sheet>();
            var sheet = (sheetName == null
                                  ? sheets.FirstOrDefault()
                                  : sheets.FirstOrDefault(s => s.Name == sheetName)) ?? sheets.FirstOrDefault();
            var worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id);
            SheetEx sheetEx = new SheetEx();
            sheetEx.Worksheet = worksheetPart.Worksheet;
            sheetEx.SheetId = sheet.Id;
            sheetEx.SheetName = sheet.Name;
            return sheetEx;
        }

        /// <summary>
        /// 获取Worksheet
        /// </summary>
        /// <param name="document">document对象</param>
        /// <param name="sheetName">sheetName可空</param>
        /// <returns>Worksheet对象</returns>
        public static List<SheetEx> GetWorksheets(this SpreadsheetDocument document)
        {
            List<SheetEx> list = new List<SheetEx>();
            var sheets = document.WorkbookPart.Workbook.Descendants<Sheet>();
            foreach (var sheet in sheets)
            {
                var worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id);
                SheetEx sheetEx = new SheetEx();
                sheetEx.Worksheet = worksheetPart.Worksheet;
                sheetEx.SheetId = sheet.Id;
                sheetEx.SheetName = sheet.Name;
                list.Add(sheetEx);
            }
            return list;
        }

        /// <summary>
        /// 获取第一个SheetData
        /// </summary>
        /// <param name="document">SpreadsheetDocument对象</param>
        /// <param name="sheetName">sheetName可为空</param>
        /// <returns>SheetData对象</returns>
        public static SheetData GetFirstSheetData(this SpreadsheetDocument document, string sheetName = null)
        {
            return document.GetWorksheet(sheetName).Worksheet.GetFirstChild<SheetData>();
        }

        /// <summary>
        /// 获取第一个SheetData
        /// </summary>
        /// <param name="worksheet">Worksheet对象</param>
        /// <returns>SheetData对象</returns>
        public static SheetData GetFirstSheetData(this Worksheet worksheet)
        {
            return worksheet.GetFirstChild<SheetData>();
        }

        /// <summary>
        /// 获了共享字符的表格对象
        /// </summary>
        /// <param name="document">SpreadsheetDocument</param>
        /// <returns>SharedStringTablePart对角</returns>
        public static SharedStringTablePart GetSharedStringTable(this SpreadsheetDocument document)
        {
            var sharedStringTable = document.WorkbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
            return sharedStringTable;
        }

        /// <summary>
        /// 修改单元格的内容.
        /// </summary>
        /// <param name="sheetData">
        /// The sheet data.
        /// </param>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <param name="cellText">
        /// The cell text.
        /// </param>
        public static void UpdateCellText(this SheetData sheetData, string cellName, string cellText)
        {
            var cell = sheetData.GetCell(cellName);
            if (cell == null)
            {
                return;
            }

            cell.UpdateCellText(cellText);
        }

        /// <summary>
        /// 设置单元格的值.
        /// </summary>
        /// <param name="sheetData">
        /// The sheet data.
        /// </param>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <param name="cellText">
        /// The cell text.
        /// </param>
        public static void SetCellValue(this SheetData sheetData, string cellName, object cellText = null)
        {
            SetCellValue(sheetData, cellName, cellText ?? string.Empty, CellStyleIndex);
        }

        /// <summary>
        /// 添加一个单元格
        /// </summary>
        /// <param name="row">Row对象</param>
        /// <param name="cellName">单元格名称</param>
        /// <param name="cellText">单元格文本</param>
        /// <param name="cellStyleIndex">样式</param>
        private static void CreateCell(this Row row, string cellName, object cellText, uint cellStyleIndex)
        {
            var refCell =
                row.Elements<Cell>()
                   .FirstOrDefault(
                       cell =>
                       cell.CellReference.Value.Length >= cellName.Length
                       && string.Compare(cell.CellReference.Value, cellName, StringComparison.OrdinalIgnoreCase) > 0);

            var resultCell = new Cell { CellReference = cellName };
            resultCell.UpdateCell(cellText, cellStyleIndex);
            row.InsertBefore(resultCell, refCell);
        }

        /// <summary>
        /// 设置单元格的值.
        /// </summary>
        /// <param name="sheetData">
        /// The sheet data.
        /// </param>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <param name="cellText">
        /// The cell text.
        /// </param>
        /// <param name="cellStyleIndex">
        /// The cell style index.
        /// </param>
        private static void SetCellValue(this SheetData sheetData, string cellName, object cellText, uint cellStyleIndex)
        {
            uint rowIndex = GetRowIndex(cellName);
            var row = sheetData.GetRow(rowIndex);
            if (row == null)
            {
                row = new Row { RowIndex = rowIndex };
                row.CreateCell(cellName, cellText, cellStyleIndex);
                sheetData.Append(row);
            }
            else
            {
                var cell = row.GetCell(cellName);
                if (cell == null)
                {
                    row.CreateCell(cellName, cellText, cellStyleIndex);
                }
                else
                {
                    cell.UpdateCell(cellText, cellStyleIndex);
                }
            }
        }

        /// <summary>
        /// The get rows count.
        /// </summary>
        /// <param name="rows">
        /// The rows.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetRowsCount(this IEnumerable<Row> rows)
        {
            return rows.GroupBy(x => x.RowIndex.Value).Count();
        }

        /// <summary>
        /// 根据行索引获取单元
        /// </summary>
        /// <param name="rows">
        /// The rows.
        /// </param>
        /// <param name="rowIndex">
        /// The row index.
        /// </param>
        /// <returns>
        /// Cell集合
        /// </returns>
        public static IList<Cell> GetCells(this IEnumerable<Row> rows, int rowIndex)
        {
            return rows.Where(row => row.RowIndex.Value == rowIndex).SelectMany(row => row.Elements<Cell>()).ToList();
        }

        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="cells">
        /// The cells.
        /// </param>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <param name="stringTablePart">
        /// The string table part.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetCellValue(this IEnumerable<Cell> cells, string cellName, SharedStringTablePart stringTablePart)
        {
            if (cells == null)
            {
                throw new ArgumentNullException("cells");
            }

            if (cellName == null)
            {
                throw new ArgumentNullException("cellName");
            }

            var cell = (from item in cells where item.CellReference == cellName select item).FirstOrDefault();
            if (cell == null)
            {
                return string.Empty;
            }

            if (cell.ChildElements.Count == 0)
            {
                return string.Empty;
            }

            var value = cell.CellValue.InnerText;
            if (cell.DataType == null)
            {
                return value;
            }

            switch (cell.DataType.Value)
            {
                case CellValues.SharedString:
                    if (stringTablePart != null)
                    {
                        value = stringTablePart.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                    }

                    break;
                case CellValues.Boolean:
                    value = value == "0" ? "FALSE" : "TRUE";
                    break;
            }

            return value;
        }

        /// <summary>
        /// 验证文档
        /// </summary>
        /// <param name="document">
        /// The document.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ValidateDocument(this SpreadsheetDocument document)
        {
            var msg = new StringBuilder();
            try
            {
                var validator = new OpenXmlValidator();
                int count = 0;
                foreach (ValidationErrorInfo error in validator.Validate(document))
                {
                    count++;
                    msg.Append("\nError " + count)
                       .Append("\nDescription: " + error.Description)
                       .Append("\nErrorType: " + error.ErrorType)
                       .Append("\nNode: " + error.Node)
                       .Append("\nPath: " + error.Path.XPath)
                       .Append("\nPart: " + error.Part.Uri)
                       .Append("\n-------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                msg.Append(ex.Message);
            }

            return msg.ToString();
        }

        /// <summary>
        /// 根据单元格名称获取行索引.
        /// </summary>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        private static uint GetRowIndex(string cellName)
        {
            var regex = new Regex(@"\d+");
            var match = regex.Match(cellName);
            return uint.Parse(match.Value);
        }

        /// <summary>
        /// The get cell data type.
        /// </summary>
        /// <param name="cellText">
        /// The cell text.
        /// </param>
        /// <returns>
        /// The <see cref="CellValues"/>.
        /// </returns>
        private static CellValues GetCellDataType(object cellText)
        {
            var type = cellText.GetType();
            switch (type.Name)
            {
                case "Int32":
                case "Decimal":
                case "Double":
                case "Int64":
                    return CellValues.Number;
                case "String":
                    return CellValues.String;
                ////            case "DateTime":
                ////                return CellValues.Date;
                default:
                    return CellValues.String;
            }
        }

        /// <summary>
        /// 修改单元格内容（文本、样式）                                                                                                                                                                                                                                            
        /// </summary>
        /// <param name="cell">
        /// The cell.
        /// </param>
        /// <param name="cellText">
        /// The cell text.
        /// </param>
        /// <param name="cellStyleIndex">
        /// The cell style index.
        /// </param>
        private static void UpdateCell(this Cell cell, object cellText, uint cellStyleIndex)
        {
            cell.UpdateCellText(cellText);
            cell.StyleIndex = cellStyleIndex;
        }

        /// <summary>
        /// 修改单元格的文本
        /// </summary>
        /// <param name="cell">Cell对象</param>
        /// <param name="cellText">文本字符串</param>
        private static void UpdateCellText(this Cell cell, object cellText)
        {
            cell.DataType = GetCellDataType(cellText);
            cell.CellValue = cell.CellValue ?? new CellValue();
            cell.CellValue.Text = cellText.ToString();
        }

        /// <summary>
        /// 获取行
        /// </summary>
        /// <param name="sheetData">
        /// The sheet data.
        /// </param>
        /// <param name="rowIndex">
        /// The row index.
        /// </param>
        /// <returns>
        /// The <see cref="Row"/>.
        /// </returns>
        private static Row GetRow(this SheetData sheetData, long rowIndex)
        {
            return sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);
        }

        /// <summary>
        /// 获取单元格
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <returns>
        /// The <see cref="Cell"/>.
        /// </returns>
        private static Cell GetCell(this Row row, string cellName)
        {
            return row.Elements<Cell>().FirstOrDefault(c => c.CellReference.Value == cellName);
        }

        /// <summary>
        /// 获取单元格
        /// </summary>
        /// <param name="sheetData">
        /// The sheet data.
        /// </param>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <returns>
        /// The <see cref="Cell"/>.
        /// </returns>
        private static Cell GetCell(this SheetData sheetData, string cellName)
        {
            return sheetData.Descendants<Cell>().FirstOrDefault(c => c.CellReference.Value == cellName);
        }
        
        public static void InsertImage(this SpreadsheetDocument document, string sheet, long x, long y, long? width, long? height, string sImagePath)
        {
            try
            {
                WorksheetPart wsp = (WorksheetPart)document.WorkbookPart.GetPartById(sheet);
                DrawingsPart dp;
                ImagePart imgp;
                WorksheetDrawing wsd;

                ImagePartType ipt;
                switch (sImagePath.Substring(sImagePath.LastIndexOf('.') + 1).ToLower())
                {
                    case "png":
                        ipt = ImagePartType.Png;
                        break;
                    case "jpg":
                    case "jpeg":
                        ipt = ImagePartType.Jpeg;
                        break;
                    case "gif":
                        ipt = ImagePartType.Gif;
                        break;
                    case "bmp":
                        ipt = ImagePartType.Bmp;
                        break;
                    default:
                        return;
                }

                if (wsp.DrawingsPart == null)
                {
                    dp = wsp.AddNewPart<DrawingsPart>();
                    imgp = dp.AddImagePart(ipt, wsp.GetIdOfPart(dp));
                    wsd = new WorksheetDrawing();
                }
                else
                {
                    dp = wsp.DrawingsPart;
                    imgp = dp.AddImagePart(ipt);
                    dp.CreateRelationshipToPart(imgp);
                    wsd = dp.WorksheetDrawing;
                }

                using (FileStream fs = new FileStream(sImagePath, FileMode.Open))
                {
                    imgp.FeedData(fs);
                }

                int imageNumber = dp.ImageParts.Count<ImagePart>();
                if (imageNumber == 1)
                {
                    Drawing drawing = new Drawing();
                    drawing.Id = dp.GetIdOfPart(imgp);
                    wsp.Worksheet.Append(drawing);
                }

                NonVisualDrawingProperties nvdp = new NonVisualDrawingProperties();
                nvdp.Id = new UInt32Value((uint)(1024 + imageNumber));
                nvdp.Name = "Picture " + imageNumber.ToString();
                nvdp.Description = "";
                DocumentFormat.OpenXml.Drawing.PictureLocks picLocks = new DocumentFormat.OpenXml.Drawing.PictureLocks();
                picLocks.NoChangeAspect = true;
                picLocks.NoChangeArrowheads = true;
                NonVisualPictureDrawingProperties nvpdp = new NonVisualPictureDrawingProperties();
                nvpdp.PictureLocks = picLocks;
                NonVisualPictureProperties nvpp = new NonVisualPictureProperties();
                nvpp.NonVisualDrawingProperties = nvdp;
                nvpp.NonVisualPictureDrawingProperties = nvpdp;

                DocumentFormat.OpenXml.Drawing.Stretch stretch = new DocumentFormat.OpenXml.Drawing.Stretch();
                stretch.FillRectangle = new DocumentFormat.OpenXml.Drawing.FillRectangle();

                BlipFill blipFill = new BlipFill();
                DocumentFormat.OpenXml.Drawing.Blip blip = new DocumentFormat.OpenXml.Drawing.Blip();
                blip.Embed = dp.GetIdOfPart(imgp);
                blip.CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print;
                blipFill.Blip = blip;
                blipFill.SourceRectangle = new DocumentFormat.OpenXml.Drawing.SourceRectangle();
                blipFill.Append(stretch);

                DocumentFormat.OpenXml.Drawing.Transform2D t2d = new DocumentFormat.OpenXml.Drawing.Transform2D();
                DocumentFormat.OpenXml.Drawing.Offset offset = new DocumentFormat.OpenXml.Drawing.Offset();
                offset.X = 0;
                offset.Y = 0;
                t2d.Offset = offset;
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(sImagePath);

                DocumentFormat.OpenXml.Drawing.Extents extents = new DocumentFormat.OpenXml.Drawing.Extents();

                if (width == null)
                    extents.Cx = (long)bm.Width * (long)((float)914400 / bm.HorizontalResolution);
                else
                    extents.Cx = width * (long)((float)914400 / bm.HorizontalResolution);

                if (height == null)
                    extents.Cy = (long)bm.Height * (long)((float)914400 / bm.VerticalResolution);
                else
                    extents.Cy = height * (long)((float)914400 / bm.VerticalResolution);

                bm.Dispose();
                t2d.Extents = extents;
                ShapeProperties sp = new ShapeProperties();
                sp.BlackWhiteMode = DocumentFormat.OpenXml.Drawing.BlackWhiteModeValues.Auto;
                sp.Transform2D = t2d;
                DocumentFormat.OpenXml.Drawing.PresetGeometry prstGeom = new DocumentFormat.OpenXml.Drawing.PresetGeometry();
                prstGeom.Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle;
                prstGeom.AdjustValueList = new DocumentFormat.OpenXml.Drawing.AdjustValueList();
                sp.Append(prstGeom);
                sp.Append(new DocumentFormat.OpenXml.Drawing.NoFill());

                DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture picture = new DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture();
                picture.NonVisualPictureProperties = nvpp;
                picture.BlipFill = blipFill;
                picture.ShapeProperties = sp;

                Position pos = new Position();
                pos.X = x * 914400 / 72;
                pos.Y = y * 914400 / 72;
                Extent ext = new Extent();
                ext.Cx = extents.Cx;
                ext.Cy = extents.Cy;
                AbsoluteAnchor anchor = new AbsoluteAnchor();
                anchor.Position = pos;
                anchor.Extent = ext;
                anchor.Append(picture);
                anchor.Append(new ClientData());
                wsd.Append(anchor);
                wsd.Save(dp);
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
    }

    /// <summary>
    /// 工作区
    /// </summary>
    public class SheetEx
    {
        /// <summary>
        /// 工作区所属的Sheet的ID
        /// </summary>
        public string SheetId { get; set; }

        /// <summary>
        /// 工作区所属的Sheet的名称
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// 工作区起始行
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// 工作区
        /// </summary>
        public Worksheet Worksheet { get; set; }
    }
}
