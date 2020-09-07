using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BusinessLayer;

namespace FunctionAppEyeBinder
{
    public static class Function1
    {
        [FunctionName("FuncIBinder")]
            public static async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
           IBinder binder,
           ILogger log)
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                ToDoItem item = JsonConvert.DeserializeObject<ToDoItem>(requestBody);
                item.Id = Guid.NewGuid().ToString();

                BlobAttribute dynamicBlobBinding = new BlobAttribute(blobPath: $"todo/{item.Category}/{item.Id}");

                using (var writer = binder.Bind<TextWriter>(dynamicBlobBinding))
                {
                    writer.Write(JsonConvert.SerializeObject(item));
                }
                var responseMessage = "Ok added" + item.Id;

                return new OkObjectResult(responseMessage);
            }
        }
        log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
