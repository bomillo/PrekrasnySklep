#nullable disable

namespace PrekrasnyDomainLayer.Models;

public sealed class BasketItem
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

    public int Quantity { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    internal BasketItem() { }

    internal OrderItem ConvertToOrderItem(Order order)
    {
        return new OrderItem()
        {
            Order = order,
            OrderId = order.Id,
            Product = Product,
            ProuctId = ProductId,
            Quantity = Quantity,
        };
    }
}
