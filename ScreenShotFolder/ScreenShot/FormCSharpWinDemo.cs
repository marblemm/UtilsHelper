using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CaptureTool;
using UtilsHelper.HotKey;

namespace ScreenShot
{
    public partial class MainForm : Form
    {
        #region Fileds

        private readonly ProfessionalCaptureImageToolColorTable _colorTable = new ProfessionalCaptureImageToolColorTable();

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            SystemHotKey.RegHotKey(Handle, 100, KeyModifiers.Ctrl | KeyModifiers.Alt, Keys.Z);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            SetVisibleCore(false);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            SystemHotKey.UnRegHotKey(Handle, 100);
        }

        protected override void WndProc(ref Message m)
        {
            const int wmHotKey = 0x0312;//热键消息
            switch (m.Msg)
            {
                case wmHotKey:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:
                            buttonCaptureImage_Click(null, null);
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion

        private void buttonCaptureImage_Click(object sender, EventArgs e)
        {
            if (checkBoxHide.Checked)
            {
                Hide();
                System.Threading.Thread.Sleep(30);
            }
            var capture = new CaptureImageTool();
            if (checkBoxCursor.Checked)
            {
                capture.SelectCursor = CursorManager.ArrowNew;
                capture.DrawCursor = CursorManager.CrossNew;
            }
            else
            {
                capture.SelectCursor = CursorManager.Arrow;
                capture.DrawCursor = CursorManager.Cross;
            }
            if (checkBoxColorTable.Checked)
            {
                capture.ColorTable = _colorTable;
            }

            if (capture.ShowDialog() == DialogResult.OK)
            {
                Image image = capture.Image;
                pictureBox.Width = image.Width;
                pictureBox.Height = image.Height;
                pictureBox.Image = image;
                if (!Visible)
                {
                    Show();
                }
            }

            //if (!Visible)
            //{
            //    Show();
            //}
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
            ShowInTaskbar = false;
            notifyIcon1.Visible = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            Activate();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
            Application.ExitThread();
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxCSharpWinDemo f = new AboutBoxCSharpWinDemo();
            f.ShowDialog();
        }
    }
}