using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTrack.Business.Abstract;
using StockTrack.Business.Concrete;
using StockTrack.DataAccess;
using StockTrack.DataAccess.Abstract.Generic;
using StockTrack.DataAccess.Concrete.EfCore.Context;
using StockTrack.DataAccess.Concrete.EfCore.Repositories.Generic;
using StockTrack.Helpers.Abstract;
using StockTrack.Helpers.Concrete;

namespace StockTrack.Business.DependencyResolvers
{
    public static class StockTrackIOC
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoctTrackContext>();

            services.AddScoped(typeof(IGenericRepository<>) , typeof (GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region Services
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserProductHistoryService, UserProductHistoryManager>();
            services.AddScoped<IUserProductService, UserProductManager>();
            services.AddScoped<IProductService, ProductManager>();
            #endregion

            #region Helpers
            services.AddScoped<IServiceResponseHelper, ServiceResponseHelper>();
            #endregion
        }
    }
}
