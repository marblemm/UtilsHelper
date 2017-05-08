using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace UtilsHelper.WindowsApiHelper
{
    public class WindowsApiHelper
    {
        public Cursor CreateCursor(Bitmap cursor, Point hotPoint)
        {
            int hotX = hotPoint.X;
            int hotY = hotPoint.Y;
            using (Bitmap cursorBmp = new Bitmap(cursor.Width * 2 - hotX, cursor.Height * 2 - hotY, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(cursorBmp))
                {
                    g.Clear(Color.FromArgb(0, 0, 0, 0));
                    g.DrawImage(cursor, cursor.Width - hotX, cursor.Height - hotY, cursor.Width, cursor.Height);
                    g.Flush();
                }
                return new Cursor(cursorBmp.GetHicon());
            }
        }

        public static Cursor CreateCursor(string fileName)
        {
            IntPtr cursorHandle = WindowsApi.LoadCursorFromFile(fileName);
            return new Cursor(cursorHandle);
        }
    }
}
