using AzureFunctions.Autofac;
using DAL;
using FunctionAppProductsGet.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FunctionAppProductsGet
{
    [DependencyInjectionConfig(typeof(DIConfiguration))]
    public static class ProductsGet
    {
        [FunctionName("ProductsGet")]
        public static async Task<IActionResult> Run([Inject] IDB fakeDB,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a get request - getting all products.");

            var json = JsonConvert.SerializeObject(new
            {
                products = fakeDB.GetProducts()
            });

            return (ActionResult)new OkObjectResult(json);
        }
    }
}
