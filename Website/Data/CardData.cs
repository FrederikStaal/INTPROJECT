using Website.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Data
{
    public class CardData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            //database context
            var context = serviceScope.ServiceProvider.GetService<GameContext>();

            //make sure database exists if not then create it
            context.Database.EnsureCreated();

            //deletes all data in card table, but leaves card table itself
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Card");
            //re add all data to database
            var cards = GetCards(context).ToArray();
            context.Card.AddRange(cards);
            context.SaveChanges();
        }

        public static List<Card> GetCards(GameContext db)
        {
            List<Card> cards = new List<Card>()
            {
                //Cards related to the general 
                new Card {ImageRef = "the_general.png", Text = "Sir!...We need more money to secure the borders, to keep the public safe from invaders", Economy1 = -15, Economy2 = 15, Military1 = 10, Military2 = -10, Relations1 = 0, Relations2 = 0, Happiness1 = 0, Happiness2 = 0, CharacterID = 1},
                new Card {ImageRef = "the_general.png", Text = "Sir!.. We should implement enlistments, so we can insure that our army keeps growing", Economy1 = 0, Economy2 = 0, Military1 = 15, Military2 = -15, Relations1 = 0, Relations2 = 0, Happiness1 = -15, Happiness2 = 15, CharacterID = 1},
                new Card {ImageRef = "the_general.png", Text = "Sir!.. We need to assign more soldiers to patrol the borders... We can’t trust the Foreigners", Economy1 = 0, Economy2 = 0, Military1 = -10, Military2 = 10, Relations1 = -20, Relations2 = 20, Happiness1 = 10, Happiness2 = -10, CharacterID = 1},
                new Card {ImageRef = "the_general.png", Text = "Sir!.. The soldiers are complaining about the food at the barracks. Let’s hire a decent chef", Economy1 = -10, Economy2 = 10, Military1 = 15, Military2 = -15, Relations1 = 0, Relations2 = 0, Happiness1 = 0, Happiness2 = 0, CharacterID = 1},
                new Card {ImageRef = "the_general.png", Text = "Sir!.. The diplomat is a STUPID F#$@ LITTLE #$@&%* apple  #$@&%*!?!?! Don’t you agree?", Economy1 = 0, Economy2 = 0, Military1 = 10, Military2 = -10, Relations1 = -10, Relations2 = 10, Happiness1 = 0, Happiness2 = 0, CharacterID = 1},

                //Cards related to the minister of trade
                new Card {ImageRef = "minister_of_trade.png", Text = "Hey boss.. We should raise the taxes! The public don’t need that money anyway", Economy1 = 20, Economy2 = -20, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Happiness1 = -20, Happiness2 = 20, CharacterID = 4},
                new Card {ImageRef = "minister_of_trade.png", Text = "Hey boss.. We should make a trade union with our neighbouring countries!", Economy1 = 25, Economy2 = -25, Military1 = -10, Military2 = 10, Relations1 = 20, Relations2 =-20, Happiness1 = -20, Happiness2 = 20, CharacterID = 4},
                //Cards related to the advicer
            };
            return cards;
        }
    }
}
