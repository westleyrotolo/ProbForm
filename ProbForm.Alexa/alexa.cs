
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net;
using Alexa.NET.Request;
using System.Text;
using ProbForm.Alexa.Common;
using System.Threading.Tasks;

namespace ProbForm.Alexa
{
    public static class alexa
    {
        [FunctionName("alexa")]
        public async static Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            if (req == null)
            {   
                throw new ArgumentNullException(nameof(req));
            }

            if (req.Body == null)
            {
                throw new ArgumentNullException(nameof(req) + "." + nameof(HttpRequestMessage.Content));
            }

            var json = await req.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Invalid or empty JSON")
                };
            }

            var skillRequest = JsonConvert.DeserializeObject<SkillRequest>(json);

            var skillResponse = await CommonFunctions.ExecuteAsync(skillRequest);

            json = JsonConvert.SerializeObject(skillResponse);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }
    }
}
