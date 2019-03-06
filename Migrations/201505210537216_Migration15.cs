namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration15 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Applicants", "USDepartureDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Applicants", "USDepartureDateTime", c => c.DateTime());
        }
    }
}
