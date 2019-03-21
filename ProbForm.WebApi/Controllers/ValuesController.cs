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
        // GET api/values
        [HttpGet("full")]
        public JsonResult Matches()
        {
            var result = AppDBHelper.MatchesOfTheDay();
            Console.WriteLine(result.Count);
            return new JsonResult(result);
        }
        [HttpGet("players/{player}")]
        public ActionResult Player(string player)
        {
            var result = AppDBHelper.PlayerInfo(player);
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
        [HttpGet]
        public JsonResult MatchOfTheDay(int day = 0)
        {
            var result = AppDBHelper.OnlyMatchesOfTheDay();
            Console.WriteLine(result.Count);
            return new JsonResult(result);
        }
    }
}
