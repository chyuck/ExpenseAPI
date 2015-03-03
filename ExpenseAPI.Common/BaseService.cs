using DIContainer;
using ExpenseAPI.Common.Helpers;

namespace ExpenseAPI.Common
{
    public abstract class BaseService
    {
        protected BaseService(IReadOnlyContainer container)
        {
            Checker.ArgumentIsNull(container, "container");

            Container = container;
        }

        protected IReadOnlyContainer Container { get; private set; }
    }
}
