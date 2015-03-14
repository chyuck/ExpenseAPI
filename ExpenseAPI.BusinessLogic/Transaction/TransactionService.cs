using System;
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


        #region Public Methods

        public TransactionGet[] GetTransactions(string categoryName)
        {
            ValidationHelper.ValidateCategoryName(categoryName);

            var userId = GetUserId();

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var category = GetCategory(persistence, userId, categoryName);

                return
                    persistence.GetEntitySet<Transaction>()
                        .Where(t => t.CategoryId == category.CategoryId)
                        .OrderByDescending(t => t.Time)
                        .AsEnumerable()
                        .Select(CreateTransaction)
                        .ToArray();
            }
        }

        public TransactionGet GetTransaction(string categoryName, string id)
        {
            ValidationHelper.ValidateCategoryName(categoryName);
            ValidationHelper.ValidateTransactionId(id);

            var userId = GetUserId();

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var dbCategory = GetCategory(persistence, userId, categoryName);
                var dbTransaction = GetTransaction(persistence, id, dbCategory.CategoryId);

                return CreateTransaction(dbTransaction);
            }
        }

        public void CreateTransaction(string categoryName, TransactionPost transaction)
        {
            var userId = GetUserId();

            ValidationHelper.ValidateCategoryName(categoryName);

            if (transaction == null)
                throw new ValidationErrorException("Transaction must be specified.");
            Validator.CheckIsValid(transaction);

            var utcNow = Time.UtcNow;

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var dbCategory = GetCategory(persistence, userId, categoryName);
                
                var dbTransaction =
                    new Transaction
                    {
                        CategoryId = dbCategory.CategoryId,
                        Id = Guid.NewGuid(),
                        USD = transaction.Usd,
                        Time = transaction.Time.ToUniversalTime(),
                        Comment = transaction.Comment,
                        CreateDate = utcNow,
                        ChangeDate = utcNow
                    };

                persistence.GetEntitySet<Transaction>().Add(dbTransaction);
                persistence.SaveChanges();
            }
        }

        public void UpdateTransaction(string categoryName, string id, TransactionPut transaction)
        {
            throw new NotImplementedException();
        }

        public void DeleteTransaction(string categoryName, string id)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Private Methods

        private static Category GetCategory(IPersistenceService persistence, int userId, string categoryName)
        {
            var category =
                    persistence.GetEntitySet<Category>()
                        .SingleOrDefault(c => c.UserId == userId && c.Name == categoryName);
            if (category == null)
                throw new ValidationErrorException("Category with '{0}' name does not exist.", categoryName);

            return category;
        }

        private static TransactionGet CreateTransaction(Transaction transaction)
        {
            return
                new TransactionGet
                {
                    Id = transaction.Id.ToString("N"),
                    Category = transaction.Category.Name,
                    Time = DateTime.SpecifyKind(transaction.Time, DateTimeKind.Utc),
                    Usd = transaction.USD,
                    Comment = transaction.Comment
                };
        }

        private static Transaction GetTransaction(IPersistenceService persistence, string id, int categoryId)
        {
            Guid guid;
            if (!Guid.TryParseExact(id, "N", out guid))
                throw new ValidationErrorException("Transaction ID has invalid format.");

            var transaction =
                persistence.GetEntitySet<Transaction>()
                    .SingleOrDefault(t => t.Id == guid && t.CategoryId == categoryId);
            if (transaction == null)
                throw new ValidationErrorException("Transaction with '{0}' ID does not exist.", id);

            return transaction;
        }

        #endregion
    }
}
