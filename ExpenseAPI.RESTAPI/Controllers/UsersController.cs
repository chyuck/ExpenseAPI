using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ExpenseAPI.BusinessLogic;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace ExpenseAPI.RESTAPI.Controllers
{
    [RoutePrefix("users")]
    [AuthorizeLevel(AuthorizationLevel.Admin)]
    public class UsersController : BaseController
    {
        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetUsersAsync()
        {
            return await ExecuteAsync(async () =>
            {
                var userService = Container.Get<IUserService>();

                return await userService.GetUsersAsync();
            });
        }
    }
}