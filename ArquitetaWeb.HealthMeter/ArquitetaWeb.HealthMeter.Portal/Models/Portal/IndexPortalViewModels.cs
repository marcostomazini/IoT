using ArquitetaWeb.Common.Infra.Componentes;
using ArquitetaWeb.HealthMeter.Entities.Tabelas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace ArquitetaWeb.HealthMeter.Portal.Models
{
    public class IndexPortalViewModel
    {
        public DadosDCAStr DadosDCA { get; set; }
    }

    public class DadosDCAStr
    {
        public string Ruido { get; set; }
        public string Temperatura { get; set; }
        public string Umidade { get; set; }
        public string Luminosidade { get; set; }
    }

    public class DadosModels
    {
        public IEnumerable<DadosDCA> Ruido { get; set; }
        public IEnumerable<DadosDCA> Temperatura { get; set; }
        public IEnumerable<DadosDCA> UmidadeRelativa { get; set; }
        public IEnumerable<DadosDCA> Luminosidade { get; set; }
    }

    public class DadosDCA
    {
        public float Valor { get; set; }
        public string Tipo { get; set; }
        public DateTime DataHoraRecebido { get; set; }
    }

    public class Ms
    {
        public float v { get; set; }
        public string p { get; set; }
        public string u { get; set; }
    }

    public class Datum
    {
        public DateTime st { get; set; }

        public Ms ms { get; set; }
    }

    public class RootObject
    {
        public List<Datum> data { get; set; }
    }
}
