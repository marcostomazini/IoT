using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ArquitetaWeb.Common.Infra.Componentes;
using ArquitetaWeb.HealthMeter.Entities.Tabelas.Enum;

namespace ArquitetaWeb.HealthMeter.Entities.Tabelas
{
    public class Configuracao : Entity
    {
        [Display(Name = "Logotipo da Empresa")]
        public string CaminhoLogotipo { get; set; }

        [Display(Name = "Tempo Ocioso da Mesa")]
        public int TempoOcioso { get; set; }

        [Display(Name = "Descrição do Sistema Externo")]
        public string DescricaoSistemaExterno { get; set; }

        [Display(Name = "Visualizar os Itens Por...")]
        public VisualizacaoPedidos TipoVisualizacaoPedidos { get; set; }

        [Display(Name = "Quantidade que Será Visualizados os Pedidos da Mesa")]
        public int QuantidadeVisualizacaoPedidos { get; set; }
    }

}
