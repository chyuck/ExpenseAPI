using System;
using System.Data.Entity;

namespace ExpenseAPI.DataAccess
{
    public interface IPersistenceService : IDisposable
    {
        IDbSet<TEntity> GetEntitySet<TEntity>()
            where TEntity : class, IEntity, new();

        int SaveChanges();
    }
}
