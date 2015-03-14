using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ExpenseAPI.DataAccess
{
    public interface IPersistenceService : IDisposable
    {
        IDbSet<TEntity> GetEntitySet<TEntity>()
            where TEntity : class, IEntity, new();

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
