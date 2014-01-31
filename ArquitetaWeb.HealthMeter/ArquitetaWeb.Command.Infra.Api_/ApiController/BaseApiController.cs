using ArquitetaWeb.Common.Infra.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ArquitetaWeb.Command.Infra.Api.Controllers
{
    public abstract class BaseApiController<T> : ApiController where T : global::ArquitetaWeb.Common.Infra.Componentes.Entity
    {
        protected IRepository<T> repositorio;

        public BaseApiController(IRepository<T> repositorio)
        {
            this.repositorio = repositorio;
        }

    }
}
