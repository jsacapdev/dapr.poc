using System;
using DaprPoc.FunctionApi.Interface;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;

[assembly: FunctionsStartup(typeof(DaprPoc.FunctionApi.Startup))]

namespace DaprPoc.FunctionApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var port = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");

            string baseUri = $"http://localhost:{port}/";

            builder.Services.AddRefitClient<IBankingClient>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUri));
        }
    }
}
