using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamAlpha.GoldenOracle.Models
{
    public class Encounter
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MonsterList { get; set; }
        public int ChallengeRating { get; set; }
    }
}