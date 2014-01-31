using ArquitetaWeb.Command.Entities.Tabelas;
using ArquitetaWeb.Command.Infra.Api.Controllers;
using ArquitetaWeb.Common.Infra.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ArquitetaWeb.Command.WebApi.Areas.Mesas.Controllers
{
    public class MesaController : BaseApiController<Mesa>
    {
        public MesaController(IRepository<Mesa> repositorio)
            : base(repositorio)
        {
            this.repositorio = repositorio;
            //db.SaveChangesAsync
        }

        [HttpGet]
        [Route("SituacaoMesas")]
        public IEnumerable<Mesa> SituacaoDasMesas()
        {
            var entidadeMesa = repositorio.Consulta();
            return entidadeMesa;
        }

        [HttpPost]
        [Route("InserirMesa")]
        public async Task<IHttpActionResult> InserirMesa(Mesa mesa)
        {
            await repositorio.InsertAsync(mesa);
            
            return Ok();
        }

        [HttpPut]
        [Route("AtualizarMesa")]
        //public async Task<IHttpActionResult> AtualizarMesa(Mesa mesa)
        public async Task<IHttpActionResult> AtualizarMesa(long id, ArquitetaWeb.Command.Entities.Tabelas.Enum.SituacaoMesa situacao)
        //public async Task<IHttpActionResult> AtualizarMesa(object teste)
        {
            var entidadeMesa = await repositorio.RetornaAsync(id);
            entidadeMesa.Situacao = situacao;
            await repositorio.UpdateAsync(entidadeMesa);
            return Ok();
        }

        [HttpDelete]
        [Route("DeletarMesa")]
        public async Task<IHttpActionResult> DeletarMesa(long id)
        {
            var mesa = repositorio.Retorna(id);
            await repositorio.DeleteAsync(mesa);
            return Ok(mesa);
        }

    }
}
