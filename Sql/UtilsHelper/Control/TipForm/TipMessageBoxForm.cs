using System;
using System.Windows.Forms;
using UtilsHelper.WindowsApiHelper;

namespace UtilsHelper.Control.TipForm
{
    public partial class TipMessageBoxForm : Form
    {
        public TipMessageBoxForm()
        {
            InitializeComponent();
            Load += FormMessageBox_Load;
            FormClosed += FormMessageBox_FormClosed;
            MouseMove += FormMessageBox_MouseMove;
            MouseLeave += FormMessageBox_MouseLeave;
        }

        private void FormMessageBox_MouseLeave(object sender, EventArgs e)
        {
            plShow_MouseLeave(sender, e);
        }

        private void FormMessageBox_MouseMove(object sender, MouseEventArgs e)
        {
            plShow_MouseMove(sender, e);
        }

        /// <summary>  
        /// 窗体加载模式  
        /// </summary>  
        private static LoadMode _formMode = LoadMode.Prompt;

        private static string _showMessage = null;
        //private Label _lblTitle;

        /// <summary>  
        /// 关闭窗口的定时器  
        /// </summary>  
        private readonly Timer _timerClose = new Timer();

        /// <summary>  
        /// 构造方法  
        /// </summary>  
        /// <param name="loadMode">加载模式</param>  
        /// <param name="message">消息正文</param>  

        public static void Show(LoadMode loadMode, string message)
        {
            _formMode = loadMode;
            _showMessage = message;
            TipMessageBoxForm box = new TipMessageBoxForm();
            box.Show();
        }

        /// <summary>  
        /// 窗体加载事件  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void FormMessageBox_Load(object sender, EventArgs e)
        {
            _lblTitle.Text = "提示";
            if (_formMode == LoadMode.Error)
            {
                _lblTitle.Text = "错误";
                //this.plShow.BackgroundImage = global::CommonApp.Properties.Resources.error;    // 更换背景  
            }
            else if (_formMode == LoadMode.Warning)
            {
                _lblTitle.Text = "警告";
                //this.plShow.BackgroundImage = global::CommonApp.Properties.Resources.warning;  // 更换背景  
            }
            else
            {
                //this.plShow.BackgroundImage = global::CommonApp.Properties.Resources.Prompt;   // 更换背景  
            }
            //this.lblMessage.Text = ShowMessage;
            int width = Screen.PrimaryScreen.Bounds.Width;
            int height = Screen.PrimaryScreen.Bounds.Height;
            int top = height - 35 - Height;
            int left = width - Width - 5;
            Top = top;
            Left = left;
            TopMost = true;
            WindowsApi.AnimateWindow(Handle, 500, WindowsApi.AnimateWindowState.AwSlide + WindowsApi.AnimateWindowState.AwVerNegative);//开始窗体动画  
            ShowInTaskbar = false;
            _timerClose.Interval = 4000;
            _timerClose.Tick += new EventHandler(Timer_Close_Tick);
            _timerClose.Start();
        }

        /// <summary>  
        /// 关闭窗口的定时器响应事件  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void Timer_Close_Tick(object sender, EventArgs e)
        {
            _timerClose.Stop();
            Close();
        }

        /// <summary>  
        /// 窗口已经关闭  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void FormMessageBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            WindowsApi.AnimateWindow(Handle, 1000, WindowsApi.AnimateWindowState.AwSlide + WindowsApi.AnimateWindowState.AwVerPositive + WindowsApi.AnimateWindowState.AwHide);


            _timerClose.Stop();
            _timerClose.Dispose();
        }


        /// <summary>  
        /// 鼠标移动在消息框上  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void plShow_MouseMove(object sender, MouseEventArgs e)
        {
            _timerClose.Stop();
        }


        /// <summary>  
        /// 鼠标移动离开消息框上  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void plShow_MouseLeave(object sender, EventArgs e)
        {
            _timerClose.Start();
        }
    }
}