using System.ComponentModel.DataAnnotations;

namespace TeamAlpha.GoldenOracle.Models
{
    public class Encounter
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MonsterList { get; set; }
        public double ChallengeRating { get; set; }
    }
}