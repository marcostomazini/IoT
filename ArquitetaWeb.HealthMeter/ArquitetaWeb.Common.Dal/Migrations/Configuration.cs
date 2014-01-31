namespace ArquitetaWeb.HealthMeter.Data.Migrations
{
    using ArquitetaWeb.HealthMeter.Entities.Tabelas;
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
            context.Configuracao.AddOrUpdate(
                  new Configuracao()
                  {
                      MaxUmidade = 70,
                      TempoAtualizacao = 10
                  }
               );
        }
        #endregion
    }
}
