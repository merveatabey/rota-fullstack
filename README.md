# Rota Fullstack Web Uygulaması

Bu proje, seyahat/tur rezervasyonlarını yönetmek amacıyla geliştirilmiş, kullanıcı ve admin panellerine sahip bir fullstack web uygulamasıdır. Uygulama ASP.NET Core (backend) ve React (frontend) teknolojileriyle geliştirilmiştir.


Kullanıcı kaydı oluşturulduktan sonra giriş yapılabilir. Kullanıcı şifresini unutursa, şifremi unuttum ekranında mail adresini girdiğinde maili kontrol edilir ve databse'deki mail ile eşleşiyorsa mailine geçici bir şifre gönderilir. Kullanıcı mailine gelen 6 haneli kodu kullanrak yeni bir şifre belirleyip tekrardan giriş yapabilir.
![register](https://github.com/user-attachments/assets/6bfed8b4-86c0-4bdb-bf51-0f702b971409)
![giris](https://github.com/user-attachments/assets/7eb68863-a55a-4e05-9a58-77a017674871)


Turlar sayfası admin tarafında eklenen her tur paketinin gösterilmesini sağlar. Kullanıcı isteğine göre filtreleme yapabilir ya da arama butonundan spesifik bir tur arayabilir. 
![turlar](https://github.com/user-attachments/assets/b8431e0d-db93-4aaf-8eba-e9c8ed6f1515)

Tur detay sayfası turun program akışını saat ve güne göre listeleler. Kullanıcı isterse tur planını pdf olarak indirebilir. Daha sonra terkar ziyaret etmek için favorilerine ekleyebilir.  
![detay](https://github.com/user-attachments/assets/51175966-4913-4168-aa02-ca72d6b20421)

Kullanıcı kendi favoriler sayfasında tur paketlerini favorilerden çıkarabilir ya da direkt satın alabilir. 
![favoriler](https://github.com/user-attachments/assets/aa67a2dc-ce9f-4838-9260-be11919f32a6)

Tur detay sayfasında rezervasyon yap ve satın al butonları var. Rezervasyon yapan kullanıcı 10 gün içerisinde rezervasyonunu onaylamalı ve satın alma işlemini tamamlamalıdır. Rezervasyon yapıldığında tur paketi kullanıcının turlar sayfasına bekliyor olarak eklenir ve admin rezervasyonu onaylarsa, rezervasyon durumu rezerve olarak değişir.
![randevu](https://github.com/user-attachments/assets/160bfc49-5f94-4e5a-afed-9727b7457fcc)

Tur satın alma sayfasında rezervasyondan farklı olarak kart bilgileri alınır.

<img width="1280" height="800" alt="Ekran Resmi 2025-07-24 11 24 12" src="https://github.com/user-attachments/assets/5f1fff45-ef38-4ed8-adf7-7925fb4c95bd" />

Bütün süreçlerin yönetimini yapan admin tarafında CRUD işlemleri ve raporlar sunumları yer almaktadır.
![admin](https://github.com/user-attachments/assets/60b5c6ae-b548-4862-919e-21dc7f9e0e89)



##  Özellikler

###  Kullanıcı Tarafı

-  Üye Olma ve Giriş Yapma (JWT Token ile)
-  Şifremi Unuttum Özelliği
  - Gerçek e-mail adresine geçici token gönderimi
  - Token ile yeni şifre belirleme
-  Tur Rezervasyonu
  - Turlar için satın alma işlemleri
  - 10 gün içinde rezervasyon iptal etme imkânı
-  Tur Değerlendirme
  - Değerlendirme puanı 4 ve üzeri olan turlar ana sayfada “Popüler Turlar” olarak listelenir
-  Gelişmiş Arama ve Filtreleme
  - Turları başlık, açıklama vb. kriterlerle arama
  - Kategori, fiyat, tarih gibi filtreleme seçenekleri
-  Favori Turlara Ekleme
-  Tur Detaylarını PDF Olarak Dışa Aktarma
  - (React tarafında `html2pdf.js` kütüphanesi kullanıldı)

###  Admin Paneli

-  Admin Girişi (JWT ile)
-  Raporlar Sayfası (Dapper ile)
  - Toplam bütçe
  - Günlük üye olan kullanıcı sayısı
  - Günlük rezervasyon sayısı
-  Rezervasyon Onayı
  - Kullanıcı tarafından yapılan rezervasyonların durumu "Bekliyor" olarak gelir, admin onayladığında "Rezerve" durumuna geçer
-  Admin Kendi Kayıt Olamaz
  - Yalnızca başka bir admin tarafından yönetim panelinden oluşturulabilir


##  Kullanılan Teknolojiler

### Backend (.NET Core)
- ASP.NET Core Web API
- Entity Framework Core (CRUD işlemleri)
- Dapper (Raporlama işlemleri için)
- JWT Authentication
- Mail Gönderimi (şifre sıfırlama için)
- SQL Server

### Frontend (React)
- React.js (VS Code ile geliştirildi)
- Axios (API istekleri için)
- React Router
- html2pdf.js (PDF dışa aktarma işlemleri için)
- TailwindCSS (isteğe bağlı)

## Başlatma (Local)

### 1. Backend
```bash
cd backend
dotnet restore
dotnet run

### 2. Frontend
cd frontend
npm install
npm start


rota-fullstack/
├── backend/     → ASP.NET Core API projesi
├── frontend/    → React uygulaması






