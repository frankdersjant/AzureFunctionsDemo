using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace FunctionAppBinding
{
    public static class Function1
    {
        [FunctionName("FunctionBinding")]
        public static async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
           ILogger log,
           IBinder binder)
        {
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Order Orderdata = JsonConvert.DeserializeObject<Order>(requestBody);
                Orderdata.OrderName = Orderdata.OrderName + "-received";

                BlobAttribute dynamicBlobBinding = new BlobAttribute(blobPath: $"orders/{Orderdata.OrderId}/{Orderdata.OrderName}");

                using (var writer = binder.Bind<TextWriter>(dynamicBlobBinding))
                {
                    writer.Write(JsonConvert.SerializeObject(Orderdata));
                }
                return new OkResult();
            }
        }
    }
}
