using System;
using System.Web.Mvc;
using System.Web.Security;
using LoyalBankWebUi.Models;

namespace Cip_Mehrabad.Controllers
{
    public  class AccountController : Controller
    {
        [HttpGet]
        public virtual ActionResult Login(string returnUrl = "", bool resetPassword = false)
        {
            if (Request.IsAuthenticated)
            {
                if (returnUrl != "")
                {
                    Redirect(returnUrl);
                }
                RedirectToAction("Index", "Home");
            }

            if (resetPassword)
                Session["resetPassword"] = "resetPassword";
            else
                Session["resetPassword"] = null;
            Session["ReturnUrl"] = returnUrl;
            HttpContext.Cache.Remove("EditDateTime");
            HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
            return View();
        }

        [HttpPost]
        public virtual ActionResult Login(LoginVm login, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
              
                    if ( login.Username.ToLower()=="admin" &&  login.Password.ToLower()=="01")
                    {
                        FormsAuthentication.SetAuthCookie(login.Username, login.RememberMe);
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public  ActionResult Logout()
        {
            Session["User"] = null;
            HttpContext.Cache.Remove("EditDateTime");
            HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }



    }
}