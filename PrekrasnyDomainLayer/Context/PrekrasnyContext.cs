using Microsoft.EntityFrameworkCore;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Models.Enums;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using System.Data.Common;

namespace PrekrasnyDomainLayer.Context
{
    public sealed class PrekrasnyContext : DbContext
    {
        private static readonly string defaultDataSource = "PrekrasnaBaza.db";

        private string? dataSource;

        private DbConnection? providedConnection;

        public PrekrasnyContext(string dataSource)
        {
            this.dataSource = dataSource;
        }

        public PrekrasnyContext(DbConnection providedConnection)
        {
            this.providedConnection = providedConnection;
        }

        public PrekrasnyContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (providedConnection is not null)
            {
                optionsBuilder.UseSqlite(providedConnection);
            }
            else
            {
                optionsBuilder.UseSqlite($"Data Source={dataSource ?? defaultDataSource};");
            }
        }

        public static void InitalizeSharedContext()
        {
            AppState.SharedContext = new PrekrasnyContext();
            var created = AppState.SharedContext.Database.EnsureCreated();
            if (created)
            {
                AppState.SharedContext.Seed();
            }
        }

        public void Seed()
        {
            UserService userService = new(this);
            userService.RegisterNewUser("admin", "admin", PrekrasnyDomainLayer.Models.Enums.UserRole.Admin);
            var admin = Users.First(u => u.UserName == "admin");
            admin.UserRole = UserRole.Admin;
            admin.Theme = UserTheme.Dark;
            Users.Update(admin);


            userService.RegisterNewUser("cso", "cso", PrekrasnyDomainLayer.Models.Enums.UserRole.CustomerService);
            var cso = Users.First(u => u.UserName == "cso");
            cso.UserRole = UserRole.CustomerService;
            cso.Theme = UserTheme.Dark;
            Users.Update(cso);


            userService.RegisterNewUser("shp", "shp", PrekrasnyDomainLayer.Models.Enums.UserRole.Shipping);
            var shipping = Users.First(u => u.UserName == "shp");
            shipping.UserRole = UserRole.Shipping;
            shipping.Theme = UserTheme.Light;
            Users.Update(shipping);

            var category = Categories.Add(new Category { Name = "Category 1" }).Entity;

            var product1 = Products.Add(new Product() { Name = "Product 1", Price = 10, Category = category, Stock = 10 }).Entity;
            var product2 = Products.Add(new Product() { Name = "Product 2", Price = 20, Category = category, Stock = 5 }).Entity;

            var order = Orders.Add(new Order() { User = cso, Status = OrderStatus.Ordered, Items = new List<OrderItem> { new OrderItem() { Product = product1, Quantity = 2 } , new OrderItem() { Product = product2, Quantity = 1 } } });

            SaveChanges();
        }
    }
}
