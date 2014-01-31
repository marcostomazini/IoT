using System.Linq;
using System.Web.Mvc;
using ArquitetaWeb.Common.Infra.Mvc.Controllers;
using ArquitetaWeb.Common.Infra.Repositorios;
using ArquitetaWeb.HealthMeter.Entities.Tabelas;

namespace ArquitetaWeb.HealthMeter.Portal.Configuracoes.Controllers
{
    public class FechaJanelaController : BaseController<Mesa>
    {
        public FechaJanelaController(IRepository<Mesa> repositorio)
            : base(repositorio)
        {
            this.repositorio = repositorio;
            ViewBag.Title = "Fechar Janelas";
        }

        #region >>> Index

        public ActionResult Index()
        {          
            return View();
        }

        #endregion
    }
}
