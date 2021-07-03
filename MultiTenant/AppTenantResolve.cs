using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SaasKit.Multitenancy;
using MultiTenant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenant
{
    public interface ITenantResolver
    {
        Task<TenantContext<AppTenant>> ResolveAsync(HttpContext context);
    }

    public class AppTenantResolver : ITenantResolver<AppTenant>
    {
        private IConfiguration _Configuration { get; }
        private readonly IDefaultServices _defaultServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppTenantResolver(IDefaultServices context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _defaultServices = context;
            _Configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TenantContext<AppTenant>> ResolveAsync(HttpContext context)
        {
            string scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            var subdomainFromUrl = context.Request.Host.Value.ToLower().Split(".")[0] ?? "";

            var sundomain = _defaultServices.CheckSubDomain(subdomainFromUrl);

            AppTenant tenant = new();

            if (!string.IsNullOrWhiteSpace(sundomain.SubDomain))
            {
                if (!sundomain.SubDomain.Equals(subdomainFromUrl.ToString()))
                {
                    return null;
                }
                else
                {
                    tenant.SubDomain = sundomain.SubDomain;
                    tenant.Hostname = context.Request.Host.Value.ToLower();
                    tenant.Scheme = scheme + "://";
                    tenant.CompanyCode = sundomain.ComCode;
                    tenant.Logo = sundomain.Logo;
                    tenant.ThemeColor = sundomain.ThemeColor;
                    tenant.ConnectionString = _Configuration.GetConnectionString("DefaultConnection").Replace("TenantTest", "Tenant" + sundomain.SubDomain);
                }
            }

            return await Task.FromResult(new TenantContext<AppTenant>(tenant));
        }
    }
}
