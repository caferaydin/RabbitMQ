using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Consumer Start");
        /* *     1 - Bağlantı Oluşturma
           *     2 - Bağlantıyı Aktifleştirme ve Kanal Açma
           *    3 - Queue Oluşturma 
           *    4 - Queue ' dan Mesaj Okuma
        */


        // 1- Bağlantı Oluşturma
        ConnectionFactory factory = new();
        factory.Uri = new("amqps://minnrvyd:vIFS9xieZvLunRb0u3buiIdIz1N8lON3@shark.rmq.cloudamqp.com/minnrvyd");

        // 2.1 Bağlantıyı Aktifleştirme
        using IConnection connection = await factory.CreateConnectionAsync();

        // 2.2 Kanal açma 
        using IChannel channel = await connection.CreateChannelAsync();

        // 3- Queue Oluşturma 
        await channel.QueueDeclareAsync(queue: "example-queue", exclusive: false);
        // Consumerda da kuyruk publisher'daki ile bire bir aynı yapılandırmada tanımlanmalıdır. 

        // 4- Q'dan Mesaj okuma 
        AsyncEventingBasicConsumer consumer = new(channel);
        //consumer.ReceivedAsync += Consumer_ReceivedAsync;
        var consumeTag = await channel.BasicConsumeAsync(queue: "example-queue", autoAck: false, consumer);
        // AutoAck parametresi : kuyruktan mesajı aldığımızda kuyruktan mesajın fiziksel olarak silinip silinmemesine göre davranış sağlayacak parametredir.


        // Message  Acknowledgement
        // Belirli Bir Q' ya 
        //await channel.BasicCancelAsync(consumeTag); 
        // BasicCancel ile gelen kuyruktaki mesajların işlenmesini reddetme


        // Message  Acknowledgement
        // Belirli bir Mesajın işlenmesinin reddi  için
        //await channel.BasicRejectAsync(deliveryTag: 3, requeue: true);
        // işlenmesini istemediğimiz durumlarda BasicReject metodunu kullanabiliriz.
        // requeue ->  True olursa tekrar kuyruğa eklenmesini sağlar. & False tekrar kuyruğa eklemez.


        consumer.ReceivedAsync += async (sender, e) =>
        {
            Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
            await channel.BasicAckAsync(deliveryTag: e.DeliveryTag, multiple: false);
            // Message Acknowledgement - Mesaj onayı için -> okunan mesajın Q dan silinmesi için multiple false olarak verilmeli
            // true olarak verilirse Q daki mesajların hepsini siler.
            // bu durum bildirilmezse Q da tekrar oluşacağı için performans kaybına yol açar.

            // Message  Acknowledgement
            //await channel.BasicNackAsync(deliveryTag: e.DeliveryTag, multiple: false, requeue: true);
            // requeue -> true  verirsek tekrar kuyruğa eklenecektir.

        };

        Console.WriteLine("Consumer End");


        Console.ReadKey();
    }

    private static Task Consumer_ReceivedAsync(object sender, BasicDeliverEventArgs @event)
    {
        // Kuyruğa gelen Messajın işlendiği yer

        // @event.Body  : Kuyruktaki mesajın verisini bütünsel olarak getirecektir.
        // @event.Body.Span ve ya @eventBody.ToArray() kuyruktaki mesajın byte verisini getirecektir.
        Console.WriteLine(Encoding.UTF8.GetString(@event.Body.Span));

        return Task.CompletedTask;
    }
}