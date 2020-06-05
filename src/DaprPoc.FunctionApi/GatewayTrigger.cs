using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DaprPoc.FunctionApi.Interface;
using DaprPoc.FunctionApi.Model;

namespace DaprPoc.FunctionApi
{
    public class GatewayTrigger
    {
        private readonly IBankingClient _bankingClient;

        private readonly ILogger<GatewayTrigger> _logger;

        public GatewayTrigger(
            IBankingClient bankingClient,
            ILogger<GatewayTrigger> logger)
        {
            _bankingClient = bankingClient;

            _logger = logger;
        }

        [FunctionName(Constants.GatewayTrigger)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] Transaction transaction)
        {
            var account = await _bankingClient.Deposit(transaction);

            _logger.LogInformation("Submitted transaction OK");

            return new OkObjectResult(account);
        }
    }
}
