using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CaptureTool.Properties;

namespace CaptureTool
{
    partial class DrawToolsControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip = new ToolStrip();
            this.toolStripButtonRectangular = new ToolStripButton();
            this.toolStripButtonEllipse = new ToolStripButton();
            this.toolStripButtonText = new ToolStripButton();
            this.toolStripButtonArrow = new ToolStripButton();
            this.toolStripButtonLine = new ToolStripButton();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.toolStripButtonRedo = new ToolStripButton();
            this.toolStripButtonSave = new ToolStripButton();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.toolStripButtonExit = new ToolStripButton();
            this.toolStripButtonAccept = new ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new ToolStripItem[] {
            this.toolStripButtonRectangular,
            this.toolStripButtonEllipse,
            this.toolStripButtonText,
            this.toolStripButtonArrow,
            this.toolStripButtonLine,
            this.toolStripSeparator1,
            this.toolStripButtonRedo,
            this.toolStripButtonSave,
            this.toolStripSeparator2,
            this.toolStripButtonExit,
            this.toolStripButtonAccept});
            this.toolStrip.Location = new Point(2, 2);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new Size(220, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonRectangular
            // 
            this.toolStripButtonRectangular.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRectangular.Image = Resources.Rectangular;
            this.toolStripButtonRectangular.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonRectangular.Name = "toolStripButtonRectangular";
            this.toolStripButtonRectangular.Size = new Size(23, 22);
            this.toolStripButtonRectangular.Text = "添加矩形";
            // 
            // toolStripButtonEllipse
            // 
            this.toolStripButtonEllipse.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEllipse.Image = Resources.Ellipse;
            this.toolStripButtonEllipse.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonEllipse.Name = "toolStripButtonEllipse";
            this.toolStripButtonEllipse.Size = new Size(23, 22);
            this.toolStripButtonEllipse.Text = "添加椭圆";
            // 
            // toolStripButtonText
            // 
            this.toolStripButtonText.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButtonText.Image = Resources.Text;
            this.toolStripButtonText.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonText.Name = "toolStripButtonText";
            this.toolStripButtonText.Size = new Size(23, 22);
            this.toolStripButtonText.Text = "添加文字";
            // 
            // toolStripButtonArrow
            // 
            this.toolStripButtonArrow.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButtonArrow.Image =Resources.Arrow;
            this.toolStripButtonArrow.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonArrow.Name = "toolStripButtonArrow";
            this.toolStripButtonArrow.Size = new Size(23, 22);
            this.toolStripButtonArrow.Text = "添加箭头";
            // 
            // toolStripButtonLine
            // 
            this.toolStripButtonLine.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLine.Image = Resources.Line;
            this.toolStripButtonLine.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonLine.Name = "toolStripButtonLine";
            this.toolStripButtonLine.Size = new Size(23, 22);
            this.toolStripButtonLine.Text = "画笔";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripButtonRedo
            // 
            this.toolStripButtonRedo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRedo.Image =Resources.Redo;
            this.toolStripButtonRedo.ImageTransparentColor = Color.Transparent;
            this.toolStripButtonRedo.Name = "toolStripButtonRedo";
            this.toolStripButtonRedo.Size = new Size(23, 22);
            this.toolStripButtonRedo.Text = "撤销";
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = Resources.Save;
            this.toolStripButtonSave.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new Size(23, 22);
            this.toolStripButtonSave.Text = "保存";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripButtonExit
            // 
            this.toolStripButtonExit.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExit.Image = Resources.Exit;
            this.toolStripButtonExit.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonExit.Name = "toolStripButtonExit";
            this.toolStripButtonExit.Size = new Size(23, 22);
            this.toolStripButtonExit.Text = "退出截图";
            // 
            // toolStripButtonAccept
            // 
            this.toolStripButtonAccept.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAccept.Image = Resources.Accept;
            this.toolStripButtonAccept.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonAccept.Name = "toolStripButtonAccept";
            this.toolStripButtonAccept.Size = new Size(23, 20);
            this.toolStripButtonAccept.Text = "完成截图";
            // 
            // DrawToolsControl
            // 
            this.AutoScaleDimensions = new SizeF(6F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Name = "DrawToolsControl";
            this.Padding = new Padding(2);
            this.Size = new Size(224, 29);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripButton toolStripButtonRectangular;
        private ToolStripButton toolStripButtonEllipse;
        private ToolStripButton toolStripButtonText;
        private ToolStripButton toolStripButtonArrow;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButtonLine;
        private ToolStripButton toolStripButtonRedo;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButtonExit;
        private ToolStripButton toolStripButtonAccept;
        private ToolStripButton toolStripButtonSave;
    }
}
