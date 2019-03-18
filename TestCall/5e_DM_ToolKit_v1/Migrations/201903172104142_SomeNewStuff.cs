namespace TeamAlpha.GoldenOracle.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeNewStuff : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Encounter", "ChallengeRating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Encounter", "ChallengeRating", c => c.Int(nullable: false));
        }
    }
}
