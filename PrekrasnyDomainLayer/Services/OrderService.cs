using PrekrasnyDomainLayer.Context;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Models.Enums;
using PrekrasnyDomainLayer.State;

namespace PrekrasnyDomainLayer.Services;

public sealed class OrderService
{
    private readonly PrekrasnyContext context;
    private readonly BasketService basketService;
    private readonly ProductService productService;

    public OrderService() : this(AppState.SharedContext) { }

    public OrderService(PrekrasnyContext context)
    {
        this.context = context;
        basketService = new BasketService(context);
        productService = new ProductService(context);
    }

    public bool PlaceOrder(string recepient = "")
    {
        if (AppState.CurrentUser is null)
        {
            throw new NullReferenceException(nameof(AppState.CurrentUser));
        }
        var basket = basketService.GetCurrentBasket();

        if (!productService.VerifyBasket(basket)) {
            return false;
        }


        var order = new Order() { Status = OrderStatus.Ordered, UserId = AppState.CurrentUser.Id, Recepient= recepient };

        context.Orders.Add(order);
        context.SaveChanges();

        foreach (var item in basket)
        {
            context.OrderItems.Add(item.ConvertToOrderItem(order));
            productService.AdjustStock(item.Product, -1 * item.Quantity);
        }
        basketService.ClearBasket();
        context.SaveChanges();
        return true;
    }

    public bool ProgressOrderStatus(Order order)
    {
        if (order.Status == OrderStatus.Delivered)
        {
            return false;
        }

        order.Status++;

        context.Update(order);
        context.SaveChanges();

        return true;
    }
}
