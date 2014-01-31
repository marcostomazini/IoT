using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using System.ComponentModel.DataAnnotations;

namespace ArquitetaWeb.Common.Infra.Mvc
{
    public static class HtmlEditorFieldExtensions
    {
        #region Format Strings
        private const string EditorField =
                "<div class=\"{0}\" {3}>" +
                "{1}" +
                "{2}" +
                "</div>";
       
        #endregion

        public static MvcHtmlString EditorFieldFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            Width width = Width.Three,
            Width widthFilhos = Width.Four,
            string eventsBinding = null,
            string clientClickBinding = null,
            string enabledBinding = null,
            string disabledBinding = null,
            string visibleBinding = null,
            string hasFocusBinding = null,
            string attrBinding = null,
            string customValueBinding = null
            )
        {
            var span = width.ToSpanString();

            var metadado = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var member = expression.Body as MemberExpression;
            var label = htmlHelper.LabelFor(expression);
            var visibility = (visibleBinding != null)
                ? String.Format("data-bind=\"visible : {0}\"", visibleBinding)
                : String.Empty;

            var htmlAttributes = new Dictionary<string, object>();
            var stringLength = member.Member.GetCustomAttributes(typeof(StringLengthAttribute), false).FirstOrDefault() as StringLengthAttribute;
            if (stringLength != null)
            {
                htmlAttributes["maxlength"] = stringLength.MaximumLength;
            }
            var valor = metadado.Model != null ? ((TProperty)metadado.Model).ToString() : string.Empty;
            if (metadado.Description != null)
                htmlAttributes["title"] = metadado.Description;

            MvcHtmlString field;

            field = htmlHelper.TextBox(ExpressionHelper.GetExpressionText(expression), valor, htmlAttributes);
            return MvcHtmlString.Create(string.Format(EditorField,
                                                      span,
                                                      label.ToHtmlString(),
                                                      field.ToHtmlString(),
                                                      visibility));
        }

    }
}
