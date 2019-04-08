using System;
using System.Threading.Tasks;
using ProbForm.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProbForm.AppContext
{
    public static class AppDBHelper
    {
        public static async Task<bool> InsertOrUpdate(Match m)
        {
            using (var appContext = new ProbFormDBContext())
            {
                try
                {
                    if (appContext.Matches.Count(x =>
                        x.HomeTeam.Name == m.HomeTeam.Name
                        && x.AwayTeam.Name == m.AwayTeam.Name
                        && x.Day == m.Day
                    ) > 0)
                    {
                        appContext.Remove(m.HomeTeam);
                        appContext.Remove(m.AwayTeam);
                        m.HomeTeam.Players.ForEach((x) =>
                        {
                            appContext.Remove(x);
                            appContext.Remove(x.Player);
                        });
                        m.AwayTeam.Players.ForEach((x) =>
                        {
                            appContext.Remove(x);
                            appContext.Remove(x.Player);
                        });
                        appContext.Entry(m).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                        await appContext.SaveChangesAsync();
                    }

                    appContext.Matches.Add(m);
                    appContext.Teams.Add(m.HomeTeam);
                    appContext.Teams.Add(m.AwayTeam);

                    await appContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("PORCO DIO");
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// Fetch all match incluse player
        /// </summary>
        /// <param name="day">Week match</param>
        /// <returns></returns>
        public static List<Match> MatchesOfTheDay(int day = 0)
        {
            using (var context = new ProbFormDBContext())
            {

                CheckDay(ref day);
                return context.Matches.Where(x =>
                        x.Day == day)
                        .Include(x => x.HomeTeam)
                        .Include(x => x.AwayTeam)
                        .Include(x => x.HomeTeam.Players)
                        .Include("HomeTeam.Players.Player")
                        .Include(x => x.AwayTeam.Players)
                        .Include("AwayTeam.Players.Player")
                        .ToList();
            }
        }
        /// <summary>
        /// Fetch list match
        /// </summary>
        /// <param name="day">Week day</param>
        /// <returns>Match</returns>
        public static List<Match> OnlyMatchesOfTheDay(int day = 0)
        {
            using (var context = new ProbFormDBContext())
            {
                CheckDay(ref day);
                return context.Matches.Where(x =>
                        x.Day == day)
                        .Include(x => x.HomeTeam)
                        .Include(x => x.AwayTeam)
                        .ToList();
            }
        }
        /// <summary>
        /// Fetch all team
        /// </summary>
        /// <returns>Teams</returns>
        public static List<string> Teams()
        {
            using (var context = new ProbFormDBContext())
            {
                return context.Teams.Select(x => 
                        x.Name)
                        .ToList();
            }
        }
        /// <summary>
        /// Search match
        /// </summary>
        /// <param name="day">Week match</param>
        /// <param name="team1">First team</param>
        /// <param name="team2">Second team</param>
        /// <returns>Match</returns>
        public static Match MatchOfTheDay(int day = 0, string team1 = "", string team2 ="")
        {
            using (var context = new ProbFormDBContext())
            {
                CheckDay(ref day);
                team1 = team1.ToLower();
                team2 = team2.ToLower();
              
                if (string.IsNullOrEmpty(team1) && string.IsNullOrEmpty(team2))
                {
                    throw new ArgumentException("At least one of team1 and team2 has must been evaluated");
                }
                var r = context.Matches.Where(x => x.AwayTeam.Name.ToLower().Contains(team1)).ToList();
                return context.Matches
                        .Include(x => x.HomeTeam)
                        .Include(x => x.AwayTeam)
                        .Include(x => x.HomeTeam.Players)
                        .Include("HomeTeam.Players.Player")
                        .Include(x => x.AwayTeam.Players)
                        .Include("AwayTeam.Players.Player")
                          .First(x =>
                           x.Day == day
                           && (x.HomeTeam.Name.ToLower().Contains(team1)
                               && x.AwayTeam.Name.ToLower().Contains(team2))
                           || (x.AwayTeam.Name.ToLower().Contains(team1)
                               && x.HomeTeam.Name.ToLower().Contains(team2)));
            }
        }
        /// <summary>
        /// Status of player in a match
        /// </summary>
        /// <param name="name">Name of player</param>
        /// <param name="day">Week match</param>
        /// <returns>Player</returns>
        public static List<TeamPlayer> PlayerInfo(string name, int day = 0)
        {
            using (var context = new ProbFormDBContext())
            {
                CheckDay(ref day);
                return context.TeamPlayers.Where(x =>
                            x.Player.Name.Contains(name))
                            .Include(x=>x.Player)
                            .Include(x=>x.Player.Team)
                            .ToList();
            }
        } 


        private static void CheckDay(ref int d)
        {
            using (var context = new ProbFormDBContext())
            {
                if (d == 0)
                {
                    d = context.Matches.Max(x => x.Day);
                }
            }
        }
    }
}
