using TeamAlpha.GoldenOracle.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TeamAlpha.GoldenOracle.DAL
{
    public class DungeonContext : DbContext
    { 
        public DungeonContext() : base("DungeonContext")
        {
            Database.SetInitializer(new DungeonInitializer());

        }

        public DbSet<Characters> Characters { get; set; }
        public DbSet<Monsters> Monsters { get; set; }
        public DbSet<Actions> Actions { get; set; }
        public DbSet<Conditions> Conditions { get; set; }
        public DbSet<Cost> Cost { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Races> Races { get; set; }
        public DbSet<Damage> Damages { get; set; }
        public DbSet<DamageType> DamageType { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<LegendaryActions> LegendaryActions { get; set; }
        public DbSet<Proficiencies> Proficiencies { get; set; }
        public DbSet<ProficiencyChoices> ProficiencyChoices { get; set; }
        public DbSet<Properties> Properties { get; set; }
        public DbSet<Range> Range { get; set; }
        public DbSet<SavingThrows> SavingThrows { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<SpecialAbilities> SpecialAbilities { get; set; }
        public DbSet<Spells> Spells { get; set; }
        public DbSet<StartingProficiencies> StartingProficiencies { get; set; }
        public DbSet<StartingProficiencyOptions> StartingProficiencyOptions { get; set; }
        public DbSet<Subraces> Subraces { get; set; }
        public DbSet<ThrowRange> ThrowRanges { get; set; }
        public DbSet<Traits> Traits { get; set; }
        public DbSet<Encounter> Encounters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}