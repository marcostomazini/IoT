using ArquitetaWeb.Common.Infra.Repositorios;
using System.Web.Mvc;

namespace ArquitetaWeb.Common.Infra.Mvc.Controllers
{
    public abstract class BaseController<T> : AsyncController where T : global::ArquitetaWeb.Common.Infra.Componentes.Entity
    {
        protected IRepository<T> repositorio;

        public BaseController(IRepository<T> repositorio)
        {
            this.repositorio = repositorio;
        }

    }
}
