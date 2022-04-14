using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<EntityEntry<TEntity>> Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void Delete(object id);
        Task<TEntity> GetById(object id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> SelectWhere(params Expression<Func<TEntity, bool>>[] predictes);
        Task<IEnumerable<TEntity>> SelectWhereInclude(string[] includes, params Expression<Func<TEntity, bool>>[] predictes);
      
    }
}
