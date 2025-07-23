using System;
using System.Linq.Expressions;


namespace Rota.Core.Interfaces
{
	public interface IGenericRepository<T> where T:class
	{
		Task<T> GetByIdAsync(object id);
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        IQueryable<T> Query();

    }
}

