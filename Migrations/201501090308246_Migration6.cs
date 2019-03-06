namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applicants", "PassportNumber", c => c.String(maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applicants", "PassportNumber", c => c.String(maxLength: 20));
        }
    }
}
