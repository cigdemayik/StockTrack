using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StockTrack.DataAccess.Concrete.EfCore.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.DataAccess.Concrete.EfCore.Context
{
    public class StoctTrackContext:DbContext
    {
        private readonly IConfiguration _configuration;

        public StoctTrackContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new UserConfiguration());
            modelbuilder.ApplyConfiguration(new UserProductConfiguration());
            modelbuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
