using System;
using System.Drawing;
using System.Windows.Forms;
using UtilsHelper.HookHelper;

namespace TestForm
{
    /*
     上面的5个类编译成Dll引用,使用例子
     */
    public class HookTestForm : Form
    {
        private readonly MouseHook _mouseHook = new MouseHook();
        private ListView _listView2;
        private ListView _listView1;
        private Label _curXyLabel;
        readonly KeyboardHook _keyboardHook = new KeyboardHook();
        public HookTestForm()
        {
            InitializeComponent();
            Load += TestForm_Load;
            FormClosed += TestForm_FormClosed;
        }
        private void TestForm_Load(object sender, EventArgs e)
        {
            _mouseHook.MouseMove += mouseHook_MouseMove;
            _mouseHook.MouseDown += mouseHook_MouseDown;
            _mouseHook.MouseUp += mouseHook_MouseUp;
            _mouseHook.MouseWheel += mouseHook_MouseWheel;
            _keyboardHook.KeyDown += keyboardHook_KeyDown;
            _keyboardHook.KeyUp += keyboardHook_KeyUp;
            _keyboardHook.KeyPress += keyboardHook_KeyPress;
            _mouseHook.Start();
            _keyboardHook.Start();
            //_keyHookHelper.HookStart();
            SetXyLabel(MouseSimulator.X, MouseSimulator.Y);
        }
        void keyboardHook_KeyPress(object sender, KeyPressEventArgs e)
        {
            AddKeyboardEvent("KeyPress", "", e.KeyChar.ToString(), "", "", "");
        }
        void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {
            AddKeyboardEvent("KeyUp", e.KeyCode.ToString(), "", e.Shift.ToString(), e.Alt.ToString(), e.Control.ToString());
        }
        void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            AddKeyboardEvent("KeyDown", e.KeyCode.ToString(), "", e.Shift.ToString(), e.Alt.ToString(), e.Control.ToString());
        }
        void mouseHook_MouseWheel(object sender, MouseEventArgs e)
        {
            AddMouseEvent("MouseWheel", "", "", "", e.Delta.ToString());
        }
        void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {
            AddMouseEvent("MouseUp", e.Button.ToString(), e.X.ToString(), e.Y.ToString(), "");
        }
        void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {
            AddMouseEvent("MouseDown", e.Button.ToString(), e.X.ToString(), e.Y.ToString(), "");
        }
        void mouseHook_MouseMove(object sender, MouseEventArgs e)
        {
            SetXyLabel(e.X, e.Y);
        }
        void SetXyLabel(int x, int y)
        {
            _curXyLabel.Text = String.Format("Current Mouse Point: X={0}, y={1}", x, y);
        }
        void AddMouseEvent(string eventType, string button, string x, string y, string delta)
        {
            _listView1.Items.Insert(0, new ListViewItem(new[] { eventType, button, x, y, delta }));
        }
        void AddKeyboardEvent(string eventType, string keyCode, string keyChar, string shift, string alt, string control)
        {
            _listView2.Items.Insert(0, new ListViewItem(new[] { eventType, keyCode, keyChar, shift, alt, control }));
        }
        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Not necessary anymore, will stop when application exits
            //mouseHook.Stop();
            //keyboardHook.Stop();
        }

        private void InitializeComponent()
        {
            _listView2 = new ListView();
            _listView1 = new ListView();
            _curXyLabel = new Label();
            SuspendLayout();
            // 
            // listView2
            // 
            _listView2.Location = new Point(288, 47);
            _listView2.Name = "_listView2";
            _listView2.Size = new Size(228, 332);
            _listView2.TabIndex = 1;
            _listView2.UseCompatibleStateImageBehavior = false;
            // 
            // listView1
            // 
            _listView1.Location = new Point(33, 47);
            _listView1.Name = "_listView1";
            _listView1.Size = new Size(230, 332);
            _listView1.TabIndex = 1;
            _listView1.UseCompatibleStateImageBehavior = false;
            // 
            // curXYLabel
            // 
            _curXyLabel.AutoSize = true;
            _curXyLabel.Location = new Point(31, 9);
            _curXyLabel.Name = "_curXyLabel";
            _curXyLabel.Size = new Size(41, 12);
            _curXyLabel.TabIndex = 0;
            _curXyLabel.Text = @"label1";
            // 
            // HookTestForm
            // 
            ClientSize = new Size(528, 391);
            Controls.Add(_listView2);
            Controls.Add(_listView1);
            Controls.Add(_curXyLabel);
            Name = "HookTestForm";
            ResumeLayout(false);
            PerformLayout();

        }
    }
}
