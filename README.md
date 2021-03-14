# ReadingIsGood
ReadingIsGood Repository

Docker Linux-Ubuntu için proje dockerize edildi. Windows container'ları kullanılamadı çünkü, kullandığım işletim sistemi Windows 10 Home.
ReadinIsGood projesini docker'a image olarak deploy etmeden önce, MSSQL server 2019 image'ini docker'da çalıştırmak ve appsettings.json içerisinde DefaultConnection'a bağlantı cümlesini vermek gerekiyor. Örnek: "DefaultConnection" : "{Server=192.168.80.1,1401;Database=ReadingIsGood;User Id=sa;Password=1q2w3e4r5t6y!}"
