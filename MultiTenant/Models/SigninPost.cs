using System.ComponentModel.DataAnnotations;

namespace MultiTenant.Models
{
    public class SigninPost
    {
        [Key]
        public string Success { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public int RoleID { get; set; }        
        public string Msg { get; set; }
    }
}
