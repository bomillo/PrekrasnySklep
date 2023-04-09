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

public class CategoryServiceTests : IDisposable
{
    private readonly PrekrasnyContext context;
    private readonly CategoryService categoryService;

    private readonly InMemoryDbConecttion dbConnection = new();

    public CategoryServiceTests()
    {
        this.context = new PrekrasnyContext(dbConnection.DbConnection);
        context.Database.EnsureCreated();

        AppState.SharedContext = context;

        categoryService = new CategoryService(context);
    }

    public void Dispose()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [Fact]
    public void AddCategory_CreatesNewCategory()
    {
        string categoryName = "TestCategory";

        Assert.False(context.Categories.Any(c => c.Name == categoryName));

        categoryService.AddCategory(categoryName);

        Assert.True(context.Categories.Any(c => c.Name == categoryName));
    }

    [Fact]
    public void RenameCategory_ChangesCategoryName()
    {
        string categoryName = "TestCategory";
        string newCategoryName = "NewTestCategory";

        categoryService.AddCategory(categoryName);
        var categoryToRename = context.Categories.First(c => c.Name == categoryName);

        categoryService.RenameCategory(categoryToRename, newCategoryName);

        Assert.False(context.Categories.Any(c => c.Name == categoryName));
        Assert.True(context.Categories.Any(c => c.Name == newCategoryName));
    }

    [Fact]
    public void RemoveCategory_RemovesCategoryWithoutProducts()
    {
        string categoryName = "EmptyTestCategory";

        categoryService.AddCategory(categoryName);
        var categoryToRemove = context.Categories.First(c => c.Name == categoryName);

        categoryService.RemoveCategory(categoryToRemove);

        Assert.False(context.Categories.Any(c => c.Name == categoryName));
    }

    [Fact]
    public void RemoveCategory_ThrowsExceptionWhenCategoryHasProducts()
    {
        string categoryName = "TestCategoryWithProducts";

        categoryService.AddCategory(categoryName);
        var categoryWithProducts = context.Categories.First(c => c.Name == categoryName);

        context.Products.Add(new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            ImagePath = "test.jpg",
            Price = 10.0,
            Stock = 10,
            CategoryId = categoryWithProducts.Id,
            Category = categoryWithProducts
        });
        context.SaveChanges();

        Assert.Throws<Exception>(() => categoryService.RemoveCategory(categoryWithProducts));
    }
}