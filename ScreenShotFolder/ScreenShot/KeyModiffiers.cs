using System;

namespace ScreenShot
{
    [Flags()]
    public enum KeyModiffiers
    {
        None = 0,
        Alt = 1,
        Ctrl = 2,
        Shift = 4,
        WindowsKey = 8,
    }
}