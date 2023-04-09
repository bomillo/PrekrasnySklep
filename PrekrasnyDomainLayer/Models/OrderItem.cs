#nullable disable


namespace PrekrasnyDomainLayer.Models;

public sealed class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public Order Order { get; set; }

    public int ProuctId { get; set; }

    public Product Product { get; set; }

    public int Quantity { get; set; }

    internal OrderItem() { }
}
