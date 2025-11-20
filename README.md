# ScrewTest (TestBench)

Atlas Copco PF8000 ve MT6000 tork kontrollü vidalama cihazlarını simüle edip test etmek için hazırlanmış Windows Forms tabanlı bir test bench uygulamasıdır. Ethernet üzerinden gelen/giden MID mesajlarını dinleyip üretir, operatörün tork, açı ve OK/NOK sonuçlarını elle belirleyerek sahada cihaz bağlı olmadan doğrulama yapmasına imkân tanır.

## Mimari Genel Bakış

- `testUygulamasi` ana formu, PF8000 ve MT6000 kullanıcı kontrolleri arasında geçiş yapar.
- `Screw` klasörü altındaki `PF8000` ve `MT6000` kontrolleri, kullanıcı girdilerini alır ve loglar.
- `Screw/Communication` katmanı Atlas Copco makineleri için Ethernet tabanlı iletişimi soyutlar (`ICommunicationStrategy`, `IMachine`, `EthernetCommunicaton`, `EthernetScrewMachineBase`).
- `DTO` katmanı çizgi/hat yapısı (`LineStructure`), standart cevap sarmalayıcısı (`StandardData`) ve makine tiplerini (`MachineType`) içerir.
- `EthernetCommunicaton` sınıfı aynı anda TCP listener, stream yönetimi, MID yanıtları (0001/0002, 0060/0005, 0018 vb.) ve loglamayı üstlenir.

## Öne Çıkan Özellikler

- PF8000 ve MT6000 sürücüleri için ayrı kullanıcı kontrolleri
- Ethernet portu aç/kapat, dinleme ve istemci kabulü
- MID0001, MID0005, MID0018, MID0060, MID9999 gibi temel Atlas Copco mesajlarını üretip cevaplama
- Kullanıcı tarafından belirlenen tork, açı ve OK/NOK sonuçlarını örnek MID gövdelerine enjekte edip gönderme
- Dinamik Pset listesi oluşturma ve MID0018 doğrulaması
- Renk kodlu log penceresi ile operasyon durumlarını izleme

## Kurulum & Çalıştırma

```bash
cd ScrewTest
dotnet restore
dotnet build
dotnet run --project TestBench.csproj
```

> Uygulama Windows Forms olduğu için `dotnet run` komutu GUI’yi başlatır. Visual Studio kullanıyorsanız `TestBench.sln` dosyasını açıp F5 ile de çalıştırabilirsiniz.

## Kullanım Adımları

1. Uygulama açıldığında PF8000 veya MT6000 butonlarından birini seçin.
2. `Ethernet Portu Aç` butonuyla TCP dinlemesini başlatın. Log alanında durum mesajları yeşil/turuncu olarak görünür.
3. Tork ve açı değerlerini girin, OK/NOK durumunu seçin. Girilen değerler hazır MID gövdesindeki placeholder’ların yerine yazılır.
4. `Send Data` ile istemciye örnek üretim sonuç paketi gönderin. Gelen/çıkan mesajlar logda izlenebilir.
5. Pset testleri için değer ekleyip MID0018 akışıyla doğrulayabilirsiniz.
6. İş bitince `Ethernet Portu Kapat` ile listener ve açık bağlantıları temizleyin.

## Protokol Akışı

- **MID0001 → MID0002:** Cihaz bağlantı el sıkışması.
- **MID0060 → MID0005:** PF8000 abonelik doğrulaması.
- **MID0008 → MID0005:** MT6000 abonelik doğrulaması.
- **MID0018:** Pset sorgusu; girilen Pset listesine göre başarılı/başarısız cevap üretir.
- **MID9999 → MID9999:** Keep-alive mesajları.

Tüm yanıtlar `EthernetCommunicaton` içindeki `ReadLoop` metodunda yönetilir ve gönderilen ham stringler Atlas Copco MID formatına sadık kalır.

## Klasör Yapısı

```text
ScrewTest/
├── DTO/                     # LineStructure, MachineType, StandardData
├── Screw/
│   ├── Communication/       # İletişim stratejileri ve Atlas Copco taban sınıfı
│   ├── MT6000.*             # MT6000 kullanıcı kontrolü + designer
│   └── PF8000.*             # PF8000 kullanıcı kontrolü + designer
├── testUygulamasi.*         # Ana WinForms formu
├── Program.cs
├── TestBench.csproj
└── TestBench.sln
```



