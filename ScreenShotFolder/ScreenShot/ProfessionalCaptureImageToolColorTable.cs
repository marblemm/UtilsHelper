using System.Drawing;
using CaptureTool;

namespace ScreenShot
{
    public class ProfessionalCaptureImageToolColorTable : CaptureImageToolColorTable
    {
        public override Color BorderColor { get; } = Color.FromArgb(106, 255, 34);

        public override Color BackColorNormal { get; } = Color.FromArgb(221, 255, 205);

        public override Color BackColorHover { get; } = Color.FromArgb(106, 255, 34);

        public override Color BackColorPressed { get; } = Color.FromArgb(74, 226, 0);

        public override Color ForeColor { get; } = Color.FromArgb(41, 126, 0);
    }
}
