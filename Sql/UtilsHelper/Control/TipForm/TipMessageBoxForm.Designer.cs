using System.Windows.Forms;

namespace UtilsHelper.Control.TipForm
{
    partial class TipMessageBoxForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Location = new System.Drawing.Point(44, 36);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(41, 12);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.Text = "label1";
            // 
            // TipMessageBoxForm
            // 
            this.ClientSize = new System.Drawing.Size(319, 163);
            this.Controls.Add(this._lblTitle);
            this.Name = "TipMessageBoxForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private Label _lblTitle;
        #endregion
    }
}