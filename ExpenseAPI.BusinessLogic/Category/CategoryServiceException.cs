using System;

namespace ExpenseAPI.BusinessLogic
{
    [Serializable]
    public class CategoryServiceException : Exception
    {
         public CategoryServiceException(string message)
            : base(message)
        {
        }

         public CategoryServiceException(string format, params object[] args)
            : this(string.Format(format, args))
        {
        }
    }
}
