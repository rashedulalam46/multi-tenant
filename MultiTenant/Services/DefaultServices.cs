using Microsoft.EntityFrameworkCore;
using MultiTenant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenant.Services
{
    public interface IDefaultServices
    {
        Task<SigninPre> SigninPre(string email);
        CheckSubDomain CheckSubDomain(string subDomain);
    }
    public class DefaultServices : IDefaultServices
    {
        private readonly DefaultDBContext _defaultDBContext;
        public DefaultServices(DefaultDBContext defaultDBContext)
        {
            _defaultDBContext = defaultDBContext;
        }

        public async Task<SigninPre> SigninPre(string email)
        {
            try
            {
                var result = await _defaultDBContext.SigninPre
                                .FromSqlRaw("Signin @p0"
                                , parameters: new[] { email })
                                .AsNoTracking()
                                .ToListAsync();
                return result.FirstOrDefault() ?? new SigninPre();

            }
            catch (Exception ex)
            {
                return new SigninPre();
            }
        }
        public CheckSubDomain CheckSubDomain(string subDomain)
        {
            try
            {
                var result = _defaultDBContext.CheckSubDomain
                                .FromSqlRaw("CheckSubDomain @p0"
                                , parameters: new[] { subDomain })
                                .AsNoTracking()
                                .ToList();
                return result.FirstOrDefault() ?? new CheckSubDomain();

            }
            catch (Exception ex)
            {
                return new CheckSubDomain();
            }
        }

    }
}
