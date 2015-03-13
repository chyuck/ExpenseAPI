using System;
using System.Data.Entity.Infrastructure;
using DIContainer;
using ExpenseAPI.Common;
using ExpenseAPI.Common.Helpers;
using Validator;

namespace ExpenseAPI.BusinessLogic
{
    public abstract class BaseBusinessLogicService : BaseService
    {
        #region Constructor

        protected BaseBusinessLogicService(IReadOnlyContainer container) 
            : base(container)
        {
        }
        
        #endregion


        #region Properties
        
        protected IValidator Validator
        {
            get { return Container.Get<IValidator>(); }
        }

        protected ITimeService Time
        {
            get { return Container.Get<ITimeService>(); }
        }
        
        #endregion


        #region Methods
        
        protected int GetUserId()
        {
            var userService = Container.Get<IUserService>();

            return userService.UserId;
        }
        
        protected void RethrowUniqueKeyException(string uniqueKeyName, Func<Exception> getException, Action action)
        {
            Checker.ArgumentIsWhitespace(uniqueKeyName, "uniqueKeyName");
            Checker.ArgumentIsNull(getException, "getException");
            Checker.ArgumentIsNull(action, "action");

            try
            {
                action();
            }
            catch (DbUpdateException ex)
            {
                if (DoesExceptionContainText(ex, uniqueKeyName))
                    throw getException();

                throw;
            }
        }

        private static bool DoesExceptionContainText(Exception exception, string text)
        {
            Checker.ArgumentIsWhitespace(text, "text");

            if (exception == null)
                return false;

            if (exception.Message.Contains(text))
                return true;

            return DoesExceptionContainText(exception.InnerException, text);
        }

        #endregion
    }
}
