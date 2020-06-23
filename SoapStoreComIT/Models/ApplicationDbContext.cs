using System;
using Microsoft.EntityFrameworkCore;
using SoapStoreComIT.Models;

namespace SoapStore.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Soap> Soap { get; set; }
        public DbSet<Shampoo> Shampoo { get; set; }

    }
}
