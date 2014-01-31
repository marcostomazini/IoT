using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ArquitetaWeb.Common.Infra.Mvc;

namespace ArquitetaWeb.Common.Infra.Mvc
{
    public static class HtmlDisplayFieldExtensions
    {
        #region Format Strings
        private const string DisplayField =
                "<div class=\"{0}\">" +
                "   {1}" +
                "   {2}" +
                "</div>";       
        #endregion

        public static MvcHtmlString DisplayFieldFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            Width width = Width.Three)
        {
            var metadado = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var htmlAttributes = new Dictionary<string, object>();

            if (metadado.Description != null)
                htmlAttributes["title"] = metadado.Description;

            var span = width.ToSpanString();

            MvcHtmlString field;
            htmlAttributes["readonly"] = "readonly";
            htmlAttributes["disabled"] = "disabled";

            var label = htmlHelper.LabelFor(expression);

            var htmlAttributesHidden = new Dictionary<string, object>();
            
            var hidden = htmlHelper.HiddenFor(expression, htmlAttributesHidden);

            field = htmlHelper.TextBoxFor(expression, htmlAttributes);

            return MvcHtmlString.Create(string.Format(DisplayField,
                span,
                label.ToHtmlString(),
                hidden.ToHtmlString() + field.ToHtmlString()));
        }
    }
}
