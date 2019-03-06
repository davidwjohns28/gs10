namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "USDepartureDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Applicants", "GuatemalaReturnDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "GuatemalaReturnDateTime");
            DropColumn("dbo.Applicants", "USDepartureDateTime");
        }
    }
}
