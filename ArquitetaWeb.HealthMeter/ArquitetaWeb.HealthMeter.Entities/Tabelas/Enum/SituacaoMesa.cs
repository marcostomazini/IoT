using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArquitetaWeb.HealthMeter.Entities.Tabelas.Enum
{
    public enum SituacaoMesa
    {
        //[Description("Livre")]
        Livre = 1,
        //[Description("Ocupada")]
        Ocupada = 2,
        //[Description("Em Conta")]
        EmConta = 3,
        //[Description("Limpar")]
        Limpar = 4,
        //[Description("Agrupada")]
        Agrupada = 5,
        //[Description("Reservada")]
        Reservada = 6,
        //[Description("Ociosa")]
        Ociosa = 7// Aberta Com um Tempo Definido Sem Movimentação
    }
}
