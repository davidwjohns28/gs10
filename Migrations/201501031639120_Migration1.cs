namespace gs10.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        ApplicantID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Year = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        PreferredName = c.String(),
                        Email = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        ZipCode = c.String(),
                        HomePhoneNumber = c.String(),
                        CellPhoneNumber = c.String(),
                        EmergencyContactOneName = c.String(),
                        EmergencyContactOnePhoneNumber = c.String(),
                        EmergencyContactTwoName = c.String(),
                        EmergencyContactTwoPhoneNumber = c.String(),
                        HealthIssues = c.String(),
                        SpanishAbility = c.String(),
                        RoomMateRequest = c.String(),
                        PassportNumber = c.String(),
                        NameOnPassport = c.String(),
                        RoleInformation = c.String(),
                        USDepartureAirline = c.String(),
                        USDepartureFlightNumber = c.String(),
                        USDepartureDateTime = c.String(),
                        GuatemalaArrivalDateTime = c.String(),
                        GuatemalaReturnAirline = c.String(),
                        GuatemalaReturnFlightNumber = c.String(),
                        GuatemalaReturnDateTime = c.String(),
                        TeamID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                        StateID = c.Int(nullable: false),
                        SpanishLevelID = c.Int(nullable: false),
                        RoomShareID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicantID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.RoomShares", t => t.RoomShareID, cascadeDelete: true)
                .ForeignKey("dbo.SpanishLevels", t => t.SpanishLevelID, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.RoomShareID)
                .Index(t => t.SpanishLevelID)
                .Index(t => t.StateID)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.RoomShares",
                c => new
                    {
                        RoomShareID = c.Int(nullable: false, identity: true),
                        RoomShareName = c.String(),
                    })
                .PrimaryKey(t => t.RoomShareID);
            
            CreateTable(
                "dbo.SpanishLevels",
                c => new
                    {
                        SpanishLevelID = c.Int(nullable: false, identity: true),
                        SpanishLevelName = c.String(),
                    })
                .PrimaryKey(t => t.SpanishLevelID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.StateID);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                    })
                .PrimaryKey(t => t.TeamID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Applicants", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Applicants", "StateID", "dbo.States");
            DropForeignKey("dbo.Applicants", "SpanishLevelID", "dbo.SpanishLevels");
            DropForeignKey("dbo.Applicants", "RoomShareID", "dbo.RoomShares");
            DropForeignKey("dbo.Applicants", "RoleID", "dbo.Roles");
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Applicants", new[] { "TeamID" });
            DropIndex("dbo.Applicants", new[] { "StateID" });
            DropIndex("dbo.Applicants", new[] { "SpanishLevelID" });
            DropIndex("dbo.Applicants", new[] { "RoomShareID" });
            DropIndex("dbo.Applicants", new[] { "RoleID" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Teams");
            DropTable("dbo.States");
            DropTable("dbo.SpanishLevels");
            DropTable("dbo.RoomShares");
            DropTable("dbo.Roles");
            DropTable("dbo.Applicants");
        }
    }
}
