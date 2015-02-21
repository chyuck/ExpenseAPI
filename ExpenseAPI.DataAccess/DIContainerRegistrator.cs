using DIContainer;
using ExpenseAPI.Common.Helpers;

namespace ExpenseAPI.DataAccess
{
    public static class DIContainerRegistrator
    {
        public static void Register(IContainer container)
        {
            Checker.ArgumentIsNull(container, "container");

            container.RegisterImplementation<IPersistentService, ExpenseAPIEntities>(Lifetime.PerContainer);
        }
    }
}
