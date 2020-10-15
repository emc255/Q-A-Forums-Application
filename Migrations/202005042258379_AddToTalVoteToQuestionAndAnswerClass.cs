namespace CourseProject_StackOverFlowV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToTalVoteToQuestionAndAnswerClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "TotalVote", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "TotalVote", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "TotalVote");
            DropColumn("dbo.Answers", "TotalVote");
        }
    }
}
