using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionAppChaining
{
    public static class SendMail
    {
        [FunctionName("SendMail")]
        public static void Run([QueueTrigger("orders", Connection = "")] string myQueueItem, ILogger log)
        {
            log.LogInformation("Send EMail via SendGrid");
        }
    }
}
