using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ExpenseAPI.BusinessLogic;
using ExpenseAPI.Models;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace ExpenseAPI.RESTAPI.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    [RoutePrefix("categories/{category}/transactions")]
    public class TransactionController : BaseController
    {
        [HttpGet]
        [Route("~/transactions")]
        public async Task<HttpResponseMessage> GetTransactionsAsync(DateTime? from = null, DateTime? to = null)
        {
            return await ExecuteAsync(async () =>
            {
                var transactionService = Container.Get<ITransactionService>();

                return await transactionService.GetTransactionsAsync(from, to);
            });
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetTransactionsAsync([FromUri]string category, DateTime? from = null, DateTime? to = null)
        {
            return await ExecuteAsync(async () =>
            {
                var transactionService = Container.Get<ITransactionService>();

                return await transactionService.GetTransactionsAsync(category, from, to);
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetTransactionAsync([FromUri]string category, [FromUri]string id)
        {
            return await ExecuteAsync(async () =>
            {
                var transactionService = Container.Get<ITransactionService>();

                return await transactionService.GetTransactionAsync(category, id);
            });
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateTransactionAsync([FromUri]string category, [FromBody]TransactionPost transaction)
        {
            return await ExecuteAsync(async () =>
            {
                var transactionService = Container.Get<ITransactionService>();

                await transactionService.CreateTransactionAsync(category, transaction);

                return new Information { Message = "Transaction created." };
            });
        }

        [HttpPut]
        [HttpPatch]
        [Route("{id}")]
        public async Task<HttpResponseMessage> UpdateTransactionAsync([FromUri]string category, [FromUri]string id, [FromBody]TransactionPut transaction)
        {
            return await ExecuteAsync(async () =>
            {
                var transactionService = Container.Get<ITransactionService>();

                await transactionService.UpdateTransactionAsync(category, id, transaction);

                return new Information { Message = "Transaction updated." };
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<HttpResponseMessage> DeleteTransactionAsync([FromUri]string category, [FromUri]string id)
        {
            return await ExecuteAsync(async () =>
            {
                var transactionService = Container.Get<ITransactionService>();

                await transactionService.DeleteTransactionAsync(category, id);

                return new Information { Message = "Transaction deleted." };
            });
        }
    }
}