using FunctionAppDurableFunctionsOrder.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FunctionAppDurableFunctionsOrder
{
    public static class ProcessOrderActvities
    {
        [FunctionName("A_OrderCalcCommision")]
        public static async Task<Order> CalcCommision([ActivityTrigger] Order order, ILogger log)
        {
            log.LogInformation("Starting order commision calculation");

            decimal commission = order.Price * 1.2m;
            return order;
        }
    }
}
