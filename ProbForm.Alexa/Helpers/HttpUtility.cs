using Newtonsoft.Json;
using ProbForm.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProbForm.Alexa.Helpers
{
    public static class HttpUtility
    {
        public static async Task<List<Match>> GetMatches()
        {
            var client = new HttpClient();
            var jsonMatches = await client.GetStringAsync(Helpers.UrlKeys.GET_LIST_MATCHES);
            var matches = JsonConvert.DeserializeObject<List<Match>>(jsonMatches);
            return matches;
        }
        public static async Task<List<Match>> GetFullMatches()
        {
            var client = new HttpClient();
            var jsonMatches = await client.GetStringAsync(Helpers.UrlKeys.GET_ALL_MATCHES);
            var matches = JsonConvert.DeserializeObject<List<Match>>(jsonMatches);
            return matches;
        }
        public static async Task<Match> GetMatch(string team1 = " ", string team2 = " ", string day = "0")
        {
            var client = new HttpClient();
            var jsonMatch = await client.GetStringAsync(string.Format(Helpers.UrlKeys.GET_MATCH, team1, team2));
            var match = JsonConvert.DeserializeObject<Match>(jsonMatch);
            return match;
        }

        public static async Task<List<TeamPlayer>> GetTeamPlayers(string player)
        {
            var client = new HttpClient();
            var jsonPlayers = await client.GetStringAsync(string.Format(Helpers.UrlKeys.GET_TEAMPLAYERS, player));
            var players = JsonConvert.DeserializeObject<List<TeamPlayer>>(jsonPlayers);
            return players;
        }

        public static async Task<List<Team>> GetTeams()
        {
            var client = new HttpClient();
            var jsonTeams = await client.GetStringAsync(Helpers.UrlKeys.GET_TEAMS);
            var teams = JsonConvert.DeserializeObject<List<Team>>(jsonTeams);
            return teams;

        }


    }
}
