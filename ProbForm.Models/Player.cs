using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ProbForm.Models
{
    public class Player
    {

        private string name;

        [MaxLength(50)]
        public string Name
        {
            get
            {
                return name.ToTitleCase();
            }
            set
            {
                name = value;
            }
        }
        public string Number { get; set; }
        
        [MaxLength(50)]
        public string TeamId { get; set; }
        [JsonIgnore]
        public virtual Team Team { get; set; }
        [JsonIgnore]
        public virtual List<TeamPlayer> TeamPlayers { get; set; }

    }
}

