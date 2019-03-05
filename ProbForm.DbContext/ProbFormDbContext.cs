using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
using ProbForm.Models;

namespace ProbForm.DBContext
{
    public class ProbFormDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (optionsBuilder != null)
            {
                optionsBuilder.UseMySQL("Server=localhost;Database=ProbForm;Uid=root;Pwd=wesrot92");
            }
        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<Match> Matches { get; set; }
    }
}
