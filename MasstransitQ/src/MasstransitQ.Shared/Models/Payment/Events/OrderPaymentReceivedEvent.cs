using MassTransit;

namespace MasstransitQ.Shared.Models.Payment.Events
{
    [EntityName("payment.orderpaymentreceivedevent")]
    public record OrderPaymentReceivedEvent(
     Guid CustomerId,
     double Amount,
     string CardNumber,
     Guid OrderId,
     Guid PaymentNumberGuid)
    {
        public Guid CustomerId { get; private set; } = CustomerId;
        public double Amount { get; private set; } = Amount;
        public string CardNumber { get; private set; } = CardNumber;
        public Guid OrderId { get; private set; } = OrderId;
        public Guid PaymentNumberGuid { get; private set; } = PaymentNumberGuid;
    }
}
