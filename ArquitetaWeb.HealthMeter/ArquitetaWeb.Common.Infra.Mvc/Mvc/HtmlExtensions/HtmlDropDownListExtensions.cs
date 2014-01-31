using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using System.ComponentModel;

namespace ArquitetaWeb.Common.Infra.Mvc
{
    public static class HtmlDropDownListExtensions
    {
        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };

        public static MvcHtmlString DropDownListEditorFor<TModel, T>(this HtmlHelper<TModel> htmlHelper,
                                                                     Expression<Func<TModel, T>> propriedade,
                                                                     IEnumerable<SelectListItem> lista=null,
                                                                     Width width = Width.Two,
                                                                     String options="", String optionsText="", String optionsValue="")
        {
            var htmlAttributes = new Dictionary<string, object>();

            var binding = String.Format("value: {0}",
                                        ExpressionHelper.GetExpressionText(propriedade));

            htmlAttributes.Add("data-bind",binding);

            var div = new TagBuilder("div");
            div.AddCssClass(width.ToSpanString());
            div.InnerHtml = htmlHelper.LabelFor(propriedade).ToHtmlString();

            div.InnerHtml += htmlHelper.DropDownListFor(propriedade, lista, htmlAttributes).ToHtmlString();
            return MvcHtmlString.Create(div.ToString());    
        }


    }
}
