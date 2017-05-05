using System;

namespace UtilsHelper.HotKey
{
    /// <summary>
    /// 辅助键名称。
    /// Alt, Ctrl, Shift, WindowsKey
    /// </summary>
    [Flags]
    public enum KeyModifiers
    {
        None = 0,
        Alt = 1,
        Ctrl = 2,
        Shift = 4,
        WindowsKey = 8
    }
}