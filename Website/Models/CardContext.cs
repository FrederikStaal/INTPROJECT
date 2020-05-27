/*
 * Group 6
 * Rasmus, Joseph, Tony and Frederik
 * Class type: DbContext for our Model classes
 * - 
 */

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
        public DbSet<SituationCard> SituationCard { get; set; } 
    }
}
