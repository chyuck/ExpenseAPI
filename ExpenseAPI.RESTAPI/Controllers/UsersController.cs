using System.Net.Http;
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
        public HttpResponseMessage GetUsers()
        {
            return Execute(() =>
            {
                var userService = Container.Get<IUserService>();

                return userService.GetUsers();
            });
        }
    }
}