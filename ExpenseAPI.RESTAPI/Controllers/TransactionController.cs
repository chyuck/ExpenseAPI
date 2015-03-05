using System.Net.Http;
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
        [Route("")]
        public HttpResponseMessage GetTransactions([FromUri]string category)
        {
            return Execute(() =>
            {
                var transactionService = Container.Get<ITransactionService>();

                return transactionService.GetTransactions(category);
            });
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetTransaction([FromUri]string category, [FromUri]string id)
        {
            return Execute(() =>
            {
                var transactionService = Container.Get<ITransactionService>();

                return transactionService.GetTransaction(category, id);
            });
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateTransaction([FromUri]string category, [FromBody]TransactionPost transaction)
        {
            return Execute(() =>
            {
                var transactionService = Container.Get<ITransactionService>();

                transactionService.CreateTransaction(category, transaction);

                return new Information { Message = "Transaction created." };
            });
        }

        [HttpPut]
        [HttpPatch]
        [Route("{id}")]
        public HttpResponseMessage UpdateTransaction([FromUri]string category, [FromUri]string id, [FromBody]TransactionPut transaction)
        {
            return Execute(() =>
            {
                var transactionService = Container.Get<ITransactionService>();

                transactionService.UpdateTransaction(category, id, transaction);

                return new Information { Message = "Transaction updated." };
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteTransaction([FromUri]string category, [FromUri]string id)
        {
            return Execute(() =>
            {
                var transactionService = Container.Get<ITransactionService>();

                transactionService.DeleteTransaction(category, id);

                return new Information { Message = "Transaction deleted." };
            });
        }
    }
}