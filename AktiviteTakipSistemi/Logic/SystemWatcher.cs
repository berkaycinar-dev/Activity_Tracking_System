using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AktiviteTakipSistemi.Data;
using Microsoft.Win32;
using System.Data.SQLite; 

namespace AktiviteTakipSistemi.Logic
{
    public class SystemWatcher
    {
        private Timer activityTimer;
        private bool isPassive = false;
        private const int PASSIVE_THRESHOLD_SECONDS = 600; // 10 dakika hareketsizlik durumunda pasif moda geçicek

        private DateTime lastTickTime;

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        public void Start()
        {
            SystemEvents.SessionSwitch += OnSessionSwitch;
            SystemEvents.SessionEnding += OnSessionEnding;

            DatabaseManager.SaveLog("OTURUM_ACILDI", "Uygulama arka planda dinlemeye başladı.");

            lastTickTime = DateTime.Now;

            activityTimer = new Timer();
            activityTimer.Interval = 5000;
            activityTimer.Tick += ActivityTimer_Tick;
            activityTimer.Start();
        }

        public void Stop()
        {
            SystemEvents.SessionSwitch -= OnSessionSwitch;
            SystemEvents.SessionEnding -= OnSessionEnding;

            if (activityTimer != null)
            {
                activityTimer.Stop();
                activityTimer.Dispose();
            }

            DatabaseManager.SaveLog("OTURUM_KAPANDI", "Kullanıcı uygulamayı veya sistemi kapattı.");
        }

        private void ActivityTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            TimeSpan timePassedSinceLastTick = now - lastTickTime;


            if (timePassedSinceLastTick.TotalSeconds > 12)
            {
                string uykuBaslangic = lastTickTime.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss");
                string uyanisZamani = now.ToString("yyyy-MM-dd HH:mm:ss");

      
                SaveManualSleepLogs(uykuBaslangic, uyanisZamani);
            }

           
            lastTickTime = now;
            uint idleTime = GetIdleTimeInSeconds();

            if (idleTime >= PASSIVE_THRESHOLD_SECONDS)
            {
                if (!isPassive)
                {
                    isPassive = true;
                    DatabaseManager.SaveLog("PASIF", $"Kullanıcı {PASSIVE_THRESHOLD_SECONDS / 60} dakikadır hareketsiz.");
                }
            }
            else
            {
                if (isPassive)
                {
                    isPassive = false;
                    DatabaseManager.SaveLog("AKTIF", "Kullanıcı bilgisayar başına geri döndü.");
                }
            }
        }


        private uint GetIdleTimeInSeconds()
        {
            LASTINPUTINFO lii = new LASTINPUTINFO();
            lii.cbSize = (uint)Marshal.SizeOf(lii);
            if (GetLastInputInfo(ref lii))
            {
                uint envTicks = (uint)Environment.TickCount;
                return (envTicks - lii.dwTime) / 1000;
            }
            return 0;
        }
        private void SaveManualSleepLogs(string startTime, string endTime)
        {
            try
            {
                string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SistemAktiviteLog.db");
                using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    conn.Open();

                    
                    string query1 = "INSERT INTO SystemLogs (LogTime, LogType, Description, DeviceName, MacAddress) VALUES (@time, 'UYKU', 'Modern Standby zaman sapmasıyla yakalandı.', @device, @mac)";
                    using (var cmd = new SQLiteCommand(query1, conn))
                    {
                        cmd.Parameters.AddWithValue("@time", startTime);
                        cmd.Parameters.AddWithValue("@device", Environment.MachineName);
                        cmd.Parameters.AddWithValue("@mac", GetMacAddressLocal());
                        cmd.ExecuteNonQuery();
                    }

                    
                    string query2 = "INSERT INTO SystemLogs (LogTime, LogType, Description, DeviceName, MacAddress) VALUES (@time, 'UYKUDAN_UYANDI', 'Modern Standby uyanışı tespit edildi.', @device, @mac)";
                    using (var cmd = new SQLiteCommand(query2, conn))
                    {
                        cmd.Parameters.AddWithValue("@time", endTime);
                        cmd.Parameters.AddWithValue("@device", Environment.MachineName);
                        cmd.Parameters.AddWithValue("@mac", GetMacAddressLocal());
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        private string GetMacAddressLocal()
        {
            try
            {
                foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up && nic.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback)
                    {
                        return nic.GetPhysicalAddress().ToString();
                    }
                }
            }
            catch { }
            return "UNKNOWN_MAC";
        }

        private void OnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    DatabaseManager.SaveLog("EKRAN_KAPANDI", "Oturum kilitlendi veya ekran kapandı.");
                    break;
                case SessionSwitchReason.SessionUnlock:
                    DatabaseManager.SaveLog("EKRAN_ACILDI", "Oturum kilidi açıldı.");
                    break;
            }
        }

        private void OnSessionEnding(object sender, SessionEndingEventArgs e)
        {
            if (e.Reason == SessionEndReasons.SystemShutdown)
            {
                DatabaseManager.SaveLog("SISTEM_KAPATILDI", "Kullanıcı bilgisayarı tamamen kapattı (Shutdown).");
            }
            else if (e.Reason == SessionEndReasons.Logoff)
            {
                DatabaseManager.SaveLog("YENIDEN_BASLATILDI", "Bilgisayar yeniden başlatılıyor veya oturum kapatılıyor.");
            }
        }
    }
}