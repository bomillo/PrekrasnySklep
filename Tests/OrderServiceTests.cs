using Microsoft.EntityFrameworkCore;
using PrekrasnyDomainLayer.Context;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Models.Enums;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using Tests.Util;
using Xunit;

namespace Tests;

public class OrderServiceTests: IDisposable
{
    private readonly PrekrasnyContext context;
    private readonly OrderService orderService;
    private readonly UserService userService;
    private readonly ProductService productService;
    private readonly BasketService basketService;
    private readonly InMemoryDbConecttion dbConnection = new();

    public OrderServiceTests()
    {
        this.context = new PrekrasnyContext(dbConnection.DbConnection);
        context.Database.EnsureCreated();

        AppState.SharedContext = context;
        AppState.CurrentUser = null;

        userService = new UserService(context);
        productService = new ProductService(context);
        basketService = new BasketService(context);
        orderService = new OrderService(context);

        AddTestCategory();
    }

    public void Dispose()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    private void AddTestCategory()
    {
        var category = new Category { Name = "TestCategory"};
        context.Categories.Add(category);
        context.SaveChanges();
    }

    private void AddTestOrder()
    {
        var testOrder = new Order
        {
            Status = OrderStatus.Ordered,
            UserId = 1
        };

        context.Orders.Add(testOrder);
        context.SaveChanges();
    }

    private void AddTestUser()
    {
        userService.RegisterNewUser("test", "test");
    }

    [Fact]
    public void PlaceOrder_Succeeds_WithValidBasket()
    {
        userService.RegisterNewUserAndLogin("testUser", "testPassword");
        var testProduct = AddTestProduct();

        basketService.AddToBasket(testProduct, 2);

        bool result = orderService.PlaceOrder();

        Assert.True(result);
        var orders = context.Orders.Include(o => o.Items).ToList();
        Assert.Single(orders);
        Assert.Equal(2, orders[0].Items[0].Quantity);
    }

    [Fact]
    public void PlaceOrder_Fails_WithInsufficientStock()
    {
        userService.RegisterNewUserAndLogin("testUser", "testPassword");
        var testProduct = AddTestProduct(stock: 1);

        basketService.AddToBasket(testProduct, 2);

        bool result = orderService.PlaceOrder();

        Assert.False(result);
        Assert.Empty(context.Orders);
    }

    [Fact]
    public void ProgressOrderStatus_ProgressesOrderStatus()
    {
        AddTestUser();
        AddTestOrder();

        var testOrder = context.Orders.First();
        var orderService = new OrderService(context);

        var progressed = orderService.ProgressOrderStatus(testOrder);
        var updatedOrder = context.Orders.Find(testOrder.Id);

        Assert.True(progressed);
        Assert.Equal(OrderStatus.Packing, updatedOrder.Status);

        progressed = orderService.ProgressOrderStatus(updatedOrder);
        updatedOrder = context.Orders.Find(testOrder.Id);

        Assert.True(progressed);
        Assert.Equal(OrderStatus.Sent, updatedOrder.Status);

        progressed = orderService.ProgressOrderStatus(updatedOrder);
        updatedOrder = context.Orders.Find(testOrder.Id);

        Assert.True(progressed);
        Assert.Equal(OrderStatus.Received, updatedOrder.Status);

        progressed = orderService.ProgressOrderStatus(updatedOrder);
        updatedOrder = context.Orders.Find(testOrder.Id);

        Assert.False(progressed);
        Assert.Equal(OrderStatus.Received, updatedOrder.Status);
    }

    [Fact]
    public void ProgressOrderStatus_Fails_WhenOrderIsReceived()
    {
        userService.RegisterNewUserAndLogin("testUser", "testPassword");
        var testProduct = AddTestProduct();

        basketService.AddToBasket(testProduct, 2);
        orderService.PlaceOrder();

        var order = context.Orders.First();
        order.Status = OrderStatus.Received;
        context.Update(order);
        context.SaveChanges();

        bool result = orderService.ProgressOrderStatus(order);

        Assert.False(result);
        Assert.Equal(OrderStatus.Received, order.Status);
    }

    private Product AddTestProduct(string name = "TestProduct", double price = 10.0, int stock = 10)
    {
        var product = new Product
        {
            Name = name,
            Price = price,
            Stock = stock,
            CategoryId = 1
        };

        context.Products.Add(product);
        context.SaveChanges();
        return product;
    }
}