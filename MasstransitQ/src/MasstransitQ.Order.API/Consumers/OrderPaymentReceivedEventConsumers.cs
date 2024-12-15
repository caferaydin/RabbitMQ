using MassTransit;
using MasstransitQ.Shared.Models.Payment.Events;

namespace MasstransitQ.Order.API.Consumers
{
    public class OrderPaymentReceivedEventConsumers : IConsumer<OrderPaymentReceivedEvent>
    {
        private readonly ILogger<OrderPaymentReceivedEventConsumers> _logger;

        public OrderPaymentReceivedEventConsumers(ILogger<OrderPaymentReceivedEventConsumers> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderPaymentReceivedEvent> context)
        {
            // Business rules
            // Change order status
            // Update the stocks quantity

            var orderId = context.Message.OrderId;
            var customerId = context.Message.CustomerId;

            _logger.LogInformation("Order payment received, OrderId: {orderId}, CustomerId: {customerId}", orderId, customerId);

            return Task.CompletedTask;
        }
    }
}
