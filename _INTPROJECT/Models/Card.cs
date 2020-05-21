using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _INTPROJECT.Models
{
    public class Card
    {
        public int CardID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Military1 { get; set; }
        public int Military2 { get; set; }
        public int Thepublic1 { get; set; }
        public int Thepublic2 { get; set; }
        public int Economy1 { get; set; }
        public int Economy2 { get; set; }
        public int Relations1 { get; set; }
        public int Relations2 { get; set; }
        public int CharacterID { get; set; }
        public Character Character { get; set; }
    }
}
