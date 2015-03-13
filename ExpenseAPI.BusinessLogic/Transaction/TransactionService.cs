using System.Linq;
using DIContainer;
using DIContainer.Attributes;
using ExpenseAPI.DataAccess;
using ExpenseAPI.Models;

namespace ExpenseAPI.BusinessLogic
{
    internal class TransactionService : BaseBusinessLogicService, ITransactionService
    {
        #region Constructor

        [Inject]
        public TransactionService(IReadOnlyContainer container) 
            : base(container)
        {
        }
        
        #endregion


        #region Methods

        public TransactionGet[] GetTransactions(string categoryName)
        {
            ValidationHelper.ValidateCategoryName(categoryName);

            var userId = GetUserId();

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var dbCategory =
                    persistence.GetEntitySet<Category>()
                        .SingleOrDefault(c => c.UserId == userId && c.Name == categoryName);
                if (dbCategory == null)
                    throw new ValidationErrorException("Category with '{0}' name does not exist.", categoryName);

                return
                    persistence.GetEntitySet<Transaction>()
                        .Where(t => t.CategoryId == dbCategory.CategoryId)
                        .OrderByDescending(t => t.Time)
                        .AsEnumerable()
                        .Select(t => 
                            new TransactionGet
                            {
                                Id = t.Id.ToString("N"),
                                Category = dbCategory.Name,
                                UtcTime = t.Time,
                                Usd = t.USD,
                                Comment = t.Comment
                            })
                        .ToArray();
            }
        }

        public TransactionGet GetTransaction(string categoryName, string id)
        {
            throw new System.NotImplementedException();
        }

        public void CreateTransaction(string categoryName, TransactionPost transaction)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateTransaction(string categoryName, string id, TransactionPut transaction)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTransaction(string categoryName, string id)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
