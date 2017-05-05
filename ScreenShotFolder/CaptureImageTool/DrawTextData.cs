using System.Drawing;

namespace CaptureTool
{
    internal class DrawTextData
    {
        public DrawTextData() { }

        public DrawTextData(string text, Font font, Rectangle textRect) 
        {
            Text = text;
            Font = font;
            TextRect = textRect;
        }

        public string Text { get; set; }

        public Font Font { get; set; }

        public Rectangle TextRect { get; set; }

        public bool Completed { get; set; }
    }
}
