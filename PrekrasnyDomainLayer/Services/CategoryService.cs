using Microsoft.EntityFrameworkCore;
using PrekrasnyDomainLayer.Context;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.State;

namespace PrekrasnyDomainLayer.Services;

public sealed class CategoryService
{
    private readonly PrekrasnyContext context;

    public CategoryService() : this(AppState.SharedContext) { }

    public CategoryService(PrekrasnyContext context)
    {
        this.context = context;
    }

    public void AddCategory(string name) { 
        context.Categories.Add(new Category { Name = name });
        context.SaveChanges();
    }

    public void RenameCategory(Category categoryToRename, string name) {
        categoryToRename.Name = name;
        context.Categories.Update(categoryToRename);
        context.SaveChanges();
    }

    public bool RemoveCategory(Category categoryToRemove) 
    {
        var freshCategory = context.Categories.Include(c => c.Products).First(c => c.Id == categoryToRemove.Id);
        var hasProducts = freshCategory.Products.Any();
        if (hasProducts)
        {
            //throw new Exception(nameof(hasProducts));
            return false;
        }

        context.Categories.Remove(freshCategory);
        context.SaveChanges();
        return true;
    }
}
