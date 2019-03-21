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
        [HttpGet]
        public JsonResult Matches()
        {
            var result = AppDBHelper.MatchesOfTheDay();
            Console.WriteLine(result.Count);
            return new JsonResult(result);
        }
        [HttpGet("search")]
        public JsonResult Player(string player)
        {
            var result = AppDBHelper.PlayerInfo(player);
            Console.WriteLine(result.Count);
            return new JsonResult(result);
        }

       
    }
}
