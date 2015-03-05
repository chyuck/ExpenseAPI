using ExpenseAPI.Models;

namespace ExpenseAPI.BusinessLogic
{
    /// <summary>Transaction service</summary>
    public interface ITransactionService
    {
        /// <summary>Returns all transactions for category</summary>
        TransactionGet[] GetTransactions(string categoryName);

        /// <summary>Returns transaction</summary>
        TransactionGet GetTransaction(string categoryName, string id);

        /// <summary>Creates transaction</summary>
        void CreateTransaction(string categoryName, TransactionPost transaction);

        /// <summary>Updates transaction</summary>
        void UpdateTransaction(string categoryName, string id, TransactionPut transaction);

        /// <summary>Deletes transaction</summary>
        void DeleteTransaction(string categoryName, string id);
    }
}
