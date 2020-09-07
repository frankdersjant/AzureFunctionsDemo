using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionAppQueTrigger
{
    public static class Function1
    {

        [FunctionName("QTrigger")]
        public static void Run([QueueTrigger("demoqueue")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
