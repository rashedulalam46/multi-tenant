using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenant.CommonServices;
using MultiTenant.Models;
using MultiTenant.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MultiTenant.Controllers
{
    public class LoginController : Controller
    {
        private readonly IDefaultServices _defaultServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        

        private readonly string themeColor = "#0c75bc";       
        private string companyLogo = "test-logo.png";
        private readonly string companyCode = "test";
        public LoginController(
           IDefaultServices defaultServices
           , IWebHostEnvironment webHostEnvironment
           , IHttpContextAccessor httpContextAccessor          
           )
        {
            _defaultServices = defaultServices;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;            
        }
        public async Task<IActionResult> Index(string v = "", string signupfor = "")
        {
            SigninViewModel model = new();

            try
            {
                if (!string.IsNullOrWhiteSpace(v))
                {
                    #region After
                    var param = CryptographyService.DecodeServerName(v);
                    var decryptedEmail = param.Split("|");
                    model.UserName = decryptedEmail[0];
                    model.LoginViewModel.UserName = model.UserName;
                    model.LoginViewModel.RememberMe = decryptedEmail[2];

                    var haveSubdomain = _defaultServices.CheckSubDomain(decryptedEmail[1]);

                    if (!string.IsNullOrWhiteSpace(haveSubdomain.SubDomain))
                    {
                        model.ThemeColor = haveSubdomain.ThemeColor ?? themeColor;
                        model.ComCode = haveSubdomain.ComCode;
                        model.Logo = haveSubdomain.Logo;
                        model.SubDomain = haveSubdomain.SubDomain;
                        model.LoginViewModel.ThemeColor = haveSubdomain.ThemeColor ?? themeColor;

                    }
                    else
                    {
                        model.ThemeColor = model.ThemeColor ?? themeColor;
                        model.ComCode = companyCode;
                        model.Logo = companyLogo;
                        model.SubDomain = companyCode;
                        model.LoginViewModel.ThemeColor = model.ThemeColor ?? themeColor;
                    }
                    #endregion
                }
                else
                {
                    #region Next
                    string subdomainFromUrl = string.Empty;
                    string host = _httpContextAccessor.HttpContext.Request.Host.Value.ToLower();
                    string[] hosts = host.Split(".");

                    if (host.Contains("localhost"))
                    {
                        if (hosts.Length == 1)
                        {
                            subdomainFromUrl = companyCode;

                            model.ThemeColor = model.ThemeColor ?? themeColor;
                            model.ComCode = companyCode;
                            model.Logo = companyLogo;
                            model.SubDomain = subdomainFromUrl ?? companyCode;
                            model.LoginViewModel.ThemeColor = model.ThemeColor ?? themeColor;

                        }
                        else
                        {
                            subdomainFromUrl = hosts[0];
                            var haveSubdomain = _defaultServices.CheckSubDomain(subdomainFromUrl);

                            model.ThemeColor = haveSubdomain.ThemeColor ?? themeColor;
                            model.ComCode = haveSubdomain.ComCode ?? companyCode;
                            model.Logo = haveSubdomain.Logo;
                            model.SubDomain = string.IsNullOrWhiteSpace(haveSubdomain.SubDomain) == true ? companyCode : haveSubdomain.SubDomain;
                            model.LoginViewModel.ThemeColor = haveSubdomain.ThemeColor ?? themeColor;
                        }
                    }
                    else
                    {
                        if (hosts.Length == 2)
                        {
                            subdomainFromUrl = companyCode;

                            model.ThemeColor = model.ThemeColor ?? themeColor;
                            model.ComCode = companyCode;
                            model.Logo = companyLogo;
                            model.SubDomain = subdomainFromUrl ?? companyCode;
                            model.LoginViewModel.ThemeColor = model.ThemeColor ?? themeColor;

                        }
                        else
                        {
                            subdomainFromUrl = hosts[0];
                            var haveSubdomain = _defaultServices.CheckSubDomain(subdomainFromUrl);

                            model.ThemeColor = haveSubdomain.ThemeColor ?? themeColor;
                            model.ComCode = haveSubdomain.ComCode ?? companyCode;
                            model.Logo = haveSubdomain.Logo;
                            model.SubDomain = string.IsNullOrWhiteSpace(haveSubdomain.SubDomain) == true ? companyCode : haveSubdomain.SubDomain;
                            model.LoginViewModel.ThemeColor = haveSubdomain.ThemeColor ?? themeColor;
                        }
                    }
                    #endregion
                }
                model.LoginViewModel.SubDomain = model.SubDomain;
                model.LoginViewModel.Logo = model.Logo;
                //var hostingPath = Path.Combine(_webHostEnvironment.WebRootPath, "files\\" + model.SubDomain + "\\com");
                //var existingFiles = Directory.GetFiles(hostingPath, model.ComCode + ".*");

                //if (existingFiles.Length > 0)
                //{
                //    companyLogo = model.ComCode + Path.GetExtension(existingFiles[0]);
                //    //companyLogoPath = "/files/" + model.SubDomain + "/com/logo/" + model.ComCode + Path.GetExtension(existingFiles[0]);
                //}

                model.SignupFor = signupfor;
                return await Task.Run(() => View(model));
            }
            catch (Exception ex)
            {
                //_errorLogService.WriteErrorLog(ex.ToString());
                return await Task.Run(() => Redirect("/Error/Index".ToLower()));
            }
        }

        ///[HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginPre(LoginViewModel model)
        {
            CommonViewModel commModel = new CommonViewModel();
            try
            {
                
                    string makingNewUrl = string.Empty;
                    string scheme = _httpContextAccessor.HttpContext.Request.Scheme;
                    string hostValue = _httpContextAccessor.HttpContext.Request.Host.Value;

                    string[] hosts = hostValue.Split(".");

                    var loginSundomain = await _defaultServices.SigninPre(model.UserName);

                    if (loginSundomain.Success.Equals("Y"))
                    {

                        if (hostValue.Contains("localhost"))
                        {
                            if (hosts.Length == 1)
                            {
                                makingNewUrl += scheme + "://" + loginSundomain.SubDomain + "." + hosts[0];
                            }

                            if (hosts.Length == 2)
                            {
                                makingNewUrl += scheme + "://" + loginSundomain.SubDomain + "." + hosts[1];
                            }
                        }
                        else
                        {
                            if (hosts.Length == 2)
                            {
                                makingNewUrl += scheme + "://" + hosts[0] + "." + hosts[1];
                            }
                            if (hosts.Length == 3)
                            {
                                makingNewUrl += scheme + "://" + loginSundomain.SubDomain + "." + hosts[1] + "." + hosts[2];
                            }

                        }
                    }
                    else
                    {
                        if (hostValue.Contains("localhost"))
                        {
                            if (hosts.Length == 1)
                            {
                                makingNewUrl += scheme + "://" + hosts[0];
                            }

                            if (hosts.Length == 2)
                            {
                                makingNewUrl += scheme + "://" + hosts[1];
                            }
                        }
                        else
                        {
                            makingNewUrl += scheme + "://" + hostValue;
                        }

                        return Redirect(makingNewUrl + "/login".ToLower());
                    }

                    var param = CryptographyService.EncodeServerName(model.UserName + "|" + loginSundomain.SubDomain + "|" + model.RememberMe);

                commModel.Success = "Y";
                commModel.ReturnValue = makingNewUrl + "/login?v=".ToLower() + param;
                //return Redirect(returnUrl);
                return await Task.Run(() => Json(commModel));

            }
            catch (Exception ex)
            {
                //_errorLogService.WriteErrorLog(ex.ToString());
                //return await Task.Run(() => Redirect("/login".ToLower()));
                commModel.ReturnValue = "/login";
                commModel.Success = "N";
                commModel.Msg = ex.Message.ToString();
                return await Task.Run(() => Json(commModel));
            }
        }
                
       

    }



}
