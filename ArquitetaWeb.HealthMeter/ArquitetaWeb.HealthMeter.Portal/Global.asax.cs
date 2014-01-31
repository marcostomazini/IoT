using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ArquitetaWeb.Common.Infra.Mvc.Rotas;

namespace ArquitetaWeb.HealthMeter.Portal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");         

            routes.MapRoute("Acoes",
                          "Acoes",
                          "ArquitetaWeb.HealthMeter.Portal.*",
                          "Acoes/{controller}/{action}/{id}",
                          new { action = "Index", id = UrlParameter.Optional },
                          null
            );

            routes.MapRoute("Configuracoes",
                            "Configuracoes",
                            "ArquitetaWeb.HealthMeter.Portal.*",
                            "Configuracoes/{controller}/{action}/{id}",
                            new { action = "Index", id = UrlParameter.Optional },
                            null
            );

            routes.MapRoute("Default", // Route name
                            "{controller}/{action}/{id}", // URL with parameters
                            new { controller = "Portal", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            AlterarCaminhoDataBase();
        }

        private static void AlterarCaminhoDataBase()
        {
            string pathFull = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var parent = System.IO.Directory.GetParent(pathFull).ToString();
            var parent2 = string.Format("{0}{1}", System.IO.Directory.GetParent(parent).ToString(), "/Database");
            AppDomain.CurrentDomain.SetData("DataDirectory", parent2);
        }
    }
}