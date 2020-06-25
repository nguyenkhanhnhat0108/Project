using Project.Repository.Account;
using System.Web.Mvc;
using System.Web.Security;

namespace Project.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        readonly IAccountRepository accountRepository;
        public AccountController(IAccountRepository repository)
        {
            this.accountRepository = repository;
        }

        // GET: Admin/Account
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //GET: Admin/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email)
        {
            if (ModelState.IsValid)
            {
                accountRepository.CheckEmailandSendOOP(email);
                Session["EMAIL"] = email;
                return RedirectToAction("CheckOOP", "Account");
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult CheckOOP()
        {
            var email = Session["EMAIL"];
            if (email == null)
            {
                return RedirectToAction("Login", "Account");
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
                if (accountRepository.CheckOOP(oop,email.ToString()))
                {
                    FormsAuthentication.SetAuthCookie(oop, false);
                    return RedirectToAction("Index", "Account");
                }
            }
            return View();
        }
    }
}