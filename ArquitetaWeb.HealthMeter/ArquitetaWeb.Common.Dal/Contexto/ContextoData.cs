using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ArquitetaWeb.HealthMeter.Entities.Tabelas;

namespace ArquitetaWeb.Common.Data.Contexto
{
    public partial class ContextoAcesso
    {
        public IDbSet<Configuracao> Configuracao { get; set; }
    }
}
