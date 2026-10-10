using System;
using System.Data.SQLite;
using System.IO;

namespace AktiviteTakipSistemi.Logic
{
    public class ReportAnalyst
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SistemAktiviteLog.db");
        private static string connString = $"Data Source={dbPath};Version=3;";

        public struct ActivitySummary
        {
            public TimeSpan TotalWorkTime;     
            public TimeSpan ActiveTime;        
            public TimeSpan PassiveTime;       
            public TimeSpan SleepTime;         
            public TimeSpan PureScreenOffTime; 
            public TimeSpan TotalScreenOff;    
        }

        public static ActivitySummary CalculateDeviceSummary(string mac, int daysAgo = 0)
        {
            ActivitySummary summary = new ActivitySummary();

            using (var conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string query = @"
                    SELECT LogTime, LogType FROM SystemLogs 
                    WHERE MacAddress LIKE @mac AND date(LogTime) = date('now', 'localtime', @days) 
                    ORDER BY LogTime ASC";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mac", "%" + mac.Trim() + "%");
                    cmd.Parameters.AddWithValue("@days", $"-{daysAgo} day");

                    using (var reader = cmd.ExecuteReader())
                    {
                        DateTime? lastTime = null;
                        string lastType = null;

                        while (reader.Read())
                        {
                            DateTime curTime = Convert.ToDateTime(reader["LogTime"]);
                            string curType = reader["LogType"].ToString();

                            if (lastTime != null)
                            {
                                TimeSpan diff = curTime - lastTime.Value;
                                AddToCategory(ref summary, lastType, diff);
                            }
                            lastTime = curTime;
                            lastType = curType;
                        }

                        if (lastTime != null && daysAgo == 0 && lastType != "OTURUM_KAPANDI" && lastType != "UYKU")
                        {
                            TimeSpan kalanDiff = DateTime.Now - lastTime.Value;
                            AddToCategory(ref summary, lastType, kalanDiff);
                        }
                    }
                }
            }

            summary.TotalScreenOff = summary.PureScreenOffTime + summary.SleepTime;

           
            double aktifSaniye = summary.TotalWorkTime.TotalSeconds - (summary.PassiveTime.TotalSeconds + summary.TotalScreenOff.TotalSeconds);
            summary.ActiveTime = TimeSpan.FromSeconds(Math.Max(0, aktifSaniye));

            return summary;
        }

        private static void AddToCategory(ref ActivitySummary s, string type, TimeSpan d)
        {
            switch (type)
            {
                case "AKTIF":
                case "OTURUM_ACILDI":
                case "UYKUDAN_UYANDI":
                case "EKRAN_ACILDI":
                    s.TotalWorkTime += d;
                    break;

                case "PASIF":
                    s.PassiveTime += d;
                    s.TotalWorkTime += d;
                    break;

                case "UYKU":
                    s.SleepTime += d;
                    break;

                case "EKRAN_KAPANDI":
                    s.PureScreenOffTime += d; 
                    break;
            }
        }
    }
}