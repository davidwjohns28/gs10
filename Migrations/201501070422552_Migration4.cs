namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Applicants", "StateID", "dbo.States");
            DropIndex("dbo.Applicants", new[] { "StateID" });
            AlterColumn("dbo.Applicants", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Applicants", "StateID", c => c.Int());
            AlterColumn("dbo.Applicants", "Comments", c => c.String(maxLength: 500));
            CreateIndex("dbo.Applicants", "StateID");
            AddForeignKey("dbo.Applicants", "StateID", "dbo.States", "StateID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicants", "StateID", "dbo.States");
            DropIndex("dbo.Applicants", new[] { "StateID" });
            AlterColumn("dbo.Applicants", "Comments", c => c.String());
            AlterColumn("dbo.Applicants", "StateID", c => c.Int(nullable: false));
            AlterColumn("dbo.Applicants", "FirstName", c => c.String());
            CreateIndex("dbo.Applicants", "StateID");
            AddForeignKey("dbo.Applicants", "StateID", "dbo.States", "StateID", cascadeDelete: true);
        }
    }
}
