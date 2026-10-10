using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using AktiviteTakipSistemi.Logic; 

namespace AktiviteTakipSistemi
{
    public partial class GunlukRaporForm : Form
    {
        private string targetMac;
        private string targetName;
        private string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SistemAktiviteLog.db");
        private string connString;

        
        public GunlukRaporForm(string mac, string name)
        {
            InitializeComponent();
            this.targetMac = mac;
            this.targetName = name;
            this.connString = $"Data Source={dbPath};Version=3;";

            
            lblCihazBilgi.Text = $"Seçili Cihaz: {targetName} [{targetMac}]";
        }


        private void btnOzetle_Click(object sender, EventArgs e)
        {
            try
            {
             
                ReportAnalyst.ActivitySummary summary = ReportAnalyst.CalculateDeviceSummary(targetMac, 0);

                
                lblToplam.Text = $"Toplam Çalışma Süresi: {summary.TotalWorkTime.ToString(@"hh\:mm\:ss")}";
                lblAktif.Text = $"Aktif Çalışma Süresi: {summary.ActiveTime.ToString(@"hh\:mm\:ss")}";
                lblPasif.Text = $"Pasif (Boşta) Süre: {summary.PassiveTime.ToString(@"hh\:mm\:ss")}";
                lblUyku.Text = $"Uyku Modu Süresi: {summary.SleepTime.ToString(@"hh\:mm\:ss")}";

                
                lblEkranKapali.Text = $"Ekran Kapalı Süresi (Toplam): {summary.TotalScreenOff.ToString(@"hh\:mm\:ss")}";

                SonBesLoguYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Raporlama hatası: {ex.Message}");
            }
        }

     
        private void SonBesLoguYukle()
        {
            lstSonBesLog.Items.Clear();

            try
            {
                using (var conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    
                    string query = "SELECT LogTime, LogType FROM SystemLogs WHERE MacAddress = @mac ORDER BY LogTime DESC LIMIT 5";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@mac", targetMac);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string zaman = Convert.ToDateTime(reader["LogTime"]).ToString("HH:mm:ss");
                                string logTuru = reader["LogType"].ToString();

                                lstSonBesLog.Items.Add($"[{zaman}] -> {logTuru}");
                            }
                        }
                    }
                }

                
                if (lstSonBesLog.Items.Count == 0)
                {
                    lstSonBesLog.Items.Add("Bugün henüz bir işlem logu kaydedilmedi.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Son 5 log yüklenirken hata: " + ex.Message);
            }
        }


        private void btnTumLoglar_Click(object sender, EventArgs e)
        {
            TumLoglarForm tumLoglarForm = new TumLoglarForm(targetMac);
            tumLoglarForm.ShowDialog();
        }

        private void btnHaftalikRapor_Click(object sender, EventArgs e)
        {
            HaftalikRaporForm haftalikForm = new HaftalikRaporForm(targetMac, targetName);
            haftalikForm.ShowDialog();
        }

        private void GunlukRaporForm_Load(object sender, EventArgs e)
        {

        }

        private void lblToplam_Click(object sender, EventArgs e)
        {

        }

        private void grpSureler_Enter(object sender, EventArgs e)
        {

        }
    }
}