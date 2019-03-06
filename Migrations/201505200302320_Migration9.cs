namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration9 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Applicants", "GuatemalaArrivalDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Applicants", "GuatemalaArrivalDateTime", c => c.String(maxLength: 30));
        }
    }
}
