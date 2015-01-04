using DIContainer;
using ExpenseAPI.Common.Helpers;

namespace ExpenseAPI.BusinessLogic
{
    public static class DIContainerFactory
    {
        public static IReadOnlyContainer Create()
        {
            var container = ContainerFactory.Create();

            RegisterDependencies(container);

            return container;
        }

        private static void RegisterDependencies(IContainer container)
        {
            Checker.ArgumentIsNull(container, "container");

            // Add DI container dependencies below
        }

    }
}
