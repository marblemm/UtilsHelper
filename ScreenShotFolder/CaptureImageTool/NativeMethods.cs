using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CaptureTool
{
    internal class NativeMethods
    {
        public const int WsExTransparent = 0x00000020;

        [DllImport("user32.dll")]
        public static extern bool ClipCursor(ref RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hDc);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(Rectangle rect)
            {
                Left = rect.Left;
                Top = rect.Top;
                Right = rect.Right;
                Bottom = rect.Bottom;
            }

            public Rectangle Rect
            {
                get
                {
                    return new Rectangle(
                        Left,
                        Top,
                        Right - Left,
                        Bottom - Top);
                }
            }

            public Size Size
            {
                get
                {
                    return new Size(Right - Left, Bottom - Top);
                }
            }

            public static RECT FromXywh(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            public static RECT FromRectangle(Rectangle rect)
            {
                return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }
        }

        public enum TernaryRasterOperations
        {
            Srccopy = 0x00CC0020, /* dest = source*/
            Srcpaint = 0x00EE0086, /* dest = source OR dest*/
            Srcand = 0x008800C6, /* dest = source AND dest*/
            Srcinvert = 0x00660046, /* dest = source XOR dest*/
            Srcerase = 0x00440328, /* dest = source AND (NOT dest )*/
            Notsrccopy = 0x00330008, /* dest = (NOT source)*/
            Notsrcerase = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */
            Mergecopy = 0x00C000CA, /* dest = (source AND pattern)*/
            Mergepaint = 0x00BB0226, /* dest = (NOT source) OR dest*/
            Patcopy = 0x00F00021, /* dest = pattern*/
            Patpaint = 0x00FB0A09, /* dest = DPSnoo*/
            Patinvert = 0x005A0049, /* dest = pattern XOR dest*/
            Dstinvert = 0x00550009, /* dest = (NOT dest)*/
            Blackness = 0x00000042, /* dest = BLACK*/
            Whiteness = 0x00FF0062 /* dest = WHITE*/
        }
    }
}
