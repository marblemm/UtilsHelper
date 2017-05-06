// <copyright file="Class1.cs" company="zondy">
//		Copyright (c) Zondy. All rights reserved.
// </copyright>
// <author>Administrator</author>
// <date>2017/5/5 18:56:17</date>
// <summary>文件功能描述</summary>
// <modify>
//		修改人:		
//		修改时间:	
//		修改描述:	
//		版本: 1.0	
// </modify>

using System.Drawing;
using System.Windows.Forms;

namespace UtilsHelper.ClipBoard
{
    class ClipboardHelper
    {
        //复制到剪切板
        public void CopyToClipboard(object data)
        {
            Clipboard.SetDataObject(data);
        }

        //粘贴图像
        public Image PasteImage()
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData != null && iData.GetDataPresent(DataFormats.Bitmap))
            {
                return (Bitmap)iData.GetData(DataFormats.Bitmap);
            }
            return null;
        }

        public void ClearClipboard()
        {
            Clipboard.Clear();
        }

        public string PasterText()
        {
            IDataObject iData = Clipboard.GetDataObject();
            // Determines whether the data is in a format you can use.
            if (iData != null && iData.GetDataPresent(DataFormats.Text))
            {
                return (string) iData.GetData(DataFormats.Text);
            }
            return null;
        }
    }
}
