using DIContainer;
using ExpenseAPI.Common.Helpers;

namespace ExpenseAPI.Common
{
    public static class DIContainerRegistrator
    {
        public static void Register(IContainer container)
        {
            Checker.ArgumentIsNull(container, "container");

            container.RegisterImplementation<ITimeService, TimeService>(Lifetime.PerContainer);
        }
    }
}
