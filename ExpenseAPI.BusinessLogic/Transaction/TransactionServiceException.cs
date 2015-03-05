using System;

namespace ExpenseAPI.BusinessLogic
{
    [Serializable]
    public class TransactionServiceException : Exception
    {
         public TransactionServiceException(string message)
            : base(message)
        {
        }

         public TransactionServiceException(string format, params object[] args)
            : this(string.Format(format, args))
        {
        }
    }
}
