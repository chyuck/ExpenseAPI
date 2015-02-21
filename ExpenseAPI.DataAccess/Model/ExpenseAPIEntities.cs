using System.Data.Entity;

namespace ExpenseAPI.DataAccess
{
    internal partial class ExpenseAPIEntities : IPersistentService
    {
        public IDbSet<TEntity> GetEntitySet<TEntity>() 
            where TEntity : class, IEntity, new()
        {
            return Set<TEntity>();
        }
    }
}
