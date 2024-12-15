using MassTransit;
using MasstransitQ.Order.API.Models;
using MasstransitQ.Shared.Models.Payment.Command;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasstransitQ.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IBus bus, ILogger<OrderController> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        [HttpPost]
        public async Task<AcceptedResult> CreateOrder([FromBody] CreateOrder order)
        {
            var orderId = Guid.NewGuid();
            var customerId = order.customerId;
            var amount = order.products.Sum(x => x.Quantity * x.Price);

            await _bus.Publish<CreditCardPaymentCommand>(new CreditCardPaymentCommand(customerId, amount, orderId));

            _logger.LogInformation("Order accepted {orderId}", orderId);
            return Accepted();
        }
    }
}
