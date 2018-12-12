using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMyDoctor.Controllers
{
    public class AppUserController : Controller
    {
        // GET: AppUser
        public ActionResult Index()
        {
            return View();
        }
    }
}