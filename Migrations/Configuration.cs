namespace gs10.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web;
    using gs10.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<gs10.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(gs10.Models.ApplicationDbContext context)
        {
            var teams = new List<Team>
            {
            new Team{TeamID= 1, TeamName="Any Team"},
            new Team{TeamID= 2, TeamName="Team 1 (Oct. 5-12, 2019)"},
            new Team{TeamID= 3, TeamName="Team 2 (Oct. 12-19, 2019)"},
            new Team{TeamID= 4, TeamName="Team 3 (Oct. 20-26, 2019)"},
            };
            teams.ForEach(s => context.Teams.AddOrUpdate(s));
            context.SaveChanges();

            ////////////////////////////////////////////////////////

            var roles = new List<Role>
            {
            new Role{RoleID= 1,RoleName="General Suregon"},
            new Role{RoleID= 2,RoleName="GYN Surgeon"},
            new Role{RoleID= 3,RoleName="Surgeon - Other Specialty (Enter below.)"},
            new Role{RoleID= 4,RoleName="Anesthesiologist"},
            new Role{RoleID= 5,RoleName="CRNA"},
            new Role{RoleID= 6,RoleName="Recovery Room Nurse"},
            new Role{RoleID= 7,RoleName="OR Nurse Cirulcation"},
            new Role{RoleID= 8,RoleName="RN Nurse Scrub"},
            new Role{RoleID= 9,RoleName="OR RN Only Ciruclator"},
            new Role{RoleID= 10,RoleName="OR RN Can Scrub"},
            new Role{RoleID= 11,RoleName="Scrub Tech"},
            new Role{RoleID= 12,RoleName="Physician's Assistant"},
            new Role{RoleID= 13,RoleName="Translator"},
            new Role{RoleID= 14,RoleName="Non-Medical"},
            new Role{RoleID= 15,RoleName="Other (Enter below.)"},
            };

            roles.ForEach(s => context.Roles.AddOrUpdate(s));
            context.SaveChanges();

            ////////////////////////////////////////////////////////

            var states = new List<State>
            {
            new State{StateID= 1,StateName="Alabama"},
            new State{StateID= 2,StateName="Alaska"},
            new State{StateID= 3,StateName="Arizona"},
            new State{StateID= 4,StateName="Arkansas"},
            new State{StateID= 5,StateName="California"},
            new State{StateID= 6,StateName="Colorado"},
            new State{StateID= 7,StateName="Connecticut"},
            new State{StateID= 8,StateName="Delaware"},
            new State{StateID= 9,StateName="Florida"},
            new State{StateID= 10,StateName="Georgia"},
            new State{StateID= 11,StateName="Hawaii"},
            new State{StateID= 12,StateName="Idaho"},
            new State{StateID= 13,StateName="Illinois"},
            new State{StateID= 14,StateName="Indiana"},
            new State{StateID= 15,StateName="Iowa"},
            new State{StateID= 16,StateName="Kansas"},
            new State{StateID= 17,StateName="Kentucky"},
            new State{StateID= 18,StateName="Louisiana"},
            new State{StateID= 19,StateName="Maine"},
            new State{StateID= 20,StateName="Maryland"},
            new State{StateID= 21,StateName="Massachusetts"},
            new State{StateID= 22,StateName="Michigan"},
            new State{StateID= 23,StateName="Minnesota"},
            new State{StateID= 24,StateName="Mississippi"},
            new State{StateID= 25,StateName="Missouri"},
            new State{StateID= 26,StateName="Montana"},
            new State{StateID= 27,StateName="Nebraska"},
            new State{StateID= 28,StateName="Nevada"},
            new State{StateID= 29,StateName="New Hampshire"},
            new State{StateID= 30,StateName="New Jersey"},
            new State{StateID= 31,StateName="New Mexico"},
            new State{StateID= 32,StateName="New York"},
            new State{StateID= 33,StateName="North Carolina"},
            new State{StateID= 34,StateName="North Dakota"},
            new State{StateID= 35,StateName="Ohio"},
            new State{StateID= 36,StateName="Oklahoma"},
            new State{StateID= 37,StateName="Oregon"},
            new State{StateID= 38,StateName="Pennsylvania"},
            new State{StateID= 39,StateName="Rhode Island"},
            new State{StateID= 40,StateName="South Carolina"},
            new State{StateID= 41,StateName="South Dakota"},
            new State{StateID= 42,StateName="Tennessee"},
            new State{StateID= 43,StateName="Texas"},
            new State{StateID= 44,StateName="Utah"},
            new State{StateID= 45,StateName="Vermont"},
            new State{StateID= 46,StateName="Virginia"},
            new State{StateID= 47,StateName="Washington"},
            new State{StateID= 48,StateName="West Virginia"},
            new State{StateID= 49,StateName="Wisconsin"},
            new State{StateID= 50,StateName="Wyoming"},
            };

            states.ForEach(s => context.States.AddOrUpdate(s));
            context.SaveChanges();

            ////////////////////////////////////////////////////////

            var spanishLevels = new List<SpanishLevel>
            {
            new SpanishLevel{SpanishLevelID = 1,SpanishLevelName="None"},
            new SpanishLevel{SpanishLevelID = 2,SpanishLevelName="Some"},
            new SpanishLevel{SpanishLevelID = 3,SpanishLevelName="Intermediate"},
            new SpanishLevel{SpanishLevelID = 4,SpanishLevelName="Advanced"},
            };

            spanishLevels.ForEach(s => context.SpanishLevels.AddOrUpdate(s));
            context.SaveChanges();

            ////////////////////////////////////////////////////////

            var roomShares = new List<RoomShare>
            {
            new RoomShare{RoomShareID = 1,RoomShareName="I have no preference."},
            new RoomShare{RoomShareID = 2,RoomShareName="I prefer my own room--if possible."},
            new RoomShare{RoomShareID = 3,RoomShareName="I would like to share a room. (Enter room mate name below.)"},
            };

            roomShares.ForEach(s => context.RoomShares.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}
