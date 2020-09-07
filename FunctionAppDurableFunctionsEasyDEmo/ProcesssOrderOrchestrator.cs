using FunctionAppDurableFunctionsEasyDemo;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FunctionAppDurableFunctionsEasyDEmo
{
    public static class ProcesssOrderOrchestrator
    {
        [FunctionName("O_ProcessOrder")]
        public static async Task<Object> ProcessOrder([OrchestrationTrigger] IDurableOrchestrationContext ctx, ILogger log)
        {
            try
            {
                //the actual Uri where the order is checked
                //if (!ctx.IsReplaying)
                //    log.LogInformation("Calling my Orders Overlord");

                var checkOrderLocation = await ctx.CallActivityAsync<string>("A_CheckOrder", "tst");

                //if (!ctx.IsReplaying)
                //    log.LogInformation("Calling my Orders Calc method");
                var calcOrderLocation = await ctx.CallActivityAsync<string>("A_CalcOrder", checkOrderLocation);

                //if (!ctx.IsReplaying)
                //    log.LogInformation("Im trying Ok?");
                var severalRetriesLaterLocation = await ctx.CallActivityWithRetryAsync<string>("A_Retry", new RetryOptions(TimeSpan.FromSeconds(5), 4), calcOrderLocation);

                return new
                {
                    CheckOrderLocation = checkOrderLocation,
                    CalcOrderLocation = calcOrderLocation,
                    Retried = severalRetriesLaterLocation
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
