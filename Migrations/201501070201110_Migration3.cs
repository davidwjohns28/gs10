namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "Comments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "Comments");
        }
    }
}
