using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ProbForm.Models;
using Match = ProbForm.Models.Match;
using System.Globalization;
namespace ProbForm.ConsoleApplication.Services
{
    public class GazzettaLineupService : ILineupsService
    {
        public GazzettaLineupService()
        {

        }

        public async Task<string> FetchData()
        {
#if DEBUG
            var file =
                Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Mock", "prob_form.html");
            using (var reader = File.OpenText(file))
            {
                return await reader.ReadToEndAsync();
            }

#else
            string page = "https://www.gazzetta.it/Calcio/prob_form/?refresh_ce";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                try
                {
                    return await content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
#endif
        }
        List<string> secondaryPlayer = new List<string>
          {
                    "Panchina",
                    "Squalificati",
                    "Indisponibili",
                    "Altri"

          };
        public async Task<List<Match>> Matches()
        {
            var matches = new List<Match>();
            HtmlDocument _doc = new HtmlDocument();
            string html = await FetchData();
            _doc.LoadHtml(html);

            var matchField = _doc.DocumentNode.Descendants("div")
                    .Where(d => d.GetAttributeValue("class", "")
                        .Contains("matchFieldContainer"));

            foreach (var match in matchField)
            {
                var Match = new Match();
                var HomeTeam = new Team();
                var AwayTeam = new Team();
                Match.MatchTime = MatchDateTime(match);

                HomeTeam.Name = TeamName(match);
                AwayTeam.Name = TeamName(match, false);

                HomeTeam.Module = Module(match);
                AwayTeam.Module = Module(match, false);

                HomeTeam.Mister = Mister(match);
                AwayTeam.Mister = Mister(match, false);

                HomeTeam.Players = PrincipalTeamsPlayer(match);
                AwayTeam.Players = PrincipalTeamsPlayer(match, false);
                secondaryPlayer.ForEach((x) => {
                    HomeTeam.Players.AddRange(SecondaryTeamPlayer(match, x));
                    AwayTeam.Players.AddRange(SecondaryTeamPlayer(match, x, false));
                });

                Match.HomeTeam = HomeTeam;
                Match.AwayTeam = AwayTeam;
                matches.Add(Match);

            }

            return matches;
        }
        public int DayMatch(HtmlNode html)
        {
            return int.Parse(Regex.Match(html.Descendants("div")
                    .First(x => x.GetAttributeValue("class", "")
                    .Contains("mainHeading")).Descendants("h3")
                    .First().InnerText, @"\d+").Value);
        }
        public string TeamName(HtmlNode html, bool home = true)
        {
            return html.Descendants("div")
                    .First(x => x.GetAttributeValue("class", "")
                    .Contains("match")).Descendants("div")
                    .First(x => x.GetAttributeValue("class", "")
                    .Contains(home ? "homeTeam" : "awayTeam"))
                    .InnerText.Trim();
        }
        public string Module(HtmlNode html, bool home = true)
        {
            return html.Descendants("div")
                    .First(x => x.GetAttributeValue("class", "")
                    .Contains(home ? "homeModule" : "awayModule"))
                    .Descendants("span")
                    .First(x => x.GetAttributeValue("class", "")
                    .Contains("modulo")).InnerText.Trim();
        }
        public DateTime MatchDateTime(HtmlNode html)
        {
            //Domenica 3 marzo - Ore 18
            return DateTime.ParseExact( html.Descendants("div")
                    .First(x => x.GetAttributeValue("class", "")
                    .Contains("matchDateTime"))
                    .InnerText.Trim().Replace(" - Ore", "")
                    ,new[] { "dddd d MMMM HH", "dddd d MMMM HH:mm" },CultureInfo.GetCultureInfo("it-IT") );
        }
        public string Mister(HtmlNode html, bool home = true)
        {
            return html.Descendants("div")
                   .First(x => x.GetAttributeValue("class", "")
                   .Contains(home ? "homeModule" : "awayModule"))
                   .Descendants("span")
                   .First(x => x.GetAttributeValue("class", "")
                   .Contains("mister")).InnerText.Trim();
        }
        public List<TeamPlayer> PrincipalTeamsPlayer(HtmlNode html, bool home = true)
        {
            List<HtmlNode> players;
            if (home)
            {
                players = html.Descendants("ul")
                            .First(x => x.GetAttributeValue("class", "")
                            .Equals("team-players"))
                            .Descendants("li").ToList();
            }
            else
            {
                players = html.Descendants("ul")
                            .Last(x => x.GetAttributeValue("class", "")
                            .Equals("team-players"))
                            .Descendants("li").ToList();
            }

            return players.Select((x, i) =>
                new TeamPlayer
                {
                    player = new Player
                    {
                        Name = string.Join(' ', Regex.Matches(x.InnerText, @"[^0-9.]+")).Trim(),
                        Number = Regex.Match(x.InnerText, @"\d +").Value
                    },
                    Status = StatusPlayer.TITOLARE,
                    Order = i
                }).ToList();
        }

        /// <summary>
        /// Detailses the team player.
        /// </summary>
        /// <returns>The team player.</returns>
        /// <param name="html">Html.</param>
        /// <param name="type">Type Panchina, Indisponibili, Squalificati, Altri</param>
        /// <param name="home">If set to <c>true</c> home.</param>
        public List<TeamPlayer> SecondaryTeamPlayer(HtmlNode html, string type, bool home = true)
        {
            return TeamPlayerDetails(html, home)
                .First(x => x.InnerText.Contains($"{type}:"))
                .InnerText
                .Replace($"{type}:", "")
                .Replace("nessuno", "")
                .Replace("Nessuno", "")
                .Replace("&ensp;","")
                .Split(',').Select((x, i) =>
                 new TeamPlayer
                 {
                     player = new Player
                     {
                         Name = string.Join(' ', Regex.Matches(Regex.Replace(x, @"\((.|\n)*?\)", ""), @"[^0-9.]+")).Trim(),
                         Number = Regex.Match(Regex.Replace(x, @"\((.|\n)*?\)", ""), @"\d +").Value.Trim()
                     },
                     Status =
                        (StatusPlayer)Enum.Parse(typeof(StatusPlayer),
                            type.ToUpper()
                                    .Replace("INDISPONIBILI", "INDISPONIBILE")
                                    .Replace("SQUALIFICATI", "SQUALIFICATO")
                                    .Replace("ALTRI", "ALTRO")),
                     Order = i,
                     Info = Regex.Match(x, @"(?<=\().+?(?=\))").Value
                 })
                 .Where(x=> !string.IsNullOrEmpty(x.player.Name))
                 .ToList();
        }
        private IEnumerable<HtmlNode> TeamPlayerDetails(HtmlNode html, bool home = true)
        {
            return html.Descendants("div")
             .First(x => x.GetAttributeValue("class", "")
             .Equals(home ? "homeDetails" : "awayDetails"))
             .Descendants("p");
        }
    }
}
