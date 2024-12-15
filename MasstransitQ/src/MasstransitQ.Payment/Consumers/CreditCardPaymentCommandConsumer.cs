using MassTransit;
using MasstransitQ.Shared.Models.Payment.Command;
using MasstransitQ.Shared.Models.Payment.Events;

namespace MasstransitQ.Payment.Consumers
{
    public class CreditCardPaymentCommandConsumer : IConsumer<CreditCardPaymentCommand>
    {
        private readonly ILogger<CreditCardPaymentCommandConsumer> _logger;
        private readonly IBus _bus;

        public CreditCardPaymentCommandConsumer(ILogger<CreditCardPaymentCommandConsumer> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }
        public async Task Consume(ConsumeContext<CreditCardPaymentCommand> context)
        {
            var customerId = context.Message.CustomerId;
            var orderId = context.Message.OrderId;
            var amount = context.Message.Amount;

            var cardNumber = "**** 6767";
            var paymentId = Guid.NewGuid();

            // push Event 
            await _bus.Publish<OrderPaymentReceivedEvent>(new OrderPaymentReceivedEvent(customerId, amount, cardNumber, orderId, paymentId));

            _logger.LogInformation("Payment received, OrderId : {orderId}, PaymentId: {paymentId}", orderId, paymentId);
            
        }
    }
}
