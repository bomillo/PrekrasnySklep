#nullable disable

using PrekrasnyDomainLayer.Models.Enums;

namespace PrekrasnyDomainLayer.Models;

public sealed class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public List<OrderItem> Items { get; set; }

    public OrderStatus Status { get; set; }

    internal Order() { }
}
