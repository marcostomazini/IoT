using ArquitetaWeb.HealthMeter.Entities.Tabelas.Enum;
using ArquitetaWeb.Common.Infra.Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ArquitetaWeb.HealthMeter.Entities.Tabelas
{
    public class Mesa : Entity
    {
        //[DisplayExterno(Name = "Nome", "Var GLOBAL AKI")] // Para concatenar nome sistema externo
        [Required]
        [Display(Name = "Código")]
        public string CodigoExterno { get; set; }

        [Display(Name = "Número da Mesa")]
        public long NumeroMesa { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(30)]
        public string Descricao { get; set; }

        [Display(Name = "Situação")]
        public SituacaoMesa Situacao { get; set; }

        //[ForeignKey("Tecnologia")]
        //public long AgrupadaId { get; set; }

        //public Mesa Agrupada { get; set; }

        //public IList<Produto> ListaProdutos { get; set; } // esse vai para o modelo

        [Display(Name = "Observação")]
        [StringLength(100)]
        public string Observacao { get; set; }
    }

}
