namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "Medications", c => c.String(maxLength: 500));
            AlterColumn("dbo.Applicants", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Applicants", "LastName", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applicants", "LastName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Applicants", "FirstName", c => c.String(maxLength: 30));
            DropColumn("dbo.Applicants", "Medications");
        }
    }
}
