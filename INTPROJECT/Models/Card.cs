using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace INTPROJECT.Models
{
    [Table("Cards")]
    public class Card
    {
        //[Column("CardID")]
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CardID { get; set; }
        //[Column("Name")]
        public string Name { get; set; }
        //[Column("Text")]
        public string Text { get; set; }
        //[Column("Military1")]
        public int Military1 { get; set; }
        //[Column("Military2")]
        public int Military2 { get; set; }
        //[Column("Thepublic1")]
        public int Thepublic1 { get; set; }
        //[Column("Thepublic2")]
        public int Thepublic2 { get; set; }
        //[Column("Economy1")]
        public int Economy1 { get; set; }
        //[Column("Economy2")]
        public int Economy2 { get; set; }
        //[Column("Relations1")]
        public int Relations1 { get; set; }
        //[Column("Relations2")]
        public int Relations2 { get; set; }
        //[Column("CharacterID")]
        public int CharacterID { get; set; }
        //[Column("Character")]
        public Character Character { get; set; }
    }
}
