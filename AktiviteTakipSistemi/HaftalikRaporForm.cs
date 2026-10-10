using System;
using System.Windows.Forms;
using AktiviteTakipSistemi.Logic;

namespace AktiviteTakipSistemi
{
    public partial class HaftalikRaporForm : Form
    {
        public HaftalikRaporForm(string mac, string name)
        {
            InitializeComponent();
            this.Text = $"{name} - Son 7 Günlük Performans";

            lstHaftalik.Items.Clear();
            try
            {
                for (int i = 0; i < 7; i++)
                {
                    ReportAnalyst.ActivitySummary gunluk = ReportAnalyst.CalculateDeviceSummary(mac, i);
                    DateTime tarih = DateTime.Now.AddDays(-i);

                    string satir = $"{tarih.ToString("dd.MM.yyyy dddd")} -> Aktif: {gunluk.ActiveTime:hh\\:mm\\:ss}, Toplam Açık: {gunluk.TotalWorkTime:hh\\:mm\\:ss}";
                    if (gunluk.TotalWorkTime.TotalSeconds == 0) satir = $"{tarih.ToString("dd.MM.yyyy")} -> Cihazda aktivite yok.";

                    lstHaftalik.Items.Add(satir);
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
    }
}