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


            //deletes all data in card table, but leaves card table itself
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Card");
            //re add all data to database
            var cards = GetCards(context).ToArray();
            context.Card.AddRange(cards);
            var sitCards = GetSituationCards(context).ToArray();
            context.SituationCard.AddRange(sitCards);
            context.SaveChanges();
        }

        public static List<Card> GetSituationCards(GameContext db)
        {
            List<Card> sitCards = new List<Card>()
            {
                //outcome, death cards
                //death, general, if military 0
                new Card {ImageRef = "the_general.png", Text = "We are being invaded by the neighbouring country and we have no army… We should surrender!",
                        Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 = -100, Happiness1 = -100, Happiness2 = -100,
                        CharacterID = 1 },
                // if no
                new Card {ImageRef = "dead.png",Text = "You did not surrender, which resulted in you being put to death",
                        Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 = -100, Happiness1 = -100, Happiness2 = -100,
                        CharacterID = 7 },
                // if yes
                new Card {ImageRef = "prison.png",Text = "You surrendered to the invaders, and spent the rest of your days in prison",
                        Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 = -100, Happiness1 = -100, Happiness2 = -100,
                        CharacterID = 7 },

                //death, general if military 100
                new Card {ImageRef = "the_general.png", Text = "Sir.. The army have no more need of you.. I’ll take over from here",
                        Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 = -100, Happiness1 = -100, Happiness2 = -100,
                        CharacterID = 1 },
                // if yes
                new Card {ImageRef = "prison.png",Text = "The generel spared you and threw you to prison..",
                        Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 = -100, Happiness1 = -100, Happiness2 = -100,
                        CharacterID = 7 },
                // if no
                new Card {ImageRef = "dead.png",Text = "The generel didn’t take a no for an answer and shot you on the spot",
                        Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 = -100, Happiness1 = -100, Happiness2 = -100,
                        CharacterID = 7 },
            };
            return sitCards;
        }

        public static List<Card> GetCards(GameContext db)
        {
            List<Card> cards = new List<Card>()
            {
                //Cards related to the general 
                new Card {ImageRef = "the_general.png", Text = "Sir!...We need more money to secure the borders, to keep the public safe from invaders",
                    Economy1 = -15, Economy2 = 15, Military1 = 10, Military2 = -10, Relations1 = 0, Relations2 = 0, Happiness1 = 0, Happiness2 = 0,
                    CharacterID = 1},
                new Card {ImageRef = "the_general.png", Text = "Sir!.. We should implement enlistments, so we can insure that our army keeps growing",
                    Economy1 = 0, Economy2 = 0, Military1 = 15, Military2 = -15, Relations1 = 0, Relations2 = 0, Happiness1 = -15, Happiness2 = 15,
                    CharacterID = 1},
                new Card {ImageRef = "the_general.png", Text = "Sir!.. We need to assign more soldiers to patrol the borders... We can’t trust the Foreigners",
                    Economy1 = 0, Economy2 = 0, Military1 = -10, Military2 = 10, Relations1 = -20, Relations2 = 20, Happiness1 = 10, Happiness2 = -10,
                    CharacterID = 1},
                new Card {ImageRef = "the_general.png", Text = "Sir!.. The soldiers are complaining about the food at the barracks. Let’s hire a decent chef",
                    Economy1 = -10, Economy2 = 10, Military1 = 15, Military2 = -15, Relations1 = 0, Relations2 = 0, Happiness1 = 0, Happiness2 = 0,
                    CharacterID = 1},
                new Card {ImageRef = "the_general.png",Text = "Sir!.. The diplomat is a STUPID F#$@ LITTLE #$@&%* apple  #$@&%*!?!?! Don’t you agree?",
                    Economy1 = 0, Economy2 = 0, Military1 = 10, Military2 = -10, Relations1 = -10, Relations2 = 10, Happiness1 = 0, Happiness2 = 0,
                    CharacterID = 1},

                ////outcome, death cards
                ////death, general, if military 0
                //new Card {ImageRef = "the_general.png", Text = "We are being invaded by the neighbouring country and we have no army… We should surrender!",
                //        Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 = -100, Happiness1 = -100, Happiness2 = -100,
                //        CharacterID = 1 },
                //// if no
                //new Card {ImageRef = "dead.png",Text = "You did not surrender, which resulted in you being put to death",
                //        Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 = -100, Happiness1 = -100, Happiness2 = -100,
                //        CharacterID = 7 },
                //// if yes
                //new Card {ImageRef = "prison.png",Text = "You surrendered to the invaders, and spent the rest of your days in prison",
                //        Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 = -100, Happiness1 = -100, Happiness2 = -100,
                //        CharacterID = 7 },

                ////death, general if military 100
                //new Card {ImageRef = "the_general.png", Text = "Sir.. The army have no more need of you.. I’ll take over from here",
                //        Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 = -100, Happiness1 = -100, Happiness2 = -100,
                //        CharacterID = 1 },
                //// if yes
                //new Card {ImageRef = "prison.png",Text = "The generel spared you and threw you to prison..",
                //        Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 = -100, Happiness1 = -100, Happiness2 = -100,
                //        CharacterID = 7 },
                //// if no
                //new Card {ImageRef = "dead.png",Text = "The generel didn’t take a no for an answer and shot you on the spot",
                //        Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 = -100, Happiness1 = -100, Happiness2 = -100,
                //        CharacterID = 7 },

                //diplomat - relation 100
                new Card {ImageRef = "diplomat.png",Text = "My friend… We have been doing the bidding of the major countries for so long, that we have no influence on anything anymore",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                new Card {ImageRef = "outsiderelation=100.png",Text = "it doesn’t matter what you say.. You are already a puppet of the outside world",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                new Card {ImageRef = "outsiderelation=100.png",Text = "it doesn’t matter what you say.. You are already a puppet of the outside world",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                // relation 0
                new Card {ImageRef = "diplomat.png",Text = "My friend… The major countries have made you an enemy of the world society and demand you to resign",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                //yes
                new Card {ImageRef = "bike.png",Text = "You agreed to resign from your post, but luckily you were able to get your old paper route back",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                //no
                new Card {ImageRef = "dead.png",Text = "The diplomat was hired to assassinate you and poisoned your cereal",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                // advisor - happiness 0
                new Card {ImageRef = "advicer.png",Text = "Master..The population is so low, that there is no one left to rule?",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                // advisor - happiness 100
                new Card {ImageRef = "advicer.png",Text = "The people has gathered outside and they want democracy",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                //yes
                new Card {ImageRef = "bike.png",Text = "You were not elected and was forced to work your old paper route again",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                //no
                new Card {ImageRef = "prison.png",Text = "You were not able to contain the masses and you were thrown to prison by the public",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                // trade minister 100
                new Card {ImageRef = "minister_of_trade.png",Text = "Hey boss… The country  is 100% owned by the private sector..which means me",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                //yes
                 new Card {ImageRef = "traderboss.png",Text = "The minister of trade took all the money and bought the entire country.. Luckily he gave you a job as his secretary",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                 //no
                  new Card {ImageRef = "traderboss.png",Text = "The minister of trade took all the money and bought the entire country.. Luckily he gave you a job as his secretary",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                  //trade minister 0
                  new Card {ImageRef = "minister_of_trade.png",Text = "Hey boss.. You are in huge debt and the payday is now!",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},
                  //yes&&no
                  new Card {ImageRef = "dead.png",Text = "The debt collectors threw you in a river and you were never seen again",
                    Economy1 = -100, Economy2 = -100, Military1 = -100, Military2 = -100, Relations1 = -100, Relations2 =-100, Happiness1 = -100, Happiness2 = -100,
                    CharacterID = 4},


                //Normal Cards related to the minister of trade
                new Card {ImageRef = "minister_of_trade.png", Text = "Hey boss.. We should raise the taxes! The public don’t need that money anyway",
                    Economy1 = 20, Economy2 = -20, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 = 0, Happiness1 = -20, Happiness2 = 20,
                    CharacterID = 4},
                new Card {ImageRef = "minister_of_trade.png", Text = "Hey boss.. We should make a trade union with our neighbouring countries!",
                    Economy1 = 25, Economy2 = -25, Military1 = -10, Military2 = 10, Relations1 = 20, Relations2 =-20, Happiness1 = -20, Happiness2 = 20,
                    CharacterID = 4},
                new Card {ImageRef = "minister_of_trade.png", Text = "THIS IF FOR TESTING DELETE ME LATER",
                    Economy1 = 25, Economy2 = -25, Military1 = -10, Military2 = 10, Relations1 = 20, Relations2 =-20, Happiness1 = -20, Happiness2 = 20, CharacterID = 4},
                new Card {ImageRef = "minister_of_trade.png", Text = "if this works i will be happy",
                    Economy1 = 25, Economy2 = -25, Military1 = -10, Military2 = 10, Relations1 = 20, Relations2 =-20, Happiness1 = -20, Happiness2 = 20,
                    CharacterID = 4},

                new Card {ImageRef = "minister_of_trade.png",Text = " Hey boss.. Let us increase tax complexity, so people wont fill them right and we can fine them!",
                    Economy1 = 10, Economy2 = 0, Military1 = -10, Military2 = 0, Relations1 = 0, Relations2 =0, Happiness1 = -20, Happiness2 = 0,
                    CharacterID = 4 },
                new Card {ImageRef = "minister_of_trade.png",Text = " Hey boss.. Let us increase the taxes for nonprofits, they are making too much profit!",
                    Economy1 = 15, Economy2 = -15, Military1 = 0, Military2 = 0, Relations1 = 20, Relations2 =-20, Happiness1 = -10, Happiness2 = 0,
                    CharacterID = 4},
                new Card {ImageRef = "minister_of_trade.png",Text = " Hey boss.. We should maybe support our single mothers some more with some cheap benefits",
                    Economy1 = -5, Economy2 = 0, Military1 = -10, Military2 = 0, Relations1 = 0, Relations2 = 0, Happiness1 = 10, Happiness2 = -10,
                    CharacterID = 4},
                new Card {ImageRef = "minister_of_trade.png",Text = " MORNING.. How about investing in education and raise the salary for teachers of public schools?",
                    Economy1 = -5, Economy2 = 0, Military1 = -5, Military2 = 0, Relations1 = 0, Relations2 = 0, Happiness1 = 10, Happiness2 = -10,
                    CharacterID = 4},
                new Card {ImageRef = "minister_of_trade.png",Text = "G' day... Entrepreneurs are potentially dodging taxes, let us check a small sample pool of these suckers and punish them to encourage others to pay proper taxes!",
                    Economy1 = 10, Economy2 = -10, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 =0, Happiness1 = -10,Happiness2 = 10,
                    CharacterID = 4},
                new Card {ImageRef = "minister_of_trade.png",Text = "", Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = 0, Relations2 =0, Happiness1 = 0, Happiness2 = 0,
                    CharacterID = 4},

                

                //advisor
                new Card {ImageRef = "advicer.png",Text = "We miss essential skilled workforce, I suggest we import foreigners and treat them like slaves",
                    Economy1 = 20, Economy2 = -10, Military1 = 10, Military2 = -10, Relations1 = 20, Relations2 =-10, Happiness1 = 10, Happiness2 = 5,
                    CharacterID = 4},
                new Card {ImageRef = "advicer.png",Text = "These immigirants gather in same neighborhoods where high drug related crime is being reported, I advice we legalize all the banned substances",
                    Economy1 = 40, Economy2 = -10, Military1 = -50, Military2 = 0, Relations1 = 20, Relations2 =00, Happiness1 = 30, Happiness2 = -10,
                    CharacterID = 4},
                new Card {ImageRef = "advicer.png",Text = "Master..The people grow old, we could use more newborns in our country, let us run creation-of-family stimulating ads on national television",
                    Economy1 = -10, Economy2 = 0, Military1 = 10, Military2 = 00, Relations1 = 10, Relations2 =0, Happiness1 = 30, Happiness2 = -10,
                    CharacterID = 4},
                new Card {ImageRef = "advicer.png",Text = "There are rumours of chinese having quantum teleport device, I advice we takeover Hong Kong and Taiwan",
                    Economy1 = -20, Economy2 = 0, Military1 = -20, Military2 = 00, Relations1 = -20, Relations2 = 0, Happiness1 = -10, Happiness2 = 0,
                    CharacterID = 4},
                new Card {ImageRef = "advicer.png",Text = "Some script kiddies from 3rd world Russia are attacking our hospital networks, I advice we invest in securing Crimea",
                    Economy1 = -10, Economy2 = 0, Military1 = -10, Military2 = 0, Relations1 = -20, Relations2 =0, Happiness1 = -10, Happiness2 = 0,
                    CharacterID = 4},
                 new Card {ImageRef = "advicer.png",Text = "The people like dogs, we should lower taxes on dog products and support animal adoption",
                    Economy1 = -5, Economy2 = 0, Military1 = 10, Military2 = 0, Relations1 = 10, Relations2 =0, Happiness1 = 10, Happiness2 = 0,
                    CharacterID = 4},

                //diplomat
                new Card {ImageRef = "diplomat.png",Text = "The North Korean leaders invite us for dinner with Dennis Rodman. Should I join?",
                    Economy1 = 0, Economy2 = 0, Military1 = 10, Military2 = 0, Relations1 = -20, Relations2 = 10, Happiness1 = -10, Happiness2 = 10,
                    CharacterID = 4},
                new Card {ImageRef = "diplomat.png",Text = "I plan to congratulate the Arabian Prince for his 5th bab...ehm wife, maybe to strike that better oil deal, what do you think?",
                    Economy1 = 10, Economy2 = 0, Military1 = 10, Military2 = 0, Relations1 = -10, Relations2 = 10, Happiness1 = -10, Happiness2 = 10,
                    CharacterID = 4},
                new Card {ImageRef = "diplomat.png",Text = "The neighbors plan to celebrate anniversary of genocides, i dont think we should do that",
                    Economy1 = 0, Economy2 = 0, Military1 = 0, Military2 = 0, Relations1 = -10, Relations2 = 10, Happiness1 = 10, Happiness2 = -10,
                    CharacterID = 4},
                new Card {ImageRef = "diplomat.png",Text = "The aliens came down yesterday in their ships and offered us huge containers made of gold, real cash and bitcoins, should we accept the gift? ",
                    Economy1 = 20, Economy2 = 0, Military1 = 20, Military2 = 0, Relations1 = 20, Relations2 = 10, Happiness1 = 20, Happiness2 = 10,
                    CharacterID = 4},
                new Card {ImageRef = "diplomat.png",Text = "Let us take break and have a goodnight sleep, then think make a clear decision the next day",
                    Economy1 = 0, Economy2 = 0, Military1 = 10, Military2 = 0, Relations1 = 0, Relations2 = 0, Happiness1 = 0, Happiness2 = 10,
                    CharacterID = 4},


                //media
                new Card {ImageRef = "media.png",Text = "We saw your leaked emails, discussing on joining forces with your old brother, can you elaborate for us?",
                    Economy1 = 5, Economy2 = 5, Military1 = 5, Military2 = 5, Relations1 = 5, Relations2 = 5, Happiness1 = 5, Happiness2 = 5,
                    CharacterID = 4},

            };
            return cards;
        }
    }
}
