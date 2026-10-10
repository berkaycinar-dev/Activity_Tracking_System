using System;
using System.Data.SQLite;
using System.IO;
using System.Net.NetworkInformation;

namespace AktiviteTakipSistemi.Data
{
    public static class DatabaseManager
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SistemAktiviteLog.db");
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        public static void InitializeDatabase()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS SystemLogs (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        LogTime DATETIME NOT NULL,
                        LogType TEXT NOT NULL,
                        Description TEXT,
                        DeviceName TEXT NOT NULL,
                        MacAddress TEXT NOT NULL
                    );";

                using (var cmd = new SQLiteCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static string GetMacAddress()
        {
            try
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    {
                        return nic.GetPhysicalAddress().ToString();
                    }
                }
            }
            catch { }
            return "UNKNOWN_MAC";
        }

        public static void SaveLog(string logType, string description)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string insertQuery = @"
                        INSERT INTO SystemLogs (LogTime, LogType, Description, DeviceName, MacAddress) 
                        VALUES (@time, @type, @desc, @device, @mac);";

                    using (var cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@type", logType);
                        cmd.Parameters.AddWithValue("@desc", description);
                        cmd.Parameters.AddWithValue("@device", Environment.MachineName);
                        cmd.Parameters.AddWithValue("@mac", GetMacAddress());

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Log yazma hatası: " + ex.Message);
            }
        }
    }
}