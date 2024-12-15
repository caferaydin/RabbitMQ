using MassTransit;
using MasstransitQ.Shared.Models.Notification.Commands;
using MasstransitQ.Shared.Models.Payment.Events;

namespace MasstransitQ.Notification.Consumers
{
    public class OrderPaymentReceivedEventConsumers : IConsumer<OrderPaymentReceivedEvent>
    {
        private readonly ILogger<OrderPaymentReceivedEventConsumers> _logger;
        private readonly IBus _bus;

        public OrderPaymentReceivedEventConsumers(ILogger<OrderPaymentReceivedEventConsumers> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<OrderPaymentReceivedEvent> context)
        {
            // Business rules
            

            var orderId = context.Message.OrderId;
            var amount = context.Message.Amount;
            var customerId = context.Message.CustomerId;
            var phoneNumber = "+90 542";
            var emailAddress = "zng.caferaydin@gmail.com";

            var content = $"Payment received for this order, OrderId: {orderId}, Payment Amount: {amount}";

            await _bus.Publish<SendSmsCommand>(new SendSmsCommand(phoneNumber, content));
            await _bus.Publish<SendEmailCommand>(new SendEmailCommand( new List<string>()
            {
                emailAddress
            },"Payment received", content));

            _logger.LogInformation("Order payment received, OrderId: {orderId}, CustomerId: {customerId}", orderId, customerId);

           
        }
    }
}
