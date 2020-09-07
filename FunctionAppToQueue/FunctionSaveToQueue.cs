using BusinessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FunctionAppToQueue
{
    //Microsoft.Azure.WebJobs.Extensions.Storage

    //    postman:
    //    {
    //    "orderid" : "5678"
    //}
    public static class FunctionSaveToQueue
    {
        [FunctionName("SaveOrder")]

        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Queue("demoqueue")] IAsyncCollector<OrderDTO> applicationQueue,
            ILogger log)
        {
            var requetsbody = await new StreamReader(req.Body).ReadToEndAsync();
            var myOrder = JsonConvert.DeserializeObject<OrderDTO>(requetsbody);

            try
            {
                await applicationQueue.AddAsync(myOrder);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
            return (IActionResult)new OkObjectResult("Your Order is Placed");
        }
    }
}
