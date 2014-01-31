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
        public IDbSet<Usuario> Usuario { get; set; }
        public IDbSet<Configuracao> Configuracao { get; set; }
        public IDbSet<Device> Equipamento { get; set; }
        public IDbSet<Garcom> Garcom { get; set; }
        public IDbSet<Mesa> Mesa { get; set; }
        public IDbSet<Pedido> Pedido { get; set; }
        public IDbSet<Produto> Produto { get; set; }
    }
}
