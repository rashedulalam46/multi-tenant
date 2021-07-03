using Microsoft.EntityFrameworkCore;
using MultiTenant.Models;

namespace MultiTenant.Services
{
    public class DefaultDBContext : DbContext
    {
        public DefaultDBContext(DbContextOptions<DefaultDBContext> options) : base(options)
        { }

        public DbSet<SigninPre> SigninPre { get; set; }
        public DbSet<CheckSubDomain> CheckSubDomain { get; set; }

    }
}
