using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace ArquitetaWeb.Command.Portal.Metodos
{
    public class Regras
    {
        public Retorno Executar()
        {
            var dadosDCA = new GetDadosDCA();
            var dados = dadosDCA.DadosDCA("R");
            var ruido = (dados.Take(5).Average(x => x.Valor) / 10);

            if (ruido >= 67)
                return new Retorno(true, "Sucesso"); // fechar janela
            return new Retorno(false, "Sem Sucesso");
        }
    }
}