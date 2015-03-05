using DIContainer;
using DIContainer.Attributes;
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

        public TransactionGet[] GetTransactions(string categoryName)
        {
            throw new System.NotImplementedException();
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
    }
}
