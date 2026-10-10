namespace AktiviteTakipSistemi
{
    partial class CihazSecimForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }



        private void InitializeComponent()
        {
            this.cmbCihazlar = new System.Windows.Forms.ComboBox();
            this.btnRaporuAc = new System.Windows.Forms.Button();
            this.lblSecimBaslik = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // cmbCihazlar
             
            this.cmbCihazlar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCihazlar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbCihazlar.FormattingEnabled = true;
            this.cmbCihazlar.Location = new System.Drawing.Point(30, 65);
            this.cmbCihazlar.Name = "cmbCihazlar";
            this.cmbCihazlar.Size = new System.Drawing.Size(320, 28);
            this.cmbCihazlar.TabIndex = 1;
             
            // btnRaporuAc
             
            this.btnRaporuAc.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRaporuAc.Location = new System.Drawing.Point(30, 115);
            this.btnRaporuAc.Name = "btnRaporuAc";
            this.btnRaporuAc.Size = new System.Drawing.Size(320, 45);
            this.btnRaporuAc.TabIndex = 2;
            this.btnRaporuAc.Text = "Cihaz Onay";
            this.btnRaporuAc.UseVisualStyleBackColor = true;
            this.btnRaporuAc.Click += new System.EventHandler(this.btnRaporuAc_Click);
             
            // lblSecimBaslik
             
            this.lblSecimBaslik.AutoSize = true;
            this.lblSecimBaslik.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSecimBaslik.Location = new System.Drawing.Point(26, 25);
            this.lblSecimBaslik.Name = "lblSecimBaslik";
            this.lblSecimBaslik.Size = new System.Drawing.Size(225, 20);
            this.lblSecimBaslik.TabIndex = 0;
            this.lblSecimBaslik.Text = "Analiz Edilecek Cihazı Seçiniz:";
             
            // CihazSecimForm
             
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 191);
            this.Controls.Add(this.lblSecimBaslik);
            this.Controls.Add(this.btnRaporuAc);
            this.Controls.Add(this.cmbCihazlar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CihazSecimForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cihaz Yönetim Paneli Giriş";
            this.Load += new System.EventHandler(this.CihazSecimForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private System.Windows.Forms.ComboBox cmbCihazlar;
        private System.Windows.Forms.Button btnRaporuAc;
        private System.Windows.Forms.Label lblSecimBaslik;
    }
}