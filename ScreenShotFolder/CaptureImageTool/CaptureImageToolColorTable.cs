using System.Drawing;

namespace CaptureTool
{
    public class CaptureImageToolColorTable
    {
        public virtual Color BorderColor { get; } = Color.FromArgb(65, 173, 236);

        public virtual Color BackColorNormal { get; } = Color.FromArgb(229, 243, 251);

        public virtual Color BackColorHover { get; } = Color.FromArgb(65, 173, 236);

        public virtual Color BackColorPressed { get; } = Color.FromArgb(24, 142, 206);

        public virtual Color ForeColor { get; } = Color.FromArgb(12, 83, 124);
    }
}
