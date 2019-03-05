using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProbForm.Models
{

   public class TeamPlayer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamPlayerId { get; set; }
        public Player player { get; set; }
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