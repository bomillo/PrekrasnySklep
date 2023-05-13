using Microsoft.EntityFrameworkCore;
using PrekrasnyDomainLayer.Context;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;

namespace PrekrasnyDomainLayer.Services;

public sealed class BasketService
{
    private readonly PrekrasnyContext context;

    public BasketService() : this(AppState.SharedContext) { }

    public BasketService(PrekrasnyContext context)
    {
        this.context = context;
    }

    public List<BasketItem> GetCurrentBasket()
    {
        if (AppState.CurrentUser is null)
        {
            throw new NullReferenceException(nameof(AppState.CurrentUser));
        }

        return context.BasketItems.Include(b => b.Product).Include(b => b.User).Where(b => b.UserId == AppState.CurrentUser.Id).ToList();
    }

    public void AddToBasket(Product product, int quantity = 1)
    {
        if (AppState.CurrentUser is null)
        {
            throw new NullReferenceException(nameof(AppState.CurrentUser));
        }

        var existingBasketItem = context.BasketItems.Include(b => b.Product).Include(b => b.User).FirstOrDefault(b => b.UserId == AppState.CurrentUser.Id && b.ProductId == product.Id);
        if (existingBasketItem is not null)
        {
            existingBasketItem.Quantity += quantity;
            context.Update(existingBasketItem);
        }
        else
        {
            var newBasketItem = new BasketItem()
            {
                Product = product,
                UserId = AppState.CurrentUser.Id,
                Quantity = quantity
            };

            context.BasketItems.Add(newBasketItem);
        }

        context.SaveChanges();
    }

    public void RemoveFromBasket(Product product, int quantity = 1)
    {
        if (AppState.CurrentUser is null)
        {
            throw new NullReferenceException(nameof(AppState.CurrentUser));
        }

        var existingBasketItem = context.BasketItems.Include(b => b.Product).Include(b => b.User).FirstOrDefault(b => b.UserId == AppState.CurrentUser.Id && b.ProductId == product.Id);
        if (existingBasketItem is not null)
        {
            existingBasketItem.Quantity -= quantity;
            if (existingBasketItem.Quantity > 0)
            {
                context.Update(existingBasketItem);
            }
            else
            {
                context.Remove(existingBasketItem);
            }
            context.SaveChanges();
        }
    }

    public void AdjustBasketQuantity(Product product, int quantity)
    {
        if(quantity < 0)
        {
            RemoveFromBasket(product, -1 * quantity);
        }
        else if(quantity > 0)
        {
            AddToBasket(product, quantity);
        }
    }

    public void ClearBasket()
    {
        if (AppState.CurrentUser is null)
        {
            throw new NullReferenceException(nameof(AppState.CurrentUser));
        }

        var toBeRemoved = context.BasketItems.Where(b => b.UserId == AppState.CurrentUser.Id);
        context.RemoveRange(toBeRemoved);
        context.SaveChanges();
    }
}
