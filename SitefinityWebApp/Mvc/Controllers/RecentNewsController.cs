using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    public class RecentNewsController : Controller
    {
        // GET: RecentNews
        public ActionResult Index()
        {
            return View();
        }
    }
}