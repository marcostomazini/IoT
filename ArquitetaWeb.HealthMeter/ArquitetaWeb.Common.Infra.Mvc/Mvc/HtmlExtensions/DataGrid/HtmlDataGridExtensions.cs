using System.Web.Mvc;
using System;
using System.Web.Routing;
using ArquitetaWeb.Common.Infra.Mvc.HtmlElements;
using System.Collections.Generic;
using ArquitetaWeb.Common.Infra.Mvc.Rotas;
using System.Text;
using ArquitetaWeb.Common.Infra.MvcHtmlExtensions.Interface;
using ArquitetaWeb.Common.Infra.Componentes;
using System.Linq.Expressions;
using ArquitetaWeb.Common.Infra.MvcHtmlExtensions;

namespace ArquitetaWeb.Common.Infra.Mvc
{
    public static class HtmlDataGridExtensions
    {
        public static IHtmlDataGrid<TViewModel, TModel> DataGridFor<TViewModel, TModel>(this HtmlHelper<TViewModel> htmlHelper,
            IEnumerable<TModel> enumeracao, bool editable = false) where TModel : Entity
        {
            return new HtmlDataGrid<TViewModel, TModel>(htmlHelper, enumeracao, editable);
        }
    }
}
