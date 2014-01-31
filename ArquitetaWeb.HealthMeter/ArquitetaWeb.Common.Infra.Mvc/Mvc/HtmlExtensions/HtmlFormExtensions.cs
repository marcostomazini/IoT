using System.Web.Mvc;
using System;
using System.Web.Routing;
using ArquitetaWeb.Common.Infra.Mvc.HtmlElements;
using System.Collections.Generic;
using ArquitetaWeb.Common.Infra.Mvc.Rotas;
using System.Text;

namespace ArquitetaWeb.Common.Infra.Mvc
{    
    public static class HtmlFormExtensions
    {

        public static SectionForm Form(this HtmlHelper htmlHelper)
        {
            return Form(htmlHelper, string.Empty, string.Empty);
        }

        public static SectionForm Form(this HtmlHelper htmlHelper, String titleMin, String title, Boolean gerarBotoes = true, String labelBotao = null)
        {
            string formAction = htmlHelper.ViewContext.HttpContext.Request.RawUrl;
            return FormHelper(htmlHelper, formAction, FormMethod.Post, new RouteValueDictionary(), titleMin, title, gerarBotoes, labelBotao);
        }

        private static SectionForm FormHelper(this HtmlHelper htmlHelper, string postUrl, FormMethod method, IDictionary<string, object> htmlAttributes, string titleMin, string title, Boolean gerarBotoes, String labelBotao, FormType formType = FormType.Generic)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"page-header\">");
            html.AppendLine(" <h1>");
            html.AppendLine("     {0}");
            html.AppendLine("     <small>");
            html.AppendLine("         <i class=\"icon-double-angle-right\"></i>");
            html.AppendLine("             {1}");
            html.AppendLine("     </small>");
            html.AppendLine(" </h1>");
            html.AppendLine("</div><!-- /.page-header -->");
            htmlHelper.ViewContext.Writer.Write(String.Format(html.ToString(), titleMin, title));

            var builderSection = new TagBuilder("div");
            builderSection.AddCssClass("col-xs-12");

            var tagBuilder = new TagBuilder("form");
            tagBuilder.MergeAttributes(htmlAttributes);

            tagBuilder.MergeAttribute("action", postUrl);

            tagBuilder.MergeAttribute("method", HtmlHelper.GetFormMethodString(method), true);

            tagBuilder.MergeAttribute("id", "formPrincipal");
            tagBuilder.MergeAttribute("name", "formPrincipal");
            tagBuilder.AddCssClass("form-horizontal");

            htmlHelper.ViewContext.Writer.Write(builderSection.ToString(TagRenderMode.StartTag));

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));

            htmlHelper.ViewContext.Writer.Write("<div class=\"form-group\">");

            var theForm = new SectionForm(htmlHelper, gerarBotoes, labelBotao, formType);

            return theForm;
        }
    }
}
