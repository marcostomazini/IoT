using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ArquitetaWeb.Common.Infra.MvcHtmlExtensions.Interface
{
    public interface IHtmlDataGrid<TViewModel, TModel>
    {
        IHtmlDataGrid<TViewModel, TModel> Title(string title);

        IHtmlDataGrid<TViewModel, TModel> Id(string id);

        IHtmlDataGrid<TViewModel, TModel> AddColumn<TProperty>(Expression<Func<TModel, TProperty>> expression,  bool editable = false, bool hidden = false, string columnName = "");

        void Render();
    }
}
