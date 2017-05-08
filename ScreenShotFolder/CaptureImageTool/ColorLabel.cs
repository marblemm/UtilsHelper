using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CaptureTool
{
    public class ColorLabel : Control
    {
        #region Fields

        private Color _borderColor = Color.FromArgb(65, 173, 236);

        #endregion

        #region Constructors

        public ColorLabel()
        {
            SetStyles();
        }

        #endregion

        #region Properties

        [DefaultValue(typeof(Color), "65, 173, 236")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        protected override Size DefaultSize
        {
            get { return new Size(16, 16); }
        }

        #endregion

        #region Private Methods

        private void SetStyles()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            UpdateStyles();
        }

        #endregion

        #region OverideMethods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Rectangle rect = ClientRectangle;
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                g.FillRectangle(brush, rect);
            }

            ControlPaint.DrawBorder(g, rect, _borderColor, ButtonBorderStyle.Solid);

            rect.Inflate(-1, -1);
            ControlPaint.DrawBorder(g, rect, Color.White, ButtonBorderStyle.Solid);
        }

        #endregion
    }
}
