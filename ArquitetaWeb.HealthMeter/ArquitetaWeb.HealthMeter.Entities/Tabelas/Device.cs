using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArquitetaWeb.Common.Infra.Componentes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquitetaWeb.HealthMeter.Entities.Tabelas
{
    public class Device : Entity
    {
        [Required]
        [Display(Name = "Número de Série Do Aparelho")]
        //[MaxLength(16)]
        public string NumeroEquipamento { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(100)]
        public string Descricao { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        // opcional, se tiver preenchido ele não deixa escolher o login, se não existir Id do Garcom ele cria um Dropbox e lista os Garcom se listar o garcom a senha de acesso deverá ser a do cadastro de garçom
        [ForeignKey("Garcom")]
        public long GarcomId { get; set; }

        public Garcom Garcom { get; set; }
    }

}
