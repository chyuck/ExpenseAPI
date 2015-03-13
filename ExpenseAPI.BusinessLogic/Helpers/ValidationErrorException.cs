using System;

namespace ExpenseAPI.BusinessLogic
{
    [Serializable]
    public class ValidationErrorException : Exception
    {
        public ValidationErrorException(string message)
            : base(message)
        {
        }

        public ValidationErrorException(string format, params object[] args)
            : this(string.Format(format, args))
        {
        }
    }
}
