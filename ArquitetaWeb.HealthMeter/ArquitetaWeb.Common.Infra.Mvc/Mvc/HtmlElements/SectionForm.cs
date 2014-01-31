using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ArquitetaWeb.Common.Infra.Mvc.HtmlElements
{
    public class SectionForm : MvcForm
    {
        private readonly HtmlHelper helper;
        private readonly ViewContext viewContext;
        private readonly Boolean gerarBotoes;
        private readonly String labelBotao;
        private readonly FormType formType;

        public SectionForm(HtmlHelper helper, bool gerarBotoes, string labelBotao, FormType formType)
            : base(helper.ViewContext)
        {
            this.helper = helper;
            this.viewContext = helper.ViewContext;
            this.gerarBotoes = gerarBotoes;
            this.labelBotao = labelBotao;
            this.formType = formType;
        }

        protected override void Dispose(bool disposing)
        {
            switch (formType)
            {
                case FormType.Filter:
                    EndSectionFormFilter();
                    break;
                case FormType.Generic:
                    EndSectionForm();
                    break;
                default:
                    EndSectionForm();
                    break;
            }
            base.Dispose(true);
        }

        private void EndSectionForm()
        {
            viewContext.Writer.Write("</div>");
            if (gerarBotoes)
            {
                String tipoView = viewContext.RouteData.Values["action"].ToString().ToUpper();
                Int64 Id = Convert.ToInt64(helper.ViewContext.RouteData.Values["id"]);
                if (labelBotao == null)
                {
                    switch (tipoView)
                    {
                        case "EDITAR":
                            viewContext.Writer.Write(String.Format(TemplatesForm.SubmitCancelarTemplate,
                                                                    "SALVAR"));
                            break;
                        case "INSERIR":
                            viewContext.Writer.Write(String.Format(TemplatesForm.SubmitCancelarTemplate,
                                                                    tipoView));
                            break;
                        //case "DETALHES":
                        //    viewContext.Writer.Write(String.Format(TemplatesForm.DetalhesRodape,
                        //                                           helper.ActionLinkIndex("Listar", new { @class = "button blackB" }).ToHtmlString(),
                        //                                           helper.ActionLinkEditar(Id, "Editar"),
                        //                                           helper.ActionLinkExcluir(Id, "Excluir"),
                        //                                           helper.ActionLinkInserir("Inserir")));
                        //    break;
                        case "INDEX":
                            viewContext.Writer.Write(String.Format(TemplatesForm.SubmitPesquisarTemplate, "Pesquisar"));

                            break;

                        default:
                            viewContext.Writer.Write(String.Format(TemplatesForm.SubmitPesquisarTemplate,
                                                     tipoView.ToLower()));

                            break;
                    }
                }
                else
                {
                    viewContext.Writer.Write(String.Format(TemplatesForm.SubmitPesquisarTemplate, labelBotao));
                }
            }
            viewContext.Writer.Write("</div>");
        }

        private void EndSectionFormFilter()
        {
            viewContext.Writer.Write("</div>");
            if (gerarBotoes)
            {
                Int64 Id = Convert.ToInt64(helper.ViewContext.RouteData.Values["id"]);

                viewContext.Writer.Write(String.Format(TemplatesForm.ActionsFilterTemplate));

            }
            //viewContext.Writer.Write("</div>");
        }
    }

    internal static class TemplatesForm
    {
        public static string DetalhesRodape
        {
            get
            {
                var tag = new TagBuilder("div");
                tag.AddCssClass("submit");
                tag.InnerHtml += "{0}{1}{2}{3}";

                return tag.ToString();
            }
        }

        public static string ActionsFilterTemplate
        {
            get
            {
                var tag = new TagBuilder("div");
                tag.AddCssClass("submit");

                var cleanFilter = new TagBuilder("input");

                cleanFilter.MergeAttribute("type", "button");
                cleanFilter.MergeAttribute("id", "limparFiltroButton");
                cleanFilter.MergeAttribute("value", "Limpar filtro");
                cleanFilter.AddCssClass("basic");

                tag.InnerHtml += cleanFilter.ToString(TagRenderMode.SelfClosing);

                var filter = new TagBuilder("input");

                filter.MergeAttribute("type", "submit");
                filter.MergeAttribute("id", "pesquisarButton");
                filter.MergeAttribute("value", "Pesquisar");
                filter.AddCssClass("basic");

                tag.InnerHtml += filter.ToString(TagRenderMode.SelfClosing);

                return tag.ToString();
            }
        }

        public static string SubmitPesquisarTemplate
        {
            get
            {
                //var tag = new TagBuilder("div");
                //tag.AddCssClass("submit");

                //var submit = new TagBuilder("input");

                //submit.MergeAttribute("type", "submit");
                //submit.MergeAttribute("value", "{0}");
                //submit.AddCssClass("basic");

                //tag.InnerHtml += submit.ToString(TagRenderMode.SelfClosing);

                //return tag.ToString();

                StringBuilder submitHtml = new StringBuilder();

                submitHtml.AppendLine("<div class=\"clearfix form-actions\">");
                submitHtml.AppendLine("    <div class=\"col-md-offset-3 col-md-9\">");
                submitHtml.AppendLine("        <button class=\"btn btn-info\" type=\"submit\">");
                submitHtml.AppendLine("            <i class=\"icon-ok bigger-110\"></i>");
                submitHtml.AppendLine("            {0}");
                submitHtml.AppendLine("        </button>");
                submitHtml.AppendLine("");
                submitHtml.AppendLine("        &nbsp; &nbsp; &nbsp;");
                submitHtml.AppendLine("        <button class=\"btn\" type=\"reset\">");
                submitHtml.AppendLine("            <i class=\"icon-undo bigger-110\"></i>");
                submitHtml.AppendLine("            Reset");
                submitHtml.AppendLine("        </button>");
                submitHtml.AppendLine("    </div>");
                submitHtml.AppendLine("</div>");

                return submitHtml.ToString();
            }
        }

        public static string SubmitCancelarTemplate
        {
            get
            {
                //var tag = new TagBuilder("div");
                //tag.AddCssClass("clearfix form-actions");

                //var divForm = new TagBuilder("div");
                //divForm.AddCssClass("clearfix form-actions");

                //tag.InnerHtml += divForm.ToString(TagRenderMode.SelfClosing);

                //var submit = new TagBuilder("button");
                //submit.AddCssClass("btn btn-info");
                //submit.MergeAttribute("type", "button");
                //submit.MergeAttribute("value", "{0}");
                //submit.MergeAttribute("id", "btn_submit");
                //submit.AddCssClass("blueB");
                //tag.InnerHtml += submit.ToString(TagRenderMode.SelfClosing);

                //return tag.ToString();

                StringBuilder submitHtml = new StringBuilder();

                submitHtml.AppendLine("<div class=\"clearfix form-actions\">");
                submitHtml.AppendLine("    <div class=\"col-md-offset-3 col-md-9\">");
                submitHtml.AppendLine("        <button class=\"btn btn-info\" type=\"button\">");
                submitHtml.AppendLine("            <i class=\"icon-ok bigger-110\"></i>");
                submitHtml.AppendLine("            {0}");
                submitHtml.AppendLine("        </button>");
                submitHtml.AppendLine("");
                submitHtml.AppendLine("        &nbsp; &nbsp; &nbsp;");
                submitHtml.AppendLine("        <button class=\"btn\" type=\"reset\">");
                submitHtml.AppendLine("            <i class=\"icon-undo bigger-110\"></i>");
                submitHtml.AppendLine("            Reset");
                submitHtml.AppendLine("        </button>");
                submitHtml.AppendLine("    </div>");
                submitHtml.AppendLine("</div>");

                return submitHtml.ToString();

            }
        }
    }

    public enum FormType
    {
        Filter = 0,
        Detail = 1,
        Insert = 2,
        Edit = 3,
        Generic = 4
    }
}
