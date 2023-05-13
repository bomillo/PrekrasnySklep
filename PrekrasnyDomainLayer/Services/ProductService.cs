using PrekrasnyDomainLayer.Context;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;

namespace PrekrasnyDomainLayer.Services;

public sealed class ProductService
{
    private readonly PrekrasnyContext context;

    public ProductService() : this(AppState.SharedContext) { }

    public ProductService(PrekrasnyContext context)
    {
        this.context = context;
    }

    public bool AdjustStock(Product product, int change)
    {
        var freshProduct = context.Products.First(p => p.Id == product.Id);
        
        if (freshProduct.Stock + change < 0)
        {
            return false;
        }

        freshProduct.Stock += change;
        context.Update(freshProduct);
        context.SaveChanges();
        return true;
    }

    public bool VerifyBasket(List<BasketItem> basketItems)
    {
        bool valid = true;
        foreach (var item in basketItems)
        {
            // nie ma &&= :(
            valid = valid && context.Products.Any(p => p.Id == item.ProductId && p.Stock >= item.Quantity);
        }
        return valid;
    }

    public List<Product> GetAvailableProducts()
    {
        return context.Products.Where(x => x.Stock > 0).ToList();
    }

}
