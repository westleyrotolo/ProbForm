using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProbForm.Models
{
    public class Match
    {

        public int Day { get; set; }
        [ForeignKey("HomeTeam")]
        [MaxLength(50)]
        public string HomeTeamId { get; set; }
        [ForeignKey("AwayTeam")]
        [MaxLength(50)]
        public string AwayTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public string HomeModule { get; set; }
        public string AwayModule { get; set; }
        public DateTime MatchTime { get; set; }
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