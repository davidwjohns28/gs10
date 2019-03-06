namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "GuatemalaArrivalDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "GuatemalaArrivalDateTime");
        }
    }
}
