using System.Linq;
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

        public CategoryGet[] GetCategories()
        {
            var userId = GetUserId();

            using (var persistence = Container.Get<IPersistenceService>())
            {
                return
                    persistence.GetEntitySet<Category>()
                        .Where(c => c.UserId == userId)
                        .OrderBy(c => c.Name)
                        .AsEnumerable()
                        .Select(CreateCategory)
                        .ToArray();
            }
        }

        public CategoryGet GetCategory(string name)
        {
            var userId = GetUserId();

            ValidationHelper.ValidateCategoryName(name);

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var dbCategory = GetCategory(persistence, userId, name);

                return CreateCategory(dbCategory);
            }
        }

        public void CreateCategory(CategoryPost category)
        {
            var userId = GetUserId();

            if (category == null)
                throw new ValidationErrorException("Category must be specified.");
            Validator.CheckIsValid(category);

            var utcNow = Time.UtcNow;

            RethrowUniqueKeyException(
                "UK_Category",
                () => new ValidationErrorException("Category with '{0}' name already exists.", category.Name),
                () =>
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
                        persistence.SaveChanges();
                    }
                });
        }

        public void UpdateCategory(string name, CategoryPut category)
        {
            var userId = GetUserId();
            
            ValidationHelper.ValidateCategoryName(name);

            if (category == null)
                throw new ValidationErrorException("Category must be specified.");
            Validator.CheckIsValid(category);

            var utcNow = Time.UtcNow;

            RethrowUniqueKeyException(
                "UK_Category",
                () => new ValidationErrorException("Category with '{0}' name already exists.", category.Name),
                () =>
                {
                    using (var persistence = Container.Get<IPersistenceService>())
                    {
                        var dbCategory = GetCategory(persistence, userId, category.Name);

                        dbCategory.Name = category.Name;
                        dbCategory.Type = category.Type;
                        dbCategory.ChangeDate = utcNow;
                        
                        persistence.SaveChanges();
                    }
                });
        }

        public void DeleteCategory(string name)
        {
            var userId = GetUserId();

            ValidationHelper.ValidateCategoryName(name);

            using (var persistence = Container.Get<IPersistenceService>())
            {
                var dbCategory = GetCategory(persistence, userId, name);
                if (dbCategory.Transactions.Any())
                    throw new ValidationErrorException("Category cannot be deleted because it has transaction.");

                persistence.GetEntitySet<Category>().Remove(dbCategory);
                persistence.SaveChanges();
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

        private static Category GetCategory(IPersistenceService persistence, int userId, string name)
        {
            var category =
                    persistence.GetEntitySet<Category>()
                        .SingleOrDefault(c => c.UserId == userId && c.Name == name);
            if (category == null)
                throw new ValidationErrorException("Category with '{0}' name does not exist.", name);

            return category;
        }

        #endregion
    }
}
