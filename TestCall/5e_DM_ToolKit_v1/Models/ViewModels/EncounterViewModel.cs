using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamAlpha.GoldenOracle.Models.ViewModels
{
    public class EncounterViewModel
    {
        public Queue<EncounterCreature> EncounterCreatures { get; set; }
        public EncounterCreature Creature { get; set; }
        public Monsters Monsters { get; set; }
        public Characters Characters { get; set; }
    }
}