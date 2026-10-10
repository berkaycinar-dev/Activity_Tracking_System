using System;
using System.Drawing;
using System.Windows.Forms;

namespace AktiviteTakipSistemi
{
    public partial class Form1 : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        public Form1()
        {
            InitializeComponent();

            this.Size = new Size(1, 1);
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.Opacity = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Cihaz Yönetim Paneli", OnShowManagementPanel);
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Sistemden Çıkış", OnExit);


            trayIcon = new NotifyIcon();
            trayIcon.Text = "Aktivite ve Cihaz Takip Sistemi (Arka Plan Motoru)";

      
            trayIcon.Icon = SystemIcons.Application;

            trayIcon.ContextMenu = trayMenu;
            trayIcon.DoubleClick += OnShowManagementPanel;

            trayIcon.Visible = true;
        }

        private void OnShowManagementPanel(object sender, EventArgs e)
        {
            CihazSecimForm secimEkrani = new CihazSecimForm();
            secimEkrani.Show();
        }

 
        private void OnExit(object sender, EventArgs e)
        {
       
            if (trayIcon != null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
            }

    
            Application.Exit();
        }

     
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
            base.OnFormClosing(e);
        }
    }
}