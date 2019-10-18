using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterMind.Data
{
    [Table("mmGame")]
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Guid { get; set; } //represented as string for sqlite 
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsCompleted { get; set; }


        public List<GamePattern> Patterns { get; set; }
    }
    [Table("mmGamePattern")]
    public class GamePattern
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Range(0, 8)]
        public int Level { get; set; } //Level 0 represents code , 1-8 represent user guesses
        
        [Range(0,5)]
        public int Color1 { get; set; }//0-White,1-Purple and so on (using integers due to past issues of enums with sqllite)
        [Range(0, 5)]
        public int Color2 { get; set; }
        [Range(0, 5)]
        public int Color3 { get; set; }
        [Range(0, 5)]
        public int Color4 { get; set; }

        [Range(-1, 1)]
        public int Key1 { get; set; } //-1: Not set, 0: Black, 1: White
        [Range(-1, 1)]
        public int Key2 { get; set; }
        [Range(-1, 1)]
        public int Key3 { get; set; }
        [Range(-1, 1)]
        public int Key4 { get; set; }

        public Game Game { get; set; }

        public bool IsCompleted() => Key1 + Key2 + Key3 + Key4 == 4;
    }
}
