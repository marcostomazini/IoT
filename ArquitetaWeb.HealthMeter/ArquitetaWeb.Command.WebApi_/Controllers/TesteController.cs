using ArquitetaWeb.Command.Entities.Tabelas;
using ArquitetaWeb.Command.Infra.Api.Controllers;
using ArquitetaWeb.Common.Infra.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArquitetaWeb.Command.WebApi.Areas.Mesas.Controllers
{
    public class TesteController : BaseApiController<Garcom>
    {
        public TesteController(IRepository<Garcom> repositorio)
            : base(repositorio)
        {
            this.repositorio = repositorio;
        }

        [Route("Teste")] // http://localhost:8080/Api/teste
        [HttpGet]
        public IEnumerable<Garcom> GetSituacaoMesas()
        {
            var teste = repositorio.Consulta();
            return teste;
        }
    }
}