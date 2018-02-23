using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.EntityFramework
{
    internal abstract class GenericRepository<T> where T : class
    {
        protected AppContextProvaASPNET Context;

        protected GenericRepository(AppContextProvaASPNET context)
        {
            Context = context;
        }

        protected virtual IQueryable<T> AsQueryable => Context.Set<T>();


        protected T FirstOrDefault(Expression<Func<T, bool>> @where)
        {
            var result = AsQueryable.FirstOrDefault(where);
            return result;
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity != null)
            {
                Context.Entry(entity).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
        }

        public async Task InsertAsync(T entity)
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();

        }

        public async Task DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

    }
}
