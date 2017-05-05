using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CaptureTool.Properties;

namespace CaptureTool
{
    partial class CaptureImageTool
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            this.toolTip = new ToolTip(this.components);
            this.saveFileDialog = new SaveFileDialog();
            this.textBox = new TextBox();
            this.contextMenuStrip = new ContextMenuStrip(this.components);
            this.menuItemRedo = new ToolStripMenuItem();
            this.menuItemReselect = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.menuItemAccept = new ToolStripMenuItem();
            this.menuItemSave = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.menuItemExit = new ToolStripMenuItem();
            this.colorSelector = new ColorSelector();
            this.drawToolsControl = new DrawToolsControl();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "bmp";
            this.saveFileDialog.Filter = "BMP 文件(*.bmp)|*.bmp|JPEG 文件(*.jpg,*.jpeg)|*.jpg,*.jpeg|PNG 文件(*.png)|*.png|GIF 文件" +
                "(*.gif)|*.gif";
            // 
            // textBox
            // 
            this.textBox.BorderStyle = BorderStyle.None;
            this.textBox.ImeMode = ImeMode.On;
            this.textBox.Location = new Point(2, 233);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new Size(100, 21);
            this.textBox.TabIndex = 4;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new ToolStripItem[] {
            this.menuItemRedo,
            this.menuItemReselect,
            this.toolStripSeparator1,
            this.menuItemAccept,
            this.menuItemSave,
            this.toolStripSeparator2,
            this.menuItemExit});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new Size(167, 126);
            // 
            // menuItemRedo
            // 
            this.menuItemRedo.Image = Resources.Redo;
            this.menuItemRedo.Name = "menuItemRedo";
            this.menuItemRedo.Size = new Size(166, 22);
            this.menuItemRedo.Text = "撤销编辑";
            // 
            // menuItemReselect
            // 
            this.menuItemReselect.Name = "menuItemReselect";
            this.menuItemReselect.Size = new Size(166, 22);
            this.menuItemReselect.Text = "重新选择截图区域";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(163, 6);
            // 
            // menuItemAccept
            // 
            this.menuItemAccept.Image = Resources.Accept;
            this.menuItemAccept.Name = "menuItemAccept";
            this.menuItemAccept.Size = new Size(166, 22);
            this.menuItemAccept.Text = "复制并退出截图";
            // 
            // menuItemSave
            // 
            this.menuItemSave.Image = Resources.Save;
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.Size = new Size(166, 22);
            this.menuItemSave.Text = "另存为...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(163, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Image = Resources.Exit;
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new Size(166, 22);
            this.menuItemExit.Text = "退出截图";
            // 
            // colorSelector
            // 
            this.colorSelector.Location = new Point(2, 189);
            this.colorSelector.Name = "colorSelector";
            this.colorSelector.Padding = new Padding(2);
            this.colorSelector.Size = new Size(189, 38);
            this.colorSelector.TabIndex = 3;
            // 
            // drawToolsControl
            // 
            this.drawToolsControl.Location = new Point(2, 154);
            this.drawToolsControl.Name = "drawToolsControl";
            this.drawToolsControl.Padding = new Padding(2);
            this.drawToolsControl.Size = new Size(224, 29);
            this.drawToolsControl.TabIndex = 0;
            // 
            // CaptureImageTool
            // 
            this.AutoScaleDimensions = new SizeF(6F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(319, 266);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.colorSelector);
            this.Controls.Add(this.drawToolsControl);
            this.Name = "CaptureImageTool";
            this.Text = "CaptureImageTool";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolTip toolTip;
        private DrawToolsControl drawToolsControl;
        private SaveFileDialog saveFileDialog;
        private ColorSelector colorSelector;
        private TextBox textBox;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem menuItemRedo;
        private ToolStripMenuItem menuItemReselect;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menuItemAccept;
        private ToolStripMenuItem menuItemSave;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuItemExit;
    }
}