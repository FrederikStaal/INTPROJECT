using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Models
{
    public class SituationCard
    {
        [Key]
        public int SituationCardID { get; set; }
        public string ImageRef { get; set; }
        public string Text { get; set; }
        public int Military1 { get; set; }
        public int Military2 { get; set; }
        public int Happiness1 { get; set; }
        public int Happiness2 { get; set; }
        public int Economy1 { get; set; }
        public int Economy2 { get; set; }
        public int Relations1 { get; set; }
        public int Relations2 { get; set; }
        public int CharacterID { get; set; }
    }
}
