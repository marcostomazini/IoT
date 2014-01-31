using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ArquitetaWeb.Common.Infra.Repositorios
{   
    public interface IRepository<T>
    {
        ArquitetaWeb.Common.Data.Contexto.ContextoAcesso GetContext();

        void Save();
        Task<int> SaveChangesAsync();

        IQueryable<T> Consulta(Expression<Func<T, bool>> filter = null);        
        IQueryable<S> Query<S>(Expression<Func<S, bool>> filter = null) where S : class;
        
        S SingleOrDefault<S>(Expression<Func<S, bool>> predicate) where S : class;
        Task<S> SingleOrDefaultAsync<S>(Expression<Func<S, bool>> predicate) where S : class;
        
        T Retorna(long id);
        Task<T> RetornaAsync(long id);

        void Insert<S>(S entity) where S : class;
        Task InsertAsync<S>(S entity) where S : class;

        void Update<S>(S entity) where S : class;
        Task UpdateAsync<S>(S entity) where S : class;

        S UpdateFlush<S>(S entity) where S : class;
        Task<S> UpdateFlushAsync<S>(S entity) where S : class;

        void Delete<S>(S entity) where S : class;
        Task DeleteAsync<S>(S entity) where S : class;        
    }    
}
