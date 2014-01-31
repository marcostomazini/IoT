using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ArquitetaWeb.Command.WebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
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