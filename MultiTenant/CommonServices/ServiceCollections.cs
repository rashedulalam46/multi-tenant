using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenant.CommonServices
{
    public static class ServiceCollections
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<CryptographyService>();
            services.AddTransient<IDefaultServices, DefaultServices>();
            services.AddTransient<IAccountServices, AccountServices>();
            return services;
        }
        public static IServiceCollection ConnectionStringRegisterServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<TenantDBContext>();

            string defaultConString = Configuration.GetConnectionString("TenantConnection");
            services.AddDbContext<DefaultDBContext>(options => options.UseSqlServer(defaultConString));

            return services;
        }
    }
}
