Unity Version -> 2022.03.22f
Testler Unity Profiler kullanılarak editör üzerinde yapılmıştır

Bu repository 2025 UDO yaz staj programı case 1 görevleri için hazırlanmıştır  

CASE-1

Proje Amacı;
oyunlarda sıkça kullanılan bazı kurgusal mekaniklerin alternatif çözümleri ile optimizasyona etkisi analiz edilip sonuçlar değerlendirilmiştir.

Proje İçeriği
2 adet peroje içeriği bulunmaktadır. Projelerin içinde kendine has farklı optimizasyon çözümleri içerir.

Proje-1 --> "Security Camera" 
-Hazır modeller ile güvenlik kamera sistemi kurgulanmış ve bu kameralar ile görüntü ortak bir ekranda görüntülenmek istenmiştir. (3 adet kamera ve 1 adet ekran)
-Projenin Samsung A23 gibi örnek bir cihazda çalışması için gereken optimizasyonlar incelenmiştir.

Proje-2 --> "Destructible Objects"
-Unity harici bir yazılım kullanmadan 3 boyutlu nesneleri çalışma zamanında yada editor üzerinde nasıl parçalarız, ne gibi yöntemlerle mümkün gibi sorular yanıtlanmıştır.
-Parçalanan bu nesnelere fizik motoru kullanarak gerçekçi kuvvet fonksiyonları nasıl ekleyebiliriz.



Proje-1 
Mekanik -> Güvenlik sistemini aktif veya pasif hale getiren (Kameranın görüntüsünü ekranda görmek için açtığımız yada kapattığımız bir mekanik) Açma-Kapama butonu ile etkileşim nasıl sağlanır. Bu etkileşimi sağlama yöntemleri nelerdir ne gibi avantajları bulunur.
Sahne -> "Security Cameras"

Sahnede konuyla ilgili update fonksiyonu içerisinde "Physics" sınıfının "Raycast" fonksiyonu kullanılarak çarpılan obje kontrol edilmiş eğer açma kapama tuşumuza çarpıyorsa ve sol tıklanırsa, kamera aktif veya pasif hale getirilmiştir. 

Analiz, 
Hazırlanan scriptin update fonksiyonu inceledindiğinde atılan ışın hesaplamaya değer bir süre almamaktadır (0.00). Ancak yinede bazı dokunuşlarla daha optimize hale getirilebilir.
1. Atılan ray(ışın) layer mask(Katman filtreleme) yapılarak yanlızca ilgili obje ile çarpışması kontrol edilebilir. Her frame ışın atmanın önüne geçmesede her çarpıştığı nesneyi kontrol etmesi gerekmez.
2. Harita statik (haritadaki nesneler sabit) ve tuşun konumu belli olduğundan, tuşa basılabilir konuma bir collider eklenerek trigger check yapılabilir, bu sayede oyuncu oraya yakın değilse ray atılmaz ve collider ile trigger olduğunda ray atmaya başlar.
Benzer şekilde collider kullanmak istemezsek mevcut konumu sürekli olarak kontrol edip mesafemizi hesaplayabiliriz ve tuşa yakınsak ray atabiliriz fakat OnTriggerEnter ve OnTriggerExit fonksiyonları birer event ile tetiklendiğinden,
fonksiyon tek frame de çalışarak sürekli mesafe hesaplamadan kaçınmamızı sağlar.
3. Etkileşime geçmek için IPointer Interface kullanılabilir, bu yöntemde temelinde ray kullanan bir yapı olduğundan arayüz anlamında bize kolaylık sağlasada (manual olarak ray yazmamız gerekmez sadece fonksiyonun görevini tanımlamamız yeterli.) performans anlamında 
gözle görülür bir fark yaratmaz.

Bu projeyi Samsung A23 gibi bir cihazda çalıştırmak istesek ne gibi optimizasyonlar gerekir?,

1.Hedeflenen proje bir mobil oyun ise ray atılan noktayı mouse ile referans almak yerine (Camera.main.ScreenPointToRay(Input.mousePosition))
benzer şekilde telefon ekranının dokunulduğu noktayı referans alması doğru olacaktır (Camera.main.ScreenPointToRay(touch.position))
Bunun yanında farklı fonksiyonlar kullanarak çoklu tıklama ve tıklamaları takip etme gibi işlevler eklenebilir.(Telefon multitouch özelliği içeriyor.)
2.Telefon teknik özellikleri incelendiğinde, 1080x2408 ve 90 Hz yenileme hızına sahip olduğunu gördüm. Oyun Editör Profiler penceresinde incelediğinde değerler telefon ile aynı olamayacağından projenin varsa uygun bir simulator veya telefon ile test edilmesi ve optimizasyon
ayarlarının değiştirilmesi gerekir. (Proje grafik seviyesi yüksek kabul edilebilir seviyede ve görsel düzenlemeler yapılabilir.)
2. UI elementleri ekran boyutuna uygun hale getirilmeli.
3. Harita statik olduğundan gölge ve ışıklar kesinlikle bake edilmeli ve runtime da hesaplanmamalı, bu büyük bir maliyet oluşturur.
4. Mobil oyunların geneli 60 fps tasarlanır bu yüzden 90 hz yenileme mümkün olsada bunu 60 Hz ile kısıtlandırmak (veya oyuncu için ayarlanabilir kılmak) performansı olumlu etkiler.
5. Harita boyutuna ve oyun mekaniğine göre oldukça değişiklik göstersede, aynı anda birçok obje bulundurmak yerine ihtiyaç duydukça nesnelerin oluşturulması (Object Pooling gibi tekniklerle) veya farklı sahneler arası yükleme ekranı ile geçiş sağlanması oyunun performansını olumlu etkiler.


 





