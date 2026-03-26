using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application;
using MyApp.Core;
using MyApp.Infrastructure;

namespace clean_architecture_demo_v1
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(
            this IServiceCollection services,
            IConfiguration configuration)        // ✅ accepts configuration
        {
            services.AddCoreDI(configuration)            // ✅ passed in
                    .AddApplicationDI()
                    .AddInfrastructureDI(configuration); // ✅ passed in

            return services;
        }
    }
}