/********************************************************************************************
 * 文件名称:	OpenXmlImages
 * 设计人员:	徐少年[QQ:78018999 email:xushaonian@qq.com]
 * 设计时间:	2013-10-16 15:59:02
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
using System.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// 导入
    /// </summary>
    public class OpenXmlImportImages
    {
        public string RefId { get; set; }
        /// <summary>
        /// 列
        /// </summary>
        public int FromRow { get; set; }
        /// <summary>
        /// 行
        /// </summary>
        public int FromCol { get; set; }
        public byte[] Image { get; set; }
    }

    /// <summary>
    /// 导出
    /// </summary>
    public class OpenXmlExportImages
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public long X { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public long Y { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public long? Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public long? Height { get; set; }

        /// <summary>
        /// 图片路径如c:\eee.png
        /// </summary>
        public string ImagePath { get; set; }
    }
}
