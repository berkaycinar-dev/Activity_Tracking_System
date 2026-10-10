**🌐 Language / Dil:** [English](README.md) | Türkçe

# Aktivite Takip Sistemi — Windows Bilgisayar Aktivite ve Çalışma Süresi Takibi

![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.6.2-512BD4?logo=dotnet&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-0078D6?logo=windows&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-003B57?logo=sqlite&logoColor=white)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey)

**Aktivite Takip Sistemi**, Windows bilgisayarlarda arka planda çalışan ve bilgisayarın ne zaman açıldığını, kullanıcının ne zaman aktif ya da boşta olduğunu, ekranın ne zaman kilitlendiğini ve cihazın ne zaman uykuya geçtiğini kaydeden bir masaüstü uygulamasıdır. Kaydedilen olaylardan günlük ve haftalık **çalışma süresi raporları** üretir.

Uygulama sistem tepsisinde (saat yanında) çalışır; raporlar tepsi simgesinden açılan bir yönetim panelinden görüntülenir. Proje, Hetabil Yazılım stajı sürecinde geliştirilmiştir.

---

## İçindekiler

1. [Özellikler](#1-özellikler)
2. [Nasıl çalışır?](#2-nasıl-çalışır)
3. [Gizlilik: Ne kaydedilir, ne kaydedilmez?](#3-gizlilik-ne-kaydedilir-ne-kaydedilmez)
4. [Kullanılan teknolojiler](#4-kullanılan-teknolojiler)
5. [Proje yapısı](#5-proje-yapısı)
6. [Kurulum ve çalıştırma](#6-kurulum-ve-çalıştırma)
7. [Kullanım](#7-kullanım)
8. [Veritabanı yapısı](#8-veritabanı-yapısı)
9. [Otomatik başlatmayı kaldırma](#9-otomatik-başlatmayı-kaldırma)
10. [Bilinen sınırlamalar](#10-bilinen-sınırlamalar)

---

## 1. Özellikler

- **Arka planda sessiz çalışma:** Pencere açmaz, görev çubuğunda görünmez; yalnızca sistem tepsisinde bir simge bulunur.
- **Otomatik başlatma:** İlk çalıştırmada kendini Windows başlangıcına ekler.
- **Aktif / pasif tespiti:** 10 dakika boyunca klavye veya fare kullanılmazsa kullanıcıyı pasif (boşta) sayar.
- **Ekran kilidi takibi:** Oturum kilitlenme ve kilit açılma olaylarını kaydeder.
- **Uyku modu tespiti:** Sayaçtaki zaman sıçramasından uykuya geçişi ve uyanışı yakalar (Modern Standby dahil).
- **Kapanış takibi:** Sistemin kapatılması ve oturumun sonlanması kaydedilir.
- **Günlük rapor:** Toplam çalışma, aktif çalışma, pasif, uyku ve ekran kapalı süreleri.
- **Haftalık rapor:** Son 7 günün özeti.
- **Ham log dökümü:** Günün tüm kayıtlarını tablo olarak gösterir.
- **Yerel veri saklama:** Tüm veriler uygulama klasöründeki bir SQLite dosyasında tutulur.

---

## 2. Nasıl çalışır?

### 2.1 Başlangıç akışı

1. Uygulama açılır ve `SistemAktiviteLog.db` veritabanını (yoksa) oluşturur.
2. Windows başlangıç kaydı yoksa ekler.
3. Arka plan izleyicisi (`SystemWatcher`) başlar ve `OTURUM_ACILDI` kaydı yazılır.
4. Görünmez ana form çalışır ve sistem tepsisine simge eklenir.

### 2.2 İzleme mantığı

| Olay | Nasıl tespit edilir? | Kaydedilen log türü |
|------|----------------------|---------------------|
| Uygulama başladı | Başlangıçta | `OTURUM_ACILDI` |
| Uygulama kapandı | Çıkışta | `OTURUM_KAPANDI` |
| Kullanıcı pasif oldu | Son girdiden bu yana 600 sn (10 dk) geçti | `PASIF` |
| Kullanıcı geri döndü | Pasif durumdayken girdi algılandı | `AKTIF` |
| Ekran / oturum kilitlendi | Windows `SessionLock` olayı | `EKRAN_KAPANDI` |
| Kilit açıldı | Windows `SessionUnlock` olayı | `EKRAN_ACILDI` |
| Uykuya geçti | 5 sn'lik sayaç ardışık iki tetiklenme arasında 12 sn'den fazla boşluk gördü | `UYKU` |
| Uykudan uyandı | Aynı boşluğun bitiş zamanı | `UYKUDAN_UYANDI` |
| Sistem kapatıldı | `SessionEnding` olayı (kapatma) | `SISTEM_KAPATILDI` |
| Oturum kapatıldı / yeniden başlatıldı | `SessionEnding` olayı (oturum kapatma) | `YENIDEN_BASLATILDI` |

Boşta kalma süresi, Windows'un `GetLastInputInfo` fonksiyonuyla (user32.dll) okunur. Sayaç her **5 saniyede** bir çalışır.

### 2.3 Rapor hesaplama

Raporlar (`ReportAnalyst`), seçilen güne ait kayıtları zamana göre sıralar ve **ardışık iki kayıt arasındaki süreyi, önceki kaydın türüne göre** bir kategoriye ekler:

| Önceki kaydın türü | Eklendiği kategori |
|--------------------|--------------------|
| `AKTIF`, `OTURUM_ACILDI`, `UYKUDAN_UYANDI`, `EKRAN_ACILDI` | Toplam çalışma süresi |
| `PASIF` | Pasif süre ve toplam çalışma süresi |
| `UYKU` | Uyku süresi |
| `EKRAN_KAPANDI` | Ekran kapalı süresi |

- **Ekran kapalı süresi (toplam)** = Yalnızca ekran kapalı süresi + uyku süresi
- **Aktif çalışma süresi** = Toplam çalışma − (Pasif + Ekran kapalı toplamı); negatif çıkarsa 0 gösterilir.
- Bugünün raporunda, son kayıttan şu ana kadar geçen süre de ilgili kategoriye eklenir (son kayıt `OTURUM_KAPANDI` veya `UYKU` değilse).

---

## 3. Gizlilik: Ne kaydedilir, ne kaydedilmez?

**Kaydedilenler** (her kayıt için): zaman damgası, olay türü, kısa açıklama, bilgisayar adı ve ağ kartının MAC adresi.

**Kaydedilmeyenler:** basılan tuşlar, fare hareketleri, ekran görüntüleri, açık pencereler veya uygulamalar, ziyaret edilen web siteleri, dosya içerikleri.

**Ağ bağlantısı:** Kaynak kodda herhangi bir ağ üzerinden veri gönderimi yoktur; tüm kayıtlar yalnızca yerel `SistemAktiviteLog.db` dosyasında kalır.

> Bu tür bir yazılımı yalnızca **kendi bilgisayarınızda** veya ilgili kişilerin **bilgisi ve onayı** ile kullanın. Çalışan takibi amacıyla kullanımda yürürlükteki mevzuata (örneğin KVKK) uymak kullanıcının sorumluluğundadır.

---

## 4. Kullanılan teknolojiler

| Teknoloji | Kullanım amacı |
|-----------|----------------|
| **C#** | Uygulama dili |
| **.NET Framework 4.6.2** | Çalışma zamanı |
| **Windows Forms** | Tepsi simgesi ve rapor ekranları |
| **SQLite** (`System.Data.SQLite` 1.0.118.0) | Yerel olay kaydı |
| **P/Invoke** (`user32.dll`) | Son kullanıcı girdisi zamanını okuma |
| **`Microsoft.Win32`** | Oturum olayları (`SystemEvents`) ve Windows başlangıç kaydı (`Registry`) |
| **Visual Studio 2022** | Çözüm dosyası (`.sln`) bu sürümle oluşturulmuştur |

---

## 5. Proje yapısı

```text
Activity_Tracking_System/
├── AktiviteTakipSistemi.sln
├── README.md                       # English documentation
├── README.tr.md                    # Türkçe dokümantasyon
├── .gitignore
└── AktiviteTakipSistemi/
    ├── AktiviteTakipSistemi.csproj
    ├── App.config
    ├── packages.config             # NuGet bağımlılıkları
    ├── Program.cs                  # Giriş noktası: veritabanı, otomatik başlatma, izleyici
    ├── Form1.cs                    # Görünmez ana form + sistem tepsisi simgesi ve menüsü
    ├── CihazSecimForm.cs           # Cihaz seçim ekranı
    ├── GunlukRaporForm.cs          # Günlük rapor paneli
    ├── HaftalikRaporForm.cs        # Son 7 gün özeti
    ├── TumLoglarForm.cs            # Günün ham log tablosu
    ├── Data/
    │   └── DatabaseManager.cs      # Veritabanı oluşturma ve log yazma
    ├── Logic/
    │   ├── SystemWatcher.cs        # Aktif/pasif, uyku, kilit ve kapanış izleme
    │   ├── ReportAnalyst.cs        # Süre hesaplama
    │   └── StartupManager.cs       # Windows başlangıç kaydı
    └── Properties/
```

---

## 6. Kurulum ve çalıştırma

> **Gereksinimler:** Windows (Modern Standby uyku tespiti için Windows 10 veya üzeri önerilir), **Visual Studio 2022** (".NET masaüstü geliştirme" iş yükü ile) ve .NET Framework 4.6.2 geliştirici paketi.

### Yöntem A — Visual Studio ile (önerilen)

1. Depoyu klonlayın:
   ```bash
   git clone https://github.com/berkaycinar-dev/Activity_Tracking_System.git
   ```
2. `AktiviteTakipSistemi.sln` dosyasını Visual Studio ile açın.
3. NuGet paketlerinin otomatik geri yüklenmesini bekleyin. Olmazsa **Çözüm → sağ tık → NuGet Paketlerini Geri Yükle** seçeneğini kullanın.
4. Üst menüden yapılandırmayı **Debug** veya **Release** seçin.
5. **F5** ile çalıştırın. Pencere açılmaz; saat yanındaki gizli simgeler bölümünde (`^` oku) uygulamanın simgesini göreceksiniz.

### Yöntem B — Komut satırından derleme

1. Visual Studio **Developer Command Prompt**'u açın.
2. Depo klasörüne girin:
   ```bat
   cd Activity_Tracking_System
   ```
3. Paketleri geri yükleyin (`nuget.exe` yüklü değilse [nuget.org](https://www.nuget.org/downloads) adresinden indirip PATH'e ekleyin):
   ```bat
   nuget restore AktiviteTakipSistemi.sln
   ```
4. Derleyin:
   ```bat
   msbuild AktiviteTakipSistemi.sln /p:Configuration=Release
   ```
5. Çıktı `AktiviteTakipSistemi\bin\Release\` klasöründe oluşur. `AktiviteTakipSistemi.exe` dosyasını çalıştırın.

> **Dikkat:**
> - `x86` ve `x64` klasörleri ile `System.Data.SQLite.dll`, `.exe` ile **aynı klasörde** kalmalıdır; SQLite yerel kütüphaneleri buradan yüklenir.
> - Veritabanı `.exe`'nin bulunduğu klasöre yazılır. Yazma izni olmayan bir konuma (ör. `Program Files`) koymayın.
> - Visual Studio'dan çalıştırdığınızda otomatik başlatma kaydı `bin\Debug` içindeki `.exe` yolunu gösterir. Geliştirme sonrası [kaydı kaldırmayı](#9-otomatik-başlatmayı-kaldırma) unutmayın.

---

## 7. Kullanım

1. Uygulamayı başlatın. Arka planda kayıt almaya hemen başlar.
2. Sistem tepsisindeki uygulama simgesine **çift tıklayın** (veya sağ tıklayıp **Cihaz Yönetim Paneli**'ni seçin).
3. **Cihaz Yönetim Paneli Giriş** ekranında listeden cihazı seçin ve **Cihaz Onay**'a basın. Liste, veritabanındaki kayıtlı cihazları `BilgisayarAdı [MAC]` biçiminde gösterir.
4. **Cihaz Günlük Takip Paneli** açılır:
   - **Bugünün Raporunu Özetle:** Toplam, aktif, pasif, uyku ve ekran kapalı sürelerini hesaplar; ayrıca *Son 5 Sistem Hareketi* listesini doldurur.
   - **Bugüne Ait Tüm Loglar:** Günün tüm kayıtlarını tablo olarak açar.
   - **Haftalık Raporu Göster:** Son 7 günün her biri için aktif ve toplam açık süreyi listeler.
5. Uygulamayı tamamen kapatmak için tepsi simgesine sağ tıklayıp **Sistemden Çıkış**'ı seçin.

---

## 8. Veritabanı yapısı

Dosya: `SistemAktiviteLog.db` (uygulamanın `.exe` dosyasıyla aynı klasörde, ilk çalıştırmada oluşur)

**Tablo: `SystemLogs`**

| Sütun | Tür | Açıklama |
|-------|-----|----------|
| `Id` | INTEGER, PK, AUTOINCREMENT | Kayıt numarası |
| `LogTime` | DATETIME | `yyyy-MM-dd HH:mm:ss` biçiminde olay zamanı |
| `LogType` | TEXT | Olay türü (bkz. [İzleme mantığı](#22-izleme-mantığı)) |
| `Description` | TEXT | Kısa açıklama |
| `DeviceName` | TEXT | Bilgisayar adı |
| `MacAddress` | TEXT | Aktif ağ kartının MAC adresi (bulunamazsa `UNKNOWN_MAC`) |

Dosyayı [DB Browser for SQLite](https://sqlitebrowser.org/) gibi bir araçla inceleyebilirsiniz.

---

## 9. Otomatik başlatmayı kaldırma

Uygulama ilk çalıştırmada şu kayıt defteri değerini oluşturur:

```text
HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
Değer adı: AktiviteTakipSistemi
```

Kaldırmak için şu adımları izleyin:

1. Tepsi simgesinden **Sistemden Çıkış** ile uygulamayı kapatın.
2. **PowerShell**'i açın.
3. Şu komutu çalıştırın:
   ```powershell
   Remove-ItemProperty -Path "HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Run" -Name "AktiviteTakipSistemi"
   ```
4. (İsteğe bağlı) Kayıtları silmek için `SistemAktiviteLog.db` dosyasını silin.

Alternatif olarak **Görev Yöneticisi → Başlangıç uygulamaları** sekmesinden de devre dışı bırakabilirsiniz.

---

## 10. Bilinen sınırlamalar

- **Yalnızca Windows** üzerinde çalışır (Windows Forms, `user32.dll` ve kayıt defteri kullanır).
- Veritabanı **cihaza yereldir**; cihaz seçim ekranı yalnızca o veritabanında kayıtlı cihazları listeler. Farklı bilgisayarların verisi otomatik olarak birleştirilmez.
- Uykuya geçiş, kayıt anında değil **uyanış anında** tespit edilir; başlangıç zamanı son sayaç tetiklenmesine göre tahmin edilir.
- Uygulama sistem kapanışını yalnızca düzgün kapanış/oturum sonlandırma olaylarıyla kaydeder; ani elektrik kesintisi gibi durumlarda kapanış kaydı oluşmaz.
- Cihaz Yönetim Paneli her açıldığında bir `AKTIF` kaydı yazılır; bu kayıt raporlardaki sürelere dahil olur.
- Otomatik başlatma uygulama içinden kapatılamaz; [9. bölümdeki](#9-otomatik-başlatmayı-kaldırma) adımlar gerekir.
