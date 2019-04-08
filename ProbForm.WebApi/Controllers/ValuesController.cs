using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Match = ProbForm.Models.Match;
using ProbForm.AppContext;
using Newtonsoft.Json;
using ProbForm.Models;
namespace ProbForm.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("full")]
        public JsonResult Matches()
        {
            var result = AppDBHelper.MatchesOfTheDay();
            Console.WriteLine(result.Count);
            return new JsonResult(result);
        }
        [HttpGet("teamplayers/{player}")]
        public ActionResult Player(string player, int day = 0)
        {
            var result = AppDBHelper.PlayerInfo(player, day);
            Console.WriteLine(result.Count);
            return new JsonResult(result);
        }

        [HttpGet("teams")]
        public ActionResult Teams()
        {
            var result = AppDBHelper.Teams();
            Console.WriteLine(result.Count);
            return new JsonResult(result);
        }
        [HttpGet("match")]
        public ActionResult Match(string team1="", string team2="", int day =0)
        {
            team2 = team2 ?? "";
            var result = AppDBHelper.MatchOfTheDay(day, team1.Trim(), team2.Trim());
            Console.WriteLine($"#{result.Day}, {result.HomeTeam.Name} - {result.AwayTeam.Name}");
            return new JsonResult(result);
        }
        [HttpGet]
        public JsonResult MatchOfTheDay(int day = 0)
        {
            var result = AppDBHelper.OnlyMatchesOfTheDay(day);
            Console.WriteLine(result.Count);
            return new JsonResult(result);
        }
    }
}
