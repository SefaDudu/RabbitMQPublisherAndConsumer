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
	
	
