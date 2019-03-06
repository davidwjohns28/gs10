namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration11 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Applicants", "USDepartureDateTime");
            DropColumn("dbo.Applicants", "GuatemalaReturnDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Applicants", "GuatemalaReturnDateTime", c => c.String(maxLength: 30));
            AddColumn("dbo.Applicants", "USDepartureDateTime", c => c.String(maxLength: 30));
        }
    }
}
