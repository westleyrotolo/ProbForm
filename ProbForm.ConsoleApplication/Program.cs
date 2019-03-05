using System;
using System.Threading.Tasks;
using ProbForm.ConsoleApplication.Services;

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
            Console.ReadLine();

        }
    }
}
