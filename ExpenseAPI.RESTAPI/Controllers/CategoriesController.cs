using System.Net.Http;
using System.Web.Http;
using ExpenseAPI.BusinessLogic;
using ExpenseAPI.Models;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace ExpenseAPI.RESTAPI.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    [RoutePrefix("categories")]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetCategories()
        {
            return Execute(() =>
            {
                var categoryService = Container.Get<ICategoryService>();

                return categoryService.GetCategories();
            });
        }

        [HttpGet]
        [Route("{name}")]
        public HttpResponseMessage GetCategory([FromUri]string name)
        {
            return Execute(() =>
            {
                var categoryService = Container.Get<ICategoryService>();

                return categoryService.GetCategory(name);
            });
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateCategory([FromBody]CategoryPost category)
        {
            return Execute(() =>
            {
                var categoryService = Container.Get<ICategoryService>();

                categoryService.CreateCategory(category);

                return new Information { Message = "Category created." };
            });
        }

        [HttpPut]
        [HttpPatch]
        [Route("{name}")]
        public HttpResponseMessage UpdateCategory([FromUri]string name, [FromBody]CategoryPut category)
        {
            return Execute(() =>
            {
                var categoryService = Container.Get<ICategoryService>();

                categoryService.UpdateCategory(name, category);

                return new Information { Message = "Category updated." };
            });
        }

        [HttpDelete]
        [Route("{name}")]
        public HttpResponseMessage DeleteCategory([FromUri]string name)
        {
            return Execute(() =>
            {
                var categoryService = Container.Get<ICategoryService>();

                categoryService.DeleteCategory(name);

                return new Information { Message = "Category deleted." };
            });
        }
    }
}
