namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "USDepartureDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "USDepartureDateTime");
        }
    }
}
