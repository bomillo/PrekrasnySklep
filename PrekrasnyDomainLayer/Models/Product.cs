#nullable disable

namespace PrekrasnyDomainLayer.Models;

public sealed class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string ImagePath { get; set; }

    public double Price { get; set; }

    public int Stock { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }

    public Product() { }
}
