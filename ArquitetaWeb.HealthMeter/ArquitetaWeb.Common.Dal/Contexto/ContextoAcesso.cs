using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ArquitetaWeb.HealthMeter.Entities.Tabelas;

namespace ArquitetaWeb.Common.Data.Contexto
{
    public partial class ContextoAcesso : DbContext
    {
        public ContextoAcesso() : base("DB")
        {
            Database.SetInitializer<ContextoAcesso>(new DropCreateDatabaseIfModelChanges<DbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>().Property(p => p.Quantidade).HasPrecision(3, 2);

            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<CandidatoDevice>()
            //        .HasOptional(a => a.Candidato)
            //        .WithOptionalDependent()
            //        .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }


}
