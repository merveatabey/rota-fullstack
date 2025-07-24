# Rota Fullstack Web Uygulaması

Bu proje, seyahat/tur rezervasyonlarını yönetmek amacıyla geliştirilmiş, kullanıcı ve admin panellerine sahip bir fullstack web uygulamasıdır. Uygulama ASP.NET Core (backend) ve React (frontend) teknolojileriyle geliştirilmiştir.

##  Özellikler

###  Kullanıcı Tarafı

- ✅ Üye Olma ve Giriş Yapma (JWT Token ile)
- 🔑 Şifremi Unuttum Özelliği
  - Gerçek e-mail adresine geçici token gönderimi
  - Token ile yeni şifre belirleme
- 🧳 Tur Rezervasyonu
  - Turlar için satın alma işlemleri
  - 10 gün içinde rezervasyon iptal etme imkânı
- ⭐ Tur Değerlendirme
  - Değerlendirme puanı 4 ve üzeri olan turlar ana sayfada “Popüler Turlar” olarak listelenir
- 🔍 Gelişmiş Arama ve Filtreleme
  - Turları başlık, açıklama vb. kriterlerle arama
  - Kategori, fiyat, tarih gibi filtreleme seçenekleri
- ❤️ Favori Turlara Ekleme
- 📄 Tur Detaylarını PDF Olarak Dışa Aktarma
  - (React tarafında `html2pdf.js` kütüphanesi kullanıldı)

###  Admin Paneli

- 👨‍💼 Admin Girişi (JWT ile)
- 📊 Raporlar Sayfası (Dapper ile)
  - Toplam bütçe
  - Günlük üye olan kullanıcı sayısı
  - Günlük rezervasyon sayısı
- ✅ Rezervasyon Onayı
  - Kullanıcı tarafından yapılan rezervasyonların durumu "Bekliyor" olarak gelir, admin onayladığında "Rezerve" durumuna geçer
- 🔒 Admin Kendi Kayıt Olamaz
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

## 🚀 Başlatma (Local)

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






