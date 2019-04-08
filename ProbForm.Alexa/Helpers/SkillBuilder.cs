using Alexa.NET;
using Alexa.NET.Response;
using ProbForm.Alexa.Resources;
using ProbForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbForm.Alexa.Helpers
{
    public static class SkillBuilder
    {


        public static SkillResponse BuildInvalidSkillResponse()
        {
            var text = ResourceLoader.Current.GetResource("QueryDateParsingFailedErrorMessage");
            var title = ResourceLoader.Current.GetResource("QueryDateParsingFailedErrorTitle");

            return ResponseBuilder.TellWithCard(text, title, text);
        }
        public static SkillResponse BuildLaunchSkillResponse()
        {
            var title = ResourceLoader.Current.GetResource("LaunchRequestMessage");

            var text = GetTutorialText();

            text = title + text;

            return ResponseBuilder.TellWithCard(text, title, text);
        }

        public static SkillResponse BuildPlayersSkillResponse(List<TeamPlayer> players)
        {
            var pluralize = (players.Count == 1) ? 'e' : 'i';
            var title = $"Ho trovato {players.Count} giocator{pluralize}";
            var text = "";
            foreach (var player in players)
            {
                var predicate =
                text += $"{player.Player.Name}, {player.Player.TeamId} è {adjustStatus(player.Status)}.\n";
            }
            return ResponseBuilder.TellWithCard(text, title, text);
        }
        private static string adjustStatus(StatusPlayer status)
        {
            switch (status)
            {
                case StatusPlayer.ALTRO:
                    return "non convocato";
                case StatusPlayer.INDISPONIBILE:
                    return "indisponibile";
                case StatusPlayer.PANCHINA:
                    return "in panchina";
                default:
                    return status.ToString().ToLower();
            }
        }
        public static SkillResponse BuildMatchesSkillResponse(List<Match> matches, int day = 0)
        {
            var title = $"Ecco le partite della giornata {day}";
            var text = "";
            foreach (var match in matches.Select((item, i) => (item, i)))
            {
                text += $"{match.i}. {match.item.HomeTeam.Name} {match.item.AwayTeam.Name}";
            }
            text = title + text;
            return ResponseBuilder.TellWithCard(text, title, text);
        }
        public static SkillResponse BuildMatchSkillResponse(Match match)
        {
            var title = $"Ecco le formazioni di {match.HomeTeam.Name} {match.AwayTeam.Name}";
            var text = "";
            text += $"{match.HomeTeam.Name} si schiera con il {match.HomeModule}.  ";
            foreach (var teamPlayer in match.HomeTeam.Players
                                        .Where(x => x.Status == StatusPlayer.TITOLARE)
                                        .OrderBy(x => x.Order))
            {
                text += $"{teamPlayer.Player.Name}, ";
            }
            text += $"{match.AwayTeam.Name} si schiera con il {match.AwayModule}.  ";
            foreach (var teamPlayer in match.AwayTeam.Players
                                        .Where(x => x.Status == StatusPlayer.TITOLARE)
                                        .OrderBy(x => x.Order))
            {
                text += $"{teamPlayer.Player.Name}, ";
            }
            text = text.Substring(0, text.Length - 2);
            text = title + text;
            return ResponseBuilder.TellWithCard(text, title, text);
        }

        public static SkillResponse BuildTutorialResponse()
        {

            var text = GetTutorialText();
            return ResponseBuilder.TellWithCard(text, ResourceLoader.Current.GetResource("HelpIntentTitle"), text);
        }

        private static string GetTutorialText()
        {
            var tutorialText1 = ResourceLoader.Current.GetResource("TutorialText1");
            var tutorialText2 = ResourceLoader.Current.GetResource("TutorialText2");

            return tutorialText1 + " " + tutorialText2;
        }
    }
}
