using DriverHire.Data.Context;
using DriverHire.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Repository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DriverHireContext context;
        internal DbSet<TEntity> dbSet;
        public Repository(DriverHireContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public async ValueTask<EntityEntry<TEntity>> Insert(TEntity entity)
        {
            return await dbSet.AddAsync(entity);
        }
        public void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            dbSet.Remove(entityToDelete);
        }
        public void Update(TEntity entityToUpdate)
        {
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public async Task<TEntity> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> SelectWhere(params Expression<Func<TEntity, bool>>[] predictes)
        {
            IQueryable<TEntity> query = dbSet;
            foreach (var predicate in predictes)
            {
                query = dbSet.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> SelectWhereInclude(string[] includes, params Expression<Func<TEntity, bool>>[] predicates)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            if (predicates != null)
                foreach (var predicate in predicates)
                {
                    query = query.Where(predicate);
                }

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
