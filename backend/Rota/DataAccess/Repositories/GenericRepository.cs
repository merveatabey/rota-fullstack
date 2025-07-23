using System;
using System.Linq.Expressions;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

		public GenericRepository(AppDbContext context)
		{
            _context = context;
            _dbSet = context.Set<T>();
		}

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}

