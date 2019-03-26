using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore;
using ProbForm.Models;

namespace ProbForm.AppContext
{
    public class ProbFormDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            base.OnConfiguring(optionsBuilder);
            if (optionsBuilder != null)
            {
                optionsBuilder.UseMySQL("Server=localhost;Database=ProbForm;Uid=root;Pwd=admin");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                    .HasKey(m => new { m.Day, m.HomeTeamId, m.AwayTeamId });
            modelBuilder.Entity<Player>()
                    .HasKey(p => new { p.Name, p.TeamId });
            modelBuilder.Entity<TeamPlayer>()
                    .HasKey(tp => new { tp.MatchDay, tp.MatchHomeTeamId, tp.MatchAwayTeamId, tp.PlayerNameId, tp.PlayerTeamId });
            modelBuilder.Entity<Match>()
                    .HasMany(m => m.TeamPlayers)
                    .WithOne(tp => tp.Match)
                    .HasForeignKey(tp => new { tp.MatchDay, tp.MatchHomeTeamId, tp.MatchAwayTeamId });
            modelBuilder.Entity<Player>()
                     .HasMany(p => p.TeamPlayers)
                     .WithOne(tp => tp.Player)
                     .HasForeignKey(tp => new { tp.PlayerNameId, tp.PlayerTeamId });
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