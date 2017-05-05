using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CaptureTool
{
    internal static class RegionHelper
    {
        public static void CreateRegion(Control control, Rectangle rect)
        {
            using (GraphicsPath path =
                GraphicsPathHelper.CreatePath(rect, 8, RoundStyle.All, false))
            {
                if (control.Region != null)
                {
                    control.Region.Dispose();
                }
                control.Region = new Region(path);
            }
        }
    }
}
