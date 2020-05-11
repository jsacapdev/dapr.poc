using DaprPoc.BankingApi.Model;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace DaprPoc.BankingApi.Controllers
{

    /// <summary>
    /// Sample showing Dapr integration with controller.
    /// </summary>
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// State store name.
        /// </summary>
        public const string StoreName = "statestore";

        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets the account information as specified by the id.
        /// </summary>
        /// <param name="account">Account information for the id from Dapr state store.</param>
        /// <returns>Account information.</returns>
        [HttpGet("{account}")]
        public ActionResult<Account> Get([FromState(StoreName)] StateEntry<Account> account)
        {
            if (account.Value is null)
            {
                _logger.LogWarning($"Account with Id {account.Key} not found.");

                return this.NotFound();
            }

            _logger.LogInformation($"Account with Id {account.Key} and Amount {account.Value} returned to client OK.");

            return account.Value;
        }

        /// <summary>
        /// Method for depositing to account as specified in transaction.
        /// </summary>
        /// <param name="transaction">Transaction info.</param>
        /// <param name="daprClient">State client to interact with Dapr runtime.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Topic("deposit")]
        [HttpPost("deposit")]
        public async Task<ActionResult<Account>> Deposit(Transaction transaction, [FromServices] DaprClient daprClient)
        {
            var state = await daprClient.GetStateEntryAsync<Account>(StoreName, transaction.Id);

            state.Value ??= new Account() { Id = transaction.Id, };

            state.Value.Balance += transaction.Amount;

            await state.SaveAsync();

            _logger.LogInformation($"Transaction with Id {transaction.Id} and Amount {transaction.Amount} deposited OK.");

            return state.Value;
        }

        /// <summary>
        /// Method for withdrawing from account as specified in transaction.
        /// </summary>
        /// <param name="transaction">Transaction info.</param>
        /// <param name="daprClient">State client to interact with Dapr runtime.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Topic("withdraw")]
        [HttpPost("withdraw")]
        public async Task<ActionResult<Account>> Withdraw(Transaction transaction, [FromServices] DaprClient daprClient)
        {
            var state = await daprClient.GetStateEntryAsync<Account>(StoreName, transaction.Id);

            if (state.Value == null)
            {
                _logger.LogWarning($"Transaction with Id {transaction.Id} not found.");

                return this.NotFound();
            }

            state.Value.Balance -= transaction.Amount;

            await state.SaveAsync();

            _logger.LogInformation($"Transaction with Id {transaction.Id} and Amount {transaction.Amount} withdrawn OK.");

            return state.Value;
        }
    }
}
