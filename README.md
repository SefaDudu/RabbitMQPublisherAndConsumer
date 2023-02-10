# Rabbitmq 
# -MessageQueue Nedir?
	-yazılım sistemlerinde iletişim için kullanılan bir yapıdır.
	-birbirinden bağımsız sistemler arasında veri alışveri yapmak için kullanılır.(dil,platform)
	-gönderilen messajları kuyrukta saklar ve sonradan bu mesajların işlenmesini sağlar.
	-kuyruğa mesaj gönderene Producer yada publisher, kuyruktaki mesajları işleyen Consumer olarak adlandırılır.
	-publisher web uygulaması consumer farklı bir şey olabilir. İki yazılım birbirinden bağımsızdır.
	-Message ? iki sistem arasında iletişim için kullanılan veri birimidir. Yani Producerin consumer tarafından işlemesini istediği
	verinin kendisidir.
	-Örneğin : bir e-ticaret sisteminden örnek verirsek eğer siparişe dait mesaj olarak : sipariş numarası , müşteri bilgileri veya
	ödeme bilgileri örnek verilebilir.
	-Message queue yapısı mimarinin asenkron olarak çalışmasını sağlar.
	-bir işlemi beklemek yerine ona bir istek gönderip gerekli consumerin işinin yapılmasını bekleriz.
	-Consumer mesajları işler ya da bir başka deyişle tüketir.
	-Ayrıca, Message queue içerisindeki mesajların consumer tarafından sırasıyla işlendiğine dikkat etmemiz gerekmektedir. FIFO 
	mantığıyla çalışır:
	FIFO: sıraya ilk giren ilk çıkar
	AMAÇ:bazı senaryolarda birbirinden farklı sistemler arasında
	işlevsel açıdan senkron çalışma uygun olmayabilir.
		-bir e-ticarette ödeme neticesinde fatura oluşturmak 
		için ilgili servisin senktron bir şekilde beklemek ve 
		bunu son kullanıcıya yansıtmak mantıklı değildir.
		-bu tarz senaryolarda sistemler arasında senkrondan ziyade
		asenkron bir davranış sergilenmesi.
		-ödeme neticesinden kullanıcıya sipariş başraıyla gerçekletiğine
		dair sonuç döndürken bir yandanda message queueya fatura mesajları
		gönderilmeli
		-ödeme yaparken onay kodu beklemek senkron.
		-fautra yollamak mail yollamak bunlar asenkron olarak gerçekleşmeli
		-bu mesaj fatura servisinden ilk fırsatta alınmalı ve üretilmeli.
		-bu sayede işlemler arasında bir bekleme süresi olmayacaktır.
		
# -Senkron ve Asenkron İletişim Modelleri.
    -Senkron
	      Sistemler birbirleriyle haberleşirken sonuç bekliyorlar ise bu senkron bir iletişimdir.
	 -Asenkron
	       bir işlem sonucu hemen beklenmiyorsa bu işlem asenkrondur(mail atma)
	       mail göndermek,fatura oluşturmak,stok güncellemek.
	
	
	
# -MessageBroker Nedir ? 
  	Queue yapısını kullanan teknolojilerin genel adıdır.
   	İçerisinde message queu yapısını barındırıan publsiher ve consumer arasında iletişim kuran yapıların tümü
   	-bir broker içerisinde birden fazla queue bulunur.
   	-Teknolojiler.
   		-Rabbitmq
   		-kafka
   		-activemq
   		-Redis
   		-IronMQ
 
 
# RabbitMQ nedir?
  	-Open source olan bir message queue sistemidir.
  	-earland diliyle gerçekleştirilmiştir.
  	-cloudda hizmeti mevcuttur.
  	-farklı işletim sistemleri tarafından desteklenir.
  
# RabbitMQ yu neden kullanmalıyız ? 
	  -Yazılım uygulamarında ölçeklendirilebilir bir ortam için.(örn: Fatura oluşturma sürecini beklememek )
	  -Kullanıcılarından gelen isteklere anlık olmayan yada uzun süre bekletilen işlemlerde kullanıcı oyalamamak için.
	  -kullanıcı gereksiz bir response time süresine maruz kalmaması için
	  -bu durumları asenkron olarak kontrol etmek için rabbitmq kullanılmalıdır.
	  -response time uzun sürebilecek operasyonları farklı bir uygulamda işlem gösterir.
	  -bu mekanizma işlemi kuyruğa yollar ve ölçeklendirir.
	  örn: bir word to pdf uygulamasını senkron olarak çalıştırmak mantıklı değildir.
	  olurabilitesi olan işlemi kuyruğa alıp kullanıcıya bu durumu beklemeye maruz bırakmamak gerekir.


