using MyApp.Application;
using MyApp.Infrastructure;

namespace clean_architecture_demo_v1
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            services.AddApplicationDI()   // ✅ fixed
                    .AddInfrastructureDI(); // ✅ fixed

            return services;
        }
    }
}