namespace TeamAlpha.GoldenOracle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEncounterMonsterList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Monsters", "Encounter_ID", "dbo.Encounter");
            DropIndex("dbo.Monsters", new[] { "Encounter_ID" });
            AddColumn("dbo.Encounter", "MonsterList", c => c.String());
            DropColumn("dbo.Monsters", "Encounter_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Monsters", "Encounter_ID", c => c.Int());
            DropColumn("dbo.Encounter", "MonsterList");
            CreateIndex("dbo.Monsters", "Encounter_ID");
            AddForeignKey("dbo.Monsters", "Encounter_ID", "dbo.Encounter", "ID");
        }
    }
}
