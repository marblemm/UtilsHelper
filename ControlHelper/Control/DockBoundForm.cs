using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControlHelper.Control
{
    /// <summary>
    /// 窗口停靠隐藏类
    /// 使用方法
    /// private FormDock formDock = null;
    /// formDock = new FormDock(this,300);
    /// </summary>
    public class DockBoundForm
    {
        #region 自定义声明

        /// <summary>
        /// 定义计时器
        /// </summary>
        private readonly Timer _stopRectTimer = new Timer();

        /// <summary>
        /// 贴边设置
        /// </summary>
        internal AnchorStyles StopAanhor = AnchorStyles.None;

        /// <summary> 
        /// 父级窗口实例 
        /// </summary> 
        private readonly Form _parentForm;

        private Point _mTempPoiont; //临时点位置
        private Point _mLastPoint; //窗体最小化前的坐标点位置

        //控制是否显示一点点边界
        private const int ShowWidthPix = 9;
        private const int HideWidthPix = 3;
        private bool _showWidth;
        private int _curRemoveWidth;

        public void SetShowWidth(bool b)
        {
            _showWidth = b;
            GetRemoveWidth();
        }

        private void GetRemoveWidth()
        {
            if (_showWidth)
            {
                _curRemoveWidth = HideWidthPix;
            }
            else
            {
                _curRemoveWidth = ShowWidthPix;
            }
        }

        #endregion

        /// <summary>
        /// 自动停靠 
        /// </summary>
        /// <param name="frmParent">父窗口对象</param>
        /// <param name="trimInterval">时钟周期</param>
        public DockBoundForm(Form frmParent, int trimInterval = 500)
        {
            _parentForm = frmParent;
            _parentForm.LocationChanged += parentForm_LocationChanged;
            _stopRectTimer.Tick += timer1_Tick; //注册事件
            _stopRectTimer.Interval = trimInterval; //计时器执行周期
            _stopRectTimer.Start(); //计时器开始执行
            GetRemoveWidth();
        }

        /// <summary>
        /// 时钟的开始
        /// </summary>
        public void TimerStart()
        {
            _stopRectTimer.Start();
        }

        /// <summary>
        /// 时钟的停止
        /// </summary>
        public void TimerStop()
        {
            _stopRectTimer.Stop();
        }

        #region 窗口位置改变事件

        /// <summary>
        /// 窗口位置改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parentForm_LocationChanged(object sender, EventArgs e)
        {
            if (_parentForm.Location.X == -32000 && _parentForm.Location.Y == -32000)
            {
                _mLastPoint = _mTempPoiont; //最小化了，m_LastPoint就是最小化前的位置。
            }
            else
            {
                _mTempPoiont = _parentForm.Location;
            }

            MStopAnthor();
        }

        #endregion

        #region 计时器 周期事件

        /// <summary>
        /// 计时器 周期事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            var curCurPos = Cursor.Position;
            if (_parentForm.Bounds.Contains(curCurPos))
            {
                FormShow();
            }
            else
            {
                FormHide();
            }
        }

        #endregion

        #region 窗口停靠位置计算

        /// <summary>
        /// 窗口停靠位置计算
        /// </summary>
        private void MStopAnthor()
        {
            if (_parentForm.Top <= 0)
            {
                StopAanhor = AnchorStyles.Top;
            }
            else if (_parentForm.Left <= 0)
            {
                StopAanhor = AnchorStyles.Left;
            }
            else if (_parentForm.Left >= Screen.PrimaryScreen.Bounds.Width - _parentForm.Width)
            {
                StopAanhor = AnchorStyles.Right;
            }
            else
            {
                StopAanhor = AnchorStyles.None;
            }
        }

        #endregion

        #region 窗体不贴边显示

        /// <summary>
        /// 窗体不贴边显示
        /// </summary>
        public void FormShow()
        {
            switch (StopAanhor)
            {
                case AnchorStyles.Top:
                    _parentForm.Location = new Point(_parentForm.Location.X, 0);
                    break;
                case AnchorStyles.Left:
                    _parentForm.Location = new Point(0, _parentForm.Location.Y);
                    break;
                case AnchorStyles.Right:
                    _parentForm.Location = new Point(Screen.PrimaryScreen.Bounds.Width - _parentForm.Width, _parentForm.Location.Y);
                    break;
            }
        }

        #endregion

        #region 窗体贴边隐藏

        /// <summary>
        /// 窗体贴边隐藏
        /// </summary>
        private void FormHide()
        {
            switch (StopAanhor)
            {
                case AnchorStyles.Top:
                    if (_parentForm.WindowState == FormWindowState.Minimized)
                    {
                        _parentForm.Location = _mLastPoint;
                        break;
                    }
                    _parentForm.Location = new Point(_parentForm.Location.X, (_parentForm.Height - _curRemoveWidth) * (-1));
                    break;
                case AnchorStyles.Left:
                    _parentForm.Location = new Point((-1) * (_parentForm.Width - _curRemoveWidth), _parentForm.Location.Y);
                    break;
                case AnchorStyles.Right:
                    _parentForm.Location = new Point(Screen.PrimaryScreen.Bounds.Width - _curRemoveWidth, _parentForm.Location.Y);
                    break;
            }
        }
    }
}

#endregion