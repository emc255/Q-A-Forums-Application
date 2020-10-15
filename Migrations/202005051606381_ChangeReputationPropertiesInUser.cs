namespace CourseProject_StackOverFlowV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeReputationPropertiesInUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Reputation", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Reputation", c => c.Int());
        }
    }
}
