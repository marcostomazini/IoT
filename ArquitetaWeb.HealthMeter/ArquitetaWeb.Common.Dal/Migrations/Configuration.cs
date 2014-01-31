namespace ArquitetaWeb.HealthMeter.Data.Migrations
{
    using ArquitetaWeb.HealthMeter.Entities.Tabelas;
    using ArquitetaWeb.HealthMeter.Entities.Tabelas.Enum;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ArquitetaWeb.Common.Data.Contexto.ContextoAcesso>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            //DropCreateDatabaseAlways<ArquitetaWeb.HealthMeter.Contexto.ContextoAcesso>();
        }

        protected override void Seed(ArquitetaWeb.Common.Data.Contexto.ContextoAcesso context)
        {
            CargaTabela(context);

            //context.Device.AddOrUpdate(
            //     new Device { Id = 1, Descricao = "C# Device", DataCadastro = DateTime.Now },
            //     new Device { Id = 2, Descricao = "Visual Basic Device", DataCadastro = DateTime.Now },
            //     new Device { Id = 3, Descricao = "Delphi 7 Device", DataCadastro = DateTime.Now },
            //     new Device { Id = 4, Descricao = "SQL Server Device", DataCadastro = DateTime.Now },
            //     new Device { Id = 5, Descricao = "Oracle Device", DataCadastro = DateTime.Now }
            //   );

            //context.Usuario.AddOrUpdate(
            //     new Usuario { Id = 1, Nome = "Marcos Tomazini", DeviceId = 1, DataCadastro = DateTime.Now }
            //   );
        }

        #region Carga

        private static void CargaTabela(ArquitetaWeb.Common.Data.Contexto.ContextoAcesso context)
        {

            context.Usuario.AddOrUpdate(
                 new Usuario { Id = 1, Nome = "Marcos Tomazini", Senha = "MTIz" }
               );

            context.Configuracao.AddOrUpdate(
                  new Configuracao()
                  {
                      DescricaoSistemaExterno = "Chopperia 2626",
                      TipoVisualizacaoPedidos = VisualizacaoPedidos.Desabilitado,
                      QuantidadeVisualizacaoPedidos = 1,
                      TempoOcioso = 0
                  }
               );

            context.Garcom.AddOrUpdate(
                new Garcom()
                {
                    Id = 1,
                    CodigoExterno = "1",
                    NomeExterno = "Garçom Teste",
                    Senha = "MTIz" //base64 - 123                
                },
                new Garcom()
                {
                    Id = 2,
                    CodigoExterno = "2",
                    NomeExterno = "Garçom Teste",
                    Senha = "MTIz" //base64 - 123                
                }
            );

            context.Equipamento.AddOrUpdate(new Device()
            {
                Id = 1,
                Descricao = "Android Tablet",
                GarcomId = 1,
                NumeroEquipamento = "HEXADECIMAL",
                Senha = "MTIz", //base64 - 123                
                DataCadastro = DateTime.Now
            },
            new Device()
            {
                Id = 2,
                Descricao = "Android 2",
                GarcomId = 2,
                NumeroEquipamento = "HEXADECIMAL",
                Senha = "MTIz", //base64 - 123                
                DataCadastro = DateTime.Now
            });

            context.Mesa.AddOrUpdate(new Mesa()
            {
                Id = 1,
                CodigoExterno = "1",
                NumeroMesa = 1,
                Situacao = SituacaoMesa.Livre
            }, new Mesa()
            {
                Id = 2,
                CodigoExterno = "2",
                NumeroMesa = 2,
                Situacao = SituacaoMesa.Livre
            });

            context.Produto.AddOrUpdate(
            new Produto()
            {
                Codigo = 1,
                CodigoExterno = "1",
                DataCadastro = DateTime.Now,
                Descricao = "Nome no Sistema Web",
                DescricaoExterna = "Item Teste",
                Valor = 12.23
            }, new Produto()
            {
                Codigo = 1,
                CodigoExterno = "1",
                DataCadastro = DateTime.Now,
                DescricaoExterna = "Item Teste",
                Valor = 12.23
            });
        }
        #endregion
    }
}
