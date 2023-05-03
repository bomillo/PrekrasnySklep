#nullable disable

using PrekrasnyDomainLayer.Models.Enums;

namespace PrekrasnyDomainLayer.Models;

public sealed class User
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string HashedPassword { get; set; }

    public UserRole UserRole { get; set; }
    public UserTheme Theme { get; set; } = UserTheme.Dark;
    public bool OnInitFullScreen { get; set; } = false;
    public List<BasketItem> ItemsInBasket { get; set; }

    internal User() { }
}
