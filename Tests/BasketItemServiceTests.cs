using Microsoft.Data.Sqlite;
using PrekrasnyDomainLayer.Context;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using System;
using System.Linq;
using Tests.Util;
using Xunit;

namespace Tests;

public class BasketServiceTests : IDisposable
{
    private readonly PrekrasnyContext context;
    private readonly BasketService basketService;
    private readonly UserService userService;

    private readonly InMemoryDbConecttion dbConnection = new();

    public BasketServiceTests()
    {
        this.context = new PrekrasnyContext(dbConnection.DbConnection);
        context.Database.EnsureCreated();

        AppState.SharedContext = context;
        AppState.CurrentUser = null;

        userService = new UserService(context);
        basketService = new BasketService(context);
    }

    public void Dispose()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    private void AddTestCategory()
    {
        context.Categories.Add(new Category { Name = "TestCategory" });
        context.SaveChanges();
    }

    private Product AddTestProduct()
    {
        AddTestCategory();
        var testProduct = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            ImagePath = "test.jpg",
            Price = 10.0,
            Stock = 10,
            CategoryId = 1
        };
        context.Products.Add(testProduct);
        context.SaveChanges();

        return testProduct;
    }

    [Fact]
    public void GetCurrentBasket_ThrowsException_WhenUserNotLoggedIn()
    {
        Assert.Throws<NullReferenceException>(() => basketService.GetCurrentBasket());
    }

    [Fact]
    public void AddToBasket_ThrowsException_WhenUserNotLoggedIn()
    {
        var testProduct = AddTestProduct();
        Assert.Throws<NullReferenceException>(() => basketService.AddToBasket(testProduct));
    }

    [Fact]
    public void RemoveFromBasket_ThrowsException_WhenUserNotLoggedIn()
    {
        var testProduct = AddTestProduct();
        Assert.Throws<NullReferenceException>(() => basketService.RemoveFromBasket(testProduct));
    }

    [Fact]
    public void ClearBasket_ThrowsException_WhenUserNotLoggedIn()
    {
        Assert.Throws<NullReferenceException>(() => basketService.ClearBasket());
    }
}