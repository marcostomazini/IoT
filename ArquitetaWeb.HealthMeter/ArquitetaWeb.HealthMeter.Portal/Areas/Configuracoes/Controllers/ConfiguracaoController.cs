using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArquitetaWeb.Common.Infra.Mvc.Controllers;
using ArquitetaWeb.Common.Infra.Repositorios;
using ArquitetaWeb.HealthMeter.Entities.Tabelas;
using System.Web.Script.Serialization;
using ArquitetaWeb.HealthMeter.Portal.Configuracoes.Configuracoes.Models;

namespace ArquitetaWeb.HealthMeter.Portal.Configuracoes.Controllers
{
    public class ConfiguracaoController : BaseController<Configuracao>
    {
        public ConfiguracaoController(IRepository<Configuracao> repositorio)
            : base(repositorio)
        {
            this.repositorio = repositorio;
            ViewBag.Title = "Configuração em Geral";
        }

        #region >>> Index

        public ActionResult Index()
        {
            var lista = repositorio.Consulta().ToList();
            var viewModel = new IndexConfiguracaoViewModel()
            {
                Resultado = lista
            };

            return View(viewModel);
        }

        public ActionResult Index2()
        {
            return Index();
        }


        #endregion

        #region >>> Detalhes

        public ActionResult Detalhes(long id)
        {
            var entidade = repositorio.Retorna(id);
            var viewModel = new DetalhesConfiguracaoViewModel()
            {
                Entidade = entidade
            };
            return View(viewModel);
        }

        #endregion

        #region >>> Editar

        public ActionResult Editar(long id)
        {
            var entidade = repositorio.Retorna(id);
            var viewModel = new EditarConfiguracaoViewModel()
            {
                Entidade = entidade
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Editar(int id, EditarConfiguracaoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                repositorio.Update(viewModel.Entidade);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        #endregion

        #region >>> Inserir

        public ActionResult Inserir()
        {
            var viewModel = new InserirConfiguracaoViewModel()
            {
                Entidade = new Configuracao()
                {
                    //DataCadastro = DateTime.Now
                }
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Inserir(InserirConfiguracaoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                repositorio.Insert(viewModel.Entidade);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        #endregion

        #region >>> Excluir
        [HttpGet, ActionName("Excluir")]
        public ActionResult ExcluirConfirmed(long id)
        {
            var entidade = repositorio.Retorna(id);
            repositorio.Delete(entidade);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
