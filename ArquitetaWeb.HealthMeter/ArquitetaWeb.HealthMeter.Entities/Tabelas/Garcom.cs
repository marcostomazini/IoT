﻿using ArquitetaWeb.Common.Infra.Componentes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ArquitetaWeb.HealthMeter.Entities.Tabelas
{
    public class Garcom : Entity
    {
        //[DisplayExterno(Name = "Nome", "Var GLOBAL AKI")] // Para concatenar nome sistema externo
        [Required]
        [Display(Name = "Código")]
        public string CodigoExterno { get; set; }

        //[DisplayExterno(Name = "Nome", "Var GLOBAL AKI")] // Para concatenar nome sistema externo
        [Display(Name = "Nome")]
        public string NomeExterno { get; set; }

        [Display(Name = "Nome")]
        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; } // CAMPO DO MODELO
    }
    //[Required]
    //[DataType(DataType.EmailAddress)]
    //[Display(Name = "Email address")]
    //public string Email { get; set; }

    //[Required]
    //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //[DataType(DataType.Password)]
    //[Display(Name = "Password")]
    //public string Password { get; set; }

    //[DataType(DataType.Password)]
    //[Display(Name = "Confirm password")]
    //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //public string ConfirmPassword { get; set; }

}