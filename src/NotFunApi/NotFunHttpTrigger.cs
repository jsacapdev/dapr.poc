using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace NotFunApi
{
    public static class NotFunHttpTrigger
    {
        [FunctionName(Constants.NotFunHttpTrigger)]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            await Task.Run(() => { });

            return new OkObjectResult(DateTime.UtcNow.ToString());
        }
    }
}
