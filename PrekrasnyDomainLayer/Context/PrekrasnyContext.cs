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

            userService.RegisterNewUser("test1", "test1");
            userService.RegisterNewUser("test2", "test2");
            userService.RegisterNewUser("test3", "test3");
            userService.RegisterNewUser("admin", "admin");
            var admin = Users.First(u => u.UserName == "admin");
            admin.UserRole = UserRole.Admin;
            Users.Update(admin);
            SaveChanges();

        }
    }
}
