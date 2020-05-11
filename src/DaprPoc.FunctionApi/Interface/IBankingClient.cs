using System.Threading.Tasks;
using DaprPoc.FunctionApi.Model;
using Refit;

namespace DaprPoc.FunctionApi.Interface
{
    public interface IBankingClient
    {
        [Post("/v1.0/invoke/bankingapi/method/deposit")]
        Task<Account> Deposit([Body] Transaction transaction);
    }
}