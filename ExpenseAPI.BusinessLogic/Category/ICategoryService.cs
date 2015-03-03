using ExpenseAPI.Models;

namespace ExpenseAPI.BusinessLogic
{
    /// <summary>Category service</summary>
    public interface ICategoryService
    {
        /// <summary>Returns all categories</summary>
        CategoryGet[] GetCategories();

        /// <summary>Returns category</summary>
        CategoryGet GetCategory(string name);

        /// <summary>Creates category</summary>
        void CreateCategory(CategoryPost category);

        /// <summary>Updates category</summary>
        void UpdateCategory(string name, CategoryPut category);

        /// <summary>Deletes category</summary>
        void DeleteCategory(string name);
    }
}
