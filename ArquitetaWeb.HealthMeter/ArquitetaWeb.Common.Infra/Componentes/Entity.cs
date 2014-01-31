using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ArquitetaWeb.Common.Infra.Componentes
{
    public class Entity
    {
        [Key]
        public long Id { get; set; }

        private DateTime dataCadastro = DateTime.Now;
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro
        {
            get { return dataCadastro; }
            set { dataCadastro = value; }
        }

        [Display(Name = "Data de Alteração")]
        public DateTime? DataAlteracao { get; set; }
    }
}
