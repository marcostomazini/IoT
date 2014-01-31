using ArquitetaWeb.Common.Infra.Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ArquitetaWeb.HealthMeter.Entities.Tabelas
{
    public class Pedido : Entity
    {
        [Required]
        [Display(Name = "Device")]
        [ForeignKey("Equipamento")]
        public long EquipamentoId { get; set; }

        public Device Equipamento { get; set; }

        [Required]
        [ForeignKey("Garcom")]
        public long GarcomId { get; set; }

        public Garcom Garcom { get; set; }

        [Required]
        [ForeignKey("Mesa")]
        public long MesaId { get; set; }

        public Mesa Mesa { get; set; }

        [Required]
        [Display(Name = "Quantidade do Produto")]
        public decimal Quantidade { get; set; }

        [Required]
        [ForeignKey("Produto")]
        public long ProdutoId { get; set; }

        public Produto Produto { get; set; }

        [Required]
        [Display(Name = "Data e Hora do Pedido")]
        public DateTime DataHoraPedido { get; set; }

        [Display(Name = "Observação")]
        [StringLength(100)]
        public string Observacao { get; set; }
    }

}
