using Microsoft.AspNetCore.Mvc;
using MultiTenant.Models;
using MultiTenant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MultiTenant.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _acountServices;

        public AccountController(IAccountServices acountServices)
        {
            _acountServices = acountServices;
        }

        public async Task<IActionResult> Signin(SigninViewModel model)
        {
            CommonViewModel commModel = new();
            try
            {
                var loginSuccessData = await _acountServices.SigninPost(model);

                if (!string.IsNullOrWhiteSpace(loginSuccessData.UserID) && loginSuccessData.Success == "Y")
                {
                    commModel.Success = loginSuccessData.Success;
                    commModel.RoleID = loginSuccessData.RoleID;
                    commModel.ReturnValue = "/home";                   
                }
                else
                {
                    if (loginSuccessData.UserName == "PRST")
                    {
                        commModel.Msg = loginSuccessData.Msg;
                        commModel.ReturnValue = "ForgetPass";
                    }
                    else
                    {
                        commModel.Msg = (loginSuccessData.Msg ?? "User name or password is being wrong.");
                        commModel.Success = (loginSuccessData.Success ?? "N");
                        commModel.ReturnValue = "/".ToLower();
                    }
                }

            }
            catch (Exception ex)
            {
                commModel.Success = "N";
                commModel.Msg = "Oops! Something went wrong.";
                commModel.ReturnValue = "/login";
            }
            return await Task.Run(() => Json(commModel));
        }
    }
}