# RabbitMQ İşleyişi nasıldır ? 
  	-publisher,exchange,queue,consumer
  	-publisher mesaju queue ya atar bu queu asenkron bir şekilde consumer tarafından işlenir.
  	-bir message broker olduğu için publisher ve consumer barınıdrı.
  	-yapısal olarak exchange ve queu enstrumanları üzerindne işlevsellik gösterir.
  	-publisher mesajı publish ettikten sonra ilgili mesajı Exchange karşılayacaktır.
  	Exhange ise belirtilen route ile mesaj ilgili kuyruğa yönlendirilecektir.
  	Mejasın hangi queueya gideceği Exchange içindeki route ile belirtilir.
  	-publisher ile consumer hangi platformda hangi dil ile geliştirildiğinin bir önemi yoktur. bu mimari bütünsel olarak yazılım dilinden bağımsızdır.
  	-rabbit mq AMQP protokolü ile çalışmakta.
  	-rabbitmq mesajların güvenli iletilmesi konusundada büyük bir destek sağlamaktadır.

# Kurulum-Docker
	  docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management
	  	http://localhost:15672/
	  	username:guest
	  	pass 	:guest
	  diğer alternatif :CloudAMQP 
	  	https://www.cloudamqp.com/



# EXCHANGE VE BINDING KAVRAMLARI
	  Exchange : rabbit mq sürecinde kullanılan bir noktayı belirtir.
	  	-publisher tarafından gönderilen mesajların nasıl yönetileceğini ve hangi routlara yönlendireceği konusunda kontrol sağlayan yapıdır.
	  	(birden fazla kuyruk olabilir. hangisine mesaj göndereceğimizi buradan belirleriz.)
	  	-Route ise mesajların exchange üzerinden kuyruklara nasıl göndereceğimizi sağlayan mekanizma
	  	-publisher consumer arasındalki süreçte routing key değeri kullanılır.
	  	-burada exchange bilgileri tutulur
	  	-route ise genel olarak mesajların yolunu ifade eder.
	  	-bir exchange birdfen fazla kuyrukla eşleşebilir.
	  	-yapılacak çalışmanın maliyetine göre 4 çeşit exchange mevcuttur.
	  BINDING : exchange ile queue arasındaki ilişkiye binding denir.
	  	-exchange birden fazla queueya bind olabiliyorsa hangi kuyruğa göndereceğini nasıl anlıyor. (exchange türüne göre değişir.)
