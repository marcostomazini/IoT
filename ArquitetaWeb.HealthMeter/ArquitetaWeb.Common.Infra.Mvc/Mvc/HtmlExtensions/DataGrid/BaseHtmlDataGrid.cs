using ArquitetaWeb.Common.Infra.MvcHtmlExtensions.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Mvc.Html;
using ArquitetaWeb.Common.Infra.Mvc;


namespace ArquitetaWeb.Common.Infra.MvcHtmlExtensions
{
    internal abstract class BaseHtmlDataGrid<TViewModel, TModel> : IHtmlDataGrid<TViewModel, TModel> where TModel : class
    {
        protected string titulo;
        protected string id = "DataGrid";
        protected bool editable;
        protected readonly HtmlHelper<TViewModel> htmlHelper;
        protected readonly UrlHelper urlHelper;
        protected readonly IEnumerable<TModel> enumeracao;
        protected StringBuilder columnsModel;
        protected StringBuilder columnsHeader;

        public BaseHtmlDataGrid(HtmlHelper<TViewModel> htmlHelper, IEnumerable<TModel> enumeracao, bool editable)
        {
            this.htmlHelper = htmlHelper;
            this.urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            this.enumeracao = enumeracao;
            this.editable = editable;

            this.columnsModel = new StringBuilder();
            this.columnsHeader = new StringBuilder();
        }

        public IHtmlDataGrid<TViewModel, TModel> Title(string title)
        {
            this.titulo = title;
            return this;
        }

        public IHtmlDataGrid<TViewModel, TModel> Id(string id)
        {
            this.id = id;
            return this;
        }

        public IHtmlDataGrid<TViewModel, TModel> AddColumn<TProperty>(Expression<Func<TModel, TProperty>> expression, bool editable = false, bool hidden = false, string columnName = "")
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            var attributes = expression.GetMemberInfo().GetCustomAttributes(true).OfType<Attribute>();
            var displayAttribute = attributes.OfType<DisplayAttribute>().FirstOrDefault();

            if (propertyName.ToUpper() == "ID") // Id sempre será hidden
                hidden = true;

            if (!this.editable) editable = false; // se a grid não for editavel, forca os campos a não ser editaveis
            else 
                if (propertyName.ToUpper() == "ID") editable = true; // se a grid for editavel o Id é obrigado a ser editavel

            //var metadadoa =  ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            //if (metadado.Description != null)
            //    metadado.Description;

            if (string.IsNullOrEmpty(columnName))
                columnName = (displayAttribute == null ? propertyName : displayAttribute.Name);

            this.columnsHeader.AppendLine("'" + columnName + "',");
            this.columnsModel.AppendLine("{");
            this.columnsModel.AppendLine("name: '" + propertyName + "', index: '" + propertyName + "', width: 60, ");
            this.columnsModel.AppendLine(string.Format(" sorttype: \"string\", editable: {0}, hidden: {1},", editable.ToString().ToLower(), hidden.ToString().ToLower()));
            this.columnsModel.AppendLine("},");

            return this;
        }

