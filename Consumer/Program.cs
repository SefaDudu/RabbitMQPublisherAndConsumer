//Bağlantı oluşturma
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };

//bağlantıyı aktifleştirme ve kanal açma
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//Queue oluşturma --  yapılandırma publisher ile aynı olmalı
channel.QueueDeclare(queue: "example-queue", exclusive: false);


//Queue mesaj okuma

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue",autoAck:false,consumer);
consumer.Received += (sender, e) =>
{
    //e.Body : mesajın verisini getirir bütünsel olarak.
    //e.Body.Span veya toArray mesajın byte verisini getirir.

    //kuyruğa gelen mesajın işlendiği yer burasıdır.
    
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

};
Console.Read();