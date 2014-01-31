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
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }


}
