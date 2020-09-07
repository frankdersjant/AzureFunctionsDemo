using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionAppCosmos
{
    public static class FuncOrders
    {
        [FunctionName("Orders")]
        public static async Task<IActionResult> Run(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
                [CosmosDB("Orders", "OrdersFDE", ConnectionStringSetting = "CosmosDB", SqlQuery = "SELECT * FROM OrdersFDE")] IEnumerable<Order> Orders,
                ILogger log)
        {
            return new OkObjectResult(Orders);
        }
    }
}
