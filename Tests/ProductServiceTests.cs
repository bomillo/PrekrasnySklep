using Microsoft.Data.Sqlite;
using PrekrasnyDomainLayer.Context;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using System.Collections.Generic;
using System.Linq;
using Tests.Util;
using Xunit;

namespace Tests;

public class ProductServiceTests : IDisposable
{
    private readonly PrekrasnyContext context;
    private readonly ProductService productService;

    private readonly InMemoryDbConecttion dbConnection = new();

    public ProductServiceTests()
    {
        this.context = new PrekrasnyContext(dbConnection.DbConnection);
        context.Database.EnsureCreated();

        AppState.SharedContext = context;

        productService = new ProductService(context);
    }

    public void Dispose()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    private void AddTestCategory()
    {
        var category = new Category
        {
            Id = 1,
            Name = "TestCategory",
        };
        context.Categories.Add(category);
        context.SaveChanges();
    }

    [Fact]
    public void AdjustStock_WhenAdjustingStockSuccessfully()
    {
        AddTestCategory();

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            ImagePath = "test.jpg",
            Price = 10.0,
            Stock = 10,
            CategoryId = 1
        };
        context.Products.Add(product);
        context.SaveChanges();

        var result = productService.AdjustStock(product, 5);

        Assert.True(result);
        Assert.Equal(15, context.Products.First(p => p.Id == product.Id).Stock);
    }

    [Fact]
    public void AdjustStock_WhenAdjustingStockFails()
    {
        AddTestCategory();

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            ImagePath = "test.jpg",
            Price = 10.0,
            Stock = 10,
            CategoryId = 1
        };
        context.Products.Add(product);
        context.SaveChanges();

        var result = productService.AdjustStock(product, -15);

        Assert.False(result);
        Assert.Equal(10, context.Products.First(p => p.Id == product.Id).Stock);
    }

    [Fact]
    public void VerifyBasket_WhenBasketIsValid()
    {
        AddTestCategory();

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            ImagePath = "test.jpg",
            Price = 10.0,
            Stock = 10,
            CategoryId = 1
        };
        context.Products.Add(product);
        context.SaveChanges();

        var basketItems = new List<BasketItem>
        {
            new BasketItem
            {
                ProductId = product.Id,
                Product = product,
                Quantity = 5
            }
        };

        var result = productService.VerifyBasket(basketItems);

        Assert.True(result);
    }

    [Fact]
    public void VerifyBasket_WhenBasketIsInvalid()
    {
        AddTestCategory();

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            ImagePath = "test.jpg",
            Price = 10.0,
            Stock = 10,
            CategoryId = 1
        };
        context.Products.Add(product);
        context.SaveChanges();

        var basketItems = new List<BasketItem>
        {
            new BasketItem
            {
                ProductId = product.Id,
                Product = product,
                Quantity = 15   
            }
        };

        var result = productService.VerifyBasket(basketItems);

        Assert.False(result);
    }
}