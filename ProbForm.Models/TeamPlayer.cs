using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ProbForm.Models
{

   public class TeamPlayer
    {


        public int MatchDay { get; set; }

        [MaxLength(50)]
        public string MatchHomeTeamId { get; set; }

        [MaxLength(50)]
        public string MatchAwayTeamId { get; set; }
        [JsonIgnore]
        public virtual Match Match { get; set; }

        [JsonIgnore]
        [MaxLength(50)]
        public string PlayerNameId { get; set; }

        [JsonIgnore]
        [MaxLength(50)]
        public string PlayerTeamId { get; set; }

        public virtual Player Player { get; set; }
        public StatusPlayer Status { get; set; }
        public int Order { get; set; }
        public string Info { get; set; }

    }
    public enum StatusPlayer
    {
        TITOLARE,
        PANCHINA,
        SQUALIFICATO,
        INDISPONIBILE,
        ALTRO

    }
}