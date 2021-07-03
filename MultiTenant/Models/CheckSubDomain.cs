using System.ComponentModel.DataAnnotations;

namespace MultiTenant.Models
{
    public class CheckSubDomain
    {
        [Key]
        public string Success { get; set; }
        public string Msg { get; set; }
        public string ComCode { get; set; }
        public string SubDomain { get; set; }
        public string ThemeColor { get; set; }
        public string Logo { get; set; }
    }
}
