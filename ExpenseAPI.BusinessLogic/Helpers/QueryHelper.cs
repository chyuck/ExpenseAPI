using System;
using System.Linq;
using DIContainer;
using ExpenseAPI.Common;
using ExpenseAPI.Common.Helpers;
using ExpenseAPI.DataAccess;

namespace ExpenseAPI.BusinessLogic
{
    internal static class QueryHelper
    {
        public static IQueryable<Transaction> ApplyDateCondition(IReadOnlyContainer container, 
            IQueryable<Transaction> query, DateTime? from, DateTime? to)
        {
            Checker.ArgumentIsNull(container, "container");
            Checker.ArgumentIsNull(query, "query");

            if (!from.HasValue && !to.HasValue)
            {
                var firstDayOfCurrentMonth = container.Get<ITimeService>().FirstDayOfCurrentMonth;
                return query.Where(t => t.Date >= firstDayOfCurrentMonth);
            }

            if (from.HasValue)
                query = query.Where(t => t.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(t => t.Date <= to.Value);

            return query;
        }
    }
}
