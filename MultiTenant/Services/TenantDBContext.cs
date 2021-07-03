using Microsoft.EntityFrameworkCore;
using MultiTenant.Models;

namespace MultiTenant.Services
{
    public class TenantDBContext : DbContext
    {
        private readonly AppTenant _appTenant;
        public TenantDBContext(DbContextOptions<TenantDBContext> options, AppTenant appTenant) : base(options)
        {
            _appTenant = appTenant;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appTenant.ConnectionString);
        }
        public DbSet<SigninPost> SigninPost { get; set; }

        public DbSet<Companies> Companies { get; set; }
    }
}
