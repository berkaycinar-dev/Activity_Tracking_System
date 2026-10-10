using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace AktiviteTakipSistemi
{
    public partial class TumLoglarForm : Form
    {
        private string macAddress;
        private string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SistemAktiviteLog.db");

        public TumLoglarForm(string mac)
        {
            InitializeComponent();
            this.macAddress = mac;
        }

        private void TumLoglarForm_Load(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    conn.Open();
                    string bugun = DateTime.Now.ToString("yyyy-MM-dd");

                    string query = @"
                        SELECT LogTime as 'Zaman', LogType as 'İşlem Türü', Description as 'Açıklama' 
                        FROM SystemLogs 
                        WHERE MacAddress LIKE @mac AND date(LogTime) = date(@bugun) 
                        ORDER BY LogTime DESC";

                    using (var sqlCmd = new SQLiteCommand(query, conn))
                    {
                        string esnekMac = "%" + macAddress.Trim() + "%";
                        sqlCmd.Parameters.AddWithValue("@mac", esnekMac);
                        sqlCmd.Parameters.AddWithValue("@bugun", bugun);

                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(sqlCmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dgvLoglar.DataSource = dt;
                        }
                    }
                }

                this.Text = $"Günlük Ham Log Dökümü ({dgvLoglar.Rows.Count} Kayıt)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loglar listelenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}