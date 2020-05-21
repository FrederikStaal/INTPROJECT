using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace INTPROJECT.Models
{
    public class Character
    {
        //[Column("CharacterID")]
        [Key]
        public int CharacterID { get; set; }
        //[Column("Name")]
        public string Name { get; set; }
        //[Column("Image")]
        public string Image { get; set; }
    }
}
