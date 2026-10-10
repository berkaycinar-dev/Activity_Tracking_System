using System;
using Microsoft.Win32;
using System.Windows.Forms;

namespace AktiviteTakipSistemi.Logic
{
    public static class StartupManager
    {
        private const string RegistryKeyName = "AktiviteTakipSistemi";

        public static void SetAutostart(bool enable)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
                {
                    if (key != null)
                    {
                        if (enable)
                        {
                            key.SetValue(RegistryKeyName, $"\"{Application.ExecutablePath}\"");
                        }
                        else
                        {
                            if (key.GetValue(RegistryKeyName) != null)
                                key.DeleteValue(RegistryKeyName);
                        }
                    }
                }
            }
            catch { }
        }

        public static bool IsInStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false))
            {
                return key != null && key.GetValue(RegistryKeyName) != null;
            }
        }
    }
}