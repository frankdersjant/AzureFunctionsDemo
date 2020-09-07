using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionAppDurableFunctionsOrder.Models;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace FunctionAppDurableFunctionsOrder
{
    public static class StarterFunction
    {
        [FunctionName("StarterFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log, 
            [DurableClient] IDurableOrchestrationClient starter)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var order = JsonConvert.DeserializeObject<Order>(requestBody);

            log.LogInformation("Starting the orcestration");

            //start orchestration
            var orchestrationId = await starter.StartNewAsync("O_ProcessOrder", order);

            return new OkResult();

        }
    }
}
