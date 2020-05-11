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

        [FunctionName("GatewayTrigger")]
        public async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            try
            {
                var transaction = new Transaction { Id = "17", Amount = 10 };

                var account = await _bankingClient.Deposit(transaction);

                _logger.LogInformation("Submitted transaction OK");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
