using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using ProbForm.Services;
using ProbForm.AppContext;
namespace ProbForm.TimeTrigger
{
    public static class ProbFormTrigger
    {
        private static ILineupsService lineupService;





        [FunctionName("ProbFormTrigger")]
        public async static void Run([TimerTrigger("0 0 12 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"Timer trigger function executed at: {DateTime.Now}");
            try
            {
                lineupService = new GazzettaLineupService();
                var matches = await lineupService.Matches();
                if (matches == null)
                {
                    log.Info("Error in downloading matches. Matches are null");
                    return;
                }
                log.Info($"{matches.Count} matches downloaded.");
                matches.ForEach(async (m) =>
                   await AppDBHelper.InsertOrUpdate(m));
                log.Info("Matches saved");
            }
            catch (Exception ex)
            {
                log.Error("Error in downloading matches.", ex);
            }

        }
    }
}
