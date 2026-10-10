namespace AktiviteTakipSistemi
{
    partial class HaftalikRaporForm
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
            this.lstHaftalik = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
             
            // lstHaftalik
             
            this.lstHaftalik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHaftalik.FormattingEnabled = true;
            this.lstHaftalik.ItemHeight = 16;
            this.lstHaftalik.Location = new System.Drawing.Point(0, 0);
            this.lstHaftalik.Name = "lstHaftalik";
            this.lstHaftalik.Size = new System.Drawing.Size(800, 450);
            this.lstHaftalik.TabIndex = 0;
             
            // HaftalikRaporForm
             
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstHaftalik);
            this.Name = "HaftalikRaporForm";
            this.Text = "HaftalikRaporForm";
            this.ResumeLayout(false);

        }


        private System.Windows.Forms.ListBox lstHaftalik;
    }
}