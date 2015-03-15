using System;
using System.Threading.Tasks;
using ExpenseAPI.Models;

namespace ExpenseAPI.BusinessLogic
{
    /// <summary>Summary service</summary>
    public interface ISummaryService
    {
        /// <summary>Returns summary for period of time</summary>
        Task<Summary> GetSummaryAsync(DateTime? from = null, DateTime? to = null);
    }
}
