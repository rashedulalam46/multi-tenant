using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MultiTenant.Models;
using MultiTenant.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenant.Controllers
{
    public class HomeController : Controller
    {
        private readonly TenantDBContext _tenantDBContext;
        public HomeController(TenantDBContext tenantDBContext)
        {
            _tenantDBContext = tenantDBContext;
        }
        public async Task<IActionResult> Index()
        {
            Companies company = new Companies();
            try
            {

                company = _tenantDBContext.Companies.FirstOrDefault();
                return await Task.Run(() => View(company));
            }
            catch (Exception ex)
            {
                return await Task.Run(() => View(company));
            }
        }

    }
}
