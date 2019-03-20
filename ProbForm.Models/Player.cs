using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Team Team { get; set; }

        public List<TeamPlayer> TeamPlayers { get; set; }

    }
}

