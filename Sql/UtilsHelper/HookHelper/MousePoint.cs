using System.Drawing;

namespace UtilsHelper.HookHelper
{
    /// <summary>
    /// And X, Y point on the screen
    /// </summary>
    public struct MousePoint
    {
        public MousePoint(Point p)
        {
            X = p.X;
            Y = p.Y;
        }
        public int X;
        public int Y;
        public static implicit operator Point(MousePoint p)
        {
            return new Point(p.X, p.Y);
        }
    }
}