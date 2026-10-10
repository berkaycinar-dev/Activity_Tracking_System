namespace AktiviteTakipSistemi
{
    partial class TumLoglarForm
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
            this.dgvLoglar = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoglar)).BeginInit();
            this.SuspendLayout();
             
            // dgvLoglar
             
            this.dgvLoglar.AllowUserToAddRows = false;
            this.dgvLoglar.AllowUserToDeleteRows = false;
            this.dgvLoglar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoglar.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvLoglar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoglar.Dock = System.Windows.Forms.DockStyle.Fill; 
            this.dgvLoglar.Location = new System.Drawing.Point(0, 0);
            this.dgvLoglar.Name = "dgvLoglar";
            this.dgvLoglar.ReadOnly = true;
            this.dgvLoglar.RowHeadersVisible = false;
            this.dgvLoglar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoglar.Size = new System.Drawing.Size(700, 400);
            this.dgvLoglar.TabIndex = 0;
             
            // TumLoglarForm
             
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.Controls.Add(this.dgvLoglar); 
            this.Name = "TumLoglarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Günlük Ham Log Dökümü";
            this.Load += new System.EventHandler(this.TumLoglarForm_Load); 
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoglar)).EndInit();
            this.ResumeLayout(false);

        }


 
        private System.Windows.Forms.DataGridView dgvLoglar;
    }
}