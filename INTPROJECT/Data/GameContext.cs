using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTPROJECT.Models
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {

        }

        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Card>().ToTable("Card");
            //modelBuilder.Entity<Card>().HasData(
            //    new Card { CardID = 1, Name = "Card1", Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 2 }
            //    );

            //modelBuilder.Entity<Character>().ToTable("Character");
            //modelBuilder.Entity<Character>().HasData(
            //    new Character { CharacterID = 2, Name = "Frederik", Image = "" }
            //    );

            //var context = new GameContext();
            //var character = new Character { CharacterID = 1, Name = "Frederik", Image = "" };
            //options.Add<Character>(character);
        }

        public DbSet<Card> Card { get; set; }
        public DbSet<Character> Character { get; set; }
    }
}
