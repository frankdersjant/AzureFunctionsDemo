using FunctionAppDurableFunctionsEasyDemo;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FunctionAppDurableFunctionsEasyDEmo
{
    public static class ProcessOrderActvities
    {
        [FunctionName("A_OrderCalcCommision")]
        public static async Task<Order> CalcCommision([ActivityTrigger] Order order, ILogger log)
        {
            log.LogInformation("Starting oderdate calculation");

            decimal commission = order.Price * 1.2m;
            return order;
        }

        [FunctionName("A_CheckOrder")]
        public static async Task<string> CheckOrder([ActivityTrigger] string OrderDate, ILogger log)
        {
            log.LogInformation("Starting oderdate calculation");

            return "order date checked";
        }

        [FunctionName("A_CalcOrder")]
        public static async Task<string> CalcOrder([ActivityTrigger] string OrderDate, ILogger log)
        {
            log.LogInformation("Starting order calculation");
           
            return "order calculated";
        }

        [FunctionName("A_Retry")]
        public static async Task<string> Retry([ActivityTrigger] string OrderDate, ILogger log)
        {
            return "At least I tried";
        }
    }

}