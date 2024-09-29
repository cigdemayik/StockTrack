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
    public class UserProductHistoryConfiguration : IEntityTypeConfiguration<UserProductHistory>
    {
        public void Configure(EntityTypeBuilder<UserProductHistory> builder)
        {
            builder.ToTable("UserProductHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OperationDate).IsRequired();
            builder.Property(x => x.OperationDate).HasDefaultValueSql("getdate()");

            builder.Property(x => x.Price).IsRequired();

            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.Active).HasDefaultValue(1);

            builder.Property(x => x.Operation).IsRequired();

            builder.HasOne(x => x.UserProduct)
                .WithMany(x => x.UserProductHistories)
                .HasForeignKey(x => x.UserProductId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
