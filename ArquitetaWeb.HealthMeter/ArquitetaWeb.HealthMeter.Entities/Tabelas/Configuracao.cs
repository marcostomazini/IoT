using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ArquitetaWeb.Common.Infra.Componentes;

namespace ArquitetaWeb.HealthMeter.Entities.Tabelas
{
    public class Configuracao : Entity
    {
        [Display(Name = "Temperatura Minima")]
        public float MinTemperatura { get; set; }

        [Display(Name = "Temperatura Máxima")]
        public float MaxTemperatura { get; set; }

        [Display(Name = "Umidade Minima")]
        public float MinUmidade { get; set; }

        [Display(Name = "Umidade Máxima")]
        public float MaxUmidade { get; set; }

        [Display(Name = "Tempo Para Atualização")]
        public int TempoAtualizacao { get; set; }
    }

}
