using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DIContainer;
using ExpenseAPI.BusinessLogic;
using ExpenseAPI.Common.Helpers;
using ExpenseAPI.Models;
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

        protected HttpResponseMessage Execute<TResponse>(Func<TResponse> action)
        {
            Checker.ArgumentIsNull(action, "action");

            try
            {
                var userService = Container.Get<IUserService>();

                using (userService.LogIn(UserName))
                {
                    var response = action();

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
            catch (Exception ex)
            {
                var error = new Error { Message = ex.Message };

                return Request.CreateResponse(HttpStatusCode.InternalServerError, error);
            }
        }

        protected string UserName
        {
            get { return "chyuck"; }
        }
    }
}