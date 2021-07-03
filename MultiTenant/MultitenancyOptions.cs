using System.Collections.ObjectModel;

namespace MultiTenant
{
    public class MultitenancyOptions
    {
        public Collection<AppTenant> Tenants { get; set; }
    }
}
