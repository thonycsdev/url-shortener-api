using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;
using Service.Services;

namespace CrossCutting.Injections
{
    public static class ServicesInjections
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IUrlMappingService, UrlMappingService>();
        }
    }
}
