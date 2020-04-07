using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "Recent News", Title = "Recent News", SectionName = "MvcWidgets")]
    public class RecentNewsController : Controller
    {
        // GET: RecentNews
        public ActionResult Index()
        {

            return View();
        }
    }
}