# GELİŞMİŞ KUYRUK MİMARİSİ
 -Rabbitmq teknolojisinin ana fikri yoğun kaynak gerektiren işleri hemen yapmaya koyularak tamamlanmasını
 beklemeksizin bu işleri ölçeklendirilebilir bir vaziyette daha sonra yapılacak şekilde planlamaktır.
 -Bu planlama gerçekleştirilirken kuyruklar kullanılmakta  ve mesajlar kuyruklara atılarak tüketiciler
 tarafından bu mesajlar elde edilerek asenkron bir şekilde işlenmesi sağlanmaktadır.
 -Tüm bu süreçte kuyrukların bakımı , mesajların kalıcılığı vs konfigüre edilmesi gerekmekte.
 Ayrıca birden fazla tüketicinin söz konusu olduğu durumlarda nasıl bir davranış olacağı önem arz etmektedir.
 Gelişmiş kuyruk mimarisi tam olarak bu noktada önem arz etmektedir. kuyrukların ve mesajların kalıcılığı , 
 mesajların birden fazla tüketiciye karşı dağıtım stratejisi yahut tüketici tarafından işlenmiş bir mesajın
 kuyruktan silinebilmesi için onay/bildiri sistemi vs. tüm bu detaylar bu başlık altında işlenmektedir.
 # ROUND-ROBIN Dispatching
		-Rabibtmq default olarak tüm consumerlara sırasıyla mesaj gönderir.
		![image](https://user-images.githubusercontent.com/77778888/218112098-d0a59e34-231d-4be7-ae3a-d1b1c7b8ed92.png)

		
  # Message Acknowledgement
		-Mesaj Onaylama
		-rabbit mq tüketiciye gönderdiği herhangi bir mesajı ister başarılı olsun ister başarısız olsun kuyruktan silinmesi üzerine işaretler.
		-örneğin bir sipariş oluşturulurken hata olmasına rağmen silinmesi durumunda bir sıkıntı doğabilir. buda olası bir veri kaybına neden olur. bu durumu 		      engellemeye yarar.
		-tüketicilerin kuyruktan aldıkları mesajları işlemesi süresinde herhangi bir kesinti yahut problem tam olarak işlenemeyeceği için esasında görev      		       tamamlanmamış olacaktır.
		-bu tarz durumlara istinaden mesaj başarıyla işlendiyse eğer kuyruktan silinmesi için tüketiciden RABBİTMQ'nun uyarılması gerekmektedir.
	         Message Acknowledgement Problemleri
	 	-Bir mesaj işlenmeden consumer problem yaşarsa bu mesajın sağlıklı bir şekilde işlenebilmesi için başka bir consumer tarafından tüketilebilir 			 olmalıdır.
		-Aksi taktirde mesaj kuyruktan consumer tarafından alındığı an silinirse bu durumda veri kaybı ihtimali söz konusu olacaktır. İşte bu tarz durumlar 		     için message Acknowledgement özelliği şarttır diyebiliriz.
		-eğer ki bu özelliği kullanıyorsanız kesinlikte mesaj işleme başarıyla sonlandığı taktirde RABBİTMQ'ya mesajın silinmesi için haber göndermeyi 	     		     unutmayın. Aksi taktirde mesaj tekrar yayınlanacak ve başka bir tüketici tarafından tekrar işlenecektir.
		-Tabi ayriyeten mesajlar onaylanarak silinmediği taktirde kuyrukta kalınmasına neden olacak ve bu durum kuyrukların büyümesine ve yavaşlamasıyla   		    sonuçlanıp performans düşüklüğüne neden olacaktır.
#ÖZET
		-bu özellik sayesinde bir mesajın kaybolmadığından emin olabiliriz.
		-tüketici açısından mesajın alındığını ve işlendiğini artık kuyruktan silinebilir olduğunu anlayarak süreç daha güvenli hale gelir.
		-her işleme göre geri dönüş tipi değişmektedir. 
		-rabbitmqya tüketiciden gelen onay süresi max 30 dakikadır bu süreç uzadığı taktirde işlem tekrar kuyruğa alınır. bu süreci arttırmak mümkündür.
 ![image](https://user-images.githubusercontent.com/77778888/218112155-47dfc2af-4a8f-4582-9c6e-43f01361a158.png)
<h3>autoAck:</h3>
Consume tarafında false verilerek Message Acknowledgement onaylanma süreci aktifleştirilmesi için gereklidir. Rabbitmq varsayılan işlemi bu şekilde değiştirilmiş olur.
<h3>BasicAck:</h3>
Consumer mesajı başarıyla işlediğine dair uyarıyı bu method ile gerçekleştirir.
<h3>Multiple parametresi :</h3> 
birden fazla mesaja dair onay bildirisi gönderir. Eğer true değeri verilirse deliverytag değerine sahip olan bu mesajla birlikte bundan önceki mesajlarında işlendiğini onaylar. Aksi taktirde false verilirse sadece bu mesaj için onay bildirisinde bulunacaktır.
<h3>BasicNac :</h3>
consumerda istemsiz durumların dışında kendi kontrollerimiz neticesinde mesajları işlememek isteyebilir veyahut ilgili mesjın işlnemsi olumsuz sonuçlanması durumunda kullanılır.
![image](https://user-images.githubusercontent.com/77778888/218112403-548146f2-cdce-49c0-9e34-35c8ae43f4f3.png)
<h3>BasicCancel:</h3>
tüm mesajların işlenmesini reddetme consumer tag ile birlikte
 
![image](https://user-images.githubusercontent.com/77778888/218112731-8b828d53-72ba-4840-a19d-dc70e3246bbc.png)

<h3>BasicReject : </h3>
tek bir mesajın işlenmesini reddetebiliyoruz.
 ![image](https://user-images.githubusercontent.com/77778888/218112781-52923f79-71b4-494d-9563-457a3202ed44.png)


<h3>Message Durability </h3>
	-rabbit mq sunucu bir sorunlar karşılaştığında ne olacağına dair işlemler.
	-bir kapanmada tüm kuyruklar ve mesajlar silinecektir.
	-böyle bir durumda mesajların kalıcı olabilmesi için ekstra bir çalışma yapılması gerekmektedir.
-bu çalışma kuyruk ve mesaj açısından kalıcı olarak işaretleme yapılması gerekmektedir.
Publisherda yapılması gereken işlemler: 
![image](https://user-images.githubusercontent.com/77778888/218112931-9361745e-4455-4cae-b67e-6108f16b569a.png)
![image](https://user-images.githubusercontent.com/77778888/218112946-0a6bac85-d180-43e3-ae16-ffb646d84d49.png)
![image](https://user-images.githubusercontent.com/77778888/218112955-e955e444-d09d-496c-85b6-a266fd3ec0d5.png)

 
 
Bu işlem 100% kalıcılık sağlamaz.
Bu işlem hem publisher hem de consumerda uygulanmalıdır.

<h3>Fair Dispatch:</h3>
Tüm consumerlara eşit şekilde mesaj iletilmesi için bir özelliktir. Bu performansı düzenli bir hale getirir. Hepsi eşit işleme yapacağı anlamına gelir.
Bunu mesaj işleme konfigürasyonujj ile yaparız.
BasicQos methodu ile mesajların işleme hızını ve teslimat sırasını belirleyebiliriz.
Böylece fair dispatch özelliği çalışır.
![image](https://user-images.githubusercontent.com/77778888/218112576-9817b1d6-457d-4f2f-8796-e26f7e0ff1a7.png)


