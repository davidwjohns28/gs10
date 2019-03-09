using Microsoft.AspNet.Identity.EntityFramework;

namespace gs10.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<gs10.Models.Applicant> Applicants { get; set; }
        public System.Data.Entity.DbSet<gs10.Models.Role> Roles { get; set; }
        public System.Data.Entity.DbSet<gs10.Models.Team> Teams { get; set; }
        public System.Data.Entity.DbSet<gs10.Models.State> States { get; set; }
        public System.Data.Entity.DbSet<gs10.Models.SpanishLevel> SpanishLevels { get; set; }
        public System.Data.Entity.DbSet<gs10.Models.RoomShare> RoomShares { get; set; }
    }
}