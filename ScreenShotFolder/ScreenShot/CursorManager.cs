using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UtilsHelper.WindowsApiHelper;

namespace ScreenShot
{
    internal class CursorManager
    {
        public static readonly Cursor Arrow = WindowsApiHelper.CreateCursor(@"..\..\Cursors\Arrow.cur");

        public static readonly Cursor Cross = WindowsApiHelper.CreateCursor(@"..\..\Cursors\Cross.cur");

        public static readonly Cursor ArrowNew = WindowsApiHelper.CreateCursor(@"..\..\Cursors\ArrowNew.cur");

        public static readonly Cursor CrossNew = WindowsApiHelper.CreateCursor(@"..\..\Cursors\CrossNew.cur");

        private CursorManager() { }
    }
}
