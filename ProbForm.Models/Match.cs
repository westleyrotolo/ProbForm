using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProbForm.Models
{
    public class Match
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MatchId { get; set; }
        public int Day { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
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