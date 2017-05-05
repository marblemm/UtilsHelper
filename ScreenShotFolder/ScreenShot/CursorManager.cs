using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScreenShot
{
    internal class CursorManager
    {
        public static readonly Cursor Arrow = CreateCursor(@"..\..\Cursors\Arrow.cur");

        public static readonly Cursor Cross = CreateCursor(@"..\..\Cursors\Cross.cur");

        public static readonly Cursor ArrowNew = CreateCursor(@"..\..\Cursors\ArrowNew.cur");

        public static readonly Cursor CrossNew = CreateCursor(@"..\..\Cursors\CrossNew.cur");

        private CursorManager() { }

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
            IntPtr cursorHandle = LoadCursorFromFile(fileName);
            return new Cursor(cursorHandle);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr LoadCursorFromFile(string fileName);

        [DllImport("user32.dll")]
        private static extern uint DestroyCursor(IntPtr cursorHandle);
    }
}
