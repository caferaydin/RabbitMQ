using MassTransit;

namespace MasstransitQ.Shared.Extensions
{
    public static class MassTransitExtensions
    {
        public static void RegisterQueue<T>(this IRabbitMqBusFactoryConfigurator config, IBusRegistrationContext context, string queueNameBase, Type eventClassType, int? concurrency = null) where T : class, IConsumer
        {
            config.ReceiveEndpoint($"{queueNameBase}.{eventClassType.Name.ToLower()}", endpoint =>
            {
                endpoint.ConcurrentMessageLimit = concurrency ?? endpoint.ConcurrentMessageLimit;

                endpoint.UseMessageRetry(conf =>
                    conf.Incremental(
                        retryLimit: 2,
                        initialInterval: TimeSpan.FromSeconds(10),
                        intervalIncrement: TimeSpan.FromSeconds(5)));

                endpoint.ConfigureConsumer<T>(context);
                endpoint.SetQuorumQueue();
            });
        }
    }
}
