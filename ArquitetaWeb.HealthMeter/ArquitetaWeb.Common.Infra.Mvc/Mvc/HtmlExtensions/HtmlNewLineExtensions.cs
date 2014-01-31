using System.Web.Mvc;

namespace ArquitetaWeb.Common.Infra.Mvc
{
    public static class HtmlNewLineExtensions
    {
        public static MvcHtmlString NewLine(this HtmlHelper helper)
        {
            return NewLine(helper, false);
        }

        public static MvcHtmlString NewLine(this HtmlHelper helper, bool margin)
        {
            var novalinha = new TagBuilder("div");
            novalinha.AddCssClass("col-xs-12");

            if (!margin)
                novalinha.AddCssClass("nomargin");

            return new MvcHtmlString(novalinha.ToString(TagRenderMode.Normal));
        }
    }
}
