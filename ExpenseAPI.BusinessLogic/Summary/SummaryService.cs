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
    internal class SummaryService : BaseBusinessLogicService, ISummaryService
    {
        #region Constructor

        [Inject]
        public SummaryService(IReadOnlyContainer container) 
            : base(container)
        {
        }

        #endregion


        #region Public Methods

        public async Task<Summary> GetSummaryAsync(DateTime? from = null, DateTime? to = null)
        {
            var userId = GetUserId();

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var query =
                    persistence.GetEntitySet<Transaction>()
                        .Where(t => t.Category.UserId == userId);

                query = QueryHelper.ApplyDateCondition(Container, query, from, to);

                var categories = await 
                    query
                        .GroupBy(t => new 
                        { 
                            CategoryName = t.Category.Name,
                            CategoryType = t.Category.Type
                        })
                        .Select(g => new
                        {
                            g.Key.CategoryName, 
                            g.Key.CategoryType, 
                            Total = g.Sum(i => i.USD)
                        })
                        .ToArrayAsync();

                return
                    new Summary
                    {
                        Categories = 
                            categories
                                .Select(c => new CategorySummary
                                {
                                    Name = c.CategoryName,
                                    Type = c.CategoryType,
                                    Total = c.Total
                                })
                                .ToArray(),
                        Total = categories.Sum(c => GetRealCategoryUsd(c.CategoryType, c.Total))
                    };
            }
        }

        #endregion


        #region Private Methods

        private static decimal GetRealCategoryUsd(string categoryTypeString, decimal usd)
        {
            Checker.ArgumentIsWhitespace(categoryTypeString, "categoryTypeString");

            var categoryType = (CategoryType) Enum.Parse(typeof(CategoryType), categoryTypeString);

            switch (categoryType)
            {
                case CategoryType.Income:
                    return usd;
                case CategoryType.Expense:
                    return -usd;
            }

            throw new InvalidOperationException(string.Format("Unknown '{0}' category type.", categoryTypeString));
        }

        #endregion
    }
}
