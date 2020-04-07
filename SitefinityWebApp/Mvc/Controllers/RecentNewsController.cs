using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.GenericContent.Model;
using SitefinityWebApp.Mvc.Models;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "Recent News", Title = "Recent News", SectionName = "MvcWidgets")]
    public class RecentNewsController : Controller
    {
        // GET: RecentNews
        public ActionResult Index()
        {
            var newsManager = NewsManager.GetManager();
            var newsItems = newsManager.GetNewsItems().Where(n => n.Visible == true && n.Status == ContentLifecycleStatus.Live);
            var newsModel = new RecentNewsModel(newsItems);
            return View("Default", newsModel);
        }
    }
}