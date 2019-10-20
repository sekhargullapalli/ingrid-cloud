using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMind.WebApp.Models
{
    public class GameViewModel
    {
        public string guid { get; set; } = string.Empty;
        public int level { get; set; } = -1;
        public string codepeg1 { get; set; } = string.Empty;
        public string codepeg2 { get; set; } = string.Empty;
        public string codepeg3 { get; set; } = string.Empty;
        public string codepeg4 { get; set; } = string.Empty;
    }
}
