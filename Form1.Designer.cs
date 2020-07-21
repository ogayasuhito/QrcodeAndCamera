namespace qrCodePrj
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbCamera = new System.Windows.Forms.ComboBox();
            this.btnExec = new System.Windows.Forms.Button();
            this.pictureBoxCamera = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbCamera
            // 
            this.cmbCamera.FormattingEnabled = true;
            this.cmbCamera.Location = new System.Drawing.Point(104, 32);
            this.cmbCamera.Name = "cmbCamera";
            this.cmbCamera.Size = new System.Drawing.Size(121, 20);
            this.cmbCamera.TabIndex = 1;
            // 
            // btnExec
            // 
            this.btnExec.Location = new System.Drawing.Point(266, 29);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(167, 54);
            this.btnExec.TabIndex = 3;
            this.btnExec.Text = "開始";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // pictureBoxCamera
            // 
            this.pictureBoxCamera.Location = new System.Drawing.Point(47, 113);
            this.pictureBoxCamera.Name = "pictureBoxCamera";
            this.pictureBoxCamera.Size = new System.Drawing.Size(702, 305);
            this.pictureBoxCamera.TabIndex = 4;
            this.pictureBoxCamera.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBoxCamera);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.cmbCamera);
            this.Name = "Form1";
            this.Text = "QRコード読み取り";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbCamera;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.PictureBox pictureBoxCamera;
    }
}

