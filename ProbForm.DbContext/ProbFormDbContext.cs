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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                    .HasKey(m => new { m.Day, m.HomeTeamId, m.AwayTeamId });
            modelBuilder.Entity<Player>()
                    .HasKey(p => new { p.Name, p.TeamId });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<Match> Matches { get; set; }
        public static ProbFormDBContext Create()
        {
            return new ProbFormDBContext();
        }
    }
}
