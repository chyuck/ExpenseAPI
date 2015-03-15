using DIContainer;
using ExpenseAPI.Common.Helpers;
using Validator;

namespace ExpenseAPI.BusinessLogic
{
    public static class DIContainerFactory
    {
        public static IReadOnlyContainer Create()
        {
            var container = ContainerFactory.Create();

            Common.DIContainerRegistrator.Register(container);
            DataAccess.DIContainerRegistrator.Register(container);
            
            RegisterDependencies(container);

            return container;
        }

        private static void RegisterDependencies(IContainer container)
        {
            Checker.ArgumentIsNull(container, "container");

            container.RegisterImplementation<IValidator, Validator.Validator>(Lifetime.PerContainer);

            container.RegisterImplementation<IUserService, UserService>(Lifetime.PerContainer);
            container.RegisterImplementation<ICategoryService, CategoryService>(Lifetime.PerContainer);
            container.RegisterImplementation<ITransactionService, TransactionService>(Lifetime.PerContainer);
            container.RegisterImplementation<ISummaryService, SummaryService>(Lifetime.PerContainer);
        }
    }
}
