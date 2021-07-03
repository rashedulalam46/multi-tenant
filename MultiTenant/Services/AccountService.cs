using Microsoft.EntityFrameworkCore;
using MultiTenant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenant.Services
{
    public interface IAccountServices
    {
        //Task<SigninPre> SigninPre(string email);
        Task<SigninPost> SigninPost(SigninViewModel signinViewModel);
    }

    public class AccountServices : IAccountServices
    {
        private readonly TenantDBContext _tenantDBContext;
        public AccountServices(TenantDBContext tenantDBContext)
        {
            _tenantDBContext = tenantDBContext;
        }

        public async Task<SigninPost> SigninPost(SigninViewModel model)
        {
            try
            {
                var result = await _tenantDBContext.SigninPost
                                .FromSqlRaw("SignIn @p0, @p1"
                                , parameters: new[] { model.UserName, model.Password })
                                .AsNoTracking()
                                .ToListAsync();

                return result.FirstOrDefault() ?? new SigninPost();

            }
            catch (Exception ex)
            {
                return new SigninPost();
            }
        }

    }
}
