using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArquitetaWeb.HealthMeter.Portal.Controllers
{
    public class SettingsController : Controller
    {
        public SettingsController() { }

        public ActionResult Index()
        {
            ViewBag.Title = "Configurações Aplicação";
            return View();
        }
    }
}
