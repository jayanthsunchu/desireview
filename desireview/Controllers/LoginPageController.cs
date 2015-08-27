using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace desireview.Controllers
{
    public class LoginPageController : Controller
    {
        // GET: LoginPage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ResetPwd()
        {
            return View();
        }

        public ActionResult PasswordResetLink() {
            return View();
        }
    }
}