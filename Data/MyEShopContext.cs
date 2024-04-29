using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class MyEShopContext : DbContext
    {
        public MyEShopContext(DbContextOptions<MyEShopContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryToProduct> CategoryToProducts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Users> Users { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CategoryToProduct>().HasKey(t => new { t.CategoryId , t.ProductId });

            #region Send Data Category

            modelBuilder.Entity<Category>().HasData(new Category()
            {
                Id = 1,
                Name = "Asp.net Core",
                Description = "Asp.net Core3"
            },
            new Category()
            {
                Id = 2,
                Name = "ساعت",
                Description = "انواع ساعت"
            }, new Category()
            {
                Id = 3,
                Name = "لوازم خانگی",
                Description = "انواع لوازم منزل"

            }, new Category()
            {
                Id = 4,
                Name = "تکنولوژی",
                Description = "انواع کنسول بازی و تلوزیون"
            }

                );

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
