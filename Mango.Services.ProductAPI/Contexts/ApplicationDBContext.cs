using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Contexts
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Samosa",
                Price = 15,
                Description = "Samosa Description",
                ImageSrc = "https://dotnetviktor.blob.core.windows.net/mango/samosa.jpg",
                CategoryName = "Appetizer",
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Paneer Tikka",
                Price = 13.99,
                Description = "Paneer Tikka Description",
                ImageSrc = "https://dotnetviktor.blob.core.windows.net/mango/paneer-tikka.webp",
                CategoryName = "Appetizer",
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Sweet Pie",
                Price = 10.99,
                Description = "Sweet Pie Description",
                ImageSrc = "https://dotnetviktor.blob.core.windows.net/mango/Sweet-Pie.jpeg",
                CategoryName = "Dessert",
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Pav Bhaji",
                Price = 15,
                Description = "Pav Bhaji Description",
                ImageSrc = "https://dotnetviktor.blob.core.windows.net/mango/pav-bhaji.jpeg",
                CategoryName = "Entree",
            });
        }
    }
}
