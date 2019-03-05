using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProbForm.Models
{
    public class Team
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }
        public string Name { get; set; }
        public List<TeamPlayer> Players { get; set; }
        public string Module { get; set; }
        public string Mister { get; set; }

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
        public Team WithModule(string module)
        {
            Module = module;
            return this;
        }
        public Team WithMister(string mister)
        {
            Mister = mister;
            return this;
        }
    }
}
