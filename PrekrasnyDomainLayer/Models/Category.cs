#nullable disable

namespace PrekrasnyDomainLayer.Models;

public sealed class Category
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Product> Products { get; set; }

    public Category() { }
}
