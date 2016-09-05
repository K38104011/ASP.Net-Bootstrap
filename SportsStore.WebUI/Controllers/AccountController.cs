using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using WebMatrix.WebData;
using System.Web.Security;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                bool success = WebSecurity.Login(login.UserName, login.Password, false);
                if (success)
                {
                    string returnUrl = Request.QueryString["ReturnUrl"];
                    if (returnUrl == null)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        Response.Redirect(returnUrl);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Please enter Username and Password");
            }

            return View(login);
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (!WebSecurity.UserExists(register.UserName))
                {
                    WebSecurity.CreateUserAndAccount(register.UserName, register.Password,
                        new { FullName = register.FullName, Email = register.Email }, false);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Please enter all details");
            }

            return View(register);
        }
    }
}
