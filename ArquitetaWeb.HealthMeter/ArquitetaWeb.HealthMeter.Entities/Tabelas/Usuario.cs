using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ArquitetaWeb.Common.Infra.Componentes;

namespace ArquitetaWeb.HealthMeter.Entities.Tabelas
{
    public class Usuario : Entity
    {
        [Display(Name = "Nome")]
        [Required]
        public string Nome { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }
}
