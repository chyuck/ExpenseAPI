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
    }
}
