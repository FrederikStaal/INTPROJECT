using Microsoft.EntityFrameworkCore;

namespace Website.Models
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Card> Card { get; set; }
        public DbSet<Card> SituationCard { get; set; } 
    }
}
