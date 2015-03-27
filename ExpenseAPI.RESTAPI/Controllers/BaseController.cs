using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DIContainer;
using ExpenseAPI.BusinessLogic;
using ExpenseAPI.Common.Helpers;
using ExpenseAPI.Models;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using Validator;

namespace ExpenseAPI.RESTAPI.Controllers
{
    public abstract class BaseController : ApiController
    {
        private readonly Lazy<IReadOnlyContainer> _container = new Lazy<IReadOnlyContainer>(DIContainerFactory.Create);

        protected IReadOnlyContainer Container
        {
            get { return _container.Value; }
        }

        protected async Task<HttpResponseMessage> ExecuteAsync<TResponse>(Func<Task<TResponse>> action)
        {
            Checker.ArgumentIsNull(action, "action");

            try
            {
                var serviceUser = User as ServiceUser;
                if (serviceUser == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "User is not authorized.");
                
                var userService = Container.Get<IUserService>();

                using (userService.LogIn(serviceUser.Id, true))
                {
                    var response = await action();

                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (ValidationException ex)
            {
                var errors =
                    ex.Errors.Errors
                        .Select(e => new Error { Message = e.Message })
                        .ToArray();

                return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
            }
            catch (ValidationErrorException ex)
            {
                var error = new Error { Message = ex.Message };

                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }
            catch (Exception ex)
            {
                var error = new Error { Message = ex.Message };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, error);
            }
        }
    }
}