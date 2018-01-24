namespace MillionHerosHelper
{
    partial class DaShangForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DaShangForm));
            this.label_Wechat = new System.Windows.Forms.Label();
            this.label_AliPay = new System.Windows.Forms.Label();
            this.pictureBox_AliPay = new System.Windows.Forms.PictureBox();
            this.pictureBox_Wechat = new System.Windows.Forms.PictureBox();
            this.label_ThankYou = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AliPay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Wechat)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Wechat
            // 
            this.label_Wechat.AutoSize = true;
            this.label_Wechat.Font = new System.Drawing.Font("宋体", 15F);
            this.label_Wechat.Location = new System.Drawing.Point(163, 28);
            this.label_Wechat.Name = "label_Wechat";
            this.label_Wechat.Size = new System.Drawing.Size(62, 25);
            this.label_Wechat.TabIndex = 0;
            this.label_Wechat.Text = "微信";
            // 
            // label_AliPay
            // 
            this.label_AliPay.AutoSize = true;
            this.label_AliPay.Font = new System.Drawing.Font("宋体", 15F);
            this.label_AliPay.Location = new System.Drawing.Point(584, 28);
            this.label_AliPay.Name = "label_AliPay";
            this.label_AliPay.Size = new System.Drawing.Size(87, 25);
            this.label_AliPay.TabIndex = 1;
            this.label_AliPay.Text = "支付宝";
            // 
            // pictureBox_AliPay
            // 
            this.pictureBox_AliPay.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_AliPay.Image")));
            this.pictureBox_AliPay.Location = new System.Drawing.Point(437, 67);
            this.pictureBox_AliPay.Name = "pictureBox_AliPay";
            this.pictureBox_AliPay.Size = new System.Drawing.Size(390, 390);
            this.pictureBox_AliPay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_AliPay.TabIndex = 3;
            this.pictureBox_AliPay.TabStop = false;
            // 
            // pictureBox_Wechat
            // 
            this.pictureBox_Wechat.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Wechat.Image")));
            this.pictureBox_Wechat.Location = new System.Drawing.Point(12, 67);
            this.pictureBox_Wechat.Name = "pictureBox_Wechat";
            this.pictureBox_Wechat.Size = new System.Drawing.Size(390, 390);
            this.pictureBox_Wechat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Wechat.TabIndex = 2;
            this.pictureBox_Wechat.TabStop = false;
            // 
            // label_ThankYou
            // 
            this.label_ThankYou.AutoSize = true;
            this.label_ThankYou.Font = new System.Drawing.Font("宋体", 16F);
            this.label_ThankYou.Location = new System.Drawing.Point(113, 494);
            this.label_ThankYou.Name = "label_ThankYou";
            this.label_ThankYou.Size = new System.Drawing.Size(579, 27);
            this.label_ThankYou.TabIndex = 4;
            this.label_ThankYou.Text = "你们的支持，是我继续开发的动力！感谢大家！";
            // 
            // DaShangForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 553);
            this.Controls.Add(this.label_ThankYou);
            this.Controls.Add(this.pictureBox_AliPay);
            this.Controls.Add(this.pictureBox_Wechat);
            this.Controls.Add(this.label_AliPay);
            this.Controls.Add(this.label_Wechat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DaShangForm";
            this.Text = "打赏作者";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AliPay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Wechat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Wechat;
        private System.Windows.Forms.Label label_AliPay;
        private System.Windows.Forms.PictureBox pictureBox_AliPay;
        private System.Windows.Forms.PictureBox pictureBox_Wechat;
        private System.Windows.Forms.Label label_ThankYou;
    }
}