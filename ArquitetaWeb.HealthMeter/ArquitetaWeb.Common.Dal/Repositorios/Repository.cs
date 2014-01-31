using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using ArquitetaWeb.Common.Infra.Componentes;
using ArquitetaWeb.Common.Data.Contexto;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace ArquitetaWeb.Common.Infra.Repositorios
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly ContextoAcesso dataContext;

        public Repository(ContextoAcesso dataContext)
        {
            this.dataContext = dataContext;
        }

        public ContextoAcesso GetContext()
        {
            return dataContext;
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dataContext.SaveChangesAsync();
        }

        public IQueryable<T> Consulta(Expression<Func<T, bool>> filter = null)
        {
            return Query(filter);
        }

        public IQueryable<S> Query<S>(Expression<Func<S, bool>> filter = null) where S : class
        {
            IQueryable<S> query = dataContext.Set<S>();

            if (filter != null)
                query = query.Where(filter);

            return query;
        }

        public S SingleOrDefault<S>(Expression<Func<S, bool>> predicate) where S : class
        {
            return dataContext.Set<S>().SingleOrDefault(predicate);
        }

        public async Task<S> SingleOrDefaultAsync<S>(Expression<Func<S, bool>> predicate) where S : class
        {
            return await dataContext.Set<S>().SingleOrDefaultAsync(predicate);
        }

        public T Retorna(long id)
        {
            return dataContext.Set<T>().Find(id);
        }

        public async Task<T> RetornaAsync(long id)
        {
            return await dataContext.Set<T>().FindAsync(id);
        }

        public void Insert<S>(S entity) where S : class
        {
            dataContext.Set<S>().Add(entity);
            Save();
        }

        public async Task InsertAsync<S>(S entity) where S : class
        {
            dataContext.Set<S>().Add(entity);
            await SaveChangesAsync();
        }

        public void Update<S>(S entity) where S : class
        {
            AtualizaDataAlteracao<S>(entity);
            DbEntityEntry entityEntry = dataContext.Entry(entity);

            if (entityEntry.State == EntityState.Detached)
            {
                dataContext.Set<S>().Attach(entity);
                entityEntry.State = EntityState.Modified;
            }

            Save();
        }

        public async Task UpdateAsync<S>(S entity) where S : class
        {
            AtualizaDataAlteracao<S>(entity);
            DbEntityEntry entityEntry = dataContext.Entry(entity);
            
            if (entityEntry.State == EntityState.Detached)
            {
                dataContext.Set<S>().Attach(entity);

                //foreach (string propertyName in entityEntry.OriginalValues.PropertyNames)
                //{
                //    if (!object.Equals(entityEntry.GetDatabaseValues().GetValue<object>(propertyName),
                //        entityEntry.CurrentValues.GetValue<object>(propertyName)))
                //    {
                //        // It never makes it into this if block, even when
                //        //    the property has been updated.
                //    }
                //}

                entityEntry.State = EntityState.Modified;
            }
            await SaveChangesAsync();
        }

        public S UpdateFlush<S>(S entity) where S : class
        {
            Update(entity);
            return entity;
        }

        public async Task<S> UpdateFlushAsync<S>(S entity) where S : class
        {
            await UpdateAsync(entity);
            return entity;
        }

        public void Delete<S>(S entity) where S : class
        {
            dataContext.Set<S>().Remove(entity);
            Save();
        }

        public async Task DeleteAsync<S>(S entity) where S : class
        {
            dataContext.Set<S>().Remove(entity);
            await SaveChangesAsync();
        }

        private static void AtualizaDataAlteracao<S>(S entity) where S : class
        {
            if (entity is Entity) (entity as Entity).DataAlteracao = DateTime.Now;
        }
    }
}
