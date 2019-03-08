using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProbForm.Models
{
    public class Team
    {
        private string name;
        [Key]
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
        public List<TeamPlayer> Players { get; set; }

        private string mister;
        public string Mister
        {
            get
            {
                return mister.ToTitleCase();
            }
            set
            {
                mister = value; 
            }
        }

        public Team Create()
        {
            return new Team();
        }
        public Team WithName(string name)
        {
            Name = name;
            return this;
        }
        public Team WithPlayers(List<TeamPlayer> players)
        {
            Players = players;
            return this;
        }
        public Team WithMister(string mister)
        {
            Mister = mister;
            return this;
        }
    }
}
