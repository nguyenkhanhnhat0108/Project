using Project.Repository.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        readonly IAccountRepository accountRepository;
        public LoginController(IAccountRepository repository)
        {
            this.accountRepository = repository;
        }

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string email)
        {
            if (ModelState.IsValid)
            {
                if(accountRepository.CheckEmail(email))
                {
                    accountRepository.SendOOPByEmail(email);
                    Session["EMAIL"] = email;
                    return RedirectToAction("CheckOOP", "Login");
                }
                else
                {
                    ViewBag["message"] = "Đăng nhập thất bại";
                }
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult CheckOOP()
        {
            var email = Session["EMAIL"];
            if (email == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOOP(string oop)
        {
            if (ModelState.IsValid)
            {
                var email = Session["EMAIL"];
                if (accountRepository.CheckOOP(oop, email.ToString()))
                {
                    FormsAuthentication.SetAuthCookie(oop, false);
                    return RedirectToAction("Index", "Account");
                }
            }
            return View();
        }
    }
}