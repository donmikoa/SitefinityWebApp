using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using SitefinityWebApp.Mvc.Models;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "MessageWidget", Title = "Message Widget", SectionName = "MvcWidgets")]
    public class MessageWidgetController : Controller
    {
        [Category("String Properties")]
        public string Message { get; set; }
        // GET: MessageWidget
        public ActionResult Index()
        {
            var model = new MessageWidgetModel();

            if (string.IsNullOrEmpty(this.Message))
            { model.Message = "Hello World"; }
            else { model.Message = this.Message; }
            return View("Default", model);
        }
    }
}