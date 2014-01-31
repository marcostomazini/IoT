using ArquitetaWeb.Common.Infra.Componentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace ArquitetaWeb.Common.Infra.MvcHtmlExtensions
{
    internal sealed class HtmlDataGrid<TViewModel, TModel> : BaseHtmlDataGrid<TViewModel, TModel> where TModel : Entity
    {
        public HtmlDataGrid(HtmlHelper<TViewModel> htmlHelper, IEnumerable<TModel> enumeracao, bool editable)
            : base(htmlHelper, enumeracao, editable)
        {
        }
    }
}
