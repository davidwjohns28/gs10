namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Applicants", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Applicants", "RoomShareID", "dbo.RoomShares");
            DropForeignKey("dbo.Applicants", "SpanishLevelID", "dbo.SpanishLevels");
            DropForeignKey("dbo.Applicants", "TeamID", "dbo.Teams");
            DropIndex("dbo.Applicants", new[] { "RoleID" });
            DropIndex("dbo.Applicants", new[] { "RoomShareID" });
            DropIndex("dbo.Applicants", new[] { "SpanishLevelID" });
            DropIndex("dbo.Applicants", new[] { "TeamID" });
            AlterColumn("dbo.Applicants", "FirstName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Applicants", "MiddleName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Applicants", "LastName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Applicants", "PreferredName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Applicants", "Address1", c => c.String(maxLength: 50));
            AlterColumn("dbo.Applicants", "Address2", c => c.String(maxLength: 50));
            AlterColumn("dbo.Applicants", "City", c => c.String(maxLength: 50));
            AlterColumn("dbo.Applicants", "ZipCode", c => c.String(maxLength: 10));
            AlterColumn("dbo.Applicants", "HomePhoneNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Applicants", "CellPhoneNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Applicants", "PassportNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Applicants", "NameOnPassport", c => c.String(maxLength: 50));
            AlterColumn("dbo.Applicants", "EmergencyContactOneName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Applicants", "EmergencyContactOnePhoneNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Applicants", "EmergencyContactTwoName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Applicants", "EmergencyContactTwoPhoneNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Applicants", "HealthIssues", c => c.String(maxLength: 500));
            AlterColumn("dbo.Applicants", "RoleID", c => c.Int());
            AlterColumn("dbo.Applicants", "RoleInformation", c => c.String(maxLength: 50));
            AlterColumn("dbo.Applicants", "TeamID", c => c.Int());
            AlterColumn("dbo.Applicants", "SpanishLevelID", c => c.Int());
            AlterColumn("dbo.Applicants", "RoomShareID", c => c.Int());
            AlterColumn("dbo.Applicants", "RoomMateRequest", c => c.String(maxLength: 50));
            AlterColumn("dbo.Applicants", "USDepartureAirline", c => c.String(maxLength: 50));
            AlterColumn("dbo.Applicants", "USDepartureFlightNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Applicants", "USDepartureDateTime", c => c.String(maxLength: 30));
            AlterColumn("dbo.Applicants", "GuatemalaArrivalDateTime", c => c.String(maxLength: 30));
            AlterColumn("dbo.Applicants", "GuatemalaReturnAirline", c => c.String(maxLength: 50));
            AlterColumn("dbo.Applicants", "GuatemalaReturnFlightNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Applicants", "GuatemalaReturnDateTime", c => c.String(maxLength: 30));
            CreateIndex("dbo.Applicants", "RoleID");
            CreateIndex("dbo.Applicants", "RoomShareID");
            CreateIndex("dbo.Applicants", "SpanishLevelID");
            CreateIndex("dbo.Applicants", "TeamID");
            AddForeignKey("dbo.Applicants", "RoleID", "dbo.Roles", "RoleID");
            AddForeignKey("dbo.Applicants", "RoomShareID", "dbo.RoomShares", "RoomShareID");
            AddForeignKey("dbo.Applicants", "SpanishLevelID", "dbo.SpanishLevels", "SpanishLevelID");
            AddForeignKey("dbo.Applicants", "TeamID", "dbo.Teams", "TeamID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applicants", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Applicants", "SpanishLevelID", "dbo.SpanishLevels");
            DropForeignKey("dbo.Applicants", "RoomShareID", "dbo.RoomShares");
            DropForeignKey("dbo.Applicants", "RoleID", "dbo.Roles");
            DropIndex("dbo.Applicants", new[] { "TeamID" });
            DropIndex("dbo.Applicants", new[] { "SpanishLevelID" });
            DropIndex("dbo.Applicants", new[] { "RoomShareID" });
            DropIndex("dbo.Applicants", new[] { "RoleID" });
            AlterColumn("dbo.Applicants", "GuatemalaReturnDateTime", c => c.String());
            AlterColumn("dbo.Applicants", "GuatemalaReturnFlightNumber", c => c.String());
            AlterColumn("dbo.Applicants", "GuatemalaReturnAirline", c => c.String());
            AlterColumn("dbo.Applicants", "GuatemalaArrivalDateTime", c => c.String());
            AlterColumn("dbo.Applicants", "USDepartureDateTime", c => c.String());
            AlterColumn("dbo.Applicants", "USDepartureFlightNumber", c => c.String());
            AlterColumn("dbo.Applicants", "USDepartureAirline", c => c.String());
            AlterColumn("dbo.Applicants", "RoomMateRequest", c => c.String());
            AlterColumn("dbo.Applicants", "RoomShareID", c => c.Int(nullable: false));
            AlterColumn("dbo.Applicants", "SpanishLevelID", c => c.Int(nullable: false));
            AlterColumn("dbo.Applicants", "TeamID", c => c.Int(nullable: false));
            AlterColumn("dbo.Applicants", "RoleInformation", c => c.String());
            AlterColumn("dbo.Applicants", "RoleID", c => c.Int(nullable: false));
            AlterColumn("dbo.Applicants", "HealthIssues", c => c.String());
            AlterColumn("dbo.Applicants", "EmergencyContactTwoPhoneNumber", c => c.String());
            AlterColumn("dbo.Applicants", "EmergencyContactTwoName", c => c.String());
            AlterColumn("dbo.Applicants", "EmergencyContactOnePhoneNumber", c => c.String());
            AlterColumn("dbo.Applicants", "EmergencyContactOneName", c => c.String());
            AlterColumn("dbo.Applicants", "NameOnPassport", c => c.String());
            AlterColumn("dbo.Applicants", "PassportNumber", c => c.String());
            AlterColumn("dbo.Applicants", "CellPhoneNumber", c => c.String());
            AlterColumn("dbo.Applicants", "HomePhoneNumber", c => c.String());
            AlterColumn("dbo.Applicants", "ZipCode", c => c.String());
            AlterColumn("dbo.Applicants", "City", c => c.String());
            AlterColumn("dbo.Applicants", "Address2", c => c.String());
            AlterColumn("dbo.Applicants", "Address1", c => c.String());
            AlterColumn("dbo.Applicants", "PreferredName", c => c.String());
            AlterColumn("dbo.Applicants", "LastName", c => c.String());
            AlterColumn("dbo.Applicants", "MiddleName", c => c.String());
            AlterColumn("dbo.Applicants", "FirstName", c => c.String(maxLength: 50));
            CreateIndex("dbo.Applicants", "TeamID");
            CreateIndex("dbo.Applicants", "SpanishLevelID");
            CreateIndex("dbo.Applicants", "RoomShareID");
            CreateIndex("dbo.Applicants", "RoleID");
            AddForeignKey("dbo.Applicants", "TeamID", "dbo.Teams", "TeamID", cascadeDelete: true);
            AddForeignKey("dbo.Applicants", "SpanishLevelID", "dbo.SpanishLevels", "SpanishLevelID", cascadeDelete: true);
            AddForeignKey("dbo.Applicants", "RoomShareID", "dbo.RoomShares", "RoomShareID", cascadeDelete: true);
            AddForeignKey("dbo.Applicants", "RoleID", "dbo.Roles", "RoleID", cascadeDelete: true);
        }
    }
}
