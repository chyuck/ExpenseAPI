using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ExpenseAPI.BusinessLogic;
using ExpenseAPI.Models;
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
            try
            {
                var userService = Container.Get<IUserService>();

                var response = await userService.GetUsersAsync();

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var error = new Error { Message = CreateErrorMessage(ex) };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, error);
            }
        }
    }
}