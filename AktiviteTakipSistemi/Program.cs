using System;
using System.Windows.Forms;
using AktiviteTakipSistemi.Data;
using AktiviteTakipSistemi.Logic;

namespace AktiviteTakipSistemi
{
    static class Program
    {
        private static SystemWatcher watcher;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
             
                DatabaseManager.InitializeDatabase();

          
                if (!StartupManager.IsInStartup())
                {
                    StartupManager.SetAutostart(true);
                }

       
                watcher = new SystemWatcher();
                watcher.Start();


                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sistem başlatılırken kritik hata: " + ex.Message, "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            
                if (watcher != null)
                {
                    watcher.Stop();
                }
            }
        }
    }
}