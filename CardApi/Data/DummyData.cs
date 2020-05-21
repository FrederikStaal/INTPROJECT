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

            // The characters for the cards

            List<Character> characters = new List<Character>()
            {
               new Character { CharacterID = 1, Name = "The General", Image = "" },
                new Character { CharacterID = 2, Name = "The Advicer", Image = "" },
                new Character { CharacterID = 3, Name = "The diplomat", Image = "" },
                new Character { CharacterID = 4, Name = "Minister of trade", Image = "" },
                new Character { CharacterID = 5, Name = "The Media", Image=""},
                new Character { CharacterID = 7, Name = "Death", Image=""},
                new Character {CharacterID = 8, Name = "Minister of Trade rules", Image=""},
                new Character {CharacterID = 9, Name = "Prison", Image=""},
                new Character {CharacterID = 10, Name = "Paper Route", Image=""},
                new Character {CharacterID = 11, Name = "No population", Image=""},
                new Character {CharacterID = 12, Name = "You are a puppet", Image=""}

            };
            return characters;
        }

        public static List<Card> GetCards(GameContext db)
        {
            List<Card> cards = new List<Card>()
            {
                //Cards relatedto the general 

                new Card {Text = " Sir!...We need more money to secure the borders, to keep the public safe from invaders", Economy1 = -15, Economy2 = 15, Military1 = 10, Military2 = -10, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 1, Character = db.Character.Where(c => c.CharacterID == 1).Single()},
                new Card {Text = " Sir!.. We should implement enlistments, so we can insure that our army keeps growing", Economy1 = 0, Economy2 = 0, Military1 = 15, Military2 = -15, Relations1 = 0, Relations2 = 0, Thepublic1 = -15, Thepublic2 = 15, CharacterID = 1, Character = db.Character.Where(c => c.CharacterID == 1).Single()},
                new Card {Text = "Sir!.. We need to assign more soldiers to patrol the borders... We can’t trust the Foreigners", Economy1 = 0, Economy2 = 0, Military1 = -10, Military2 = 10, Relations1 = -20, Relations2 = 20, Thepublic1 = 10, Thepublic2 = -10, CharacterID = 1, Character = db.Character.Where(c => c.CharacterID == 1).Single()},
                new Card {Text = " Sir!.. The soldiers are complaining about the food at the barracks. Let’s hire a decent chef", Economy1 = -10, Economy2 = 10, Military1 = 15, Military2 = -15, Relations1 = 0, Relations2 = 0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 1, Character = db.Character.Where(c => c.CharacterID == 1).Single()},
                new Card {Text = " Sir!.. The diplomat is a STUPID F#$@ LITTLE #$@&%* apple  #$@&%*!?!?! Don’t you agree?", Economy1 = 0, Economy2 = 0, Military1 = 10, Military2 = -10, Relations1 = -10, Relations2 = 10, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 1, Character = db.Character.Where(c => c.CharacterID == 1).Single()},

                //Cards related to the minister of trade

                new Card {Text = " Hey boss.. We should raise the taxes! The public don’t need that money anyway", Economy1 = 20, Economy2 = -20, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Thepublic1 = -20, Thepublic2 = 20, CharacterID = 4, Character = db.Character.Where(c => c.CharacterID == 4).Single()},
                new Card {Text = " Hey boss.. We should make a trade union with our neighbouring countries!", Economy1 = 25, Economy2 = -25, Military1 = -10, Military2 = 10, Relations1 = 20, Relations2 =-20, Thepublic1 = -20, Thepublic2 = 20, CharacterID = 4, Character = db.Character.Where(c => c.CharacterID == 4).Single()},
                new Card {Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 =0, Thepublic1 = 0, Thepublic2 = 0, CharacterID = 4, Character = db.Character.Where(c => c.CharacterID == 4).Single()}

          
            };
            return cards;
        }
    }
}
