using RabbitMQ.Client;
using System.Text;

//Bağlantı oluşturma
var factory = new ConnectionFactory { HostName = "localhost" };

//bağlantıyı aktifleştirme ve kanal açma
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//Queue oluşturma
channel.QueueDeclare(queue: "example-queue",exclusive:false);

//Queue'ya mesaj gönderme
//Rabbitmq kuyruğa atacağı mesajları byte türünden kabul eder. mesajları byte'a çevirmemiz gerekir.

//byte[] message = Encoding.UTF8.GetBytes("Merhaba");
//channel.BasicPublish(exchange:"" , routingKey: "example-queue",body:message);

for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

}
Console.Read();