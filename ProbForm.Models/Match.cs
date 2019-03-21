using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ProbForm.Models
{
    public class Match
    {

        public int Day { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        [JsonIgnore]
        [MaxLength(50)]
        public string HomeTeamId { get; set; }
        [JsonIgnore]
        [MaxLength(50)]
        public string AwayTeamId { get; set; }

        public string HomeModule { get; set; }
        public string AwayModule { get; set; }
        public DateTime MatchTime { get; set; }

        [JsonIgnore]
        public List<TeamPlayer> TeamPlayers { get; set; }

        public static Match Create()
        {
            return new Match();
        }
        public Match WithHomeTeam(Team home)
        {
            HomeTeam = home;
            return this;
        }
        public Match WithAwayTeam(Team away)
        {
            AwayTeam = away;
            return this;
        }
        public Match WithMachTime(DateTime date)
        {
            MatchTime = date;
            return this;
        }
    }
}