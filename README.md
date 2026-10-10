Activity Tracking System — Windows Computer Activity and Work-Time Tracker

Show Image Show Image Show Image Show Image Show Image

Activity Tracking System is a desktop application that runs in the background on Windows computers and records when the computer is started, when the user is active or idle, when the screen is locked and when the device goes to sleep. From these events it produces daily and weekly work-time reports.

The application lives in the system tray (next to the clock); reports are viewed from a management panel opened via the tray icon. The project was developed during an internship at Hetabil Yazılım.

Table of Contents
Features
How it works
Privacy: what is and isn't recorded
Technologies used
Project structure
Installation and running
Usage
Database schema
Removing autostart
Known limitations
1. Features
Silent background operation: No window is shown and nothing appears on the taskbar; only a tray icon is present.
Autostart: Adds itself to Windows startup on first run.
Active / idle detection: Treats the user as idle after 10 minutes without keyboard or mouse input.
Screen lock tracking: Records session lock and unlock events.
Sleep detection: Detects going to sleep and waking up from a jump in the timer (including Modern Standby).
Shutdown tracking: System shutdown and session end are recorded.
Daily report: Total work, active work, idle, sleep and screen-off durations.
Weekly report: Summary of the last 7 days.
Raw log view: Shows all of the day's records as a table.
Local data storage: All data is kept in a SQLite file next to the application.
2. How it works
2.1 Startup flow
The application starts and creates the SistemAktiviteLog.db database (if it doesn't exist).
It adds the Windows startup entry if missing.
The background watcher (SystemWatcher) starts and an OTURUM_ACILDI record is written.
The invisible main form runs and the tray icon is added.
2.2 Monitoring logic
Event	How it is detected	Log type recorded
Application started	At startup	OTURUM_ACILDI
Application closed	On exit	OTURUM_KAPANDI
User became idle	600 s (10 min) since the last input	PASIF
User came back	Input detected while idle	AKTIF
Screen / session locked	Windows SessionLock event	EKRAN_KAPANDI
Unlocked	Windows SessionUnlock event	EKRAN_ACILDI
Went to sleep	The 5 s timer saw a gap of more than 12 s between two consecutive ticks	UYKU
Woke from sleep	End time of the same gap	UYKUDAN_UYANDI
System shut down	SessionEnding event (shutdown)	SISTEM_KAPATILDI
Session ended / restart	SessionEnding event (logoff)	YENIDEN_BASLATILDI

Idle time is read with the Windows GetLastInputInfo function (user32.dll). The timer ticks every 5 seconds.

The log type names are stored in Turkish in the database, exactly as shown above.

2.3 Report calculation

The reports (ReportAnalyst) sort the selected day's records by time and add the duration between two consecutive records to a category based on the type of the earlier record:

Type of the earlier record	Category it is added to
AKTIF, OTURUM_ACILDI, UYKUDAN_UYANDI, EKRAN_ACILDI	Total work time
PASIF	Idle time and total work time
UYKU	Sleep time
EKRAN_KAPANDI	Screen-off time
Screen-off time (total) = screen-off-only time + sleep time
Active work time = total work − (idle + total screen-off); shown as 0 if negative.
In today's report, the time from the last record up to now is also added to the matching category (unless the last record is OTURUM_KAPANDI or UYKU).
3. Privacy: what is and isn't recorded

Recorded (for each entry): timestamp, event type, short description, computer name and the network adapter's MAC address.

Not recorded: keystrokes, mouse movements, screenshots, open windows or applications, visited websites, file contents.

Network: The source code contains no network transmission of any kind; all records stay in the local SistemAktiviteLog.db file.

Use software like this only on your own computer or with the knowledge and consent of the people concerned. Complying with applicable law (for example GDPR/KVKK) when using it for employee monitoring is the user's responsibility.

4. Technologies used
Technology	Purpose
C#	Application language
.NET Framework 4.6.2	Runtime
Windows Forms	Tray icon and report screens
SQLite (System.Data.SQLite 1.0.118.0)	Local event log
P/Invoke (user32.dll)	Reading the last user input time
Microsoft.Win32	Session events (SystemEvents) and the Windows startup entry (Registry)
Visual Studio 2022	The solution file (.sln) was created with this version
5. Project structure
text
Activity_Tracking_System/
├── AktiviteTakipSistemi.sln
├── README.md                       # Turkish documentation
├── README.en.md                    # English documentation
├── .gitignore
└── AktiviteTakipSistemi/
    ├── AktiviteTakipSistemi.csproj
    ├── App.config
    ├── packages.config             # NuGet dependencies
    ├── Program.cs                  # Entry point: database, autostart, watcher
    ├── Form1.cs                    # Invisible main form + tray icon and menu
    ├── CihazSecimForm.cs           # Device selection screen
    ├── GunlukRaporForm.cs          # Daily report panel
    ├── HaftalikRaporForm.cs        # Last 7 days summary
    ├── TumLoglarForm.cs            # Raw log table for the day
    ├── Data/
    │   └── DatabaseManager.cs      # Database creation and log writing
    ├── Logic/
    │   ├── SystemWatcher.cs        # Active/idle, sleep, lock and shutdown monitoring
    │   ├── ReportAnalyst.cs        # Duration calculation
    │   └── StartupManager.cs       # Windows startup entry
    └── Properties/
6. Installation and running

Requirements: Windows (Windows 10 or later is recommended for Modern Standby sleep detection), Visual Studio 2022 (with the ".NET desktop development" workload) and the .NET Framework 4.6.2 developer pack.

Method A — With Visual Studio (recommended)
Clone the repository:
bash
   git clone https://github.com/berkaycinar-dev/Activity_Tracking_System.git
Open AktiviteTakipSistemi.sln in Visual Studio.
Wait for the NuGet packages to restore automatically. If they don't, use Solution → right-click → Restore NuGet Packages.
Choose Debug or Release in the toolbar.
Press F5 to run. No window opens; look for the application's icon in the hidden icons area (the ^ arrow) next to the clock.
Method B — Building from the command line
Open the Visual Studio Developer Command Prompt.
Go into the repository folder:
bat
   cd Activity_Tracking_System
Restore the packages (if nuget.exe is not installed, download it from nuget.org and add it to PATH):
bat
   nuget restore AktiviteTakipSistemi.sln
Build:
bat
   msbuild AktiviteTakipSistemi.sln /p:Configuration=Release
The output is created in AktiviteTakipSistemi\bin\Release\. Run AktiviteTakipSistemi.exe.

Note:

The x86 and x64 folders and System.Data.SQLite.dll must stay in the same folder as the .exe; the native SQLite libraries are loaded from there.
The database is written to the folder containing the .exe. Don't place it in a location without write permission (e.g. Program Files).
When you run from Visual Studio, the autostart entry points to the .exe in bin\Debug. Remember to remove the entry after development.
7. Usage
Start the application. It begins recording in the background immediately.
Double-click the application's icon in the system tray (or right-click it and choose Cihaz Yönetim Paneli).
On the Cihaz Yönetim Paneli Giriş (Device Management Panel) screen, pick a device from the list and press Cihaz Onay. The list shows the devices in the database as ComputerName [MAC].
The Cihaz Günlük Takip Paneli (Daily Device Tracking Panel) opens:
Bugünün Raporunu Özetle (Summarize Today's Report): calculates total, active, idle, sleep and screen-off durations and fills the Son 5 Sistem Hareketi (Last 5 System Events) list.
Bugüne Ait Tüm Loglar (All Logs for Today): opens all of the day's records as a table.
Haftalık Raporu Göster (Show Weekly Report): lists the active and total on-time for each of the last 7 days.
To quit the application completely, right-click the tray icon and choose Sistemden Çıkış (Exit System).

The user interface is in Turkish.

8. Database schema

File: SistemAktiviteLog.db (in the same folder as the application's .exe, created on first run)

Table: SystemLogs

Column	Type	Description
Id	INTEGER, PK, AUTOINCREMENT	Record number
LogTime	DATETIME	Event time in yyyy-MM-dd HH:mm:ss format
LogType	TEXT	Event type (see Monitoring logic)
Description	TEXT	Short description
DeviceName	TEXT	Computer name
MacAddress	TEXT	MAC address of the active network adapter (UNKNOWN_MAC if none is found)

You can inspect the file with a tool such as DB Browser for SQLite.

9. Removing autostart

On first run the application creates this registry value:

text
HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
Value name: AktiviteTakipSistemi

To remove it, follow these steps:

Close the application with Sistemden Çıkış from the tray icon.
Open PowerShell.
Run this command:
powershell
   Remove-ItemProperty -Path "HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Run" -Name "AktiviteTakipSistemi"
(Optional) Delete the SistemAktiviteLog.db file to remove the records.

Alternatively, you can disable it from Task Manager → Startup apps.

10. Known limitations
Works on Windows only (uses Windows Forms, user32.dll and the registry).
The database is local to the device; the device selection screen only lists devices recorded in that database. Data from different computers is not merged automatically.
Sleep is detected at wake-up time, not when it happens; the start time is estimated from the last timer tick.
System shutdown is recorded only through proper shutdown/session-end events; a sudden power loss, for example, leaves no shutdown record.
Each time the Device Management Panel opens, an AKTIF record is written; it counts toward the durations in the reports.
Autostart cannot be turned off from within the application; the steps in section 9 are required.
