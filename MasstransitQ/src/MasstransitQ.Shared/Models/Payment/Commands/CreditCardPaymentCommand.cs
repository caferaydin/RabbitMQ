using MassTransit;

namespace MasstransitQ.Shared.Models.Payment.Command;

[EntityName("payment.creditcardpaymentcommand")]
public record CreditCardPaymentCommand
{
    public Guid CustomerId { get; private set; }
    public double Amount { get; private set; }
    public Guid OrderId { get; private set; }

    public CreditCardPaymentCommand(Guid customerId, double amount, Guid orderId)
    {
        CustomerId = customerId;
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Amount = amount;
        OrderId = orderId;
    }
}
