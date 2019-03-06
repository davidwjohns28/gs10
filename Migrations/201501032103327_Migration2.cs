namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Applicants", "SpanishAbility");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Applicants", "SpanishAbility", c => c.String());
        }
    }
}
