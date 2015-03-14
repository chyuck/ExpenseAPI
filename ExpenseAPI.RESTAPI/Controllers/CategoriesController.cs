using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task<HttpResponseMessage> GetCategoriesAsync()
        {
            return await ExecuteAsync(async () =>
            {
                var categoryService = Container.Get<ICategoryService>();

                return await categoryService.GetCategoriesAsync();
            });
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<HttpResponseMessage> GetCategoryAsync([FromUri]string name)
        {
            return await ExecuteAsync(async () =>
            {
                var categoryService = Container.Get<ICategoryService>();

                return await categoryService.GetCategoryAsync(name);
            });
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateCategoryAsync([FromBody]CategoryPost category)
        {
            return await ExecuteAsync(async () =>
            {
                var categoryService = Container.Get<ICategoryService>();

                await categoryService.CreateCategoryAsync(category);

                return new Information { Message = "Category created." };
            });
        }

        [HttpPut]
        [HttpPatch]
        [Route("{name}")]
        public async Task<HttpResponseMessage> UpdateCategoryAsync([FromUri]string name, [FromBody]CategoryPut category)
        {
            return await ExecuteAsync(async () =>
            {
                var categoryService = Container.Get<ICategoryService>();

                await categoryService.UpdateCategoryAsync(name, category);

                return new Information { Message = "Category updated." };
            });
        }

        [HttpDelete]
        [Route("{name}")]
        public async Task<HttpResponseMessage> DeleteCategoryAsync([FromUri]string name)
        {
            return await ExecuteAsync(async () =>
            {
                var categoryService = Container.Get<ICategoryService>();

                await categoryService.DeleteCategoryAsync(name);

                return new Information { Message = "Category deleted." };
            });
        }
    }
}
