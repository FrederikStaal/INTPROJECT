using CardAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardAPI.Data
{
    public class DummyData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<GameContext>();
                // Check if Database exists
                context.Database.EnsureCreated();

                // Check if Database is seeded
                if (context.Card != null && context.Card.Any())
                    return; // Database has already been seeded

                // Seeding Database
                var characters = GetCharacters().ToArray();
                context.Character.AddRange(characters);
                context.SaveChanges();

                var cards = GetCards(context).ToArray();
                context.Card.AddRange(cards);
                context.SaveChanges();
            }
        }

        public static List<Character> GetCharacters()
        {
            List<Character> characters = new List<Character>()
            {
                new Character { Name = "Frederik", Image = "" },
                new Character { Name = "Rasmus", Image = "" },
                new Character { Name = "Joseph", Image = "" },
                new Character { Name = "Toby", Image = "" }
            };
            return characters;
        }

        public static List<Card> GetCards(GameContext db)
        {
            List<Card> cards = new List<Card>()
            {
                new Card { Name = "Card1", Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 1, Character = db.Character.Where(c => c.CharacterID == 1).Single() },
                new Card { Name = "Card2", Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 2, Character = db.Character.Where(c => c.CharacterID == 2).Single() },
                new Card { Name = "Card3", Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 3, Character = db.Character.Where(c => c.CharacterID == 3).Single() },
                new Card { Name = "Card4", Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 4, Character = db.Character.Where(c => c.CharacterID == 4).Single() },
                new Card { Name = "Card5", Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 1, Character = db.Character.Where(c => c.CharacterID == 1).Single() },
                new Card { Name = "Card6", Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 2, Character = db.Character.Where(c => c.CharacterID == 2).Single() },
                new Card { Name = "Card7", Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 3, Character = db.Character.Where(c => c.CharacterID == 3).Single() },
                new Card { Name = "Card8", Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 3, Character = db.Character.Where(c => c.CharacterID == 3).Single() },
                new Card { Name = "Card9", Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 4, Character = db.Character.Where(c => c.CharacterID == 4).Single() },
                new Card { Name = "Card10", Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 4, Character = db.Character.Where(c => c.CharacterID == 4).Single() }
            };
            return cards;
        }
    }
}
