# ReadingIsGood
ReadingIsGood Repository

1 - Docker Linux-Ubuntu için proje dockerize edildi. Windows container'ları kullanılamadı çünkü, kullandığım işletim sistemi Windows 10 Home.
2 - ReadinIsGood projesini docker'a image olarak deploy etmeden önce, MSSQL server 2019 image'ini docker'da çalıştırmak ve appsettings.json
içerisinde DefaultConnection'a bağlantı cümlesini vermek gerekiyor.
Örnek: "DefaultConnection" : "Server=192.168.80.1,1401;Database=ReadingIsGood;User Id=sa;Password=1q2w3e4r5t6y!"
3 - Veritabanı CodeFirst ile oluşturuluyor. Öncelikle, kodun içerisinden veritabanı ayağa kaldırılmalı.
(PM Console -> add-migration InitialCreate)
(PM Console -> update-database)
Yada,DbBacup altındaki ReadingIsGood.bak dosyası ile restore edebilirsiniz.
4 - Web Api arayüzü için OpenAPI Swagger kullanıldı.
5 - Sistem loglaması için NLog kullanıldı. Loglara ..\ReadingIsGood\bin\Debug\net5.0\Logs ya da ReadingIsGood\bin\Release\net5.0\Logs ya da uygulamanın 
yüklü olduğu dizinden erişebilirsiniz.
6 - Dependency Injection ile bağımlılıklar katmanlara inject edildi.
Controllers katmanı backend arayüz katmanıdır.
Repository katmanı veritabanı erişim katmanıdır.
Domain katmanı arka plan kodlama mantığının çalıştırıldığı derin katmandır.
Dto katmanı arayüze dönen verilerin veritabanı modellerinden ayrıştırıldığı katmandır.
Resource dosyası sistem geneli verilen mesajlar ve logların tutulduğu yerdir.
Startup file içinde gerekli yapılandırmalar yapılmaktadır.
Sistem geneli hata mesajları ReadingIsGoodException tipinde dönülmektedir.
EntityFramework Core ve Linq veritabanı erişim katmanında kullanılmıştır.
Code-First ile veritabanı yapılandırılmıştır.