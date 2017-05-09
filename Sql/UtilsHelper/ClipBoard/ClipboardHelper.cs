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
