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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(256);

            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(64);

            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.Active).HasDefaultValue(1);

            builder.HasData(new List<User>()
            {
                new User()
                {
                    Id=1,
                    UserName="selen",
                    Password="12345",
                },
                 new User()
                {
                    Id=2,
                    UserName="alp",
                    Password="12345",
                }
            });
        }
    }
}
