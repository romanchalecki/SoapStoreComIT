using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoapStoreComIT.Models;

namespace SoapStoreComIT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<StoreItem> StoreItem { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }



        public DbSet<Soap> Soap { get; set; }
        public DbSet<Shampoo> Shampoo { get; set; }

    }
}
