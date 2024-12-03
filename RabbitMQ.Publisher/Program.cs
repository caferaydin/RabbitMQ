using RabbitMQ.Client;
using System.Text;

internal class Program
{
    private static async Task Main(string[] args)
    {

        Console.WriteLine("Publisher Start");


        /*  1 - Bağlantı Oluşturma
         *  2 - Bağlantıyı Aktifleştirme ve Kanal Açma
         *  3 - Queue Oluşturma 
         *  4 - Queue' ya Mesaj Gönderme
         * */

        // 1- Bağlantı Oluşturma
        ConnectionFactory factory = new();
        factory.Uri = new("amqps://minnrvyd:vIFS9xieZvLunRb0u3buiIdIz1N8lON3@shark.rmq.cloudamqp.com/minnrvyd");


        // 2.1 Bağlantıyı Aktifleştirme
        using IConnection connection = await factory.CreateConnectionAsync();

        // 2.2 Kanal açma 
        using IChannel channel = await connection.CreateChannelAsync();

        // 3- Queue Oluşturma 
        await channel.QueueDeclareAsync(queue: "example-queue", exclusive: false);

        /* Exclusive
         * exclusive parametressi bu kuyruğun özel olup olmadığını belirtir. yani birden fazla bağlantı ile bu kuyrukta işlem yapıp yapamıyacağımızı belirleyeceğimiz bir özelliktir.
         * Default true olarak geliyor.
         * True olursa bu bağlantının dışındaki bağlantı bu kuyruğu kullanamayacaktır.
         */

        // 4- Queue' ya Mesaj Gönderme
        /* RabbitMQ Q' ya atacağı mesajları byte türünden kabul etmektedir.
         * Haliyle Mesajları bizim byte'a dönüştürmemiz gerekmektedir.
         */
        // Asıl 
        //byte[] message = Encoding.UTF8.GetBytes("Merhaba");
        //await channel.BasicPublishAsync(exchange: "", routingKey: "example-queue", body: message);

        // exchande belirmiyorsan default olarak Direct exchange olarak gidecektir.


        //Test Amaçlı 
        for (int i = 0; i < 100; i++)
        {
            await Task.Delay(200);
            byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i );
            await channel.BasicPublishAsync(exchange: "", routingKey: "example-queue", body: message);
        }


        Console.WriteLine("Publisher End");

        Console.ReadKey();


    }
}