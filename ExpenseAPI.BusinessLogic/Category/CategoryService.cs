using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DIContainer;
using DIContainer.Attributes;
using ExpenseAPI.DataAccess;
using ExpenseAPI.Models;

namespace ExpenseAPI.BusinessLogic
{
    internal class CategoryService : BaseBusinessLogicService, ICategoryService
    {
        #region Constructor

        [Inject]
        public CategoryService(IReadOnlyContainer container)
            : base(container)
        {
        }
        
        #endregion


        #region Public Methods

        public async Task<CategoryGet[]> GetCategoriesAsync()
        {
            var userId = GetUserId();

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var dbCategories = await 
                    persistence.GetEntitySet<Category>()
                        .Where(c => c.UserId == userId)
                        .OrderBy(c => c.Name)
                        .ToArrayAsync();

                return dbCategories.Select(CreateCategory).ToArray();
            }
        }

        public async Task<CategoryGet> GetCategoryAsync(string name)
        {
            var userId = GetUserId();

            ValidationHelper.ValidateCategoryName(name);

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var dbCategory = await GetCategoryAsync(persistence, userId, name);

                return CreateCategory(dbCategory);
            }
        }

        public async Task CreateCategoryAsync(CategoryPost category)
        {
            var userId = GetUserId();

            if (category == null)
                throw new ValidationErrorException("Category must be specified.");
            Validator.CheckIsValid(category);

            var utcNow = Time.UtcNow;

            await RethrowUniqueKeyExceptionAsync(
                "UK_Category",
                () => new ValidationErrorException("Category with '{0}' name already exists.", category.Name),
                async () =>
                {
                    using (var persistence = Container.Get<IPersistenceService>())
                    {
                        var dbCategory =
                            new Category
                            {
                                Name = category.Name,
                                Type = category.Type,
                                UserId = userId,
                                CreateDate = utcNow,
                                ChangeDate = utcNow
                            };

                        persistence.GetEntitySet<Category>().Add(dbCategory);
                        await persistence.SaveChangesAsync();
                    }
                });
        }

        public async Task UpdateCategoryAsync(string name, CategoryPut category)
        {
            var userId = GetUserId();
            
            ValidationHelper.ValidateCategoryName(name);

            if (category == null)
                throw new ValidationErrorException("Category must be specified.");
            Validator.CheckIsValid(category);

            var utcNow = Time.UtcNow;

            await RethrowUniqueKeyExceptionAsync(
                "UK_Category",
                () => new ValidationErrorException("Category with '{0}' name already exists.", category.Name),
                async () =>
                {
                    using (var persistence = Container.Get<IPersistenceService>())
                    {
                        var dbCategory = await GetCategoryAsync(persistence, userId, category.Name);

                        dbCategory.Name = category.Name;
                        dbCategory.Type = category.Type;
                        dbCategory.ChangeDate = utcNow;
                        
                        await persistence.SaveChangesAsync();
                    }
                });
        }

        public async Task DeleteCategoryAsync(string name)
        {
            var userId = GetUserId();

            ValidationHelper.ValidateCategoryName(name);

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var dbCategory = await GetCategoryAsync(persistence, userId, name);
                if (dbCategory.Transactions.Any())
                    throw new ValidationErrorException("Category cannot be deleted because it has transaction.");

                persistence.GetEntitySet<Category>().Remove(dbCategory);
                await persistence.SaveChangesAsync();
            }
        }
        
        #endregion


        #region Private Methods

        private static CategoryGet CreateCategory(Category category)
        {
            return
                new CategoryGet
                {
                    Name = category.Name,
                    Type = category.Type
                };
        }

        private static async Task<Category> GetCategoryAsync(IPersistenceService persistence, int userId, string name)
        {
            var category = await
                    persistence.GetEntitySet<Category>()
                        .SingleOrDefaultAsync(c => c.UserId == userId && c.Name == name);
            if (category == null)
                throw new ValidationErrorException("Category with '{0}' name does not exist.", name);

            return category;
        }

        #endregion
    }
}
