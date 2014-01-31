using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArquitetaWeb.HealthMeter.Entities.Tabelas.Enum
{
    public enum VisualizacaoPedidos
    {
        //[Description("Desabilitado")]
        Desabilitado = 0,
        //[Description("Mais Pedidos do Estabelecimento")]
        MaisPedidos = 1,
        //[Description("Mais Pedidos na Mesa")]
        MaisPedidosMesa = 2,
        //[Description("Últimos Pedidos")]
        RecentesMesa = 3
    }

}
