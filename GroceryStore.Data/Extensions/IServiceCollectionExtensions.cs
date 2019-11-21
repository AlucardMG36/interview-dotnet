using GroceryStore.Core.Accessors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Data.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerAccessor, CustomerRepository>()
                .AddScoped<IOrderAccessor, OrderRepository>()
                .AddScoped<IProductAccessor, ProductRepository>();

            return services;
        }

    }
}
