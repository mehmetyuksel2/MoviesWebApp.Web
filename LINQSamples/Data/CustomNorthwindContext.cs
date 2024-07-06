using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples.Data
{
    public class CustomNorthwindContext : NorthWindContext
    {

        public DbSet<ProductModel> ProductModels { get; set; }

        public CustomNorthwindContext(DbSet<ProductModel> productModels)
        {
            ProductModels = productModels;
        }

        public CustomNorthwindContext()
        {
        }

        public CustomNorthwindContext(DbContextOptions<NorthWindContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductModel>(entity => {
                entity.HasNoKey();

                entity.Property(e => e.Name).HasColumnName("ProductName");
                entity.Property(e => e.Price).HasColumnName("UnitPrice");

            });

        }
    }
}
