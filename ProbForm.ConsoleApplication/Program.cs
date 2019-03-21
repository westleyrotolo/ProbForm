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
            matches.ForEach(async (m) =>
               await AppDBHelper.InsertOrUpdate(m));
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
