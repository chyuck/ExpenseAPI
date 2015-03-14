using Validator.Helpers;

namespace ExpenseAPI.BusinessLogic
{
    internal static class ValidationHelper
    {
        public static void ValidateCategoryName(string name)
        {
            if (!StringValidator.ValidateString(name, 1, 20, false, false))
                throw new ValidationErrorException("Category name must have 1-20 characters.");
        }

        public static void ValidateTransactionId(string id)
        {
            if (!StringValidator.ValidateString(id, 32, 32, false, false))
                throw new ValidationErrorException("Transaction ID must have 32 characters.");
        }
    }
}
