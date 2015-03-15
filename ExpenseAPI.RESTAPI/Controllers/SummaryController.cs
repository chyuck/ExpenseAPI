using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ExpenseAPI.BusinessLogic;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace ExpenseAPI.RESTAPI.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class SummaryController : BaseController
    {
        [HttpGet]
        [Route("~/transactions/summary")]
        public async Task<HttpResponseMessage> GetTransactionsAsync(DateTime? from = null, DateTime? to = null)
        {
            return await ExecuteAsync(async () =>
            {
                var summaryService = Container.Get<ISummaryService>();

                return await summaryService.GetSummaryAsync(from, to);
            });
        }
    }
}