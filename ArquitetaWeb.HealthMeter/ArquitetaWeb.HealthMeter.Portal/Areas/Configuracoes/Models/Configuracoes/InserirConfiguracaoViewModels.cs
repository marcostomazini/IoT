using ArquitetaWeb.HealthMeter.Entities.Tabelas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace ArquitetaWeb.HealthMeter.Portal.Configuracoes.Configuracoes.Models
{
    public class InserirConfiguracaoViewModel
    {
        public Configuracao Entidade { get; set; }
    }
}
