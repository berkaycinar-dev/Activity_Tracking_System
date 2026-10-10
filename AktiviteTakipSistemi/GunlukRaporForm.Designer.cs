namespace AktiviteTakipSistemi
{
    partial class GunlukRaporForm
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
            this.lblCihazBilgi = new System.Windows.Forms.Label();
            this.grpSureler = new System.Windows.Forms.GroupBox();
            this.lblEkranKapali = new System.Windows.Forms.Label();
            this.lblUyku = new System.Windows.Forms.Label();
            this.lblPasif = new System.Windows.Forms.Label();
            this.lblAktif = new System.Windows.Forms.Label();
            this.lblToplam = new System.Windows.Forms.Label();
            this.grpLoglar = new System.Windows.Forms.GroupBox();
            this.lstSonBesLog = new System.Windows.Forms.ListBox();
            this.btnOzetle = new System.Windows.Forms.Button();
            this.btnTumLoglar = new System.Windows.Forms.Button();
            this.btnHaftalikRapor = new System.Windows.Forms.Button();
            this.grpSureler.SuspendLayout();
            this.grpLoglar.SuspendLayout();
            this.SuspendLayout();
             
            // lblCihazBilgi
             
            this.lblCihazBilgi.AutoSize = true;
            this.lblCihazBilgi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCihazBilgi.Location = new System.Drawing.Point(16, 18);
            this.lblCihazBilgi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCihazBilgi.Name = "lblCihazBilgi";
            this.lblCihazBilgi.Size = new System.Drawing.Size(146, 28);
            this.lblCihazBilgi.TabIndex = 0;
            this.lblCihazBilgi.Text = "Seçili Cihaz: --";
             
            // grpSureler
             
            this.grpSureler.Controls.Add(this.lblEkranKapali);
            this.grpSureler.Controls.Add(this.lblUyku);
            this.grpSureler.Controls.Add(this.lblPasif);
            this.grpSureler.Controls.Add(this.lblAktif);
            this.grpSureler.Controls.Add(this.lblToplam);
            this.grpSureler.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.grpSureler.Location = new System.Drawing.Point(21, 62);
            this.grpSureler.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpSureler.Name = "grpSureler";
            this.grpSureler.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpSureler.Size = new System.Drawing.Size(427, 258);
            this.grpSureler.TabIndex = 1;
            this.grpSureler.TabStop = false;
            this.grpSureler.Text = "Çalışma Süreleri Özetleri";
             
            // lblEkranKapali
             
            this.lblEkranKapali.AutoSize = true;
            this.lblEkranKapali.Location = new System.Drawing.Point(20, 203);
            this.lblEkranKapali.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEkranKapali.Name = "lblEkranKapali";
            this.lblEkranKapali.Size = new System.Drawing.Size(282, 23);
            this.lblEkranKapali.TabIndex = 4;
            this.lblEkranKapali.Text = "Ekran Kapalı Süresi (Toplam): --:--:--";
             
            // lblUyku
             
            this.lblUyku.AutoSize = true;
            this.lblUyku.Location = new System.Drawing.Point(20, 160);
            this.lblUyku.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUyku.Name = "lblUyku";
            this.lblUyku.Size = new System.Drawing.Size(207, 23);
            this.lblUyku.TabIndex = 3;
            this.lblUyku.Text = "Uyku Modu Süresi: --:--:--";
             
            // lblPasif
             
            this.lblPasif.AutoSize = true;
            this.lblPasif.Location = new System.Drawing.Point(20, 117);
            this.lblPasif.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPasif.Name = "lblPasif";
            this.lblPasif.Size = new System.Drawing.Size(199, 23);
            this.lblPasif.TabIndex = 2;
            this.lblPasif.Text = "Pasif (Boşta) Süre: --:--:--";
            
            // lblAktif
             
            this.lblAktif.AutoSize = true;
            this.lblAktif.Location = new System.Drawing.Point(20, 74);
            this.lblAktif.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAktif.Name = "lblAktif";
            this.lblAktif.Size = new System.Drawing.Size(217, 23);
            this.lblAktif.TabIndex = 1;
            this.lblAktif.Text = "Aktif Çalışma Süresi: --:--:--";
             
            // lblToplam
             
            this.lblToplam.AutoSize = true;
            this.lblToplam.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplam.Location = new System.Drawing.Point(20, 31);
            this.lblToplam.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToplam.Name = "lblToplam";
            this.lblToplam.Size = new System.Drawing.Size(251, 23);
            this.lblToplam.TabIndex = 0;
            this.lblToplam.Text = "Toplam Çalışma Süresi: --:--:--";
            
            // grpLoglar
             
            this.grpLoglar.Controls.Add(this.lstSonBesLog);
            this.grpLoglar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.grpLoglar.Location = new System.Drawing.Point(469, 62);
            this.grpLoglar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpLoglar.Name = "grpLoglar";
            this.grpLoglar.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpLoglar.Size = new System.Drawing.Size(427, 258);
            this.grpLoglar.TabIndex = 2;
            this.grpLoglar.TabStop = false;
            this.grpLoglar.Text = "Son 5 Sistem Hareketi";
             
            // lstSonBesLog
             
            this.lstSonBesLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lstSonBesLog.FormattingEnabled = true;
            this.lstSonBesLog.ItemHeight = 18;
            this.lstSonBesLog.Location = new System.Drawing.Point(20, 31);
            this.lstSonBesLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstSonBesLog.Name = "lstSonBesLog";
            this.lstSonBesLog.Size = new System.Drawing.Size(385, 202);
            this.lstSonBesLog.TabIndex = 0;
             
            // btnOzetle
             
            this.btnOzetle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOzetle.Location = new System.Drawing.Point(21, 338);
            this.btnOzetle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOzetle.Name = "btnOzetle";
            this.btnOzetle.Size = new System.Drawing.Size(267, 49);
            this.btnOzetle.TabIndex = 3;
            this.btnOzetle.Text = "Bugünün Raporunu Özetle";
            this.btnOzetle.UseVisualStyleBackColor = true;
            this.btnOzetle.Click += new System.EventHandler(this.btnOzetle_Click);
             
            // btnTumLoglar
             
            this.btnTumLoglar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTumLoglar.Location = new System.Drawing.Point(309, 338);
            this.btnTumLoglar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTumLoglar.Name = "btnTumLoglar";
            this.btnTumLoglar.Size = new System.Drawing.Size(280, 49);
            this.btnTumLoglar.TabIndex = 4;
            this.btnTumLoglar.Text = "Bugüne Ait Tüm Loglar";
            this.btnTumLoglar.UseVisualStyleBackColor = true;
            this.btnTumLoglar.Click += new System.EventHandler(this.btnTumLoglar_Click);
            
            // btnHaftalikRapor
            
            this.btnHaftalikRapor.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnHaftalikRapor.Location = new System.Drawing.Point(616, 338);
            this.btnHaftalikRapor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHaftalikRapor.Name = "btnHaftalikRapor";
            this.btnHaftalikRapor.Size = new System.Drawing.Size(280, 49);
            this.btnHaftalikRapor.TabIndex = 5;
            this.btnHaftalikRapor.Text = "Haftalık Raporu Göster";
            this.btnHaftalikRapor.UseVisualStyleBackColor = true;
            this.btnHaftalikRapor.Click += new System.EventHandler(this.btnHaftalikRapor_Click);
            
            // GunlukRaporForm
             
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 414);
            this.Controls.Add(this.btnHaftalikRapor);
            this.Controls.Add(this.btnTumLoglar);
            this.Controls.Add(this.btnOzetle);
            this.Controls.Add(this.grpLoglar);
            this.Controls.Add(this.grpSureler);
            this.Controls.Add(this.lblCihazBilgi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "GunlukRaporForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cihaz Günlük Takip Paneli";
            this.grpSureler.ResumeLayout(false);
            this.grpSureler.PerformLayout();
            this.grpLoglar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label lblCihazBilgi;
        private System.Windows.Forms.GroupBox grpSureler;
        private System.Windows.Forms.Label lblEkranKapali;
        private System.Windows.Forms.Label lblUyku;
        private System.Windows.Forms.Label lblPasif;
        private System.Windows.Forms.Label lblAktif;
        private System.Windows.Forms.Label lblToplam;
        private System.Windows.Forms.GroupBox grpLoglar;
        private System.Windows.Forms.ListBox lstSonBesLog;
        private System.Windows.Forms.Button btnOzetle;
        private System.Windows.Forms.Button btnTumLoglar;
        private System.Windows.Forms.Button btnHaftalikRapor;
    }
}