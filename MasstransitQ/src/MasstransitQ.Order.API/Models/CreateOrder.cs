using MasstransitQ.Shared.Models.Product.Models;

namespace MasstransitQ.Order.API.Models
{
    public record CreateOrder(Guid customerId, List<Product> products);
}
