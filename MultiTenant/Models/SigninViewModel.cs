using System.ComponentModel.DataAnnotations;

namespace MultiTenant.Models
{
    public class SigninViewModel
    {
        public SigninViewModel()
        {
            LoginViewModel = new LoginViewModel();
        }        
        public string UserName { get; set; }        
        public string Password { get; set; }
        public string RememberMe { get; set; }
        public string ComCode { get; set; }
        public string SubDomain { get; set; }        
        public string FilePath { get; set; }
        public string SignupFor { get; set; }
        public string ThemeColor { get; set; }
        public string Logo { get; set; }

        public LoginViewModel LoginViewModel { get; set; }
    }
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string UserName { get; set; }
        public string RememberMe { get; set; }
        public string ThemeColor { get; set; }
        public string SubDomain { get; set; }
        public string Logo { get; set; }

    }
}
