using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using WebApi.Interfaces;

namespace WebApi.Data.Repo
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DbContext dc;

        public GenericRepository(DbContext dc)
        {
            this.dc = dc;
        }
        public async Task AddAsync(TEntity entity)
        {
            await dc.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dc.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await dc.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task<TEntity> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await dc.Set<TEntity>().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dc.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            dc.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dc.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            dc.Entry(entity).State = EntityState.Modified;
        }
    }
}
