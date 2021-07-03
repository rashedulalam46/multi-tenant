using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenant
{
    public class AppTenant
    {
        public string Name { get; set; }
        public string SubDomain { get; set; }
        //public string[] Hostnames { get; set; }
        public string Hostname { get; set; }
        public string Scheme { get; set; }
        public string ThemeColor { get; set; }
        public string CompanyCode { get; set; }
        public string Logo { get; set; }
        public string ConnectionString { get; set; }
    }
}
