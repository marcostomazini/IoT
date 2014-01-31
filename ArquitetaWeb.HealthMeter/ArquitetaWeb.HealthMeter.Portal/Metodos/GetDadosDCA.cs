using ArquitetaWeb.HealthMeter.Portal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace ArquitetaWeb.Command.Portal.Metodos
{
    public class GetDadosDCA
    {
        const string url = "http://dca.telefonicabeta.com/m2m/v2/services/jr5x44jp5a9o/assets/jr5x44jp5a9o/data?sortBy=!samplingTime&limit=30&offset=0&attribute={0}";

        public IEnumerable<DadosDCA> DadosDCA(string tipo)
        {
            Uri uri = null;
            string consulta = string.Empty;
            switch (tipo)
            {
                case "H":
                    consulta = "HUMIDITY";
                    uri = new Uri(string.Format(url, "relativeHumidity"));
                    break;
                case "T":
                    consulta = "TEMPERATURE";
                    uri = new Uri(string.Format(url, "temperature"));
                    break;
                case "R":
                    consulta = "SOUND";
                    uri = new Uri(string.Format(url, "sound"));
                    break;
                case "L":
                    consulta = "LUMINOUS";
                    uri = new Uri(string.Format(url, "luminousIntensity"));
                    break;
                default:
                    break;
            }
            WebRequest myWebRequest = WebRequest.Create(uri);
            WebResponse myWebResponse = myWebRequest.GetResponse();

            using (var response = (HttpWebResponse)myWebRequest.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var objText = reader.ReadToEnd();
                    RootObject dados = (RootObject)js.Deserialize(objText, typeof(RootObject));
                    return dados.data.Where(s => s.ms.p.ToUpper().Contains(consulta))
                                                .Select(x => new DadosDCA()
                                                {
                                                    DataHoraRecebido = x.st,
                                                    Tipo = x.ms.p,
                                                    Valor = x.ms.v
                                                });
                }
            }
        }

        private IndexPortalViewModel GetListas()
        {
            var dados = new DadosModels()
            {
                UmidadeRelativa = DadosDCA("H").ToList(),
                Ruido = DadosDCA("R").ToList(),
                Temperatura = DadosDCA("T").ToList(),
                Luminosidade = DadosDCA("L").ToList()
            };

            var lum = (dados.Luminosidade.Take(5).Average(x => x.Valor));
            return new IndexPortalViewModel()
            {
                DadosDCA = new DadosDCAStr()
                {
                    Ruido = (dados.Ruido.Take(5).Average(x => x.Valor) / 10).ToString("0"),
                    Temperatura = dados.Temperatura.Average(x => x.Valor).ToString("0"),
                    Umidade = dados.UmidadeRelativa.Average(x => x.Valor).ToString("0"),
                    Luminosidade = (lum > 650) ? "Alta" : (lum < 150) ? "Baixa" : "Média"
                }
            };
        }

        public IndexPortalViewModel Executar()
        {
            return GetListas();
        }
    }
}