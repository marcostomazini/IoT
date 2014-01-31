using System.Web.Mvc;
using System.Web.Routing;

namespace ArquitetaWeb.Common.Infra.Mvc.Rotas
{
    public static class RouteCollectionExtensions
    {
        public static void MapRoute(this RouteCollection routes, string areaName, string routeName, string controllersNamespace, string url, object defaults, object constraints)
        {
            var newRoute = new NamedRoute(routeName, url, new MvcRouteHandler());

            if (newRoute.Constraints == null) newRoute.Constraints = new RouteValueDictionary(constraints);
            if (newRoute.Defaults == null) newRoute.Defaults = new RouteValueDictionary(defaults);
            if (newRoute.DataTokens == null) newRoute.DataTokens = new RouteValueDictionary();

            if (areaName != null)
            {
                newRoute.Constraints.Add("area", areaName);
                newRoute.Defaults.Add("area", areaName);
                newRoute.DataTokens.Add("area", areaName);
            }
            newRoute.DataTokens.Add("namespaces", new string[] { controllersNamespace });

            routes.Add(routeName, newRoute);
        }
    }   
}
