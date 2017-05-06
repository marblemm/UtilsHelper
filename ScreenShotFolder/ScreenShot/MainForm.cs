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
        private readonly ProfessionalCaptureImageToolColorTable _colorTable = new ProfessionalCaptureImageToolColorTable();

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            SetVisibleCore(false);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case DefaultMessageValue.WmHotKey:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:
                            buttonCaptureImage_Click(null, null);
                            break;
                    }
                    break;
                case DefaultMessageValue.WmCreate:
                    SystemHotKey.RegHotKey(Handle, 100, KeyModifiers.Ctrl | KeyModifiers.Alt, Keys.Z);
                    break;
                case DefaultMessageValue.WmDestory:
                    SystemHotKey.UnRegHotKey(Handle, 100);
                    break;
                case DefaultMessageValue.WmSyscommand:
                    if (m.WParam.ToInt32() == DefaultMessageValue.ScMinimize)
                    {
                        WindowState = FormWindowState.Minimized;
                        ShowInTaskbar = true;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

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
                Clipboard.SetDataObject(image);
                if (!Visible)
                {
                    Show();
                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        WindowState = FormWindowState.Normal;
                    }
                    //WindowState = FormWindowState.Normal;
                    Activate();
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
            AboutForm f = new AboutForm();
            f.ShowDialog();
        }
    }
}