using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ProbForm.TimeTrigger
{
    public static class ScrapProbForm
    {
        [FunctionName("ScrapProbForm")]
        public static void Run([TimerTrigger("0 0 12 1/1 * ? *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
