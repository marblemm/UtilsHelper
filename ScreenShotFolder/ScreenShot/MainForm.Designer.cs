namespace ScreenShot
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelBottom = new System.Windows.Forms.Panel();
            this.checkBoxCursor = new System.Windows.Forms.CheckBox();
            this.checkBoxHide = new System.Windows.Forms.CheckBox();
            this.checkBoxColorTable = new System.Windows.Forms.CheckBox();
            this.buttonCaptureImage = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBottom.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.checkBoxCursor);
            this.panelBottom.Controls.Add(this.checkBoxHide);
            this.panelBottom.Controls.Add(this.checkBoxColorTable);
            this.panelBottom.Controls.Add(this.buttonCaptureImage);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 212);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(425, 29);
            this.panelBottom.TabIndex = 0;
            // 
            // checkBoxCursor
            // 
            this.checkBoxCursor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxCursor.AutoSize = true;
            this.checkBoxCursor.Location = new System.Drawing.Point(228, 6);
            this.checkBoxCursor.Name = "checkBoxCursor";
            this.checkBoxCursor.Size = new System.Drawing.Size(96, 16);
            this.checkBoxCursor.TabIndex = 20;
            this.checkBoxCursor.Text = "更换鼠标样式";
            this.checkBoxCursor.UseVisualStyleBackColor = true;
            // 
            // checkBoxHide
            // 
            this.checkBoxHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxHide.AutoSize = true;
            this.checkBoxHide.Checked = true;
            this.checkBoxHide.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHide.Location = new System.Drawing.Point(12, 6);
            this.checkBoxHide.Name = "checkBoxHide";
            this.checkBoxHide.Size = new System.Drawing.Size(108, 16);
            this.checkBoxHide.TabIndex = 19;
            this.checkBoxHide.Text = "截图时隐藏窗体";
            this.checkBoxHide.UseVisualStyleBackColor = true;
            // 
            // checkBoxColorTable
            // 
            this.checkBoxColorTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxColorTable.AutoSize = true;
            this.checkBoxColorTable.Location = new System.Drawing.Point(126, 6);
            this.checkBoxColorTable.Name = "checkBoxColorTable";
            this.checkBoxColorTable.Size = new System.Drawing.Size(96, 16);
            this.checkBoxColorTable.TabIndex = 18;
            this.checkBoxColorTable.Text = "更换颜色样式";
            this.checkBoxColorTable.UseVisualStyleBackColor = true;
            // 
            // buttonCaptureImage
            // 
            this.buttonCaptureImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCaptureImage.Location = new System.Drawing.Point(338, 2);
            this.buttonCaptureImage.Name = "buttonCaptureImage";
            this.buttonCaptureImage.Size = new System.Drawing.Size(75, 23);
            this.buttonCaptureImage.TabIndex = 17;
            this.buttonCaptureImage.Text = "截图";
            this.buttonCaptureImage.UseVisualStyleBackColor = true;
            this.buttonCaptureImage.Click += new System.EventHandler(this.buttonCaptureImage_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(425, 212);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // FormCSharpWinDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 241);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.panelBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCSharpWinDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "截图界面";
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button buttonCaptureImage;
        private System.Windows.Forms.CheckBox checkBoxColorTable;
        private System.Windows.Forms.CheckBox checkBoxHide;
        private System.Windows.Forms.CheckBox checkBoxCursor;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
    }
}

