using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DIContainer;
using DIContainer.Attributes;
using ExpenseAPI.Common.Helpers;
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

        public async Task<TransactionGet[]> GetTransactionsAsync(string categoryName, DateTime? from, DateTime? to)
        {
            var userId = GetUserId();

            ValidationHelper.ValidateCategoryName(categoryName);

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var category = await GetCategoryAsync(persistence, userId, categoryName);

                var query =
                    persistence.GetEntitySet<Transaction>()
                        .Where(t => t.CategoryId == category.CategoryId);

                query = ApplyDateCondition(query, from, to);

                var dbTransactions = await
                    query
                        .OrderByDescending(t => t.Date)
                        .ThenByDescending(t => t.TransactionId)
                        .ToArrayAsync();

                return dbTransactions.Select(CreateTransaction).ToArray();
            }
        }

        public async Task<TransactionGet> GetTransactionAsync(string categoryName, string id)
        {
            var userId = GetUserId();

            ValidationHelper.ValidateCategoryName(categoryName);
            ValidationHelper.ValidateTransactionId(id);
            
            using (var persistence = Container.Get<IPersistenceService>())
            {
                var dbCategory = await GetCategoryAsync(persistence, userId, categoryName);
                var dbTransaction = await GetTransactionAsync(persistence, id, dbCategory.CategoryId);

                return CreateTransaction(dbTransaction);
            }
        }

        public async Task CreateTransactionAsync(string categoryName, TransactionPost transaction)
        {
            var userId = GetUserId();

            ValidationHelper.ValidateCategoryName(categoryName);

            if (transaction == null)
                throw new ValidationErrorException("Transaction must be specified.");
            Validator.CheckIsValid(transaction);

            var utcNow = Time.UtcNow;

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var dbCategory = await GetCategoryAsync(persistence, userId, categoryName);
                
                var dbTransaction =
                    new Transaction
                    {
                        CategoryId = dbCategory.CategoryId,
                        Id = Guid.NewGuid(),
                        USD = transaction.Usd,
                        Date = transaction.Date.Date,
                        Comment = transaction.Comment,
                        CreateDate = utcNow,
                        ChangeDate = utcNow
                    };

                persistence.GetEntitySet<Transaction>().Add(dbTransaction);
                await persistence.SaveChangesAsync();
            }
        }

        public async Task UpdateTransactionAsync(string categoryName, string id, TransactionPut transaction)
        {
            var userId = GetUserId();

            ValidationHelper.ValidateCategoryName(categoryName);
            ValidationHelper.ValidateTransactionId(id);

            if (transaction == null)
                throw new ValidationErrorException("Transaction must be specified.");
            Validator.CheckIsValid(transaction);

            var utcNow = Time.UtcNow;

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var dbCategory = await GetCategoryAsync(persistence, userId, categoryName);
                var dbTransaction = await GetTransactionAsync(persistence, id, dbCategory.CategoryId);

                dbTransaction.USD = transaction.Usd;
                dbTransaction.Comment = transaction.Comment;
                dbTransaction.Date = transaction.Date.Date;
                dbTransaction.ChangeDate = utcNow;

                await persistence.SaveChangesAsync();
            }
        }

        public async Task DeleteTransactionAsync(string categoryName, string id)
        {
            var userId = GetUserId();

            ValidationHelper.ValidateCategoryName(categoryName);
            ValidationHelper.ValidateTransactionId(id);

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var dbCategory = await GetCategoryAsync(persistence, userId, categoryName);
                var dbTransaction = await GetTransactionAsync(persistence, id, dbCategory.CategoryId);

                persistence.GetEntitySet<Transaction>().Remove(dbTransaction);
                await persistence.SaveChangesAsync();
            }
        }

        #endregion


        #region Private Methods

        private async static Task<Category> GetCategoryAsync(IPersistenceService persistence, int userId, string categoryName)
        {
            var category = await
                    persistence.GetEntitySet<Category>()
                        .SingleOrDefaultAsync(c => c.UserId == userId && c.Name == categoryName);
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
                    Date = transaction.Date,
                    Usd = transaction.USD,
                    Comment = transaction.Comment
                };
        }

        private async static Task<Transaction> GetTransactionAsync(IPersistenceService persistence, string id, int categoryId)
        {
            Guid guid;
            if (!Guid.TryParseExact(id, "N", out guid))
                throw new ValidationErrorException("Transaction ID has invalid format.");

            var transaction = await
                persistence.GetEntitySet<Transaction>()
                    .SingleOrDefaultAsync(t => t.Id == guid && t.CategoryId == categoryId);
            if (transaction == null)
                throw new ValidationErrorException("Transaction with '{0}' ID does not exist.", id);

            return transaction;
        }

        private IQueryable<Transaction> ApplyDateCondition(IQueryable<Transaction> query, DateTime? from, DateTime? to)
        {
            Checker.ArgumentIsNull(query, "query");

            if (!from.HasValue && !to.HasValue)
            {
                var firstDayOfCurrentMonth = Time.FirstDayOfCurrentMonth;
                return query.Where(t => t.Date >= firstDayOfCurrentMonth);
            }

            if (from.HasValue)
                query = query.Where(t => t.Date >= from.Value);
            
            if (to.HasValue)
                query = query.Where(t => t.Date <= to.Value);

            return query;
        }

        #endregion
    }
}
