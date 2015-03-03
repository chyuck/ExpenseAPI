using System;

namespace ExpenseAPI.BusinessLogic
{
    [Serializable]
    public class UserServiceException : Exception
    {
        public UserServiceException(string message)
            : base(message)
        {
        }

        public UserServiceException(string format, params object[] args)
            : this(string.Format(format, args))
        {
        }
    }
}
