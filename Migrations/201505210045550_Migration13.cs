namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applicants", "USDepartureDateTime", c => c.DateTime());
            AlterColumn("dbo.Applicants", "GuatemalaArrivalDateTime", c => c.DateTime());
            AlterColumn("dbo.Applicants", "GuatemalaReturnDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applicants", "GuatemalaReturnDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Applicants", "GuatemalaArrivalDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Applicants", "USDepartureDateTime", c => c.DateTime(nullable: false));
        }
    }
}
