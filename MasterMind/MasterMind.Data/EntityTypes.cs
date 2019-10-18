using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        public CodePeg CodePeg1 { get; set; }
        public CodePeg CodePeg2 { get; set; }
        public CodePeg CodePeg3 { get; set; }
        public CodePeg CodePeg4 { get; set; }
        public static ValueConverter<CodePeg, string> GetCodePegConverter() => new ValueConverter<CodePeg, string>(
            v => v.ToString(),
            v => (CodePeg)Enum.Parse(typeof(CodePeg), v));

        public KeyPeg? KeyPeg1 { get; set; } = null;
        public KeyPeg? KeyPeg2 { get; set; } = null;
        public KeyPeg? KeyPeg3 { get; set; } = null;
        public KeyPeg? KeyPeg4 { get; set; } = null;
        public static ValueConverter<KeyPeg?, string> GetKeyPegConverter() => new ValueConverter<KeyPeg?, string>(
            v => v.ToString(),
            v => String.IsNullOrEmpty(v) ? null : (KeyPeg?)Enum.Parse(typeof(KeyPeg?), v));

        public Game Game { get; set; }
        public bool IsCompleted() => KeyPeg1.HasValue && KeyPeg2.HasValue && KeyPeg3.HasValue && KeyPeg4.HasValue &&
            (KeyPeg1 == KeyPeg.Black) && (KeyPeg2 == KeyPeg.Black) && (KeyPeg3 == KeyPeg.Black) && (KeyPeg4 == KeyPeg.Black);
    }
}
