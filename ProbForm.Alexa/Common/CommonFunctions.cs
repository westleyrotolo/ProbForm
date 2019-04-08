using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Newtonsoft.Json;
using ProbForm.Alexa.Helpers;
using ProbForm.Alexa.Resources;
using ProbForm.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace ProbForm.Alexa.Common
{
    public class CommonFunctions
    {
        public static async Task<SkillResponse> ExecuteAsync(SkillRequest skillRequest)
        {
            if (skillRequest == null)
            {
                throw new ArgumentNullException(nameof(skillRequest));
            }

            if (skillRequest.Request == null)
            {
                throw new ArgumentNullException(nameof(skillRequest) + "." + nameof(SkillRequest.Request));
            }

            switch (skillRequest.Request)
            {
                case var request when request is LaunchRequest launchRequest:
                    {
                        return SkillBuilder.BuildLaunchSkillResponse();
                    }
                case var request when request is IntentRequest intentRequest:
                    {
                        if (intentRequest.Intent == null)
                        {
                            throw new ArgumentNullException(nameof(intentRequest) + "." + nameof(IntentRequest.Intent));
                        }
                        switch (intentRequest.Intent.Name)
                        {
                            case SkillIntent.HELPINTENT:
                                {
                                   return SkillBuilder.BuildTutorialResponse();
                                }
                            case SkillIntent.PLAYERS:
                                {
                                    if (!intentRequest.Intent.Slots.TryGetValue(SkillSlots.PLAYER, out Slot slot))
                                    {
                                        return SkillBuilder.BuildInvalidSkillResponse();
                                    }
                                    var players = await HttpUtility.GetTeamPlayers(slot.Value);
                                    if (players == null || players.Count == 0)
                                    {
                                        // implement specific response
                                        return SkillBuilder.BuildInvalidSkillResponse();
                                    }
                                    return SkillBuilder.BuildPlayersSkillResponse(players);
                                }
                            case SkillIntent.MATCHES:
                                {
                                    var matches = await HttpUtility.GetMatches();
                                    return SkillBuilder.BuildMatchesSkillResponse(matches);
                                 
                                }
                            case SkillIntent.MATCH:
                                {
                               
                                    string firstTeam = intentRequest.Intent.Slots.TryGetValue(SkillSlots.FIRSTTEAM, out Slot slotFirstTeam) 
                                                        ? slotFirstTeam.Value 
                                                        : string.Empty;
                                    string secondTeam = intentRequest.Intent.Slots.TryGetValue(SkillSlots.SECONDTEAM, out Slot slotSecondTeam)
                                                        ? slotSecondTeam.Value
                                                        : string.Empty;
                                    string day = intentRequest.Intent.Slots.TryGetValue(SkillSlots.DAY, out Slot slotDay)
                                                    ? slotDay.Value
                                                    : "0";
                                                
                                    if (string.IsNullOrEmpty(firstTeam) && string.IsNullOrEmpty(secondTeam))
                                    {
                                        return SkillBuilder.BuildInvalidSkillResponse();
                                    }
                                    var match = await HttpUtility.GetMatch(firstTeam, secondTeam, day);
                                    return SkillBuilder.BuildMatchSkillResponse(match);
                                }
                        }
                        return ResponseBuilder.Tell(ResourceLoader.Current.GetResource("NotImplementedErrorMessage"));
                    }
                case var request when request is SessionEndedRequest sessionEndedRequest:
                    {
                        return ResponseBuilder.Tell(ResourceLoader.Current.GetResource("SessionEndedMessage"));
                    }

            }
        
            return ResponseBuilder.Tell(ResourceLoader.Current.GetResource("UnknownRequestErrorMessage"));
            
        }

        
    }
}
