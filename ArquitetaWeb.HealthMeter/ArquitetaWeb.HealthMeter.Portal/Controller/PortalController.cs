using ArquitetaWeb.HealthMeter.Entities.Tabelas;
using ArquitetaWeb.Common.Infra.Mvc.Controllers;
using ArquitetaWeb.Common.Infra.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;
using ArquitetaWeb.HealthMeter.Portal.Models;
using ArquitetaWeb.Common.Infra.Componentes;
using ArquitetaWeb.Command.Portal.Metodos;

namespace ArquitetaWeb.HealthMeter.Portal.Controllers
{
    public class PortalController : BaseController<Configuracao>
    {
        public PortalController(IRepository<Configuracao> repositorio)
            : base(repositorio)
        {
            this.repositorio = repositorio;
            ViewBag.Title = "Monitoramento do Bem Estar Comum";
        }

        #region >>> Index

        public ActionResult Index()
        {
            var dadosDCA = new GetDadosDCA();
            var viewModel = dadosDCA.Executar();

            return View(viewModel);
        }

        public JsonResult Regras()
        {
            var regra = new Regras();
            return Json(regra.Executar());
        }

        #endregion

        #region >>>

        public ActionResult FecharJanela()
        {
            return View();
        }

        #endregion

    }
}
