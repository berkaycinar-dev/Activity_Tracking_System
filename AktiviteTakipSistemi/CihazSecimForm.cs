using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using AktiviteTakipSistemi.Data;

namespace AktiviteTakipSistemi
{
    public partial class CihazSecimForm : Form
    {
        private string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SistemAktiviteLog.db");
        private string connString;

        public CihazSecimForm()
        {
            InitializeComponent();
            this.connString = $"Data Source={dbPath};Version=3;";
        }

        private void CihazSecimForm_Load(object sender, EventArgs e)
        {
            DatabaseManager.SaveLog("AKTIF", "Merkezi cihaz seçim paneli üzerinden bağlantı kuruldu.");
            CihazlariYukle();
        }

        private void CihazlariYukle()
        {
            cmbCihazlar.Items.Clear();
            try
            {
                using (var conn = new SQLiteConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT DeviceName, MacAddress FROM SystemLogs";
                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCihazlar.Items.Add($"{reader["DeviceName"]} [{reader["MacAddress"]}]");
                        }
                    }
                }
                if (cmbCihazlar.Items.Count > 0) cmbCihazlar.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cihaz listesi yüklenirken hata: {ex.Message}");
            }
        }

        private void btnRaporuAc_Click(object sender, EventArgs e)
        {
            if (cmbCihazlar.SelectedItem == null) return;
            try
            {
                string secilenMetin = cmbCihazlar.SelectedItem.ToString();
                int baslangic = secilenMetin.IndexOf('[') + 1;
                int bitis = secilenMetin.IndexOf(']');

                string temizMac = secilenMetin.Substring(baslangic, bitis - baslangic).Trim();
                string temizIsim = secilenMetin.Split('[')[0].Trim();

                GunlukRaporForm gunlukForm = new GunlukRaporForm(temizMac, temizIsim);
                this.Hide();
                gunlukForm.ShowDialog();
                this.Show();
                CihazlariYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Panel açılırken hata: {ex.Message}");
            }
        }
    }
}