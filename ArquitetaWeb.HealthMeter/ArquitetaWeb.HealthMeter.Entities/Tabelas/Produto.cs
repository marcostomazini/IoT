using ArquitetaWeb.Common.Infra.Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ArquitetaWeb.HealthMeter.Entities.Tabelas
{
    public class Produto : Entity
    {
        //[DisplayExterno(Name = "Nome", "Var GLOBAL AKI")] // Para concatenar nome sistema externo
        [Required]
        [Display(Name = "Código")]
        public string CodigoExterno { get; set; }

        [Display(Name = "Código")]
        public long Codigo { get; set; }

        [Display(Name = "Descrição")]
        public string DescricaoExterna { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(30)]
        public string Descricao { get; set; }

        [Display(Name = "Valor do Item")]
        public Double Valor { get; set; }

        [Display(Name = "Imagem")]
        public string CaminhoImagem { get; set; }
    }
}
