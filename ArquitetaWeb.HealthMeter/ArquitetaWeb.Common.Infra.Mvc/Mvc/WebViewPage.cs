using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Mvc;

namespace ArquitetaWeb.Common.Infra.Mvc
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public int MyProperty { get; set; }
        public override void ExecutePageHierarchy()
        {
            var caminhoView = this.VirtualPath;
            var javaScript = caminhoView.Replace(".cshtml", ".js");
            var localJavaScript = this.ViewContext.HttpContext.Server.MapPath(javaScript);

            if (File.Exists(localJavaScript))
            {
                WriteLiteral("<script type=\"text/javascript\"> ");
                WriteLiteral(" $(this).ready(function () { ");
                WriteLiteral(File.ReadAllText(localJavaScript));
                WriteLiteral("}); </script>");
            }
            base.ExecutePageHierarchy();            
        }
    }
}
