using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProbForm.AppContext;
using ProbForm.ConsoleApplication.Services;
using ProbForm.Models;

namespace ProbForm.ConsoleApplication
{
    class MainClass
    {
        static ILineupsService lineups;
        public async static Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            lineups = new GazzettaLineupService();
            var matches = await lineups.Matches();
            Print(matches);
            using (var appContext = new AppContext.ProbFormDBContext())
            {
                foreach (var m in matches)
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
                    }
                }
                try
                {
                }
                catch (Exception ex)
                {

                 }
            }
            Console.ReadLine();

        }
        static string breakLine = "###################################";
        static string line = "______________________________";
        static void Print(List<Match> matches)
        {
            foreach (var m in matches)
            {
                Console.WriteLine(breakLine);
                Console.WriteLine(m.HomeTeam.Name);
                Console.WriteLine(m.HomeModule);
                Console.WriteLine(line);
                PrintTeam(m.HomeTeam.Players);
                Console.WriteLine(m.HomeTeam.Mister);
                Console.WriteLine(line);
                Console.WriteLine(line);
                Console.WriteLine(m.AwayTeam.Name);
                Console.WriteLine(m.AwayModule);
                Console.WriteLine(line);
                PrintTeam(m.AwayTeam.Players);
                Console.WriteLine(m.AwayTeam.Mister);
                Console.WriteLine(line);
                Console.WriteLine(line);
                Console.WriteLine(breakLine);
            }
        }
        static void PrintTeam(List<TeamPlayer> t)
        {
            t.Where(x => x.Status == StatusPlayer.TITOLARE)
                  .OrderBy(x => x.Order)
                  .ToList()
                  .ForEach(x =>
                      Console.WriteLine(x.Player.Name));
            Console.WriteLine(line);
            Console.WriteLine("Panchina:");
            t.Where(x => x.Status == StatusPlayer.PANCHINA)
                    .OrderBy(x => x.Order)
                    .ToList()
                    .ForEach(x =>
                        Console.WriteLine(x.Player.Name));

            Console.WriteLine("Squalificati:");
            t.Where(x => x.Status == StatusPlayer.SQUALIFICATO)
                    .OrderBy(x => x.Order)
                    .ToList()
                    .ForEach(x =>
                        Console.WriteLine($"{x.Player.Name} - {x.Info}"));

            Console.WriteLine("Indisponibile:");
            t.Where(x => x.Status == StatusPlayer.INDISPONIBILE)
                        .OrderBy(x => x.Order)
                        .ToList()
                        .ForEach(x =>
                            Console.WriteLine($"{x.Player.Name} - {x.Info}"));

            Console.WriteLine("Altro:");
            t.Where(x => x.Status == StatusPlayer.ALTRO)
                        .OrderBy(x => x.Order)
                        .ToList()
                        .ForEach(x =>
                            Console.WriteLine($"{x.Player.Name} - {x.Info}"));
            Console.WriteLine(line);

        }
    }
}
