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
    public class UserProductConfiguration : IEntityTypeConfiguration<UserProduct>
    {
        public void Configure(EntityTypeBuilder<UserProduct> builder)
        {
            builder.ToTable("UserProduct");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.Active).HasDefaultValue(1);

            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.TotalPrice).IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserProducts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.UserProducts)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
