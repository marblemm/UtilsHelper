﻿using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace UtilsHelper.Control.TipForm
{

    /*调用实例
                 string afTitle = txtTitle.Text;
            string afContent = txtContent.Text;
            if (string.IsNullOrEmpty(afContent))
            {
                MessageBox.Show("内容不能为空"); return;
            }
            AlertLib.AlertForm.ShowWay showWay = AlertLib.AlertForm.ShowWay.UpDown;
            if (cbShowWay.SelectedIndex == 1) showWay = AlertLib.AlertForm.ShowWay.Fade;


            int afShowInTime, afShowTime, afShowOutTime;
            int afWidth, afHeigth;

            int.TryParse(txtShowInTime.Text, out afShowInTime);
            int.TryParse(txtShowTime.Text, out afShowTime);
            int.TryParse(txtShowOutTime.Text, out afShowOutTime);
            int.TryParse(txtWidth.Text, out afWidth);
            int.TryParse(txtHeigth.Text, out afHeigth);

            //if (rbOnly.Checked && af != null)
            //{
            //    af.Dispose();
            //}

            //宽度 400， 高度180， 出现时间 200ms, 停留时间3000，消失时间500

            var af = new AlertLib.AlertForm();
            af.Show(afContent, afTitle, showWay,afWidth,afHeigth,afShowInTime,afShowTime,afShowOutTime);
     */
    public partial class AlertForm : Form
    {
        private bool _start = true;
        private int _showYPoint;
        private int _showXPoint;
        private ShowWay _showWay = ShowWay.UpDown;
        private int _showTime = 3000;//展示时间,单位毫秒
        private int _showInTime = 200;//出现时间,单位毫秒
        private int _showOutTime = 500;//消失时间,单位毫秒
        private readonly Timer _timer = new Timer();
        private int _timerRunCount;
        private int _maxTimerRunCount;

        /// <summary>
        /// 展示方式枚举
        /// </summary>
        public enum ShowWay
        {
            UpDown,//上升下降
            Fade //淡出淡入
        }

        public AlertForm()
        {
            InitializeComponent();
        }

        public static void ShowForm(params object[] paramsList)
        {
            var frm = new AlertForm();
            frm.Show(paramsList);
        }

        /// <summary>
        /// 显示提示窗口
        /// </summary>
        /// <param name="paramsList">
        /// params[0]:内容
        /// params[1]:标题
        /// params[2]:显示方式
        /// params[3]:宽度
        /// params[4]:高度
        /// params[5]:出现时间
        /// params[6]:停留时间
        /// params[7]:消失时间
        /// </param>
        public void Show(params object[] paramsList)
        {
            if (paramsList == null || paramsList.Length == 0) return;
            string afContent = paramsList[0] != null ? paramsList[0].ToString() : "";
            string afTitle = (paramsList.Length > 1 && paramsList[1] != null) ? paramsList[1].ToString().Length < 20 ?
                paramsList[1].ToString() : paramsList[1].ToString().Substring(0, 20) + "..." : "";
            ShowWay afShowWay = (paramsList.Length > 2 && paramsList[2] != null && paramsList[2] is ShowWay) ? (ShowWay)paramsList[2] : ShowWay.UpDown;
            int afWidth = 0, afHeigth = 0;
            int afshowInTime = 200, afshowTime = 3000, afshowOutTime = 500;

            if (paramsList.Length > 3 && paramsList[3] != null) int.TryParse(paramsList[3].ToString(), out afWidth);
            else afWidth = Width;
            if (paramsList.Length > 4 && paramsList[4] != null) int.TryParse(paramsList[4].ToString(), out afHeigth);
            else afHeigth = Height;
            if (paramsList.Length > 5 && paramsList[5] != null) int.TryParse(paramsList[5].ToString(), out afshowInTime);
            else afshowInTime = _showInTime;
            if (paramsList.Length > 6 && paramsList[6] != null) int.TryParse(paramsList[6].ToString(), out afshowTime);
            else afshowTime = _showTime;
            if (paramsList.Length > 7 && paramsList[7] != null) int.TryParse(paramsList[7].ToString(), out afshowOutTime);
            else afshowTime = _showOutTime;

            afWidth = afWidth > 0 ? afWidth : Width;
            afHeigth = afHeigth > 0 ? afHeigth : Height;
            afshowInTime = afshowInTime > 0 ? afshowInTime : _showInTime;
            afshowTime = afshowTime > 0 ? afshowTime : _showTime;
            afshowOutTime = afshowOutTime > 0 ? afshowOutTime : _showOutTime;

            Show(afContent, afTitle, afShowWay, afWidth, afHeigth, afshowInTime, afshowTime, afshowOutTime);
        }

        protected void Show(string content, string title, ShowWay showWay, int width, int height, int showInTime, int showTime, int showOutTime)
        {
            _timerRunCount = 0;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            if (content != null) labContent.Text = content;//提示的内容
            //标题
            if (title != null)
            {
                Text = title.Length < 20 ? title : title.Substring(0, 20) + "...";
            }

            Width = width;
            Height = height;
            _showInTime = showInTime;
            _showTime = showTime;
            _showOutTime = showOutTime;
            _showWay = showWay;

            _showYPoint = Screen.PrimaryScreen.WorkingArea.Height - Height;
            _showXPoint = Screen.PrimaryScreen.WorkingArea.Width - Width;
            int nowYPoint = Screen.PrimaryScreen.WorkingArea.Height;

            int sleepTime = _showInTime / 10;//每次变化的时间
            switch (showWay)
            {
                case ShowWay.Fade:
                    {
                        #region 窗口淡入
                        Location = new Point(_showXPoint, _showYPoint);
                        Opacity = 0;
                        base.Show();
                        double changeOpcation = 0;
                        while (_start)
                        {
                            changeOpcation += 0.1;
                            if (changeOpcation >= 1)
                            {
                                changeOpcation = 1;
                                _start = false;
                            }
                            Opacity = changeOpcation;
                            Refresh();
                            Thread.Sleep(sleepTime);
                        }
                        #endregion
                        break;
                    }
                case ShowWay.UpDown:
                    //default:
                    {
                        #region 窗口上升
                        Location = new Point(_showXPoint, nowYPoint);
                        base.Show();
                        int reduceHeight = Height / 10;//每次移动的高度
                        while (_start)
                        {
                            nowYPoint -= reduceHeight;
                            if (nowYPoint <= _showYPoint)
                            {
                                nowYPoint = _showYPoint;
                                _start = false;
                            }
                            Location = new Point(_showXPoint, nowYPoint);
                            Refresh();
                            Thread.Sleep(sleepTime);
                        }
                        #endregion
                        break;
                    }
            }
            if (content != null) labContent.Text = content;//提示的内容
            //标题
            if (title != null)
            {
                Text = title.Length < 20 ? title : title.Substring(0, 20) + "...";
            }

            _timer.Enabled = true;
            _timer.Interval = 100;
            _timer.Tick += TimerTick;
            _maxTimerRunCount = _showTime / _timer.Interval;
            _timer.Start();
        }

        protected void TimerTick(Object obj, EventArgs ea)
        {
            _timerRunCount++;
            if (_timerRunCount >= _maxTimerRunCount && !IsDisposed)
            {
                _timer.Stop();
                _timerRunCount = 0;
                _showYPoint = Screen.PrimaryScreen.WorkingArea.Height;
                _showXPoint = Screen.PrimaryScreen.WorkingArea.Width - Width;
                int nowYPoint = Screen.PrimaryScreen.WorkingArea.Height - Height;
                int sleepTime = _showOutTime / 10;//每次变化的时间
                _start = true;
                switch (_showWay)
                {
                    case ShowWay.Fade:
                        {
                            #region 窗口淡出
                            double changeOpcation = 1;
                            while (_start)
                            {
                                changeOpcation -= 0.1;
                                if (changeOpcation <= 0)
                                {
                                    changeOpcation = 0;
                                    _start = false;
                                }
                                Opacity = changeOpcation;
                                Refresh();
                                Thread.Sleep(sleepTime);
                            }
                            #endregion
                            break;
                        }
                    case ShowWay.UpDown:
                        //default:
                        {
                            #region 窗口下降
                            int reduceHeight = Height / 10;//每次移动的高度
                            while (_start)
                            {
                                nowYPoint += reduceHeight;
                                if (nowYPoint >= _showYPoint)
                                {
                                    nowYPoint = _showYPoint;
                                    _start = false;
                                }
                                Location = new Point(_showXPoint, nowYPoint);
                                Refresh();
                                Thread.Sleep(sleepTime);
                            }
                            #endregion
                            break;
                        }
                }
                Dispose();
            }
        }
    }
}