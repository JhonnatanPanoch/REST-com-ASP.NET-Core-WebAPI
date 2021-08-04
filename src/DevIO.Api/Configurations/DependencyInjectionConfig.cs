using DevIO.Bussiness.Interfaces.Repository;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DevIO.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services
                .AddScoped<CustomDbContext>()
                .AddScoped<ISupplierRepository, SupplierRepository>()
                .AddScoped<IAddressRepository, AddressRepository>()
                .AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
