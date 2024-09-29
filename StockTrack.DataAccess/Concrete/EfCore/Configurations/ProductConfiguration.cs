using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockTrack.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.DataAccess.Concrete.EfCore.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(256);

            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.Active).HasDefaultValue(1);

            builder.HasData(new List<Product>()
            {
                new Product()
                {
                    Id=1,
                    Name="A Product"
                },
                new Product()
                {
                    Id=2,
                    Name="B Product"
                }
            });
        }
    }
}
