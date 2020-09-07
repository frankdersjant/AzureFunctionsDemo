using FunctionAppDurableFunctionsOrder.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Threading.Tasks;

namespace FunctionAppDurableFunctionsOrder
{
    public static class ProcesssOrderOrchestrator
    {
        [FunctionName("O_ProcessOrder")]
        public static async Task<Object> ProcessOrder([OrchestrationTrigger] IDurableOrchestrationContext ctx)
        {
            Order order = ctx.GetInput<Order>();
            try
            {

                var orderSavedLocation = await ctx.CallActivityWithRetryAsync<string>("A_OrderCalcCommision", new RetryOptions(TimeSpan.FromSeconds(5), 4), order);

                return new
                {
                    OrderSaved = orderSavedLocation
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    Error = "Epic Fail",
                    Message = ex.Message
                };
            }
        }
    }
}