        private string ScriptRender()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">                                                                                                    ");

            Populate(sb);

            sb.AppendLine("jQuery(function ($) {                                                                                                                ");
            sb.AppendLine("    var grid_selector = \"#" + id + "_Table\";                                                                                       ");
            sb.AppendLine("    var pager_selector = \"#" + id + "_Pager\";                                                                                        ");
            sb.AppendLine("                                                                                                                                     ");
            sb.AppendLine("    jQuery(grid_selector).jqGrid({                                                                                                   ");
            sb.AppendLine("        //direction: \"rtl\",                                                                                                        ");
            sb.AppendLine("        data: grid_data,                                                                                                             ");
            sb.AppendLine("        datatype: \"local\",                                                                                                         ");
            sb.AppendLine("        height: \"100%\",                                                                                                            ");

            sb.AppendLine("        colNames: [");
            if ("" == "") sb.AppendLine("' ',");

            sb.AppendLine(columnsHeader.ToString()); //'ID', 'Descrição', 'Data de Cadastro'

            sb.AppendLine("],                                                                      ");

            sb.AppendLine("        colModel: [                                                                                                                  ");

            CreateActions(sb);
            sb.AppendLine(columnsModel.ToString());

            sb.AppendLine("        ],                                                                                                                           ");
            sb.AppendLine("                                                                                                                                     ");
            sb.AppendLine("        viewrecords: true,                                                                                                           ");
            sb.AppendLine("        rowNum: 10,                                                                                                                  ");
            sb.AppendLine("        rowList: [10, 20, 30],                                                                                                       ");
            sb.AppendLine("        pager: pager_selector,                                                                                                       ");
            sb.AppendLine("        altRows: true,                                                                                                               ");
            sb.AppendLine("                                                                                                                                     ");
            sb.AppendLine("        multiselect: false,                                                                                                          ");
            sb.AppendLine("        multiboxonly: true,                                                                                                          ");
            sb.AppendLine("                                                                                                                                     ");
            sb.AppendLine("        loadComplete: function () {                                                                                                  ");
            sb.AppendLine("            var table = this;                                                                                                        ");
            sb.AppendLine("            setTimeout(function () {                                                                                                 ");
            sb.AppendLine("                updateActionIcons(table);                                                                                            ");
            sb.AppendLine("                updatePagerIcons(table);                                                                                             ");
            sb.AppendLine("                enableTooltips(table);                                                                                               ");
            sb.AppendLine("            }, 0);                                                                                                                   ");
            sb.AppendLine("        },                                                                                                                           ");
            sb.AppendLine("                                                                                                                                     ");
            sb.AppendLine("        beforeSelectRow: function (rowid, e) {                                                                                       ");
            if (!this.editable)
            {
                sb.AppendLine("            if ($(e.target).hasClass(\"icon-pencil blue\")) {                                                                    ");
                sb.AppendLine("                editReg(rowid);                                                                                                  ");
                sb.AppendLine("            }                                                                                                                    ");
            }
            sb.AppendLine("            if ($(e.target).hasClass(\"icon-trash red\")) {                                                                          ");
            sb.AppendLine("                deleteReg(rowid);                                                                                                    ");
            sb.AppendLine("            }                                                                                                                        ");
            sb.AppendLine("            return true;                                                                                                             ");
            sb.AppendLine("        },                                                                                                                           ");
            sb.AppendLine("                                                                                                                                     ");
            sb.AppendLine("        editurl: '" + urlHelper.Action("EditarJson") + "',                                                                           ");
            sb.AppendLine("        caption: \"" + this.titulo + "\",                                                                                            ");
            sb.AppendLine("                                                                                                                                     ");
            sb.AppendLine("        autowidth: true                                                                                                              ");
            sb.AppendLine("    });                                                                                                                              ");
            sb.AppendLine("                                                                                                                                     ");
            sb.AppendLine("    //navButtons                                                                                                                     ");
            sb.AppendLine("    jQuery(grid_selector).jqGrid('navGrid', pager_selector,                                                                          ");
            sb.AppendLine("        { 	//navbar options                                                                                                        ");
            sb.AppendLine("            edit: false,                                                                                                             ");
            sb.AppendLine("            editicon: 'icon-pencil blue',                                                                                            ");
            sb.AppendLine("            add: false,                                                                                                              ");
            sb.AppendLine("            addicon: 'icon-plus-sign purple',                                                                                        ");
            sb.AppendLine("            del: false,                                                                                                              ");
            sb.AppendLine("            delicon: 'icon-trash red',                                                                                               ");
            sb.AppendLine("            search: true,                                                                                                            ");
            sb.AppendLine("            searchicon: 'icon-search orange',                                                                                        ");
            sb.AppendLine("            refresh: true,                                                                                                           ");
            sb.AppendLine("            refreshicon: 'icon-refresh green',                                                                                       ");
            sb.AppendLine("            view: false,                                                                                                             ");
            sb.AppendLine("            viewicon: 'icon-zoom-in grey',                                                                                           ");
            sb.AppendLine("        }                                                                                                                            ");
            sb.AppendLine("    )                                                                                                                                ");
            sb.AppendLine("                                                                                                                                     ");
            sb.AppendLine("    $(\"#jqgh_" + id + "_Table_myaction\").html(\"Inserir\");                                                                        ");
            sb.AppendLine("    $(\"#jqgh_" + id + "_Table_myaction\").on('click', '', function (e) {                                                            ");
            sb.AppendLine("        document.location = '" + urlHelper.Action("Inserir") + "'                                                                    ");
            sb.AppendLine("    });                                                                                                                              ");
            sb.AppendLine("                                                                                                                                     ");
            sb.AppendLine("    deleteReg = function (selectedRowId) {                                                                                           ");
            sb.AppendLine("        var id = jQuery(grid_selector).jqGrid('getCell', selectedRowId, 'Id');                                                       ");
            sb.AppendLine("        $(\"#dData\").on('click', '', function (e) {                                                                                 ");
            sb.AppendLine("            document.location = '" + urlHelper.Action("Excluir") + "/' + id;                                                         ");
            sb.AppendLine("        });                                                                                                                          ");
            sb.AppendLine("    }                                                                                                                                ");
            sb.AppendLine("                                                                                                                                     ");
            sb.AppendLine("//jQuery(grid_selector).jqGrid('sortGrid', 'Id', true, 'asc').trigger(\"reloadGrid\");                                               ");

            if (!this.editable)
            {
                sb.AppendLine("    // se habilitar edicao em nova pagina devera as colunas não ser editaveis                                                    ");
                sb.AppendLine("    $(\"div[id^='jEditButton']\").attr(\"onclick\", \"\");                                                                       ");
                sb.AppendLine("    editReg = function (selectedRowId) {                                                                                         ");
                sb.AppendLine("        var id = jQuery(grid_selector).jqGrid('getCell', selectedRowId, 'Id');                                                   ");                
                sb.AppendLine("        document.location = '" + urlHelper.Action("Editar") + "/' + id;                                                          ");
                sb.AppendLine("    }                                                                                                                            ");
            }

            sb.AppendLine("});                                                                                                                                  ");
            sb.AppendLine("</script>                                                                                                                            ");

            return sb.ToString();
        }

        private void Populate(StringBuilder sb)
        {
            var enumeracaoJson = new JavaScriptSerializer().Serialize(this.enumeracao);
            sb.AppendLine(string.Format("var grid_data = {0}", enumeracaoJson));
        }

        private static void CreateActions(StringBuilder sb)
        {
            sb.AppendLine("            {                                                                                                                        ");
            sb.AppendLine("                name: 'myaction', index: '', width: 80, fixed: true, sortable: false, resize: false,                                 ");
            sb.AppendLine("                formatter: 'actions',                                                                                                ");
            sb.AppendLine("                formatoptions: {                                                                                                     ");
            sb.AppendLine("                    keys: false,                                                                                                     ");
            sb.AppendLine("                    delOptions: { recreateForm: true, beforeShowForm: beforeDeleteCallback },                                        ");
            sb.AppendLine("                    //editformbutton:true, editOptions:{recreateForm: true, beforeShowForm:beforeEditCallback}                       ");
            sb.AppendLine("                }                                                                                                                    ");
            sb.AppendLine("            },                                                                                                                       ");
        }

        public void Render()
        {
            TagBuilder gridTable = new TagBuilder("table");
            gridTable.GenerateId(id + "_Table");
            gridTable.ToString(TagRenderMode.EndTag);

            TagBuilder gridPager = new TagBuilder("div");
            gridPager.GenerateId(id + "_Pager");
            gridPager.ToString(TagRenderMode.EndTag);

            htmlHelper.ViewContext.Writer.Write(gridTable.ToString() + gridPager.ToString() + ScriptRender());
        }
    }
}
