using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Models
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {

        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Card>().ToTable("Card");
            //modelBuilder.Entity<Card>().HasData(
            //    new Card { CardID = 1, Name = "Card1", Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 2 }
            //    );

            //modelBuilder.Entity<Character>().ToTable("Character");
            //modelBuilder.Entity<Character>().HasData(
            //    new Character { CharacterID = 2, Name = "Frederik", Image = "" }
            //    );
        }
    }
}
