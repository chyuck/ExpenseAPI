using System.Threading.Tasks;
using ExpenseAPI.Models;

namespace ExpenseAPI.BusinessLogic
{
    /// <summary>Category service</summary>
    public interface ICategoryService
    {
        /// <summary>Returns all categories</summary>
        Task<CategoryGet[]> GetCategoriesAsync();

        /// <summary>Returns category</summary>
        Task<CategoryGet> GetCategoryAsync(string name);

        /// <summary>Creates category</summary>
        Task CreateCategoryAsync(CategoryPost category);

        /// <summary>Updates category</summary>
        Task UpdateCategoryAsync(string name, CategoryPut category);

        /// <summary>Deletes category</summary>
        Task DeleteCategoryAsync(string name);
    }
}
