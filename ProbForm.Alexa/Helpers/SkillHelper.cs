using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProbForm.Alexa.Helpers
{
    class SkillHelper
    {
        public static SkillResponse elicitSlot(string elicitSpeach, string slot)
        {
            PlainTextOutputSpeech speech = new PlainTextOutputSpeech();
            speech.Text = elicitSpeach;
            return ResponseBuilder.DialogElicitSlot(speech, slot);
        }

        public static async Task<bool> ValidateRequest(HttpRequest request, SkillRequest skillRequest)
        {
            request.Headers.TryGetValue("SignatureCertChainUrl", out var signatureChainUrl);
            if (string.IsNullOrWhiteSpace(signatureChainUrl))
            {
                return false;
            }

            Uri certUrl;
            try
            {
                certUrl = new Uri(signatureChainUrl);
            }
            catch
            {
                return false;
            }

            request.Headers.TryGetValue("Signature", out var signature);
            if (string.IsNullOrWhiteSpace(signature))
            {
                return false;
            }

            request.Body.Position = 0;
            var body = await request.ReadAsStringAsync();
            request.Body.Position = 0;

            if (string.IsNullOrWhiteSpace(body))
            {
                return false;
            }

            bool valid = await RequestVerification.Verify(signature, certUrl, body);
            bool isTimestampValid = RequestVerification.RequestTimestampWithinTolerance(skillRequest);

            if (!isTimestampValid)
            {
                valid = false;
            }

            return valid;
        }
    }
}
