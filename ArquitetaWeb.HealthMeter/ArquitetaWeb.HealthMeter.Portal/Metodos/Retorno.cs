using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArquitetaWeb.Command.Portal.Metodos
{
    public class Retorno
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }

        public Retorno(bool Sucesso, string Mensagem)
        {
            this.Sucesso = Sucesso;
            this.Mensagem = Mensagem;
        }
    }
}