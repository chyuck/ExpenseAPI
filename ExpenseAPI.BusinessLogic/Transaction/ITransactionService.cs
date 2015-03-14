using System.Threading.Tasks;
using ExpenseAPI.Models;

namespace ExpenseAPI.BusinessLogic
{
    /// <summary>Transaction service</summary>
    public interface ITransactionService
    {
        /// <summary>Returns all transactions for category</summary>
        Task<TransactionGet[]> GetTransactionsAsync(string categoryName);

        /// <summary>Returns transaction</summary>
        Task<TransactionGet> GetTransactionAsync(string categoryName, string id);

        /// <summary>Creates transaction</summary>
        Task CreateTransactionAsync(string categoryName, TransactionPost transaction);

        /// <summary>Updates transaction</summary>
        Task UpdateTransactionAsync(string categoryName, string id, TransactionPut transaction);

        /// <summary>Deletes transaction</summary>
        Task DeleteTransactionAsync(string categoryName, string id);
    }
}
